using Microsoft.EntityFrameworkCore.Storage;
using Order.Host.Data;
using Order.Host.Data.Entities;
using Order.Host.Models.Dtos;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services;
using Order.Host.Services.Interfaces;

namespace Orders.UnitTests.Services
{
	
	public class OrderBffServiceTest
	{
		private readonly IOrderBffService _orderBffService;

		private readonly Mock<IOrderBffRepository> _orderBffRepository;
		private readonly Mock<IMapper> _mapper;
		private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
		private readonly Mock<ILogger<OrderBffService>> _logger;

		private OrderEntity _testOrder = new OrderEntity
		{

			Id = 1,
			AmountProducts = 1,
			TotalPriceItems = 1,
			UserId = "123",
			Items = new List<OrderItemEntity>()

		};

		private OrderItemEntity _testItemOrder = new OrderItemEntity
		{
			Id = 2,
			Amount = 1,
			CreatedAt = DateTime.Now.ToString("dd.MM.yyyy"),
			Name = "Test",
			OrderId = 1,
			Price = 1,
			ProductId = 1,
		};

		public OrderBffServiceTest()
		{
			_orderBffRepository = new Mock<IOrderBffRepository>();
			_mapper = new Mock<IMapper>();
			_dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
			_logger = new Mock<ILogger<OrderBffService>>();

			var dbContextTransaction = new Mock<IDbContextTransaction>();
			_dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

			_orderBffService = new OrderBffService(_dbContextWrapper.Object, _logger.Object, _mapper.Object, _orderBffRepository.Object);
		}

		[Fact]
		public async Task GetOrdersByUserIdAsync_Success()
		{
			// arrange
			_testOrder.Id = 1;
			var orderDto = new OrderDto
			{
				Id = _testOrder.Id,
				AmountProducts = _testOrder.AmountProducts,
				UserId = _testOrder.UserId,
				Items = new List<OrderItemDto>(),
				TotalPriceItems = _testOrder.TotalPriceItems,
			};

			_orderBffRepository.Setup(s => s.GetOrdersByUserIdAsync(
				It.IsAny<string>())).ReturnsAsync(_testOrder);

			_mapper.Setup(s => s.Map<OrderDto>(
				It.Is<OrderEntity>(i => i.Equals(_testOrder)))).Returns(orderDto);

			// act
			var result = await _orderBffService.GetOrdersByUserIdAsync(orderDto.UserId);

			// assert
			result.Should()
				.NotBeNull();
		}

		[Fact]
		public async Task GetOrdersByUserIdAsync_Failed()
		{
			// arrange
			string userId = null;

			_testOrder = null;
			OrderDto orderDto = null;

			_orderBffRepository.Setup(s => s.GetOrdersByUserIdAsync(
				It.IsAny<string>())).ReturnsAsync(_testOrder);

			_mapper.Setup(s => s.Map<OrderDto>(
				It.Is<OrderEntity>(i => i.Equals(_testOrder)))).Returns(orderDto);

			// act
			var result = await _orderBffService.GetOrdersByUserIdAsync(userId);

			// assert
			result.Should()
				.BeNull();
		}
	}
		
	
}
