using System.ComponentModel.DataAnnotations;
using Tringle.Banking.Entities.Interfaces;

namespace Tringle.Banking.Entities.Concrete
{
    public class Transfer : IObject
    {
        [Required]
        public int SenderAccountNumber { get; set; }
        [Required]
        public int ReceiverAccountNumber { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }
}
