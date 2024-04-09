using Order.Host.Data;
using Order.Host.Data.Entities;
using Order.Host.Models.Dtos;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services.Interfaces;

namespace Order.Host.Services;

public class OrderBffService : BaseDataService<ApplicationDbContext>, IOrderBffService
{
    private readonly IMapper _mapper;
    private readonly IDbContextWrapper<ApplicationDbContext> _dbContextWrapper;
    private readonly IOrderBffRepository _orderBffRepository;

    public OrderBffService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        IMapper mapper,
        IOrderBffRepository orderBffRepository)
        : base(dbContextWrapper, logger)
    {
        _orderBffRepository = orderBffRepository;
        _dbContextWrapper = dbContextWrapper;
        _mapper = mapper;
    }

    public async Task<OrderDto?> GetOrdersByUserIdAsync(string id)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _orderBffRepository.GetOrdersByUserIdAsync(id);

			var mappedResult = _mapper.Map<OrderDto>(result);

			return mappedResult;
		});
    }

	public async Task<OrderDto?> GetOrdersByIdAsync(int id)
	{
		return await ExecuteSafeAsync(async () =>
		{
			var result = await _orderBffRepository.GetOrdersByIdAsync(id);

			var mappedResult = _mapper.Map<OrderDto>(result);

			return mappedResult;
		});
	}
}