using Jose;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TikkiePaymentRequestAPI.Constants;
using TikkiePaymentRequestAPI.Models;

namespace TikkiePaymentRequestAPI
{
    internal class Authentication
    {
        private Configuration _configuration;

        public Authentication(Configuration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<AuthenticationResponse> AuthenticateAsync()
        {
            var request = new AuthenticationRequest(CreateToken());
            var jsonRequest = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/x-www-form-urlencoded");
            content.Headers.Add(AuthenticationConstants.ApiKeyHeaderName, _configuration.ApiKey);

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync("https://api-sandbox.abnamro.com/v1/oauth/token", content);
                return JsonConvert.DeserializeObject<AuthenticationResponse>(await response.Content.ReadAsStringAsync());
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
                { AuthenticationConstants.ExpirationTimeInSecondsClaimName, DateTimeOffset.UtcNow.AddMinutes(_configuration.TokenExpirationInMinutes).ToUnixTimeSeconds() },
                { AuthenticationConstants.NotBeforeInSecondsClaimName, DateTimeOffset.UtcNow.AddMinutes(-AuthenticationConstants.DefaultNotBeforeAcceptanceInMinutes).ToUnixTimeSeconds() },
                { AuthenticationConstants.IssuerNameClaimName, _configuration.IssuerName },
                { AuthenticationConstants.SubjectClaimName, _configuration.ApiKey },
                { AuthenticationConstants.AudienceClaimName, _configuration.OAuthTokenUrl }
            };
        }
    }
}
