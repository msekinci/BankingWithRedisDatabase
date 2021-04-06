using System.Text.Json;
using System.Threading.Tasks;
using Tringle.Banking.Business.Interfaces;
using Tringle.Banking.DataAccess.Interfaces;
using Tringle.Banking.Entities.Concrete;

namespace Tringle.Banking.Business.Concrete
{
    public class AccountManager : RedisManager<Account>, IAccountService
    {
        private readonly IRedisDAL<Account> _redisDAL;
        private readonly IAccountDAL _accountDAL;

        public AccountManager(IRedisDAL<Account> redisDAL, IAccountDAL accountDAL) : base(redisDAL)
        {
            _redisDAL = redisDAL;
            _accountDAL = accountDAL;
        }

        public async Task<bool> MakeTransfer(Transfer transfer)
        {
            var sender = JsonSerializer.Deserialize<Account>(await _redisDAL.GetHashOneAsync("Accounts", transfer.SenderAccountNumber));
            var receiver = JsonSerializer.Deserialize<Account>(await _redisDAL.GetHashOneAsync("Accounts", transfer.ReceiverAccountNumber));
            sender.Balance -= transfer.Amount;
            receiver.Balance += transfer.Amount;

            return await _accountDAL.MakeTransfer(transfer.SenderAccountNumber, 
                transfer.ReceiverAccountNumber, 
                JsonSerializer.Serialize(sender), 
                JsonSerializer.Serialize(receiver));
        }
    }
}
