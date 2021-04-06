using System;
using Tringle.Banking.Entities.Interfaces;

namespace Tringle.Banking.Entities.Concrete
{
    public class Account : IObject
    {
        public int AccountNumber { get; set; }
        public string CurrencyCode { get; set; }
        public Decimal Balance { get; set; }
    }
}
