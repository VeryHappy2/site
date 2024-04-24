namespace Catalog.Host.Services.Interfaces
{
    public interface IService<T>
    {
        Task<int?> Add(T entity);
        Task<int?> Update(T entity);
        Task<string?> Delete(int id);
    }
}
