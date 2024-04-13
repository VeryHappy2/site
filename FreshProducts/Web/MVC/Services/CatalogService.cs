using MVC.Host.Models.Responses;
using MVC.Models.Enums;
using MVC.Models.Requests;
using MVC.Services.Interfaces;
using MVC.ViewModels;

namespace MVC.Services;

public class CatalogService : ICatalogService
{
    private readonly IOptions<AppSettings> _settings;
    private readonly IHttpClientService _httpClient;
    private readonly ILogger<CatalogService> _logger;

    public CatalogService(IHttpClientService httpClient, ILogger<CatalogService> logger, IOptions<AppSettings> settings)
    {
        _httpClient = httpClient;
        _settings = settings;
        _logger = logger;
    }

    public async Task<PaginatedItemsResponse<CatalogItem>> GetItems(int pageIndex, int pageSize, int? brand, int? type, string? search)
    {
        await Task.Delay(300);

        var filters = new Dictionary<CatalogTypeFilter, int>();

        if (brand.HasValue)
        {
            filters.Add(CatalogTypeFilter.Brand, brand.Value);
        }

        if (type.HasValue)
        {
            filters.Add(CatalogTypeFilter.Type, type.Value);
        }

		return await _httpClient.SendAsync<PaginatedItemsResponse<CatalogItem>, object>
            ($"{_settings.Value.CatalogUrl}/items", HttpMethod.Post, new PaginatedItemsRequest<CatalogTypeFilter>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Search = search,
                Filters = filters,
            });
    }

    public async Task<IEnumerable<SelectListItem>> GetBrands()
    {
        await Task.Delay(300);
        var serverList = await _httpClient.SendAsync<List<CatalogBrand>, object>
            ($"{_settings.Value.CatalogUrl}/getbrands", HttpMethod.Post, null);

        return serverList.Select(item => new SelectListItem { Text = item.Brand, Value = item.Id.ToString() }).ToList();
    }

    public async Task<IEnumerable<SelectListItem>> GetTypes()
    {
        await Task.Delay(300);
        var serverList = await _httpClient.SendAsync<List<CatalogType>, object>
            ($"{_settings.Value.CatalogUrl}/gettypes", HttpMethod.Post, null);

        return serverList.Select(item => new SelectListItem { Text = item.Type, Value = item.Id.ToString() }).ToList();
    }
}
