using Newtonsoft.Json;
using TikkiePaymentRequestAPI.Constants;

namespace TikkiePaymentRequestAPI.Models
{
    public class AuthenticationRequest
    {
        public AuthenticationRequest(string clientAssertionToken)
        {
            ClientAssertion = clientAssertionToken;
        }

        [JsonProperty("client_assertion")]
        public string ClientAssertion { get; set; }

        [JsonProperty("client_assertion_type")]
        public string ClientAssertionType { get; set; } = AuthenticationConstants.ClientAssertionType;

        [JsonProperty("grant_type")]
        public string GrantType { get; set; } = AuthenticationConstants.GrantType;

        [JsonProperty("scope")]
        public string Scope { get; set; } = AuthenticationConstants.DefaultScope;
    }
}
