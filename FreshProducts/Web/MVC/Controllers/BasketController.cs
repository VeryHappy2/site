using MVC.ViewModels;
using MVC.Services.Interfaces;
using MVC.ViewModels.BasketViewModels;
using MVC.Models.Requests;

namespace MVC.Controllers
{
    public class BasketController : Controller
    {
		private readonly IBasketService _basketService;

		public BasketController(IBasketService basketService)
		{
			_basketService = basketService;
		}

		public async Task<IActionResult> Index()
        {
            List<BasketProduct> basket = await _basketService.GetBasketProductsAsync();

			if (basket == null)
			{
                BasketViewModel vmNull = new BasketViewModel
				{
					Data = null
				};

				return View(vmNull);
            }

			var amount = basket.Sum(product => product.Amount);
            var sumPrice = basket.Sum(product => product.ProductPrice);

            var vm = new BasketViewModel
            {
                Data = basket,
				Amount = amount,
				SumPrice = sumPrice
            };

            return View(vm);
        }

		public async Task<IActionResult> AddProduct(BasketProduct product)
		{
			await _basketService.AddOrUpdateBasketProductAsync(product);
			return RedirectToAction("Index", "Basket");
		}

		public async Task<IActionResult> DeleteProduct(ByIdRequest productId)
		{
			await _basketService.DeleteProductAsync(productId);
			return RedirectToAction("Index", "Basket");
		}

		public async Task DeleteBasket()
		{
			await _basketService.DeleteBasketProductsAsync();
		}
	}
}
