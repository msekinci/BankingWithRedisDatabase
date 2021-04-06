using System.Runtime.Serialization;

namespace Tringle.Banking.API.Enums
{
    public static class Enums
    {
        public enum CurrencyCode
        {
            [EnumMember(Value = "TRY")]
            TRY = 0,
            [EnumMember(Value = "USD")]
            USD = 1,
            [EnumMember(Value = "EUR")]
            EUR = 2
        }

        public enum StatusCode
        {
            CONFLICT = 409,
            CREATED = 201,
            INVALID_CARD_NUMBER = 10215,
            ISSUER_OR_SWITCH_INOPERATIVE = 10228,
            INVALID_TRANSACTION = 10012,
            INVALID_AMOUNT = 10232, //geçersiz tutar
            NOT_SUFFICIENT_FUNDS = 10051, //yetersiz bakiye
        }
    }
}
