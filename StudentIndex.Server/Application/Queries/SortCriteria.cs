namespace StudentIndex.Server.Application.Queries;

public class SortCriteria
{
    public string Name { get; }
    public bool Descending { get; }

    public SortCriteria(string name, bool descending = false)
    {
        Name = name;
        Descending = descending;
    }

    /// <summary>Parsira jedan segment sort parametra, npr. "naziv|dsc" ili "ects".</summary>
    public static SortCriteria Parse(string raw)
    {
        var parts = raw.Split('|', 2);
        var descending = parts.Length == 2 &&
            (parts[1].Equals("dsc", StringComparison.OrdinalIgnoreCase) ||
             parts[1].Equals("desc", StringComparison.OrdinalIgnoreCase));
        return new SortCriteria(parts[0], descending);
    }

    public override string ToString() => Descending ? $"{Name}|dsc" : Name;
}
