using MVC.Models.Requests.BaseRequests;
using MVC.Models.Responses;

namespace MVC.Services.Interfaces
{
    public interface IOrderService
    {
		Task<List<OrderResponse>> GetOrderProductsAsync();
		Task<object> AddOrderItem(BaseOrderItemRequest product);
		Task<int?> AddOrder(BaseOrderRequest product);
	}
}
