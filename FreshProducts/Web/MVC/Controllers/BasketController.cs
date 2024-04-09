using MVC.ViewModels;
using MVC.Services.Interfaces;
using MVC.ViewModels.BasketViewModels;


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
            var vm = new BasketViewModel
            {
                Data = basket,
            };
            return View(vm);
        }

		public async Task<IActionResult> AddProduct(BasketProduct product)
		{
			await _basketService.AddOrUpdateBasketProductAsync(product);
			return RedirectToAction("Index", "Basket");
		}

		public async Task<IActionResult> DeleteProduct(int productId)
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
