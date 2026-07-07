using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

namespace StudentIndex.Server.Application.Queries;

/// <summary>
/// Rezultat primjene QueryParameters na IQueryable:
/// CountQuery je filtriran ali bez sorta/paginacije; DataQuery je kompletan.
/// </summary>
public class AppliedQuery<T>
{
    public required IQueryable<T> CountQuery { get; init; }
    public required IQueryable<T> DataQuery { get; init; }
    public required List<string> Errors { get; init; }
}

public static class QueryableExtensions
{
    public static AppliedQuery<T> ApplyQueryParameters<T>(this IQueryable<T> source, QueryParameters parameters)
    {
        var errors = new List<string>();
        var filtered = source;

        // 1. Quick search — OR po svim string kolonama, AND između tokena
        if (parameters.HasQuickSearch())
        {
            var quickSearch = BuildQuickSearchPredicate<T>(parameters.QuickSearchTokens());
            if (quickSearch != null)
                filtered = filtered.Where(quickSearch);
        }

        // 2. Filteri po kolonama
        foreach (var criteria in parameters.AdvanceSearch)
        {
            var predicate = BuildPredicate<T>(criteria);
            if (predicate == null)
            {
                errors.Add($"Nepoznat ili nevalidan parametar pretrage: '{criteria.Name}' ({criteria.Operator}|{criteria.Value}).");
                continue;
            }
            filtered = filtered.Where(predicate);
        }

        // 3. Sortiranje
        var sorted = ApplySort(filtered, parameters.Sort, errors);

        // 4. Paginacija — samo ako je klijent eksplicitno tražio
        var paged = sorted;
        if (parameters.HasPaging())
        {
            var page = Math.Max(parameters.Page ?? 1, 1);
            var pageSize = Math.Max(parameters.PageSize ?? QueryParameters.DefaultPageSize, 1);
            paged = paged.Skip((page - 1) * pageSize).Take(pageSize);
        }

        return new AppliedQuery<T>
        {
            CountQuery = filtered,
            DataQuery = paged,
            Errors = errors
        };
    }

    #region Sort

    private static IQueryable<T> ApplySort<T>(IQueryable<T> source, List<SortCriteria> sortCriteria, List<string> errors)
    {
        var result = source;
        var isFirst = true;

        foreach (var sort in sortCriteria)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var member = BuildMemberPath(parameter, sort.Name);
            if (member == null)
            {
                errors.Add($"Nepoznata kolona za sortiranje: '{sort.Name}'.");
                continue;
            }

            var keySelector = Expression.Lambda(member.Access, parameter);
            var methodName = isFirst
                ? (sort.Descending ? nameof(Queryable.OrderByDescending) : nameof(Queryable.OrderBy))
                : (sort.Descending ? nameof(Queryable.ThenByDescending) : nameof(Queryable.ThenBy));

            result = (IQueryable<T>)typeof(Queryable).GetMethods()
                .First(m => m.Name == methodName && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), member.Access.Type)
                .Invoke(null, new object[] { result, keySelector })!;

