using Catalog.Host.Models.BaseRequests;
using Catalog.Host.Models.BaseResponses;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Requests.BaseRequests;
using Catalog.Host.Models.Requests.Paginates;
using Catalog.Host.Models.Response;
using Catalog.Host.Models.Response.BaseResponses;
using Catalog.Host.Models.Response.Paginations;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogBffController : ControllerBase
{
    private readonly ILogger<CatalogBffController> _logger;
    private readonly ICatalogService _catalogService;

    public CatalogBffController(
        ILogger<CatalogBffController> logger,
        ICatalogService catalogService)
    {
        _logger = logger;
        _catalogService = catalogService;
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Items(PaginatedItemsRequest<CatalogTypeFilter> request)
    {
        var result = await _catalogService.GetCatalogItemsAsync(request.PageSize, request.PageIndex, request.Filters, request.Search);
        if(result == null) 
        { 
             return NotFound();
        }
        foreach(var item in result.Data) 
        {
            _logger.LogInformation($"Data: {item}");
        }
        
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(BaseProductResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetById(ByIdRequest request)
    {
        var result = await _catalogService.GetById(request.Id);
        if(result == null)
        {
            return NotFound();
        }
        _logger.LogInformation($"Catalog item was got object with name: {result}");
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(BaseBrandResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetByBrand(BaseBrandRequest request)
    {
        var result = await _catalogService.GetByBrand(request.Brand);
        if(result == null)
        {
            
            return NotFound();
        }
        
        _logger.LogInformation($"Brand: {result}");
        
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(BaseProductResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetByType(BaseTypeRequest request)
    {
        var result = await _catalogService.GetByType(request.Type);
        if(result == null)
        {
            return NotFound();
        }
        _logger.LogInformation($"Type: {result}");
        return Ok(result);
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(List<CatalogBrandDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetBrands()
    {
        var result = await _catalogService.GetCatalogBrands();

        if (result == null)
        {
            return NotFound();
        }

        foreach (var catalogBrand in result)
        {
            _logger.LogInformation($"Brand: {catalogBrand}");
        }

        return Ok(result);
    }

    [HttpPost]
	[AllowAnonymous]
	[ProducesResponseType(typeof(List<CatalogTypeDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetTypes()
    {
        var result = await _catalogService.GetCatalogTypes();

        if (result == null)
        {
            return NotFound();
        }

        foreach (var catalogType in result)
        {
            _logger.LogInformation($"Type: {catalogType}");
        }

        return Ok(result);
    }
}