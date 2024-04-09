using AutoMapper;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enums;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services;

namespace Catalog.UnitTest.Services;

public class CatalogServiceTest
{
    private readonly ICatalogService _catalogService;

    private readonly Mock<ICatalogItemRepository> _catalogItemRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;
    private readonly Mock<ICatalogBffRepository> _catalogBffRepository;

    public CatalogServiceTest()
    {
        _catalogItemRepository = new Mock<ICatalogItemRepository>();
        _mapper = new Mock<IMapper>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();
        _catalogBffRepository = new Mock<ICatalogBffRepository>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogService = new CatalogService(_dbContextWrapper.Object, _logger.Object, _catalogBffRepository.Object, _catalogItemRepository.Object, _mapper.Object);
    }

    [Fact]
    public async Task GetCatalogItemsAsync_Success()
    {
        // arrange
        var brandFilter = 1;
        var typeFilter = 1;
        var testPageIndex = 0;
        var testPageSize = 4;
        var testTotalCount = 12;
        var groupPages = 1;
        var search = "";
        Dictionary<CatalogTypeFilter, int> filters = new Dictionary<CatalogTypeFilter, int>();

        filters.Add(CatalogTypeFilter.Brand, 1); 
        filters.Add(CatalogTypeFilter.Type, 2);

        var pagingPaginatedItemsSuccess = new PaginatedItems<CatalogItem>()
        {
            Data = new List<CatalogItem>()
            {
                new CatalogItem()
                {
                    Name = "TestName",
                },
            },
            TotalCount = testTotalCount,
        };

        var catalogItemSuccess = new CatalogItem()
        {
            Name = "TestName"
        };

        var catalogItemDtoSuccess = new CatalogItemDto()
        {
            Name = "TestName"
        };

        _catalogItemRepository.Setup(s =>  s.GetByPageAsync(
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize),
            It.Is<int?>(i => i == brandFilter),
            It.IsAny<int?>(),
			It.IsAny<string>())).ReturnsAsync(pagingPaginatedItemsSuccess);

