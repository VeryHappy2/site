using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Services.Interfaces;
using Catalog.Host.Repositories.Abstractions;

namespace Catalog.Host.Services
{
    public class CatalogTypeService : BaseDataService<ApplicationDbContext>, ICatalogTypeService
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

        public async Task<int?> Add(string type)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var id = await _repository.AddAsync(
                   new CatalogType
                   {
                       Type = type,
                   }
                );
                return id;
            });
        }

        

        public async Task<string?> Delete(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                string result = await _repository.DeleteAsync( id );
                return result;
            });
        }

        public async Task<int?> Update(int id, string type)
        {
            return await ExecuteSafeAsync(async () =>
            {
                int? result = await _repository.UpdateAsync(id, 
                    new CatalogType { 
                        Id = id,
                        Type = type 
                        }
                    );
                return result;
            });
        }

        
    }
}
