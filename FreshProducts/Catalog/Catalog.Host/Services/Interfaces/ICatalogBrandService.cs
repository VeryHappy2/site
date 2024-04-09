﻿namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogBrandService
    {
        Task<int?> Add(string brand);
        Task<int?> Update(int id, string brand);
        Task<string?> Delete(int id);
    }
}