        _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(catalogItemSuccess)))).Returns(catalogItemDtoSuccess);

        // act
        var result = await _catalogService.GetCatalogItemsAsync(testPageSize, testPageIndex, filters, search);

        // assert
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
        result?.Count.Should().Be(testTotalCount);
        result?.PageIndex.Should().Be(testPageIndex);
        result?.PageSize.Should().Be(testPageSize);
    }

    [Fact]
    public async Task GetCatalogItemsAsync_Failed()
    {
        // arrange
        var brandFilter = 133333333;
        var typeFilter = 12222;
        var testPageIndex = 10000000;
        var testPageSize = 400000000;
        var testTotalCount = 1232111421;
        var groupPages = 2123213;
        var search = "";
        Dictionary<CatalogTypeFilter, int> filters = new Dictionary<CatalogTypeFilter, int>();

        filters.Add(CatalogTypeFilter.Brand, 1); 
        filters.Add(CatalogTypeFilter.Type, 2);

        var pagingPaginatedItemsSuccess = new PaginatedItems<CatalogItem>()
        {
            Data = new List<CatalogItem>()
            {
                new CatalogItem()
                {
                    Name = "TestName",
                },
            },
            TotalCount = testTotalCount,
        };

        var catalogItemSuccess = new CatalogItem()
        {
            Name = "TestName"
        };

        var catalogItemDtoSuccess = new CatalogItemDto()
        {
            Name = "TestName"
        };

        _catalogItemRepository.Setup(s => s.GetByPageAsync(
             It.Is<int>(i => i == testPageIndex),
             It.Is<int>(i => i == testPageSize),
             It.Is<int?>(i => i == brandFilter),
             It.IsAny<int?>(),
			 It.IsAny<string>())).ReturnsAsync(pagingPaginatedItemsSuccess);

        _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(catalogItemSuccess)))).Returns(catalogItemDtoSuccess);

        // act
        var result = await _catalogService.GetCatalogItemsAsync(testPageSize, testPageIndex, filters, search);

        // assert
        result.Should()
            .BeNull();
    }

    [Fact]

    public async Task GetByIdAsync_Success()
    {
        // arrange
        int id = 1;
        CatalogItem item = new CatalogItem();
        CatalogItemDto itemDto = new CatalogItemDto();

        _catalogBffRepository.Setup(s => s.GetByIdAsync(
            It.Is<int>(i => i == id))).ReturnsAsync(item);

        _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(item)))).Returns(itemDto);
        // act
        var result = await _catalogService.GetById(id);

        // assert
        result.Should()
            .BeOfType<CatalogItemDto>()
            .And.NotBeNull();
            
    }

    [Fact]
    public async Task GetByIdAsync_Failed()
    {
        // arrange
        int id = 1000;
        CatalogItem item = new CatalogItem();
        CatalogItemDto itemDto = new CatalogItemDto();

        _catalogBffRepository.Setup(s => s.GetByIdAsync(
            It.Is<int>(x => x == id))).ReturnsAsync(item);

        _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(item)))).Returns(itemDto);


        // act
        var result = await _catalogService.GetById(id);

        // assert
        result.Should()
            .Match<CatalogItemDto>(x => x.Id == 0);
    }

    [Fact]

    public async Task GetByBrandAsync_Success()
    {
        // arrange
        string brand = "Azure";
        CatalogBrand item = new CatalogBrand();
        CatalogBrandDto itemDto = new CatalogBrandDto();

        _catalogBffRepository.Setup(s => s.GetByBrandAsync(
            It.Is<string>(i => i == brand))).ReturnsAsync(item);

        _mapper.Setup(s => s.Map<CatalogBrandDto>(
            It.Is<CatalogBrand>(i => i.Equals(item)))).Returns(itemDto);


        // act
        var result = await _catalogService.GetByBrand(brand);

        // assert
        result.Should()
            .NotBeNull()
            .And.BeOfType<CatalogBrandDto>();
    }

    [Fact]
    public async Task GetByBrandAsync_Failed()
    {
        // arrange
        string brand = "unknown";
        CatalogBrand item = new CatalogBrand();
        CatalogBrandDto itemDto = new CatalogBrandDto();

        _catalogBffRepository.Setup(s => s.GetByBrandAsync(
            It.Is<string>(i => i == brand))).ReturnsAsync(item);

        _mapper.Setup(s => s.Map<CatalogBrandDto>(
            It.Is<CatalogBrand>(i => i.Equals(item)))).Returns(itemDto);

        // act
        var result = await _catalogService.GetByBrand(brand);

        // assert
        result.Should().Match<CatalogBrandDto>(x => x.Brand == null); 
    }

    [Fact]
    public async Task GetByTypeAsync_Success()
    {
        // arrange
        string type = "Mug";
        CatalogType item = new CatalogType();
        CatalogTypeDto itemDto = new CatalogTypeDto();

        _catalogBffRepository.Setup(s => s.GetByTypeAsync(
            It.Is<string>(i => i == type))).ReturnsAsync(item);

        _mapper.Setup(s => s.Map<CatalogTypeDto>(
            It.Is<CatalogType>(i => i.Equals(item)))).Returns(itemDto);

        // act
        var result = await _catalogService.GetByType(type);

        // assert
        result.Should()
            .NotBeNull();

    }

    [Fact]
    public async Task GetByTypeAsync_Failed()
    {
        // arrange
        string type = "232432412";
        CatalogType item = new CatalogType();
        CatalogTypeDto itemDto = new CatalogTypeDto();

        _catalogBffRepository.Setup(s => s.GetByTypeAsync(
            It.Is<string>(i => i == type))).ReturnsAsync(item);

        _mapper.Setup(s => s.Map<CatalogTypeDto>(
            It.Is<CatalogType>(i => i.Equals(item)))).Returns(itemDto);
        // act
        var result = await _catalogService.GetByType(type);

        // assert
        result.Should().Match<CatalogTypeDto>(x => x.Type == null);
    }

    [Fact]
    public async Task GetBrandsAsync_Success()
    {
        // arrange
        List<CatalogBrand> item = new List<CatalogBrand>();
		List<CatalogBrandDto> itemDto = new List<CatalogBrandDto>();

		_catalogBffRepository.Setup(s => s.GetBrandsAsync()).ReturnsAsync(item);

		_mapper.Setup(s => s.Map<List<CatalogBrandDto>>(
			It.Is<List<CatalogBrand>>(i => i.Equals(item)))).Returns(itemDto);
		// act
		var result = await _catalogService.GetCatalogBrands();

        // assert
        result.Should()
            .NotBeNull();

    }

    [Fact]
    public async Task GetBrands_Failed()
    {
        // arrange
        List<CatalogBrand> item = new List<CatalogBrand>();
		List<CatalogBrandDto> itemDto = new List<CatalogBrandDto>();
		_catalogBffRepository.Setup(s => s.GetBrandsAsync()).ReturnsAsync(item);

		_mapper.Setup(s => s.Map<List<CatalogBrandDto>>(
			It.Is<List<CatalogBrand>>(i => i.Equals(item)))).Returns(itemDto);
		// act
		var result = await _catalogService.GetCatalogBrands();

        // assert
        result.Should()
            .BeNullOrEmpty();
    }

    [Fact]
    public async Task GetTypes_Success()
    {
        // arrange
        List<CatalogType> item = new List<CatalogType>();
		List<CatalogTypeDto> itemDto = new List<CatalogTypeDto>();

		_catalogBffRepository.Setup(s => s.GetTypesAsync()).ReturnsAsync(item);

		_mapper.Setup(s => s.Map<List<CatalogTypeDto>>(
			It.Is<List<CatalogType>>(i => i.Equals(item)))).Returns(itemDto);
		// act
		var result = await _catalogService.GetCatalogTypes();

        // assert
        result.Should()
            .NotBeNull();

    }

    [Fact]
    public async Task GetTypes_Failed()
    {
        // arrange
        List<CatalogType> item = new List<CatalogType>();
		List<CatalogTypeDto> itemDto = new List<CatalogTypeDto>();
		_catalogBffRepository.Setup(s => s.GetTypesAsync()).ReturnsAsync(item);
		_mapper.Setup(s => s.Map<List<CatalogTypeDto>>(
			It.Is<List<CatalogType>>(i => i.Equals(item)))).Returns(itemDto);
		// act
		var result = await _catalogService.GetCatalogTypes();

        // assert
        result.Should()
            .BeNullOrEmpty();
    }
}