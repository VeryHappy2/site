using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services;
using Catalog.Host.Repositories.Abstractions;

namespace Catalog.UnitTest.Services;

public class CatalogItemServiceTest
{
    private readonly IService<CatalogItem> _catalogService;

    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;
    private readonly Mock<IRepository<CatalogItem>> _catalogRepository;

    private readonly CatalogItem _testItemAdd = new CatalogItem()
    {
        Name = "Name",
        Description = "Description",
        Price = 1000,
        AvailableStock = 100,
        CatalogBrandId = 1,
        CatalogTypeId = 1,
        PictureFileName = "1.png"
    };

    private CatalogItem _testItemUpdate = new CatalogItem()
    {
        Name = "3213",
        Description = "dwer",
        Price = 1,
        AvailableStock = 100,
        CatalogBrandId = 1,
        CatalogTypeId = 1,
        PictureFileName = "2131.png"
    };

    public CatalogItemServiceTest()
    {
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();
        _catalogRepository = new Mock<IRepository<CatalogItem>>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogService = new CatalogItemService(_dbContextWrapper.Object, _logger.Object, _catalogRepository.Object);
    }

    [Fact]
    public async Task AddAsync_Success()
    {
        // arrange
        var testResult = 1;

        _catalogRepository.Setup(s => s.AddAsync(
            It.IsAny<CatalogItem>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.Add(_testItemAdd);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task AddAsync_Failed()
    {
        // arrange
        int? testResult = null;

        _catalogRepository.Setup(s => s.AddAsync(
            It.IsAny<CatalogItem>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.Add(_testItemAdd);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task UpdateAsync_Success()
    {
        // arrange
        int id = 1;
        _testItemUpdate.Id = id;

        _catalogRepository.Setup(x => x.UpdateAsync(It.IsAny<CatalogItem>()))
                      .ReturnsAsync((CatalogItem entity) => id);

        // act
        var result = await _catalogService.Update(_testItemUpdate);

        // assert
        result.Should().Be(id);
    }

    [Fact]
    public async Task UpdateAsync_Failed()
    {
        // arrange
        _testItemUpdate = null;

        _catalogRepository.Setup(x => x.UpdateAsync(It.IsAny<CatalogItem>()))
                      .ReturnsAsync((CatalogItem entity) => null);

        // act
        var result = await _catalogService.Update(_testItemUpdate);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task DeleteAsync_Success()
    {
        // arrange
        int id = 1;
        string item = "";
        string status = $"Object with id: {id} was saccussfully removed";

        _catalogRepository.Setup(x => x.DeleteAsync(It.Is<int>(x => x == id)))
                      .ReturnsAsync(status);

        // act
        var result = await _catalogService.Delete(id);

        // assert
        result.Should().Be(status);
    }

    [Fact]
    public async Task DeleteAsync_Failed()
    {
        // arrange
        int id = 10000000;
        string item = "";
        string status = $"Id: {id} wasn't found";

        _catalogRepository.Setup(x => x.DeleteAsync(It.Is<int>(x => x == id)))
                      .ReturnsAsync(status);

        // act
        var result = await _catalogService.Delete(id);

        // assert
        result.Should().Be(status);
    }
}