using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class CatalogBffRepository : ICatalogBffRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly ILogger<CatalogBffRepository> _logger;

        public CatalogBffRepository(
            IDbContextWrapper<ApplicationDbContext> dbcontext,
            ILogger<CatalogBffRepository> logger)
        {
            _dbcontext = dbcontext.DbContext;
            _logger = logger;
        }

        public async Task<List<CatalogBrand>> GetBrandsAsync()
        {
            return await _dbcontext.CatalogBrands.ToListAsync();
        }

        public async Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize, int? brandFilter, int? typeFilter, string search)
        {
            IQueryable<CatalogItem> query = _dbcontext.CatalogItems;

            if (brandFilter.HasValue)
            {
                query = query.Where(w => w.CatalogBrandId == brandFilter.Value);
            }

            if (typeFilter.HasValue)
            {
                query = query.Where(w => w.CatalogTypeId == typeFilter.Value);
            }

            if (!string.IsNullOrEmpty(search))
            {
                search.ToLower();
                query = query.Where(w => w.Name.ToLower().Contains(search));
            }

            var totalItems = await query.LongCountAsync();

            var itemsOnPage = await query.OrderBy(c => c.Name)
               .Include(i => i.CatalogBrand)
               .Include(i => i.CatalogType)
               .Skip(pageSize * pageIndex)
               .Take(pageSize)
               .ToListAsync();

            foreach (var item in itemsOnPage)
            {
                _logger.LogInformation($"Data of item: {item}");
            }

            return new PaginatedItems<CatalogItem>() { TotalCount = totalItems, Data = itemsOnPage };
        }

        public async Task<CatalogBrand> GetByBrandAsync(string brand)
        {
            var result = await _dbcontext.CatalogBrands
                .FirstOrDefaultAsync(b => b.Brand == brand);
            Console.WriteLine(result);
            if (result != null)
            {
                return result;
            }

            return null;
        }

        public async Task<CatalogItem> GetByIdAsync(int id) // Get item
        {
            var result = await _dbcontext.CatalogItems
                .Include(x => x.CatalogBrand)
                .Include(x => x.CatalogType)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result != null)
            {
                return result;
            }

            return null;
        }

        public async Task<CatalogType> GetByTypeAsync(string type)
        {
            var result = await _dbcontext.CatalogTypes
                .FirstOrDefaultAsync(b => b.Type == type);

            if (result != null)
            {
                return result;
            }

            return null;
        }

        public async Task<List<CatalogType>> GetTypesAsync()
        {
            return await _dbcontext.CatalogTypes.ToListAsync();
        }
    }
}
