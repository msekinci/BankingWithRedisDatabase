namespace Tringle.Banking.API.Models
{
    public class TransferAddModel
    {
        public int SenderAccountNumber { get; set; }
        public int ReceiverAccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
