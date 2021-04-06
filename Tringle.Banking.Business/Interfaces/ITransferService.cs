using Tringle.Banking.Entities.Concrete;

namespace Tringle.Banking.Business.Interfaces
{
    public interface ITransferService : IRedisService<Transfer>
    {
    }
}
