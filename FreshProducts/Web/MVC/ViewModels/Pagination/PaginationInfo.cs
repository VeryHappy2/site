namespace MVC.ViewModels.Pagination;

public class PaginationInfo
{
    public long TotalItems { get; set; }
    public int ItemsPerPage { get; set; }
    public int TotalPages  { get; set; }
    public int CurrentPage { get; set; }
    public int? GroupPages { get; set; }
    public int MaxPages { get; set; }
}
