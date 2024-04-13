using Order.Host.Data;
using Order.Host.Data.Entities;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services.Interfaces;

namespace Order.Host.Services;

public class OrderService : BaseDataService<ApplicationDbContext>, IService<OrderEntity>
{
    private readonly IDbContextWrapper<ApplicationDbContext> _dbContextWrapper;
    private readonly IRepository<OrderEntity> _repository;

    public OrderService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        IMapper mapper,
        IRepository<OrderEntity> repository)
        : base(dbContextWrapper, logger)
    {
        _dbContextWrapper = dbContextWrapper;
        _repository = repository;
    }

    public async Task<int?> AddAsync(OrderEntity entity)
    {
        return await ExecuteSafeAsync(async () =>
        {
            entity.AmountProducts = entity.Items.Count();

            foreach (var item in entity.Items)
            {
                entity.TotalPriceItems = entity.AmountProducts * item.Price;
            }

            return await _repository.AddAsync(entity);
        });
    }

    public async Task<string?> DeleteAsync(int id)
    {
        return await ExecuteSafeAsync(async () =>
        {
            return await _repository.DeleteAsync(id);
        });
    }

    public async Task<int?> UpdateAsync(int id, OrderEntity entity)
    {
        return await ExecuteSafeAsync(async () =>
        {
            return await _repository.UpdateAsync(id, entity);
        });
    }
}