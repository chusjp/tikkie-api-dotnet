using Newtonsoft.Json;

namespace TikkiePaymentRequestAPI.Models
{
    internal class AuthenticationResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; internal set; }

        [JsonProperty("expires_in")]
        public int ExpiresInSeconds { get; internal set; }

        [JsonProperty("scope")]
        public string Scope { get; internal set; }

        [JsonProperty("token_type")]
        public string TokenType { get; internal set; }
    }
}
