using Tringle.Banking.Entities.Concrete;

namespace Tringle.Banking.DataAccess.Interfaces
{
    public interface ITransferDAL : IRedisDAL<Transfer>
    {
    }
}
