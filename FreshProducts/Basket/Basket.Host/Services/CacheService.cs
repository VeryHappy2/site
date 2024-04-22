using Infrastructure.Configuration;
using MVC.Host.Services.Interfaces;
using StackExchange.Redis;

namespace MVC.Host.Services
{
    public class CacheService : ICacheService
    {
        private readonly ILogger<CacheService> _logger;
        private readonly IRedisCacheConnectionService _redisCacheConnectionService;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly RedisConfig _config;

        public CacheService(
            ILogger<CacheService> logger,
            IRedisCacheConnectionService redisCacheConnectionService,
            IOptions<RedisConfig> config,
            IJsonSerializer jsonSerializer)
        {
            _logger = logger;
            _redisCacheConnectionService = redisCacheConnectionService;
            _jsonSerializer = jsonSerializer;
            _config = config.Value;
        }

        public Task AddOrUpdateAsync<T>(string key, T value)
        => AddOrUpdateInternalAsync(key, value);

        public async Task<T> GetAsync<T>(string key)
        {
            var redis = GetRedisDatabase();

            var cacheKey = GetItemCacheKey(key);

            var serialized = await redis.StringGetAsync(cacheKey);
            return serialized.HasValue ?
                _jsonSerializer.Deserialize<T>(serialized.ToString())
                : default(T)!;
        }

        public async Task AddOrUpdateInternalAsync<T>(
            string key,
            T value,
            IDatabase redis = null!,
            TimeSpan? expiry = null)
        {
            redis = redis ?? GetRedisDatabase();
            expiry = expiry ?? _config.CacheTimeout;

            var cacheKey = GetItemCacheKey(key);
            var serialized = _jsonSerializer.Serialize(value);

            if (await redis.StringSetAsync(cacheKey, serialized, expiry))
            {
                _logger.LogInformation($"Cached value for key {key} cached");
            }
            else
            {
                _logger.LogInformation($"Cached value for key {key} updated");
            }
        }

        public async Task RemoveFromCacheAsync(string key, IDatabase redis = null!)
		{
			redis = redis ?? GetRedisDatabase();
			var cacheKey = GetItemCacheKey(key);

			if (await redis.KeyDeleteAsync(cacheKey))
			{
				_logger.LogInformation($"Value for key {key} deleted from cache");
			}
			else
			{
				_logger.LogInformation($"No value found for key {key} in cache");
			}
		}

		private IDatabase GetRedisDatabase() => _redisCacheConnectionService.Connection.GetDatabase();
        private string GetItemCacheKey(string userId) =>
            $"{userId}";
	}
}