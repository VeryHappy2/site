using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Response.Paginations;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogService : BaseDataService<ApplicationDbContext>, ICatalogService
{
    private readonly IMapper _mapper;
    private readonly ICatalogBffRepository _catalogBffService;
    private readonly IDbContextWrapper<ApplicationDbContext> _dbContextWrapper;

    public CatalogService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogBffRepository catalogBffService,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {
        _dbContextWrapper = dbContextWrapper;
        _catalogBffService = catalogBffService;
        _mapper = mapper;
    }

    public async Task<PaginatedItemsResponse<CatalogItemDto>?> GetCatalogItemsAsync(int pageSize, int pageIndex, Dictionary<CatalogTypeFilter, int>? filters, string search)
    {
        return await ExecuteSafeAsync(async () =>
        {
            int? brandFilter = null;
            int? typeFilter = null;

            if (filters != null)
            {
                if (filters.TryGetValue(CatalogTypeFilter.Brand, out var brand))
                {
                    brandFilter = brand;
                }

                if (filters.TryGetValue(CatalogTypeFilter.Type, out var type))
                {
                    typeFilter = type;
                }
            }

            var result = await _catalogBffService.GetByPageAsync(pageIndex, pageSize, brandFilter, typeFilter, search);

            if (result == null)
            {
                return null;
            }

            return new PaginatedItemsResponse<CatalogItemDto>()
            {
                Count = result.TotalCount,
                Data = result.Data.Select(s => _mapper.Map<CatalogItemDto>(s)).ToList(),
                Search = search,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        });
    }

    public async Task<List<CatalogBrandDto>> GetCatalogBrands()
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogBffService.GetBrandsAsync();
            var mappedResult = _mapper.Map<List<CatalogBrandDto>>(result);
            return mappedResult;
        });
    }

    public async Task<List<CatalogTypeDto>> GetCatalogTypes()
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogBffService.GetTypesAsync();
            var mappedResult = _mapper.Map<List<CatalogTypeDto>>(result);
            return mappedResult;
        });
    }

    public async Task<CatalogItemDto> GetById(int id)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogBffService.GetByIdAsync(id);
            var mappedResult = _mapper.Map<CatalogItemDto>(result);
            return mappedResult;
        });
    }

    public async Task<CatalogBrandDto> GetByBrand(string brand)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogBffService.GetByBrandAsync(brand);
            var mappedResult = _mapper.Map<CatalogBrandDto>(result);
            return mappedResult;
        });
    }

    public async Task<CatalogTypeDto> GetByType(string type)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogBffService.GetByTypeAsync(type);
            var mappedResult = _mapper.Map<CatalogTypeDto>(result);
            return mappedResult;
        });
    }
}