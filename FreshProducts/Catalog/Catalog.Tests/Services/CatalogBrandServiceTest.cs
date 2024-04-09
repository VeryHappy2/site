using Catalog.Host.Repositories.Abstractions;
using Catalog.Host.Services;

namespace Catalog.UnitTest.Services;

public class CatalogBrandServiceTest
{
    private readonly ICatalogBrandService _catalogService;

    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;
    private readonly Mock<IRepository<CatalogBrand>> _catalogRepository;

    private CatalogBrand _testObject = new CatalogBrand();
    public CatalogBrandServiceTest()
    {
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();
        _catalogRepository = new Mock<IRepository<CatalogBrand>>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogService = new CatalogBrandService(_dbContextWrapper.Object, _logger.Object,  _catalogRepository.Object);
    }


    [Fact]
    public async Task AddAsync_Success()
    {
        // arrange
        string testType = "newBrand";
        int id = 0;

        _catalogRepository.Setup(x => x.AddAsync(It.IsAny<CatalogBrand>()))
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
        string testType = "newBrand";
        int? id = null;

        _catalogRepository.Setup(x => x.AddAsync(It.IsAny<CatalogBrand>()))
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
        _testObject.Brand = "updatedBrand";

        _catalogRepository.Setup(x => x.UpdateAsync(It.Is<int>(x => x == id), It.IsAny<CatalogBrand>()))
                      .ReturnsAsync((int id, CatalogBrand entity) => id);

        // act
        var result = await _catalogService.Update(id, _testObject.Brand);

        // assert
        result.Should().Be(id);
    }

    [Fact]
    public async Task UpdateAsync_Failed()
    {
        // arrange
        int id = 1000;
        _testObject.Brand = "Fail";

        _catalogRepository.Setup(x => x.UpdateAsync(It.Is<int>(x => x == id), It.IsAny<CatalogBrand>()))
                      .ReturnsAsync((int id, CatalogBrand entity) => null);

        // act
        var result = await _catalogService.Update(id, _testObject.Brand);

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