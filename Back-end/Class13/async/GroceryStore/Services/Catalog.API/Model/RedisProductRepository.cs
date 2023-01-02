using StackExchange.Redis;

namespace Catalog.API.Model
{
    public class RedisProductRepository
    {
        private readonly ConnectionMultiplexer _redis;

        public RedisProductRepository(ConnectionMultiplexer redis)
        {
            _redis = redis;
        }
    }
}
