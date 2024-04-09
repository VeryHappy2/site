using MVC.Services.Interfaces;
using MVC.ViewModels;

namespace MVC.Services
{
	public class BasketService : IBasketService
	{

		private readonly IOptions<AppSettings> _settings;
		private readonly IHttpClientService _httpClient;
		private readonly ILogger<CatalogService> _logger;

		public BasketService(IHttpClientService httpClient, ILogger<CatalogService> logger, IOptions<AppSettings> settings)
		{
			_httpClient = httpClient;
			_settings = settings;
			_logger = logger;
		}
		public async Task AddOrUpdateBasketProductAsync(BasketProduct product)
		{
			await Task.Delay(150);
			await _httpClient.SendAsync<object, object>
				($"{_settings.Value.BasketUrl}/addorupdateproduct", HttpMethod.Post, product);
		}

		public async Task DeleteProductAsync(int productId)
		{
			await Task.Delay(150);

			await _httpClient.SendAsync<object, object>
				($"{_settings.Value.BasketUrl}/removeproduct", HttpMethod.Post, productId);
		}

		public async Task<string> DeleteBasketProductsAsync()
		{
			await Task.Delay(150);

			return  await _httpClient.SendAsync<string, object>
				($"{_settings.Value.BasketUrl}/removeproducts", HttpMethod.Post, null);
		}

		public async Task<List<BasketProduct>> GetBasketProductsAsync()
		{
			await Task.Delay(150);
			return await _httpClient.SendAsync<List<BasketProduct>, object>
				($"{_settings.Value.BasketUrl}/getproducts", HttpMethod.Post, null);
		}

		public async Task<List<BasketProduct>> ReleaseBasketAsync()
		{
			await Task.Delay(150);

			return await _httpClient.SendAsync<List<BasketProduct>, object>
				($"{_settings.Value.BasketUrl}/releasebasket", HttpMethod.Post, null);
		}
	}
}
