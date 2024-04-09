namespace Catalog.Host.Models.Response.Paginations;

public class PaginatedItemsResponse<T> 
{
    public int PageIndex { get; init; }
    public int PageSize { get; init; }
    public string Search {  get; init; }
    public long Count { get; init; }
    public IEnumerable<T> Data { get; init; } = null!;
}
