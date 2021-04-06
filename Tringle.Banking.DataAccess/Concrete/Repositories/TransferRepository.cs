using Tringle.Banking.DataAccess.Concrete.Context;
using Tringle.Banking.DataAccess.Interfaces;
using Tringle.Banking.Entities.Concrete;

namespace Tringle.Banking.DataAccess.Concrete.Repositories
{
    public class TransferRepository : RedisRepository<Transfer>, ITransferDAL
    {
        public TransferRepository(RedisContext context) : base(context)
        {
        }
    }
}
