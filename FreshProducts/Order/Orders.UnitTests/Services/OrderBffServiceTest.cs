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

		private List<OrderEntity> _testOrders = new List<OrderEntity>
		{
			new OrderEntity
			{
				Id = 1,
				AmountProducts = 1,
				TotalPriceItems = 1,
				UserId = "123",
				Items = new List<OrderItemEntity>()
			},
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

		//[Fact]
		//public async Task GetOrdersByUserIdAsync_Success()
		//{
		//	// arrange
		//	var ordersDto = new List<OrderDto>();
		//	foreach (OrderEntity order in _testOrders)
		//	{
		//		ordersDto = new List<OrderDto>().Select(x => new OrderDto
		//		{
		//			Id = order.Id,
		//			Items = order.Items.Select(x => new OrderItemDto
		//			{
		//				OrderId = order.Id,
		//				Amount = order.AmountProducts,
		//				Id = order.Id,
		//				CreatedAt = order.CreatedAt
		//			})
		//		});
		//	}


		//	_orderBffRepository.Setup(s => s.GetOrdersByUserIdAsync(
		//		It.IsAny<string>())).ReturnsAsync(_testOrders);

		//	_mapper.Setup(s => s.Map<OrderDto>(
		//		It.Is<OrderEntity>(i => i.Equals(_testOrders)))).Returns(ordersDto);

		//	// act
		//	var result = await _orderBffService.GetOrdersByUserIdAsync(orderDto.UserId);

		//	// assert
		//	result.Should()
		//		.NotBeNull();
		//}

		[Fact]
		public async Task GetOrdersByUserIdAsync_Failed()
		{
			// arrange
			string userId = null;

			_testOrders = null;
			OrderDto orderDto = null;

			_orderBffRepository.Setup(s => s.GetOrdersByUserIdAsync(
				It.IsAny<string>())).ReturnsAsync(_testOrders);

			_mapper.Setup(s => s.Map<OrderDto>(
				It.Is<OrderEntity>(i => i.Equals(_testOrders)))).Returns(orderDto);

			// act
			var result = await _orderBffService.GetOrdersByUserIdAsync(userId);

			// assert
			result.Should()
				.BeNull();
		}
	}


}
