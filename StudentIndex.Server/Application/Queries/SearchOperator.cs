namespace StudentIndex.Server.Application.Queries;

public enum SearchOperator
{
    Equal,
    NotEqual,
    StartsWith,
    Contains,
    LessThan,
    LessOrEqual,
    GreaterThan,
    GreaterOrEqual,
    In,
    Empty,
    NotEmpty
}

public static class SearchOperatorParser
{
    private static readonly Dictionary<string, SearchOperator> Abbreviations =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["eq"] = SearchOperator.Equal,
            ["nq"] = SearchOperator.NotEqual,
            ["sw"] = SearchOperator.StartsWith,
            ["cn"] = SearchOperator.Contains,
            ["lt"] = SearchOperator.LessThan,
            ["le"] = SearchOperator.LessOrEqual,
            ["gt"] = SearchOperator.GreaterThan,
            ["ge"] = SearchOperator.GreaterOrEqual,
            ["in"] = SearchOperator.In,
            ["em"] = SearchOperator.Empty,
            ["nm"] = SearchOperator.NotEmpty
        };

    /// <summary>
    /// Parsira vrijednost oblika "op|vrijednost" (npr. "cn|matem", "ge|6").
    /// Bez prefiksa operator je Equal.
    /// </summary>
    public static (SearchOperator Operator, string Value) Parse(string raw)
    {
        var separatorIndex = raw.IndexOf('|');
        if (separatorIndex > 0 && Abbreviations.TryGetValue(raw[..separatorIndex], out var op))
            return (op, raw[(separatorIndex + 1)..]);

        return (SearchOperator.Equal, raw);
    }
}
