using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests.Paginates;

public class PaginatedItemsRequest<T>
    where T : notnull
{
    [Required]
	[Range(0, int.MaxValue, ErrorMessage = "The field must be greater than or equal to 1.")]
	public int PageIndex { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "The field must be greater than or equal to 1.")]
    public int PageSize { get; set; }
    [MaxLength(65)]
	public string Search { get; set; }
	public Dictionary<T, int>? Filters { get; set; }
}