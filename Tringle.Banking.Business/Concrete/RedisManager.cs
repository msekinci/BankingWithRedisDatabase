using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Tringle.Banking.Business.Interfaces;
using Tringle.Banking.DataAccess.Interfaces;
using Tringle.Banking.Entities.Interfaces;

namespace Tringle.Banking.Business.Concrete
{
    public class RedisManager<T> : IRedisService<T> where T : class, IObject, new()
    {
        private readonly IRedisDAL<T> _redisDAL;

        public RedisManager(IRedisDAL<T> redisDAL)
        {
            _redisDAL = redisDAL;
        }

        public async Task<bool> CreateHashSetDataAsync(string tableKey, int key, T obj)
        {
            var dataJson = JsonSerializer.Serialize(obj);
            var result = await _redisDAL.CreateHashSetDataAsync(tableKey, key, dataJson);
            return result;
        }

        public async Task<List<T>> GetHashAllAsync(string tableKey)
        {
            List<T> list = new List<T>();
            var dataJson = await _redisDAL.GetHashAllAsync(tableKey);
            dataJson.ToList().ForEach(x =>
            {
                list.Add(JsonSerializer.Deserialize<T>(x.Value));
            });
            return list;
        }

        public async Task<T> GetHashOneAsync(string tableKey, int key)
        {
            var dataJson = await _redisDAL.GetHashOneAsync(tableKey, key);
            return JsonSerializer.Deserialize<T>(dataJson.ToString());
        }

        public Task<bool> IsExistKey(string tableKey, int key)
        {
            return _redisDAL.IsExistKey(tableKey, key);
        }
    }
}
