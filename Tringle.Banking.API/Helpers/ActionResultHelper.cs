using Newtonsoft.Json.Linq;
using System.Text.Json;
using static Tringle.Banking.API.Enums.Enums;

namespace Tringle.Banking.API.Helpers
{
    public static class ActionResultHelper
    {
        public static JObject CreateActionResultJson(StatusCode statusCode, string message)
        {
            return new JObject 
            {
                new JProperty("errorCode", (int)statusCode),
                new JProperty("errorMessage", message) 
            };
        }
    }
}
