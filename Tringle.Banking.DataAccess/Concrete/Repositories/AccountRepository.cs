using StackExchange.Redis;
using System.Threading.Tasks;
using Tringle.Banking.DataAccess.Concrete.Context;
using Tringle.Banking.DataAccess.Interfaces;
using Tringle.Banking.Entities.Concrete;

namespace Tringle.Banking.DataAccess.Concrete.Repositories
{
    public class AccountRepository : RedisRepository<Account>, IAccountDAL
    {
        private readonly RedisContext _context;
        private readonly IDatabase db;
        private const string tableKey = "Accounts";

        public AccountRepository(RedisContext context) : base(context)
        {
            _context = context;
            db = _context.GetDB(0); ;
        }

        public async Task<bool> MakeTransfer(int senderKey, int receiverKey, string senderJson, string receiverJson)
        {
            var tran = db.CreateTransaction();
            Task senderHash = tran.HashSetAsync(tableKey, tableKey + ":" + senderKey, senderJson);
            Task receiverHash = tran.HashSetAsync(tableKey, tableKey + ":" + receiverKey, receiverJson);
            return await tran.ExecuteAsync();
        }
    }
}
