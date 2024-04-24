using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Services.Interfaces;
using Catalog.Host.Repositories.Abstractions;

namespace Catalog.Host.Services;

public class CatalogItemService : BaseDataService<ApplicationDbContext>, IService<CatalogItem>
{
    private readonly IRepository<CatalogItem> _repository;
    private readonly ILogger<CatalogItemService> _logger;

    public CatalogItemService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        IRepository<CatalogItem> repository)
        : base(dbContextWrapper, logger)
    {
        _repository = repository;
    }

    public Task<int?> Add(CatalogItem catalogItem)
    {
        return ExecuteSafeAsync(() => _repository.AddAsync(catalogItem));
    }

    public Task<int?> Update(CatalogItem entity)
    {
        return ExecuteSafeAsync(async () =>
        {
            return await _repository.UpdateAsync(entity);
        });
    }

    public Task<string?> Delete(int id)
    {
        return ExecuteSafeAsync(async () =>
        {
            return await _repository.DeleteAsync(id);
        });
    }
}