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
[Scope("catalog.catalogbrand")]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogBrandController : ControllerBase
{
    private readonly ILogger<CatalogBrandController> _logger;
    private readonly IService<CatalogBrand> _catalogBrandService;

    public CatalogBrandController(
        ILogger<CatalogBrandController> logger,
        IService<CatalogBrand> catalogBrandService)
    {
        _catalogBrandService = catalogBrandService;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(typeof(BaseResponse<int?>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Add(BaseBrandRequest request)
    {
        int? result = await _catalogBrandService.Add(new CatalogBrand
        {
            Brand = request.Brand,
        });

        if (result == null)
        {
            _logger.LogError($"Brand wasn't added");
            return BadRequest();
        }

        _logger.LogInformation($"Brand was added, id: {result}");

        return Ok(new BaseResponse<int?>() { Id = result });
    }

    [HttpPost]
    [ProducesResponseType(typeof(BaseResponse<int?>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Update(UpdateBrandRequest request)
    {
        int? result = await _catalogBrandService.Update(new CatalogBrand
        {
            Id = request.Id,
            Brand = request.Brand,
        });

        if (result == null)
        {
            _logger.LogError($"Brand wasn't found, id of request brand: {request.Id}");
            return NotFound();
        }

        _logger.LogInformation($"Brand was updated with id: {request.Id}");
        return Ok(new BaseResponse<int?>() { Id = result });
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Delete(ByIdRequest request)
    {
        string result = await _catalogBrandService.Delete(request.Id);

        if (result == null)
        {
            _logger.LogError($"Brand wasn't found, id: {result}");
            return NotFound();
        }

        _logger.LogInformation(result);

        return Ok(result);
    }
}