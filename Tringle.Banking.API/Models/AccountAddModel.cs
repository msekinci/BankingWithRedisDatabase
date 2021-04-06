using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Tringle.Banking.API.Models
{
    public class AccountAddModel
    {
        [Required]
        public int AccountNumber { get; set; }
        [Required]
        public Enums.Enums.CurrencyCode CurrencyCode { get; set; }
        [Required]
        public Decimal Balance { get; set; }
    }
}
