using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Jose;
using Newtonsoft.Json;
using TikkiePaymentRequestAPI.Exceptions;
using TikkiePaymentRequestAPI.Models;

namespace TikkiePaymentRequestAPI
{
    public class Authentication
    {
        private const double DefaultNotBeforeAcceptanceInMinutes = 1;

        private Configuration _configuration;

        public Authentication(Configuration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<AuthenticationResponse> AuthenticateAsync()
        {
            var request = new AuthenticationRequest(CreateToken());
            var content = new FormUrlEncodedContent(request.KeyValuePair);
            content.Headers.Add("API-Key", _configuration.ApiKey);

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync($"{_configuration.ApiBaseUrl}/oauth/token", content);
                var authenticationContentString = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    var errorResponses = JsonConvert.DeserializeObject<ErrorResponses>(authenticationContentString);
                    throw new TikkieErrorResponseException($"Server status code: {response.StatusCode}", errorResponses.ErrorResponseArray);
                }
                return JsonConvert.DeserializeObject<AuthenticationResponse>(authenticationContentString);
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
                { "nbf", DateTimeOffset.UtcNow.AddMinutes(-DefaultNotBeforeAcceptanceInMinutes).ToUnixTimeSeconds() },
                { "iss", _configuration.IssuerName },
                { "sub", _configuration.ApiKey },
                { "aud", _configuration.OAuthTokenUrl }
            };
        }
    }
}
