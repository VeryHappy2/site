using StackExchange.Redis;

namespace MVC.Host.Services.Interfaces
{
    public interface IRedisCacheConnectionService
    {
        public IConnectionMultiplexer Connection { get; }
    }
}