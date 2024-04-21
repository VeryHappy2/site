using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Order.Host.Data.Entities;
using Order.Host.Models.Requests.BaseRequests;
using Order.Host.Models.Requests.Update;
using Order.Host.Services.Interfaces;

namespace Order.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowClientPolicy)]
[Scope("order.item")]
[Route(ComponentDefaults.DefaultRoute)]
public class OrderItemController : ControllerBase
{
    private readonly ILogger<OrderBffController> _logger;
    private readonly IService<OrderItemEntity> _service;

    public OrderItemController(
        ILogger<OrderBffController> logger,
		IService<OrderItemEntity> service)
    {
        _logger = logger;
        _service = service;
    }

	[HttpPost]
	[ProducesResponseType(typeof(BaseResponse<int?>), (int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	public async Task<IActionResult> AddOrderItem(BaseOrderItemRequest request)
	{
		var result = await _service.AddAsync(new OrderItemEntity
		{
			Name = request.Name,
			CreatedAt = request.CreatedAt,
			Price = request.Price,
			Amount = request.Amount,
			ProductId = request.ProductId
		});

		if (result == null)
		{
			return BadRequest();
		}

		_logger.LogInformation($"Order was created with id: {result}");

		return Ok(new BaseResponse<int?> { Id = result });
	}

	[HttpPost]
	[ProducesResponseType(typeof(BaseResponse<int?>), (int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	public async Task<IActionResult> UpdateOrderItem(UpdateOrderItemRequest request)
	{
		var result = await _service.UpdateAsync(request.Id, new OrderItemEntity
		{
			Name = request.Name,
			CreatedAt = request.CreatedAt,
			Price = request.Price,
			Amount = request.Amount,
		});
		if (result == null)
		{
			return BadRequest();
		}

		_logger.LogInformation($"Id of a new order: {result}");

		return Ok(result);
	}

	[HttpPost]
	[ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.NotFound)]
	public async Task<IActionResult> DeleteOrderItem(ByIdRequest request)
	{
		var result = await _service.DeleteAsync(request.Id);

		if (result == null)
		{
			return NotFound();
		}

		_logger.LogInformation($"Status of order: {result}");

		return Ok(result);
	}
}