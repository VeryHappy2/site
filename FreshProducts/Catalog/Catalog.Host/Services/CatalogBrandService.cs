using Catalog.Host.Data.Entities;
using Catalog.Host.Data;
using Catalog.Host.Services.Interfaces;
using Catalog.Host.Repositories.Abstractions;

namespace Catalog.Host.Services
{
    public class CatalogBrandService : BaseDataService<ApplicationDbContext>, ICatalogBrandService
    {
        private IRepository<CatalogBrand> _repository { get; set; }
        public CatalogBrandService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            IRepository<CatalogBrand> repository)
            : base(dbContextWrapper, logger)
        {
            _repository = repository;
        }

        public async Task<int?> Add(string brand)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var id = await _repository.AddAsync(
                   new CatalogBrand
                   {
                       Brand = brand,
                   });
                return id;
            });
        }

        public async Task<string?> Delete(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                string? result = await _repository.DeleteAsync(id);
                return result;
            });
        }

        public async Task<int?> Update(int id, string brand)
        {
            return await ExecuteSafeAsync(async () =>
            {
                int? result = await _repository.UpdateAsync(
                    id,
                    new CatalogBrand
                    {
                        Id = id,
                        Brand = brand
                    });
                return result;
            });
        }
    }
}
