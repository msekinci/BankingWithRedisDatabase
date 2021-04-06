using System.ComponentModel.DataAnnotations;

namespace Tringle.Banking.API.Models
{
    public class TransferAddModel
    {
        [Required]
        public int SenderAccountNumber { get; set; }
        [Required]
        public int ReceiverAccountNumber { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }
}
