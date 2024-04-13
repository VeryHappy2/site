using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Requests.BaseRequests;
using Catalog.Host.Models.Requests.Update;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowClientPolicy)]
[Scope("catalog.catalogitem")]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogItemController : ControllerBase
{
    private readonly ILogger<CatalogItemController> _logger;
    private readonly ICatalogItemService _catalogItemService;

    public CatalogItemController(
        ILogger<CatalogItemController> logger,
        ICatalogItemService catalogItemService)
    {
        _logger = logger;
        _catalogItemService = catalogItemService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(BaseResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(BaseProductRequest request)
    {
        var result = await _catalogItemService.Add(request.Name, request.Description, request.Price, request.AvailableStock, request.CatalogBrandId, request.CatalogTypeId, request.PictureFileName);
        if (result != null)
        {
            _logger.LogInformation($"Catalog item was added with id: {result}");
        }

        return Ok(new BaseResponse<int?>() { Id = result });
    }

    [HttpPost]
    [ProducesResponseType(typeof(BaseResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(UpdateProductRequest request)
    {
        var result = await _catalogItemService.Update(request.Id, new CatalogItem
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            AvailableStock = request.AvailableStock,
            CatalogBrandId = request.CatalogBrandId,
            CatalogTypeId = request.CatalogTypeId,
            PictureFileName = request.PictureFileName
        });

        if (result != null)
        {
            _logger.LogInformation($"Catalog item was updated with id: {result}");
        }

        return Ok(new BaseResponse<int?>() { Id = result });
    }

    [HttpPost]
    public async Task<IActionResult> Delete(ByIdRequest request)
    {
        var result = await _catalogItemService.Delete(request.Id);
        if (result == null)
        {
            return NotFound();
        }

        _logger.LogInformation(result);
        return Ok(result);
    }
}