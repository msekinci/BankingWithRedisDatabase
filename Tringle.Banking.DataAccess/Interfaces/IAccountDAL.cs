using StackExchange.Redis;
using System.Threading.Tasks;
using Tringle.Banking.Entities.Concrete;

namespace Tringle.Banking.DataAccess.Interfaces
{
    public interface IAccountDAL : IRedisDAL<Account>
    {
        Task<bool> MakeTransfer(int senderKey, int receiverKey, string senderJson, string receiverJson);
    }
}
