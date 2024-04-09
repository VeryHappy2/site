using StackExchange.Redis;

namespace MVC.Host.Services.Interfaces
{
    public interface ICacheService
    {
        Task AddOrUpdateAsync<T>(string key, T value);
        Task RemoveFromCacheAsync(string key, IDatabase redis = null!);
		Task<T> GetAsync<T>(string key);
    }
}