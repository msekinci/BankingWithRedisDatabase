using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Tringle.Banking.DataAccess.Concrete.Context
{
    public class RedisContext
    {
        private readonly string _redisHost;
        private readonly string _redisPort;
        private ConnectionMultiplexer _redis;
        public IDatabase db { get; set; }

        public RedisContext(IConfiguration configuration)
        {
            _redisHost = configuration["Redis:Host"];
            _redisPort = configuration["Redis:Port"];
        }

        public void Connect()
        {
            var conflictString = $"{_redisHost}:{_redisPort}";
            _redis = ConnectionMultiplexer.Connect(conflictString);
        }

        public IDatabase GetDB(int dbNumber)
        {
            return _redis.GetDatabase(dbNumber);
        }
    }
}
