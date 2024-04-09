using MVC.ViewModels;

namespace MVC.Services.Interfaces
{
	public interface IBasketService
	{
		Task<List<BasketProduct>> GetBasketProductsAsync();
		Task AddOrUpdateBasketProductAsync(BasketProduct product);
		Task<string?> DeleteBasketProductsAsync();
		Task DeleteProductAsync(int productId);
		Task<List<BasketProduct>> ReleaseBasketAsync();
	}
}
