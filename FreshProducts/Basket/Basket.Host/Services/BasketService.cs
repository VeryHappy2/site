using MVC.Host.ViewModels;
using MVC.Host.Services.Interfaces;

namespace MVC.Host.Services;

public class BasketService : IBasketService
{
    private readonly ICacheService _cacheService;

    public BasketService(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

	public async Task<bool> AddProduct(string userId, Product data)
	{
        List<Product> products = await _cacheService.GetAsync<List<Product>>(userId);

		if(data == null)
		{
			return false;
		}

		if (products == null)
		{
			products = new List<Product>();
		}

		var excited = products.FirstOrDefault(x => x.ProductId == data.ProductId); 

        if (excited != null)
        {
            products[products.IndexOf(excited)].Amount++;
        }
        else
        {
            products.Add(new Product() { ProductId = data.ProductId, PictureUrl = data.PictureUrl, ProductName = data.ProductName, ProductPrice = data.ProductPrice, ProductBrand = data.ProductBrand, ProductType = data.ProductType, ProductDescription = data.ProductDescription, Amount = data.Amount, AvailableStock = data.AvailableStock});   
        }
		await _cacheService.AddOrUpdateAsync(userId, products);
		
        return true;
	}

	public async Task<List<Product>> GetProducts(string userId)
	{
		List<Product> products = await _cacheService.GetAsync<List<Product>>(userId);
        return products;
	}

	public async Task<string?> RemoveBasket(string userId)
	{
        if(userId != null)
        {
			await _cacheService.RemoveFromCacheAsync(userId);
            return "The basket successfully deleted";
		}
        return null;
        
	}

	public async Task<int?> RemoveProduct(string userId, int productId)
	{
		List<Product> basket = await _cacheService.GetAsync<List<Product>>(userId);
		var productToRemove = basket.FirstOrDefault(x => x.ProductId == productId);

		if (productToRemove != null && basket[basket.IndexOf(productToRemove)].Amount >= 1)
		{
			basket[basket.IndexOf(productToRemove)].Amount--;

            if(basket[basket.IndexOf(productToRemove)].Amount < 1)
            {
                basket.Remove(productToRemove);
            }

			await _cacheService.AddOrUpdateAsync(userId, basket);
			return productToRemove.Amount;
		}
		
		return null;
	}
}