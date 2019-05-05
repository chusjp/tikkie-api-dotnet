using Newtonsoft.Json;

namespace TikkieAPI.Models
{
    internal class AuthenticationResponse
    {
        /// <summary>
        /// The access token from the authentication response.
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; internal set; }

        /// <summary>
        /// The token expiration time in seconds.
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpiresInSeconds { get; internal set; }

        /// <summary>
        /// The scope taken from the authentication response.
        /// </summary>
        [JsonProperty("scope")]
        public string Scope { get; internal set; }

        /// <summary>
        /// The token type taken from the authentication response.
        /// </summary>
        [JsonProperty("token_type")]
        public string TokenType { get; internal set; }
    }
}
