using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using Order.Host.Data;
using Order.Host.Data.Entities;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services;
using Order.Host.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.UnitTests.Services
{
	public class OrderItemServiceTest
	{
		private readonly IService<OrderItemEntity> _orderService;

		private readonly Mock<IRepository<OrderItemEntity>> _orderRepository;
		private readonly Mock<IMapper> _mapper;
		private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
		private readonly Mock<ILogger<OrderItemService>> _logger;

		private OrderItemEntity _testOrder = new OrderItemEntity
		{
			Id = 2,
			Amount = 1,
			CreatedAt = DateTime.Now.ToString("dd.MM.yyyy"),
			Name = "Test",
			OrderId = 1,
			Price = 1,
			ProductId = 1,
		};

		public OrderItemServiceTest()
		{
			_orderRepository = new Mock<IRepository<OrderItemEntity>>();
			_mapper = new Mock<IMapper>();
			_dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
			_logger = new Mock<ILogger<OrderItemService>>();

			var dbContextTransaction = new Mock<IDbContextTransaction>();
			_dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

			_orderService = new OrderItemService(_dbContextWrapper.Object, _logger.Object, _mapper.Object, _orderRepository.Object);
		}



		[Fact]
		public async Task AddAsync_Success()
		{
			// arrange
			int id = 1;

			_orderRepository.Setup(x => x.AddAsync(It.IsAny<OrderItemEntity>()))
						 .ReturnsAsync(id);

			// act
			var result = await _orderService.AddAsync(_testOrder);

			// assert
			result.Should().Be(id);
		}
		[Fact]
		public async Task AddAsync_Failed()
		{
			// arrange
			_testOrder = null;
			int? id = null;

			_orderRepository.Setup(x => x.AddAsync(It.IsAny<OrderItemEntity>()))
						 .ReturnsAsync(id);

			// act
			var result = await _orderService.AddAsync(_testOrder);

			// assert
			result.Should().BeNull();
		}

		[Fact]
		public async Task UpdateAsync_Success()
		{
			// arrange
			int id = 1;
			_testOrder.Id = id;

			_orderRepository.Setup(x => x.UpdateAsync(It.Is<int>(x => x == id), It.IsAny<OrderItemEntity>()))
						  .ReturnsAsync((int id, OrderItemEntity entity) => id);

			// act
			var result = await _orderService.UpdateAsync(id, _testOrder);

			// assert
			result.Should().Be(id);
		}

		[Fact]
		public async Task UpdateAsync_Failed()
		{
			// arrange
			int id = 1000;

			_orderRepository.Setup(x => x.UpdateAsync(It.Is<int>(x => x == id), It.IsAny<OrderItemEntity>()))
						  .ReturnsAsync((int id, OrderItemEntity entity) => null);

			// act
			var result = await _orderService.UpdateAsync(id, _testOrder);

			// assert
			result.Should().BeNull();
		}

		[Fact]
		public async Task DeleteAsync_Success()
		{
			// arrange
			int id = 1;
			string status = $"Object with id: {id} was successfully removed";

			_orderRepository.Setup(x => x.DeleteAsync(It.Is<int>(x => x == id)))
						  .ReturnsAsync(status);

			// act
			var result = await _orderService.DeleteAsync(id);

			// assert
			result.Should().Be(status);
		}

		[Fact]
		public async Task DeleteAsync_Failed()
		{
			// arrange
			int id = 100000000;
			string? status = null;

			_orderRepository.Setup(x => x.DeleteAsync(It.Is<int>(x => x == id)))
						 .ReturnsAsync(status);

			// act
			var result = await _orderService.DeleteAsync(id);

			// assert
			result.Should().BeNullOrEmpty();
		}
	}
}
