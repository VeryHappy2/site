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
    private readonly ICatalogTypeService _catalogBrandService;

    public CatalogBrandController(
        ILogger<CatalogBrandController> logger,
        ICatalogTypeService catalogBrandService)
    {
        _catalogBrandService = catalogBrandService;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(typeof(BaseResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(BaseBrandRequest request)
    {
        int? result = await _catalogBrandService.Add(request.Brand);

        if (result == null)
        {
            return NotFound();
        }

        _logger.LogInformation($"Brand was added with id: {result}");

        return Ok(new BaseResponse<int?>() { Id = result });
    }

    [HttpPost]
    [ProducesResponseType(typeof(BaseResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(UpdateBrandRequest request)
    {
        int? result = await _catalogBrandService.Update(request.Id, request.Brand);
        if (result == null)
        {
            return NotFound();
        }
        _logger.LogInformation($"Brand was updated with id: {result}");
        return Ok(new BaseResponse<int?>() { Id = result });
    }

    [HttpPost]
    public async Task<IActionResult> Delete(ByIdRequest request)
    {
        string result = await _catalogBrandService.Delete(request.Id);

        if (result == null)
        {
            return NotFound();
        }

        _logger.LogInformation(result);

        return Ok(result);
    }
}