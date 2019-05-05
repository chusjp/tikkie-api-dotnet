using System.Collections.Generic;

namespace TikkieAPI.Models
{
    internal class AuthenticationRequest
    {
        private const string ClientAssertionType = "urn:ietf:params:oauth:client-assertion-type:jwt-bearer";
        private const string GrantType = "client_credentials";
        private const string Scope = "tikkie";

        private readonly string _clientAssertionToken;

        /// <summary>
        /// Initializes a new instance of the authentication request object with the client assertion token as parameter.
        /// The rest of the values are default to:
        /// client_assertion_type: urn:ietf:params:oauth:client-assertion-type:jwt-bearer
        /// grant_type: client_credentials
        /// scope: tikkie
        /// </summary>
        /// <param name="clientAssertionToken">The client assertion token as string.</param>
        public AuthenticationRequest(string clientAssertionToken)
        {
            _clientAssertionToken = clientAssertionToken;
        }

        /// <summary>
        /// Returns the authentication request values as a dictionary for easier parsing to form url encoded content.
        /// </summary>
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
