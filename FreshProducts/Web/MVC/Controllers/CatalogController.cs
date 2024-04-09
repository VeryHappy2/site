using MVC.Host.Models.Responses;
using MVC.Services.Interfaces;
using MVC.ViewModels;
using MVC.ViewModels.CatalogViewModels;
using MVC.ViewModels.Pagination;
using Newtonsoft.Json;
using System.Drawing.Printing;


namespace MVC.Controllers;

public class CatalogController : Controller
{
	private readonly IBasketService _basketService;
	private  readonly ICatalogService _catalogService;
	private readonly ILogger<CatalogController> _logger;
	public CatalogController(ICatalogService catalogService, 
        IBasketService basketService, ILogger<CatalogController> logger)
    {
        _basketService = basketService;
        _catalogService = catalogService;
		_logger = logger;
    }

    public async Task<IActionResult> Index(int? brandFilterApplied, int? typesFilterApplied, int? groupPage, int? page, int? pageSize)
    {
        await Task.Delay(50);
        page ??= 0;
        pageSize ??= 4;
        groupPage ??= 0;

        PaginatedItemsResponse<CatalogItem> catalogItems = await _catalogService.GetItems(page.Value, pageSize.Value, brandFilterApplied, typesFilterApplied, null);

		PaginationInfo info = null;
		IndexViewModel vm = new IndexViewModel() 
		{ 
			CatalogItems = null
		};

		if (catalogItems != null)
		{
			info = new PaginationInfo
			{
				TotalPages = (int)Math.Ceiling((decimal)catalogItems.Count / pageSize.Value),
				GroupPages = groupPage.Value,
				CurrentPage = page.Value,
				MaxPages = 4,
				TotalItems = catalogItems.Count,
				ItemsPerPage = catalogItems.Data.Count(),
			};
			vm = new IndexViewModel
			{
				CatalogItems = catalogItems.Data,
				PaginationItems = info,
				Brands = await _catalogService.GetBrands(),
				Types = await _catalogService.GetTypes(),
			};
		}

		return View(vm);
    }
    public async Task<IActionResult> SearchOrAddBasket(string search, int? brandFilterApplied, int? typesFilterApplied, int? groupPage, CatalogItem product, int? page, int? pageSize)
    {
        await Task.Delay(50);

		_logger.LogInformation($"Product: {JsonConvert.SerializeObject(product)}");

		if (product != null) 
        {
			var basketProduct = new BasketProduct
			{
				ProductBrand = product.Brand,
				PictureUrl = product.PictureUrl,
				ProductName = product.Name,
				ProductDescription = product.Description,
				Amount = 1,
				ProductId = product.Id,
				ProductPrice = product.Price,
				ProductType = product.Type,
				AvailableStock = product.AvailableStock,
			};
			await _basketService.AddOrUpdateBasketProductAsync(basketProduct);
        }

		page ??= 0;
		pageSize ??= 5;
		groupPage ??= 0;

		PaginatedItemsResponse<CatalogItem> catalogItems = await _catalogService.GetItems(page.Value, pageSize.Value, brandFilterApplied, typesFilterApplied, search);

		
        var info = new PaginationInfo
        {
			TotalPages = (int)Math.Ceiling((decimal)catalogItems.Count / pageSize.Value),
            GroupPages = groupPage.Value,
			CurrentPage = page.Value,
			MaxPages = 5,
			TotalItems = catalogItems.Count,
		};


		var vm = new IndexViewModel
        {
            CatalogItems = catalogItems.Data,
            PaginationItems = info,
			Brands = await _catalogService.GetBrands(),
			Types = await _catalogService.GetTypes(),
			Search = search,
        };

        return View("Index", vm);
    }
}