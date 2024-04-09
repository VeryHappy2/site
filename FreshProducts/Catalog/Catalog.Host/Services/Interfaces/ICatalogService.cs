using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Requests.Paginates;
using Catalog.Host.Models.Response.Paginations;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogService
{
    Task<PaginatedItemsResponse<CatalogItemDto>?> GetCatalogItemsAsync(int pageSize, int pageIndex, Dictionary<CatalogTypeFilter, int>? filters, string search);
    Task<List<CatalogBrandDto>> GetCatalogBrands();
    Task<List<CatalogTypeDto>> GetCatalogTypes();
    Task<CatalogItemDto> GetById(int id);
    Task<CatalogBrandDto> GetByBrand(string brand);
    Task<CatalogTypeDto> GetByType(string type);
}