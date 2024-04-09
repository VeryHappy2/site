using Catalog.Host.Services;
using Catalog.Host.Repositories.Abstractions;

namespace Catalog.UnitTest.Services;

public class CatalogTypeServiceTest
{
    private readonly ICatalogTypeService _catalogService;

    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;
    private readonly Mock<IRepository<CatalogType>> _catalogRepository;

    private CatalogType _testObject = new CatalogType();
    public CatalogTypeServiceTest()
    {
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();
        _catalogRepository = new Mock<IRepository<CatalogType>>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogService = new CatalogTypeService(_dbContextWrapper.Object, _logger.Object,  _catalogRepository.Object);
    }


    [Fact]
    public async Task AddAsync_Success()
    {
        // arrange
        string testType = "newType";
        int id = 0;

        _catalogRepository.Setup(x => x.AddAsync(It.IsAny<CatalogType>()))
                     .ReturnsAsync(id);

        // act
        var result = await _catalogService.Add(testType);

        // assert
        result.Should().Be(id);
    }
    [Fact]
    public async Task AddAsync_Failed()
    {
        // arrange
        string testType = "newType";
        int? id = null;

        _catalogRepository.Setup(x => x.AddAsync(It.IsAny<CatalogType>()))
                     .ReturnsAsync(id);

        // act
        var result = await _catalogService.Add(testType);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task UpdateAsync_Success()
    {
        // arrange
        int id = 1;
        _testObject.Id = id;
        _testObject.Type = "updatedType";

        _catalogRepository.Setup(x => x.UpdateAsync(It.Is<int>(x => x == id), It.IsAny<CatalogType>()))
                      .ReturnsAsync((int id, CatalogType entity) => id);

        // act
        var result = await _catalogService.Update(id, _testObject.Type);

        // assert
        result.Should().Be(id);
    }

    [Fact]
    public async Task UpdateAsync_Failed()
    {
        // arrange
        int id = 1000;
        _testObject.Type = "Fail";

        _catalogRepository.Setup(x => x.UpdateAsync(It.Is<int>(x => x == id), It.IsAny<CatalogType>()))
                      .ReturnsAsync((int id, CatalogType entity) => null);

        // act
        var result = await _catalogService.Update(id, _testObject.Type);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task DeleteAsync_Success()
    {
        // arrange
        int id = 1;
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
        int id = 100000;
        string status = $"Id: {id} wasn't found";

        _catalogRepository.Setup(x => x.DeleteAsync(It.Is<int>(x => x == id)))
                      .ReturnsAsync(status);

        // act
        var result = await _catalogService.Delete(id);

        // assert
        result.Should().Be(status);
    }
}