﻿using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogBffRepository
    {
        Task<CatalogItem> GetByIdAsync(int id);
        Task<CatalogBrand> GetByBrandAsync(string brand);
        Task<CatalogType> GetByTypeAsync(string type);
        Task<List<CatalogBrand>> GetBrandsAsync();
        Task<List<CatalogType>> GetTypesAsync();
    }
}
