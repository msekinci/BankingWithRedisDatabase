using Newtonsoft.Json.Converters;
using System;
using System.Text.Json.Serialization;

namespace Tringle.Banking.API.Models
{
    public class AccountAddModel
    {
        public int AccountNumber { get; set; }
        public Enums.Enums.CurrencyCode CurrencyCode { get; set; }
        public Decimal Balance { get; set; }
    }
}
