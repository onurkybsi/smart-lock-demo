using System.Threading.Tasks;

namespace SmartLockDemo.Data.Utilities
{
    internal class RedisClient : IRedisClient
    {
        private StackExchange.Redis.IDatabase _redisDatabase;

        public RedisClient(StackExchange.Redis.IDatabase redisDatabase)
            => _redisDatabase = redisDatabase;

        public string Get(string key)
            => _redisDatabase.StringGet(key);
        public async Task<string> GetAsync(string key)
            => (await _redisDatabase.StringGetAsync(key)).ToString();
        public void Set(string key, string value)
            => _redisDatabase.StringSet(key, value);
        public Task SetAsync(string key, string value)
            => _redisDatabase.StringSetAsync(key, value);
    }
}
