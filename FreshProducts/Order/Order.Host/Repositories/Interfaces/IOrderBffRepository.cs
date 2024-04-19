using Order.Host.Data.Entities;
using System.Threading.Tasks;

namespace Order.Host.Repositories.Interfaces
{
    public interface IOrderBffRepository
    {
		Task<List<OrderEntity>> GetOrdersByUserIdAsync(string userId);
		Task<OrderEntity> GetOrdersByIdAsync(int id);
	}
}
