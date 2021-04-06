using StackExchange.Redis;
using System.Threading.Tasks;
using Tringle.Banking.DataAccess.Concrete.Context;
using Tringle.Banking.DataAccess.Interfaces;
using Tringle.Banking.Entities.Interfaces;

namespace Tringle.Banking.DataAccess.Concrete.Repositories
{
    public class RedisRepository<T> : IRedisDAL<T> where T : class, IObject, new()
    {
        private readonly RedisContext _context;
        private readonly IDatabase db;

        public RedisRepository(RedisContext context)
        {
            _context = context;
            db = _context.GetDB(0); ;
        }

        public async Task<bool> CreateHashSetDataAsync(string tableKey, int key, string dataJson)
        {
            var tran = db.CreateTransaction();
            tran.AddCondition(Condition.HashNotExists(tableKey, key));
            Task hashSet = tran.HashSetAsync(tableKey, tableKey + ":" + key, dataJson);
            return await tran.ExecuteAsync();
        }

        public async Task<RedisValue> GetHashOneAsync(string tableKey, int key)
        {
            return await db.HashGetAsync(tableKey, tableKey + ":" + key);
        }

        public async Task<HashEntry[]> GetHashAllAsync(string tableKey)
        {
            return await db.HashGetAllAsync(tableKey);
        }

        public async Task<bool> IsExistKey(string tableKey, int key)
        {
            return await db.HashExistsAsync(tableKey, tableKey + ":" + key);
        }
    }
}
