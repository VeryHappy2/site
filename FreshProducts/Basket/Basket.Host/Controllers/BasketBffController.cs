using MVC.Host.ViewModels;
using MVC.Host.Services.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Basket.Host.Models.Reqeusts;
using Infrastructure.Filters;

namespace MVC.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
[Route(ComponentDefaults.DefaultRoute)]
public class BasketBffController : ControllerBase
{
    private readonly ILogger<BasketBffController> _logger;
    private readonly IBasketService _basketService;

	public BasketBffController(
        ILogger<BasketBffController> logger,
        IBasketService basketService)
    {
        _logger = logger;
        _basketService = basketService;
    }

	[HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	public async Task<IActionResult> AddOrUpdateProduct(Product data)
	{
		var userId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
		_logger.LogInformation($"UserId: {userId}" + $"\n Data of Product: {data}");

		if (data == null)
		{
			_logger.LogInformation($"Data is empty");
			return BadRequest();
		}

		await _basketService.AddProduct(userId!, data);
		return Ok();
	}

    [HttpPost]
    [ProducesResponseType(typeof(List<Product>), (int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.NotFound)]
	public async Task<IActionResult> GetProducts()
    {
        var userId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
        var response = await _basketService.GetProducts(userId!);

		if (response == null)
		{
			_logger.LogError($"User id {userId} doesn't contain products");
			return NotFound();
		}

		return Ok(response);
    }

	[HttpPost]
    [RateLimitFilterAtribute(15)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.NotFound)]
	public async Task<IActionResult> RemoveProducts()
	{
		var userId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;

		var result = await _basketService.RemoveBasket(userId!);
		return Ok(result);
	}

	[HttpPost]
    [ProducesResponseType(typeof(List<Product>), (int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.NotFound)]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	public async Task<IActionResult> RemoveProduct(ByIdrequest productId)
	{
		if (productId == null)
		{
            _logger.LogError($"Product id is empty");
            return BadRequest();
		}

		string userId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
		var result = await _basketService.RemoveProduct(userId!, productId.Id);

		if (result == null)
		{
			_logger.LogError("Product wasn't found or user hasn't any products");
			return NotFound();
		}

		return Ok(result);
	}

	[HttpPost]
    [ProducesResponseType(typeof(List<Product>), (int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.NotFound)]
	public async Task<IActionResult> ReleaseBasket()
	{
		var userId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
		var resultGet = await _basketService.GetProducts(userId!);

		if (resultGet == null)
		{
            _logger.LogError("User hasn't any products");
            return NotFound();
		}

		await _basketService.RemoveBasket(userId!);
		return Ok(resultGet);
	}
}