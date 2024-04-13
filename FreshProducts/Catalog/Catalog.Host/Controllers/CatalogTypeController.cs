using Catalog.Host.Models.BaseRequests;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Requests.Update;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowClientPolicy)]
[Scope("catalog.catalogtype")]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogTypeController : ControllerBase
{
    private readonly ILogger<CatalogTypeController> _logger;
    private readonly ICatalogTypeService _catalogTypeService;

    public CatalogTypeController(
        ILogger<CatalogTypeController> logger,
        ICatalogTypeService catalogTypeService)
    {
        _catalogTypeService = catalogTypeService;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(typeof(BaseResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(BaseTypeRequest request)
    {
        int? result = await _catalogTypeService.Add(request.Type);

        if (result != null)
        {
            _logger.LogInformation($"Type was added with id: {result}");
        }

        return Ok(new BaseResponse<int?>() { Id = result });
    }

    [HttpPost]
    [ProducesResponseType(typeof(BaseResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(UpdateTypeRequest request)
    {
        int? result = await _catalogTypeService.Update(request.Id, request.Type);

        if (result != null)
        {
            _logger.LogInformation($"Type was updated with id: {result}");
        }

        return Ok(new BaseResponse<int?>() { Id = result });
    }

    [HttpPost]
    public async Task<IActionResult> Delete(ByIdRequest request)
    {
        string result = await _catalogTypeService.Delete(request.Id);
        if (result == null)
        {
            return NotFound();
        }

        _logger.LogInformation(result);
        return Ok(result);
    }
}