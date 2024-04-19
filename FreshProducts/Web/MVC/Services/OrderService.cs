using MVC.Models.Requests.BaseRequests;
using MVC.Models.Responses;
using MVC.Services.Interfaces;

namespace MVC.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly IHttpClientService _httpClient;
        private readonly ILogger<CatalogService> _logger;

        public OrderService(IHttpClientService httpClient, ILogger<CatalogService> logger, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;
            _logger = logger;
        }

        public async Task<int?> AddOrder(BaseOrderRequest product)
        {
            return await _httpClient.SendAsync<int?, BaseOrderRequest>
                ($"{_settings.Value.OrderOrderUrl}/addorder", HttpMethod.Post, product);
        }

		public async Task<object> AddOrderItem(BaseOrderItemRequest product)
		{
			product.CreatedAt = DateTime.Now.ToString("dd.MM.yyyy");

			return await _httpClient.SendAsync<object, BaseOrderItemRequest>
				($"{_settings.Value.OrderItemUrl}/addorderitem", HttpMethod.Post, product);
		}

		public async Task<List<OrderResponse>> GetOrderProductsAsync()
        {
            return await _httpClient.SendAsync<List<OrderResponse>, object>
                ($"{_settings.Value.OrderUrl}/getordersbyuserid", HttpMethod.Post, null);
        }
    }
}
