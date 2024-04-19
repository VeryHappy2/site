using Order.Host.Data;
using Order.Host.Data.Entities;
using Order.Host.Repositories.Interfaces;

namespace Order.Host.Repositories
{
    public class OrderItemRepository : IRepository<OrderItemEntity>
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderItemRepository(IDbContextWrapper<ApplicationDbContext> context)
        {
            _dbContext = context.DbContext;
        }

        public async Task<int?> AddAsync(OrderItemEntity entity)
        {
			var excitedItem = await _dbContext.OrderItemEntity.FirstOrDefaultAsync(x => x.ProductId == entity.ProductId);
			var excitedOrder = await _dbContext.OrderEntity
                .Include(o => o.Items)
                .FirstOrDefaultAsync(x => x.Id == entity.OrderId);

            if (excitedItem != null && excitedOrder != null)
            {
                excitedItem.Amount += entity.Amount;
                excitedOrder.AmountProducts += entity.Amount;
                excitedOrder.TotalPriceItems += entity.Price * entity.Amount;
                excitedOrder.Items.Add(entity);

                await _dbContext.SaveChangesAsync();

                return entity.Id;
            }
            else if (excitedItem != null && excitedOrder == null)
            {
                excitedItem.Amount += entity.Amount;

                await _dbContext.SaveChangesAsync();
                return entity.Id;
            }
            else if (excitedItem == null && excitedOrder == null)
            {
				var result = await _dbContext.OrderItemEntity.AddAsync(entity);
                return result.Entity.Id;
			}
            else
            {
                var result = await _dbContext.OrderItemEntity.AddAsync(entity);

                if (result == null)
                {
                    return null;
                }

                excitedOrder.AmountProducts += entity.Amount;
                excitedOrder.TotalPriceItems += entity.Price * entity.Amount;
                excitedOrder.Items.Add(entity);
                await _dbContext.SaveChangesAsync();

                return result.Entity.Id;
            }
        }

        public async Task<int?> UpdateAsync(int id, OrderItemEntity entity)
		{
			var result = await _dbContext.OrderItemEntity.FindAsync(id);

            if (result != null)
            {
                _dbContext.Entry(result).CurrentValues.SetValues(entity);
                await _dbContext.SaveChangesAsync();
                return result.Id;
            }

            return null;
        }

        public async Task<string?> DeleteAsync(int id)
        {
			var result = await _dbContext.OrderItemEntity
				.FindAsync(id);

            if (result != null)
            {
				_dbContext.OrderItemEntity.Remove(result);
                await _dbContext.SaveChangesAsync();
                return $"Object with id: {id} was successfully removed";
            }

            return null;
        }
	}
}
