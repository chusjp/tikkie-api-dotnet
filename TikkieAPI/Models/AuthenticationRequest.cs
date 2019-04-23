using System.Collections.Generic;

namespace TikkieAPI.Models
{
    internal class AuthenticationRequest
    {
        private const string ClientAssertionType = "urn:ietf:params:oauth:client-assertion-type:jwt-bearer";
        private const string GrantType = "client_credentials";
        private const string Scope = "tikkie";

        private readonly string _clientAssertionToken;

        public AuthenticationRequest(string clientAssertionToken)
        {
            _clientAssertionToken = clientAssertionToken;
        }

        public List<KeyValuePair<string, string>> KeyValuePair
            => new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("client_assertion", _clientAssertionToken),
                new KeyValuePair<string, string>("client_assertion_type", ClientAssertionType),
                new KeyValuePair<string, string>("grant_type", GrantType),
                new KeyValuePair<string, string>("scope", Scope)
            };
    }
}
