using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Order.Host.Data.Entities;
using Order.Host.Models.Requests.BaseRequests;
using Order.Host.Models.Requests.Update;
using Order.Host.Services.Interfaces;

namespace Order.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
[Route(ComponentDefaults.DefaultRoute)]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderBffController> _logger;
    private readonly IService<OrderEntity> _service;

    public OrderController(
        ILogger<OrderBffController> logger,
		IService<OrderEntity> service)
    {
        _logger = logger;
		_service = service;
    }

    [HttpPost]
    [ProducesResponseType(typeof(BaseResponse<int?>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> AddOrder(BaseOrderRequest request)
    {
		var userId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;

		var orderItems = request.Items.Select(item => new OrderItemEntity
		{
			Name = item.Name,
			CreatedAt = item.CreatedAt,
			Price = item.Price,
			Amount = item.Amount,
			ProductId = item.ProductId
		}).ToList();

		var result = await _service.AddAsync(new OrderEntity
        {
            UserId = userId,
            AmountProducts = request.AmountProducts,
            Items = orderItems,
            TotalPriceItems = request.TotalPriceItems,
        });

        if (result == null)
        {
            _logger.LogError("Order wasn't added");
            return BadRequest();
        }

        _logger.LogInformation($"Order was created with id: {result}");

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(BaseResponse<int?>), (int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	public async Task<IActionResult> UpdateOrder(UpdateOrderRequest request)
    {
        string userId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;

		var orderItems = request.Items.Select(item => new OrderItemEntity
		{
			Name = item.Name,
			CreatedAt = item.CreatedAt,
			Price = item.Price,
			Amount = item.Amount,
			ProductId = item.ProductId
		}).ToList();

		var result = await _service.UpdateAsync(request.Id, new OrderEntity
        {
            Id = request.Id,
            AmountProducts = request.AmountProducts,
            Items = orderItems,
            TotalPriceItems = request.TotalPriceItems,
            UserId = userId
		});

        if (result == null)
        {
            _logger.LogError("Order wasn't updated.");
            return BadRequest();
        }

        _logger.LogInformation($"id of a new order: {result}.");
        return Ok(new BaseResponse<int?> { Id = result });
	}

	[HttpPost]
	[AllowAnonymous]
	[ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.NotFound)]
	public async Task<IActionResult> DeleteOrder(ByIdRequest request)
	{
		var result = await _service.DeleteAsync(request.Id);

		if (result == null)
		{
            _logger.LogError($"Order wasn't found.");
            return NotFound();
		}

		_logger.LogInformation($"Status of order: {result}.");

		return Ok(result);
	}
}