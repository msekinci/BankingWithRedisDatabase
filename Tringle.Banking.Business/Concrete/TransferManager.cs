using Tringle.Banking.Business.Interfaces;
using Tringle.Banking.DataAccess.Interfaces;
using Tringle.Banking.Entities.Concrete;

namespace Tringle.Banking.Business.Concrete
{
    public class TransferManager : RedisManager<Transfer>, ITransferService
    {
        public TransferManager(IRedisDAL<Transfer> redisDAL) : base(redisDAL)
        {
        }
    }
}
