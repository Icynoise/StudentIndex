namespace StudentIndex.Server.Application.Queries;

/// <summary>
/// Parametri upita koje klijent šalje kroz query string, po uzoru na SmartResult pattern:
/// - search=tekst                  → quick search po svim string kolonama
/// - kolona=vrijednost             → filter (operatori: eq, nq, sw, cn, lt, le, gt, ge, in, em, nm; npr. naziv=cn|matem)
/// - sort=kolona|dsc,druga         → sortiranje više kolona
/// - page=2&amp;pageSize=50        → paginacija (bez ovih parametara vraća se sve — kompatibilno sa starim pozivima)
/// - returnNumberOfRows=true       → vraća samo broj redova (poslije filtera)
/// </summary>
public class QueryParameters
{
    public const int DefaultPageSize = 20;

    public string? Search { get; set; }

    public List<SearchCriteria> AdvanceSearch { get; } = new();

    public List<SortCriteria> Sort { get; } = new();

    public int? Page { get; set; }

    public int? PageSize { get; set; }

    public bool ReturnNumberOfRows { get; set; }

    public bool HasQuickSearch() => !string.IsNullOrWhiteSpace(Search);

    /// <summary>Paginacija se primjenjuje samo ako je klijent eksplicitno tražio.</summary>
    public bool HasPaging() => Page.HasValue || PageSize.HasValue;

    public string[] QuickSearchTokens() =>
        Search?.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
}
