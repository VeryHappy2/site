using Microsoft.EntityFrameworkCore.Storage;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogTypeService
    {
        Task<int?> Add(string type);
        Task<int?> Update(int id, string type);
        Task<string?> Delete(int id);
    }
}
