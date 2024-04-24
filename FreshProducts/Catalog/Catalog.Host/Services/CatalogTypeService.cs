using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Services.Interfaces;
using Catalog.Host.Repositories.Abstractions;

namespace Catalog.Host.Services
{
    public class CatalogTypeService : BaseDataService<ApplicationDbContext>, IService<CatalogType>
    {
        private IRepository<CatalogType> _repository { get; set; }
        public CatalogTypeService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            IRepository<CatalogType> repository)
            : base(dbContextWrapper, logger)
        {
            _repository = repository;
        }

        public async Task<int?> Add(CatalogType type)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var id = await _repository.AddAsync(
                   new CatalogType
                   {
                       Type = type.Type,
                   });
                return id;
            });
        }

        public async Task<string?> Delete(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                string result = await _repository.DeleteAsync(id);
                return result;
            });
        }

        public async Task<int?> Update(CatalogType type)
        {
            return await ExecuteSafeAsync(async () =>
            {
                return await _repository.UpdateAsync(type);
            });
        }
    }
}
