using Catalog.Host.Models.Requests;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Order.Host.Models.BaseResponses;
using Order.Host.Services.Interfaces;

namespace Order.Host.Controllers;
[ApiController]
[Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
[Route(ComponentDefaults.DefaultRoute)]
public class OrderBffController : ControllerBase
{
    private readonly ILogger<OrderBffController> _logger;
    private readonly IOrderBffService _orderBffService;

    public OrderBffController(
        ILogger<OrderBffController> logger,
		IOrderBffService orderBffService)
    {
        _logger = logger;
		_orderBffService = orderBffService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(BaseOrderResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetOrdersByUserId()
    {
		var userId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
		var result = await _orderBffService.GetOrdersByUserIdAsync(userId);
        if (result == null)
        {
            return NotFound();
        }

        _logger.LogInformation($"Data: {JsonConvert.SerializeObject(result)}");

		var orderResponses = result.Select(orderDto => new BaseOrderResponse
		{
			Id = orderDto.Id,
			AmountProducts = orderDto.AmountProducts,
			TotalPriceItems = orderDto.TotalPriceItems,
			Items = orderDto.Items,
			UserId = userId
		}).ToList();

		return Ok(orderResponses);
    }

	[HttpPost]
	[ProducesResponseType(typeof(BaseOrderResponse), (int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.NotFound)]
	public async Task<IActionResult> GetOrderById(ByIdRequest request)
	{
		var result = await _orderBffService.GetOrdersByIdAsync(request.Id);

		if (result == null)
		{
			return NotFound();
		}

		_logger.LogInformation($"Data: {JsonConvert.SerializeObject(result)}");
		return Ok(new BaseOrderResponse
		{
			Id = result.Id,
			AmountProducts = result.AmountProducts,
			TotalPriceItems = result.TotalPriceItems,
			Items = result.Items,
			UserId = result.UserId,
		});
	}
}