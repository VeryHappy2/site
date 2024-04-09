using MVC.Host.Services;
using MVC.Host.Services.Interfaces;
using MVC.Host.ViewModels;
using System.Collections.Generic;
namespace Basket.UnitTests.Services
{
	public class BasketServiceTest
	{
		private readonly IBasketService _service;

		private readonly Mock<ICacheService> _cacheService;

		List<Product> _products = new List<Product>
		{ 
			new Product
			{
				ProductId = 2,
				Amount = 1,
			}
		};
		Product _product = new Product();
		public BasketServiceTest()
		{
			_cacheService = new Mock<ICacheService>();
			_service = new BasketService(_cacheService.Object);
		}

		[Fact]
		public async Task AddProduct_Success()
		{
			var userId = "2131256893";

			_cacheService.Setup(x => x.GetAsync<List<Product>>(It.IsAny<string>()))
				.ReturnsAsync(_products);

			await _service.AddProduct(userId, _product);

			_cacheService.Verify(x => x.AddOrUpdateAsync(userId, It.IsAny<List<Product>>()), Times.Once);
		}
		[Fact]
		public async Task AddProduct_Failed()
		{
			var userId = "24124233257";
			_product = null;
			_cacheService.Setup(x => x.GetAsync<List<Product>>(It.IsAny<string>()))
			 .ReturnsAsync((List<Product>)null);
			_cacheService.Setup(x => x.AddOrUpdateAsync(It.IsAny<string>(), It.IsAny<Product>()));
			
			var result = await _service.AddProduct(userId, _product);
			result.Should().Be(false);
		}

		[Fact]
		public async Task GetProduct__Success()
		{
			var userId = "2131256893";

			_cacheService.Setup(x => x.GetAsync<List<Product>>(It.IsAny<string>()))
			 .ReturnsAsync(_products);

			var result = await _service.GetProducts(userId);

			result.Should().NotBeNull();
		}
		[Fact]
		public async Task GetProduct_Failed()
		{
			string? userId = null;

			_cacheService.Setup(x => x.GetAsync<List<Product>>(It.IsAny<string>()))
			 .ReturnsAsync((List<Product>)null);

			var result = await _service.GetProducts(userId);

			result
				.Should().BeNullOrEmpty();
		}

		[Fact]
		public async Task RemoveProduct_Success()
		{
			var userId = "2131256893";
			
			_cacheService.Setup(x => x.GetAsync<List<Product>>(It.IsAny<string>()))
			 .ReturnsAsync(_products);
			_cacheService.Setup(x => x.AddOrUpdateAsync(It.IsAny<string>(), It.IsAny<List<Product>>()));

			var result = await _service.RemoveProduct(userId, 2);

			result.Should().NotBeNull();
		}

		[Fact]
		public async Task RemoveProduct_Failed()
		{
			var userId = "2131256893";
			_products.Clear();
			_cacheService.Setup(x => x.GetAsync<List<Product>>(It.IsAny<string>()))
			 .ReturnsAsync(_products);
			_cacheService.Setup(x => x.AddOrUpdateAsync(It.IsAny<string>(), It.IsAny<List<Product>>()));

			var result = await _service.RemoveProduct(userId, 1);

			result.Should().BeNull();
		}
		[Fact]
		public async Task RemoveBasket_Success()
		{
			string? userId = "2131256893";
			_cacheService.Setup(x => x.RemoveFromCacheAsync(It.IsAny<string>(), It.IsAny<IDatabase>())).Returns(Task.CompletedTask);

			var result = await _service.RemoveBasket(userId);

			result.Should().NotBeNullOrEmpty();
		}

		[Fact]
		public async Task RemoveBasket_Failed()
		{
			string? userId = null;
			_cacheService.Setup(x => x.RemoveFromCacheAsync(It.IsAny<string>(), It.IsAny<IDatabase>())).Returns(Task.CompletedTask);

			var result = await _service.RemoveBasket(userId);

			result.Should().BeNull();
		}
	}
}