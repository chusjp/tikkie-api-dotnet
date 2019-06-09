using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Jose;
using TikkieAPI.Exceptions;
using TikkieAPI.Interfaces;
using TikkieAPI.Models;
using TikkieAPI.Utilities;

namespace TikkieAPI.RequestsHandlers
{
    /// <summary>
    /// Handles the authentication requests to the Tikkie API.
    /// </summary>
    internal class AuthenticationRequestsHandler : IAuthenticationRequestsHandler
    {
        private const double DefaultNotBeforeAcceptanceInMinutes = -1;

        private readonly ITikkieConfiguration _configuration;
        private readonly Func<HttpClient> _httpClientFactory;

        /// <summary>
        /// Initializes a new instance of the class with an <see cref="ITikkieConfiguration"/> as parameter.
        /// </summary>
        /// <param name="configuration">The Tikkie API configuration</param>
        /// <exception cref="ArgumentNullException">If the parameter is null</exception>
        public AuthenticationRequestsHandler(ITikkieConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _httpClientFactory = () => new HttpClient();
            AuthorizationTokenInfo = new AuthorizationToken();
        }

        /// <summary>
        /// Internal constructor used for unit tests. 
        /// Initializes a new instance of the class with an <see cref="ITikkieConfiguration"/>, a factory of <see cref="HttpClient"/>
        /// and <see cref="AuthorizationToken"/> as parameters.
        /// </summary>
        /// <param name="configuration">The Tikkie API configuration</param>
        /// <param name="httpClientFactory">Factory to inject the HttpClient</param>
        /// <param name="authorizationToken">Injects an authorization token</param>
        /// <exception cref="ArgumentNullException">If any of the parameters are null</exception>
        internal AuthenticationRequestsHandler(
            ITikkieConfiguration configuration, 
            Func<HttpClient> httpClientFactory, 
            AuthorizationToken authorizationToken)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            AuthorizationTokenInfo = authorizationToken ?? throw new ArgumentNullException(nameof(authorizationToken));
        }

        /// <summary>
        /// Carries information about the current authorization token.
        /// </summary>
        public AuthorizationToken AuthorizationTokenInfo { get; }

        /// <summary>
        /// Authenticates when the property <see cref="AuthorizationToken.IsTokenExpired"/> is true.
        /// After the authentication is done, the property <see cref="AuthorizationTokenInfo"/> is updated as well.
        /// </summary>
        public async Task AuthenticateIfTokenExpiredAsync()
        {
            if (AuthorizationTokenInfo.IsTokenExpired)
            {
                var response = await AuthenticateAsync();
                AuthorizationTokenInfo.AccessToken = response.AccessToken;
                AuthorizationTokenInfo.TokenExpirationDate = DateTime.Now.AddSeconds(response.ExpiresInSeconds);
                AuthorizationTokenInfo.Scope = response.Scope;
                AuthorizationTokenInfo.TokenType = response.TokenType;
            }
        }

        /// <summary>
        /// Requests the authentication to the Tikkie API.
        /// </summary>
        /// <returns>The authentication response</returns>
        /// <exception cref="TikkieErrorResponseException">If the Tikkie API returns an error response.</exception>
        private async Task<AuthenticationResponse> AuthenticateAsync()
        {
            var request = new AuthenticationRequest(CreateToken());
            var content = new FormUrlEncodedContent(request.KeyValuePair);
            using (var httpClient = _httpClientFactory())
            {
                httpClient.DefaultRequestHeaders.Add("API-Key", _configuration.ApiKey);
                var response = await httpClient.PostAsync($"{_configuration.ApiBaseUrl}{UrlProvider.AuthenticationUrlSuffix}", content);

                return await response.GetContentObjectOrExceptionAsync<AuthenticationResponse>();
            }
        }

        /// <summary>
        /// Encodes the JWT token to be sent for authentication.
        /// </summary>
        /// <returns>The string token</returns>
        private string CreateToken()
        {
            return JWT.Encode(CreatePayload(), _configuration.RSAKey, JwsAlgorithm.RS256); 
        }

        /// <summary>
        /// Creates the payload to be encoded as JWT.
        /// </summary>
        /// <returns>A dictionary with all the payload key/values.</returns>
        private Dictionary<string, object> CreatePayload()
        {
            return new Dictionary<string, object>
            {
                { "exp", DateTimeOffset.UtcNow.AddMinutes(_configuration.TokenExpirationInMinutes).ToUnixTimeSeconds() },
                { "nbf", DateTimeOffset.UtcNow.AddMinutes(DefaultNotBeforeAcceptanceInMinutes).ToUnixTimeSeconds() },
                { "iss", _configuration.IssuerName },
                { "sub", _configuration.ApiKey },
                { "aud", _configuration.OAuthTokenUrl }
            };
        }
    }
}
