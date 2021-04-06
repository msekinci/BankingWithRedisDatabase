using System;
using System.ComponentModel.DataAnnotations;
using Tringle.Banking.Entities.Interfaces;

namespace Tringle.Banking.Entities.Concrete
{
    public class Account : IObject
    {
        [Required]
        public int AccountNumber { get; set; }
        [Required]
        public string CurrencyCode { get; set; }
        [Required]
        public Decimal Balance { get; set; }
    }
}
