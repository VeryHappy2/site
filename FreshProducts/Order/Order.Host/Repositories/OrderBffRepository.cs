using IdentityModel;
using Order.Host.Data;
using Order.Host.Data.Entities;
using Order.Host.Repositories.Interfaces;

namespace Order.Host.Repositories
{
    public class OrderBffRepository : IOrderBffRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderBffRepository(IDbContextWrapper<ApplicationDbContext> context)
        {
            _dbContext = context.DbContext;
        }

        public async Task<OrderEntity> GetOrdersByUserIdAsync(string userId)
        {
            var result = await _dbContext.OrderEntity
				.Include(o => o.Items)
				.FirstOrDefaultAsync(x => x.UserId == userId);

            if(result == null)
            {
                return null;
            }

            return result;
        }

		public async Task<OrderEntity> GetOrdersByIdAsync(int id)
		{
			var result = await _dbContext.OrderEntity
				.Include(o => o.Items)
				.FirstOrDefaultAsync(x => x.Id == id);

			if (result == null)
			{
				return null;
			}

			return result;
		}
	}
}
