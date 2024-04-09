using MVC.Host.ViewModels;

namespace MVC.Host.Services.Interfaces;

public interface IBasketService
{
	Task<bool> AddProduct(string userId, Product data);
	Task<List<Product>> GetProducts(string userId);
	Task<string?> RemoveBasket(string userId);
	Task<int?> RemoveProduct(string userId, int productId);
}