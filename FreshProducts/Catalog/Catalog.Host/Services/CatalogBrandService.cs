using Catalog.Host.Data.Entities;
using Catalog.Host.Data;
using Catalog.Host.Services.Interfaces;
using Catalog.Host.Repositories.Abstractions;

namespace Catalog.Host.Services
{
    public class CatalogBrandService : BaseDataService<ApplicationDbContext>, IService<CatalogBrand>
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

        public async Task<int?> Add(CatalogBrand brand)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var id = await _repository.AddAsync(
                   new CatalogBrand
                   {
                       Brand = brand.Brand,
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

        public async Task<int?> Update(CatalogBrand brand)
        {
            return await ExecuteSafeAsync(async () =>
            {
                int? result = await _repository.UpdateAsync(brand);
                return result;
            });
        }
    }
}
