namespace AAM.Application;

public class PagedResult<T> : PagedResultBase where T : class
{
    public IEnumerable<T> Results { get; set; } = default(IEnumerable<T>)!;

    public int PageRowCount => Results.Count();

    public PagedResult()
    {
    }
}
