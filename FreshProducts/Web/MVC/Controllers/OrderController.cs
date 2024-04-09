using MVC.Models.Requests.BaseRequests;
using MVC.Services;
using MVC.Services.Interfaces;
using MVC.ViewModels;
using MVC.ViewModels.OrderViewModels;

namespace MVC.Controllers
{
    public class OrderController : Controller
    {
		private readonly IOrderService _orderService;
		private readonly IBasketService _basketService;

		public OrderController(
			IOrderService orderService
			, IBasketService basketService)
		{
			_orderService = orderService;
			_basketService = basketService;
		}
		public async Task<IActionResult> Index()
        {
			var order = await _orderService.GetOrderProductsAsync();

			var vm = new IndexViewModel
			{
				Order = order,
			};
			return View(vm);
        }

		public async Task<IActionResult> AddOrder()
		{
			var resultGet = await _basketService.ReleaseBasketAsync();

			var orderItems = resultGet.Select(product => new BaseOrderItemRequest
			{
				Name = product.ProductName,
				Price = product.ProductPrice,
				Amount = product.Amount,
				CreatedAt = DateTime.Now.ToString("dd.MM.yyyy"),
				ProductId = product.ProductId,
			}).ToList();

			var order = new BaseOrderRequest
			{
				Items = orderItems,
				AmountProducts = 0,
				TotalPriceItems = 0
			};
			await _orderService.AddOrder(order);

			return RedirectToAction("Index", "Basket");
		}
	}
}
