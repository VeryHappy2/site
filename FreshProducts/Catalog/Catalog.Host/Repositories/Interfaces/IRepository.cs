namespace Catalog.Host.Repositories.Abstractions
{
    public interface IRepository<T>
    {
        Task<int?> AddAsync(T entity);
        Task<int?> UpdateAsync(int id, T entity);
        Task<string?> DeleteAsync(int id);
    }
}