            isFirst = false;
        }

        return result;
    }

    #endregion

    #region Quick search

    private static Expression<Func<T, bool>>? BuildQuickSearchPredicate<T>(string[] tokens)
    {
        var stringProperties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.PropertyType == typeof(string) && p.CanRead)
            .ToList();

        if (stringProperties.Count == 0 || tokens.Length == 0)
            return null;

        var parameter = Expression.Parameter(typeof(T), "x");
        Expression? allTokens = null;

        foreach (var token in tokens)
        {
            Expression? anyColumn = null;
            foreach (var property in stringProperties)
            {
                var member = Expression.Property(parameter, property);
                var contains = BuildStringComparison(member, nameof(string.Contains), token);
                anyColumn = anyColumn == null ? contains : Expression.OrElse(anyColumn, contains);
            }
            allTokens = allTokens == null ? anyColumn : Expression.AndAlso(allTokens, anyColumn!);
        }

        return allTokens == null ? null : Expression.Lambda<Func<T, bool>>(allTokens, parameter);
    }

    #endregion

    #region Filter predicates

    private static Expression<Func<T, bool>>? BuildPredicate<T>(SearchCriteria criteria)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var member = BuildMemberPath(parameter, criteria.Name);
        if (member == null)
            return null;

        var comparison = BuildComparison(member.Access, criteria);
        if (comparison == null)
            return null;

        // Null-guard za međučlanove dot-putanje (bitno za in-memory upite)
        var body = member.NullGuard == null ? comparison : Expression.AndAlso(member.NullGuard, comparison);
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    private static Expression? BuildComparison(Expression member, SearchCriteria criteria)
    {
        var memberType = member.Type;
        var underlyingType = Nullable.GetUnderlyingType(memberType) ?? memberType;

        switch (criteria.Operator)
        {
            case SearchOperator.Empty:
                return BuildEmptyCheck(member, negate: false);

            case SearchOperator.NotEmpty:
                return BuildEmptyCheck(member, negate: true);

            case SearchOperator.StartsWith:
            case SearchOperator.Contains:
                if (memberType != typeof(string))
                    return null;
                var method = criteria.Operator == SearchOperator.StartsWith
                    ? nameof(string.StartsWith)
                    : nameof(string.Contains);
                return BuildStringComparison(member, method, criteria.Value);

            case SearchOperator.In:
                return BuildInComparison(member, underlyingType, criteria.Value);

            case SearchOperator.Equal:
            case SearchOperator.NotEqual:
                if (memberType == typeof(string))
                {
                    var equal = BuildStringEquals(member, criteria.Value);
                    return criteria.Operator == SearchOperator.Equal ? equal : Expression.Not(equal);
                }
                goto case SearchOperator.LessThan;

            case SearchOperator.LessThan:
            case SearchOperator.LessOrEqual:
            case SearchOperator.GreaterThan:
            case SearchOperator.GreaterOrEqual:
                if (!TryConvertValue(criteria.Value, underlyingType, out var converted))
                    return null;

                var constant = CreateWrappedConstant(converted, memberType);
                return criteria.Operator switch
                {
                    SearchOperator.Equal => Expression.Equal(member, constant),
                    SearchOperator.NotEqual => Expression.NotEqual(member, constant),
                    SearchOperator.LessThan => Expression.LessThan(member, constant),
                    SearchOperator.LessOrEqual => Expression.LessThanOrEqual(member, constant),
                    SearchOperator.GreaterThan => Expression.GreaterThan(member, constant),
                    SearchOperator.GreaterOrEqual => Expression.GreaterThanOrEqual(member, constant),
                    _ => null
                };

            default:
                return null;
        }
    }

    private static Expression? BuildEmptyCheck(Expression member, bool negate)
    {
        Expression check;
        if (member.Type == typeof(string))
        {
            check = Expression.Call(
                typeof(string).GetMethod(nameof(string.IsNullOrEmpty), new[] { typeof(string) })!,
                member);
        }
        else if (Nullable.GetUnderlyingType(member.Type) != null || !member.Type.IsValueType)
        {
            check = Expression.Equal(member, Expression.Constant(null, member.Type));
        }
        else
        {
            return null; // non-nullable value type ne može biti prazan
        }

        return negate ? Expression.Not(check) : check;
    }

    /// <summary>Case-insensitive string poredba: x.Prop != null &amp;&amp; x.Prop.ToLower().Method(value.ToLower())</summary>
    private static Expression BuildStringComparison(Expression member, string methodName, string value)
    {
        var notNull = Expression.NotEqual(member, Expression.Constant(null, typeof(string)));
        var toLower = Expression.Call(member, typeof(string).GetMethod(nameof(string.ToLower), Type.EmptyTypes)!);
        var call = Expression.Call(
            toLower,
            typeof(string).GetMethod(methodName, new[] { typeof(string) })!,
            CreateWrappedConstant(value.ToLowerInvariant(), typeof(string)));
        return Expression.AndAlso(notNull, call);
    }

    private static Expression BuildStringEquals(Expression member, string value)
    {
        var notNull = Expression.NotEqual(member, Expression.Constant(null, typeof(string)));
        var toLower = Expression.Call(member, typeof(string).GetMethod(nameof(string.ToLower), Type.EmptyTypes)!);
        var equal = Expression.Equal(toLower, CreateWrappedConstant(value.ToLowerInvariant(), typeof(string)));
        return Expression.AndAlso(notNull, equal);
    }

    private static Expression? BuildInComparison(Expression member, Type underlyingType, string rawValues)
    {
        var listType = typeof(List<>).MakeGenericType(member.Type);
        var list = (System.Collections.IList)Activator.CreateInstance(listType)!;

        foreach (var raw in rawValues.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            if (!TryConvertValue(raw, underlyingType, out var converted))
                return null;
            list.Add(converted);
        }

        var containsCall = Expression.Call(
            typeof(Enumerable),
            nameof(Enumerable.Contains),
            new[] { member.Type },
            CreateWrappedConstant(list, listType),
            member);

        return containsCall;
    }

    #endregion

    #region Helpers

    private sealed record MemberPath(Expression Access, Expression? NullGuard);

    /// <summary>
    /// Gradi pristup propertiju po dot-putanji (npr. "predmet.naziv"), case-insensitive.
    /// Vraća null ako putanja ne postoji na tipu.
    /// </summary>
    private static MemberPath? BuildMemberPath(ParameterExpression parameter, string path)
    {
        Expression current = parameter;
        Expression? nullGuard = null;

        foreach (var segment in path.Split('.'))
        {
            var property = current.Type.GetProperty(
                segment, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (property == null)
                return null;

            // Prije silaska u ugniježdeni referencni tip, osiguraj da nije null
            if (current != parameter && !current.Type.IsValueType)
            {
                var guard = Expression.NotEqual(current, Expression.Constant(null, current.Type));
                nullGuard = nullGuard == null ? guard : Expression.AndAlso(nullGuard, guard);
            }

            current = Expression.Property(current, property);
        }

        return new MemberPath(current, nullGuard);
    }

    private static bool TryConvertValue(string raw, Type targetType, out object? converted)
    {
        try
        {
            converted = targetType switch
            {
                _ when targetType == typeof(string) => raw,
                _ when targetType == typeof(DateOnly) => DateOnly.Parse(raw, CultureInfo.InvariantCulture),
                _ when targetType == typeof(DateTime) => DateTime.Parse(raw, CultureInfo.InvariantCulture),
                _ when targetType == typeof(TimeOnly) => TimeOnly.Parse(raw, CultureInfo.InvariantCulture),
                _ when targetType == typeof(Guid) => Guid.Parse(raw),
                _ when targetType == typeof(bool) => bool.Parse(raw),
                _ when targetType.IsEnum => Enum.Parse(targetType, raw, ignoreCase: true),
                _ => Convert.ChangeType(raw, targetType, CultureInfo.InvariantCulture)
            };
            return true;
        }
        catch
        {
            converted = null;
            return false;
        }
    }

    /// <summary>
    /// Umotava konstantu u closure kako bi je EF Core parametrizovao (SQL parametar umjesto literala).
    /// </summary>
    private static Expression CreateWrappedConstant(object? value, Type type)
    {
        var holderType = typeof(ValueHolder<>).MakeGenericType(type);
        var holder = Activator.CreateInstance(holderType)!;
        holderType.GetField(nameof(ValueHolder<object>.Value))!.SetValue(holder, value);
        return Expression.Field(Expression.Constant(holder), nameof(ValueHolder<object>.Value));
    }

    private sealed class ValueHolder<T>
    {
        public T? Value = default; // dodjeljuje se refleksijom u CreateWrappedConstant
    }

    #endregion
}
