﻿using Jose;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TikkiePaymentRequestAPI.Constants;
using TikkiePaymentRequestAPI.Models;
using TikkiePaymentRequestAPI.Utilities;

namespace TikkiePaymentRequestAPI.RequestsHandlers
{
    internal class AuthenticationRequestsHandler
    {
        private const double DefaultNotBeforeAcceptanceInMinutes = -1;

        private TikkieConfiguration _configuration;

        public AuthenticationRequestsHandler(TikkieConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            AuthorizationToken = new AuthorizationToken();
        }

        public AuthorizationToken AuthorizationToken { get; }

        public async Task AuthenticateIfTokenExpiredAsync()
        {
            if (AuthorizationToken.IsTokenExpired)
            {
                var response = await AuthenticateAsync();
                AuthorizationToken.AccessToken = response.AccessToken;
                AuthorizationToken.TokenExpirationDate = DateTime.Now.AddSeconds(response.ExpiresInSeconds);
                AuthorizationToken.Scope = response.Scope;
                AuthorizationToken.TokenType = response.TokenType;
            }
        }

        private async Task<AuthenticationResponse> AuthenticateAsync()
        {
            var request = new AuthenticationRequest(CreateToken());
            var content = new FormUrlEncodedContent(request.KeyValuePair);
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("API-Key", _configuration.ApiKey);
                var response = await client.PostAsync($"{_configuration.ApiBaseUrl}{Urls.AuthenticationUrlSuffix}", content);

                return await response.GetContentObjectOrExceptionAsync<AuthenticationResponse>();
            }
        }

        private string CreateToken()
        {
            return JWT.Encode(CreatePayload(), _configuration.RSAKey, JwsAlgorithm.RS256); 
        }

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