using Microsoft.AspNetCore.Mvc.ModelBinding;
using StudentIndex.Server.Application.Queries;

namespace StudentIndex.Server.WebApi.ModelBinding;

/// <summary>
/// Binduje QueryParameters iz query stringa. Rezervisani ključevi (search, sort, page...)
/// i ostali parametri akcije (npr. semesterId) se preskaču; sve ostalo postaje filter po koloni.
/// </summary>
public class QueryParametersModelBinder : IModelBinder
{
    private static readonly HashSet<string> ReservedKeys = new(StringComparer.OrdinalIgnoreCase)
    {
        "search", "sort", "page", "pageSize", "returnNumberOfRows"
    };

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var query = bindingContext.HttpContext.Request.Query;
        var parameters = new QueryParameters();

        // Ključevi koje bind-uju drugi parametri akcije ne smiju postati filteri
        var skipKeys = new HashSet<string>(ReservedKeys, StringComparer.OrdinalIgnoreCase);
        foreach (var actionParameter in bindingContext.ActionContext.ActionDescriptor.Parameters)
        {
            if (actionParameter.ParameterType != typeof(QueryParameters))
                skipKeys.Add(actionParameter.Name);
        }

        foreach (var (key, values) in query)
        {
            var value = values.ToString();

            if (key.Equals("search", StringComparison.OrdinalIgnoreCase))
                parameters.Search = value;
            else if (key.Equals("sort", StringComparison.OrdinalIgnoreCase))
                parameters.Sort.AddRange(
                    value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                         .Select(SortCriteria.Parse));
            else if (key.Equals("page", StringComparison.OrdinalIgnoreCase) && int.TryParse(value, out var page))
                parameters.Page = page;
            else if (key.Equals("pageSize", StringComparison.OrdinalIgnoreCase) && int.TryParse(value, out var pageSize))
                parameters.PageSize = pageSize;
            else if (key.Equals("returnNumberOfRows", StringComparison.OrdinalIgnoreCase) && bool.TryParse(value, out var count))
                parameters.ReturnNumberOfRows = count;
            else if (!skipKeys.Contains(key))
                parameters.AdvanceSearch.Add(SearchCriteria.Parse(key, value));
        }

        bindingContext.Result = ModelBindingResult.Success(parameters);
        return Task.CompletedTask;
    }
}

public class QueryParametersModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        return context.Metadata.ModelType == typeof(QueryParameters)
            ? new QueryParametersModelBinder()
            : null;
    }
}
