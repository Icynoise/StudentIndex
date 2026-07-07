namespace StudentIndex.Server.Application.Queries;

public class SearchCriteria
{
    public string Name { get; }
    public SearchOperator Operator { get; }
    public string Value { get; }

    public SearchCriteria(string name, SearchOperator @operator, string value)
    {
        Name = name;
        Operator = @operator;
        Value = value;
    }

    /// <summary>Parsira query string par, npr. naziv=cn|matem.</summary>
    public static SearchCriteria Parse(string name, string rawValue)
    {
        var (op, value) = SearchOperatorParser.Parse(rawValue);
        return new SearchCriteria(name, op, value);
    }

    public override string ToString() => $"{Name}={Operator}|{Value}";
}
