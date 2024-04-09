using MVC.Host.Models.Responses;
using MVC.ViewModels;

namespace MVC.Services.Interfaces;

public interface ICatalogService
{
    Task<PaginatedItemsResponse<CatalogItem>> GetItems(int pageIndex, int pageSize, int? brand, int? type, string? search);
    Task<IEnumerable<SelectListItem>> GetBrands();
    Task<IEnumerable<SelectListItem>> GetTypes();
}
