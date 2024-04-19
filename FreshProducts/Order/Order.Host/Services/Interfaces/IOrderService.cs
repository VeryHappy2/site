using Order.Host.Data.Entities;
using Order.Host.Models.Dtos;
using Order.Host.Models.Requests.BaseRequests;

namespace Order.Host.Services.Interfaces
{
    public interface IOrderBffService
    {
		Task<List<OrderDto?>> GetOrdersByUserIdAsync(string orderId);
		Task<OrderDto?> GetOrdersByIdAsync(int id);
	}
}
