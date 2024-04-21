using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using Order.Host.Data;
using Order.Host.Data.Entities;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services;
using Order.Host.Services.Interfaces;

namespace Order.UnitTests.Services;

public class OrderServiceTest
{
    private readonly IService<OrderEntity> _orderService;

    private readonly Mock<IRepository<OrderEntity>> _orderRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<OrderService>> _logger;

    private List<OrderEntity> _testOrderList = new List<OrderEntity>
    {
        new OrderEntity
        {
            Id = 1,
            AmountProducts = 0,
            TotalPriceItems = 0,
            UserId = "123",
            Items = new List<OrderItemEntity>()
        },
    };

    private OrderEntity _testOrder = new OrderEntity
    {
		Id = 1,
		AmountProducts = 0,
		TotalPriceItems = 0,
		UserId = "123",
		Items = new List<OrderItemEntity>()
	};

    public OrderServiceTest()
    {
        _orderRepository = new Mock<IRepository<OrderEntity>>();
        _mapper = new Mock<IMapper>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<OrderService>>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _orderService = new OrderService(_dbContextWrapper.Object, _logger.Object, _mapper.Object, _orderRepository.Object);
    }

    [Fact]
    public async Task AddAsync_Success()
    {
        // arrange
        int id = 1;

        _orderRepository.Setup(x => x.AddAsync(It.IsAny<OrderEntity>()))
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

        _orderRepository.Setup(x => x.AddAsync(It.IsAny<OrderEntity>()))
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

        _orderRepository.Setup(x => x.UpdateAsync(It.Is<int>(x => x == id), It.IsAny<OrderEntity>()))
                      .ReturnsAsync((int id, OrderEntity entity) => id);

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

        _orderRepository.Setup(x => x.UpdateAsync(It.Is<int>(x => x == id), It.IsAny<OrderEntity>()))
                      .ReturnsAsync((int id, OrderEntity entity) => null);

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