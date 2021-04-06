using System.Collections.Generic;
using System.Threading.Tasks;
using Tringle.Banking.Entities.Interfaces;

namespace Tringle.Banking.Business.Interfaces
{
    public interface IRedisService<T> where T : class, IObject, new()
    {
        Task<bool> CreateHashSetDataAsync(string tableKey, int key, T obj);
        Task<T> GetHashOneAsync(string tableKey, int key);
        Task<List<T>> GetHashAllAsync(string tableKey);
        Task<bool> IsExistKey(string tableKey, int key);
    }
}
