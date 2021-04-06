using Tringle.Banking.Entities.Interfaces;

namespace Tringle.Banking.Entities.Concrete
{
    public class Transfer : IObject
    {
        public int SenderAccountNumber { get; set; }
        public int ReceiverAccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
