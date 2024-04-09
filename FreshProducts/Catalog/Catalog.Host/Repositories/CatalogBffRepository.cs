using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Repositories
{
    public class CatalogBffRepository : ICatalogBffRepository
    {
        private ApplicationDbContext _dbcontext;

        public CatalogBffRepository(IDbContextWrapper<ApplicationDbContext> dbcontext)
        {
            _dbcontext = dbcontext.DbContext;
        }
        public async Task<List<CatalogBrand>> GetBrandsAsync()
        {
            
            return await _dbcontext.CatalogBrands.ToListAsync();
            
        }

        public async Task<CatalogBrand> GetByBrandAsync(string brand)
        {
            var result = await _dbcontext.CatalogBrands
                .FirstOrDefaultAsync(b => b.Brand == brand);
            Console.WriteLine(result);
            if(result != null) 
            { 
                return result;
            }
            return null;
        }

        public async Task<CatalogItem> GetByIdAsync(int id) //Get item
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
