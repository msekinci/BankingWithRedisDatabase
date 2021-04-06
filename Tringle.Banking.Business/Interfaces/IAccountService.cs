using System.Threading.Tasks;
using Tringle.Banking.Entities.Concrete;

namespace Tringle.Banking.Business.Interfaces
{
    public interface IAccountService : IRedisService<Account>
    {
        Task<bool> MakeTransfer(Transfer transfer);
    }
}
