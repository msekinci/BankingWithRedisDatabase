using StackExchange.Redis;
using System.Threading.Tasks;
using Tringle.Banking.Entities.Interfaces;

namespace Tringle.Banking.DataAccess.Interfaces
{
    public interface IRedisDAL<T> where T : class, IObject, new()
    {
        Task<bool> CreateHashSetDataAsync(string tableKey, int key, string dataJson);
        Task<RedisValue> GetHashOneAsync(string tableKey, int key);
        Task<HashEntry[]> GetHashAllAsync(string tableKey);
        Task<bool> IsExistKey(string tableKey, int key);
    }
}
