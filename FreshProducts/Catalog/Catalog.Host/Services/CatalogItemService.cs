using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Catalog.Host.Repositories.Abstractions;

namespace Catalog.Host.Services;

public class CatalogItemService : BaseDataService<ApplicationDbContext>, ICatalogItemService
{
    private readonly ICatalogItemRepository _catalogItemRepository;
    private readonly IRepository<CatalogItem> _repository;
    readonly ILogger<CatalogItemService> _logger;

    public CatalogItemService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogItemRepository catalogItemRepository,
        IRepository<CatalogItem> repository
        )
        : base(dbContextWrapper, logger)
    {
        _repository = repository;
        _catalogItemRepository = catalogItemRepository;
    }

    public Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        return ExecuteSafeAsync(() => _catalogItemRepository.Add(name, description, price, availableStock, catalogBrandId, catalogTypeId, pictureFileName));
    }

    public Task<int?> Update(int id, CatalogItem entity)
    {
        return ExecuteSafeAsync( async () => {
            int? result = await _repository.UpdateAsync(id, new CatalogItem
            {
                Id = id,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                AvailableStock = entity.AvailableStock,
                CatalogBrandId = entity.CatalogBrandId,
                PictureFileName = entity.PictureFileName,
                CatalogTypeId = entity.CatalogTypeId,
            });
            
            return result;
        });
    }

    public Task<string?> Delete(int id)
    {
        return ExecuteSafeAsync( async () => { 
            string result = await _repository.DeleteAsync(id); 
            return result;
        });
    }
}