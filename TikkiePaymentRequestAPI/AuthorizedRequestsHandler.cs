using System;
using System.Net.Http;
using System.Threading.Tasks;
using TikkiePaymentRequestAPI.Models;
using TikkiePaymentRequestAPI.Utilities;

namespace TikkiePaymentRequestAPI
{
    internal class AuthorizedRequestsHandler
    {
        private Configuration _configuration;
        private Authentication _authentication;

        public AuthorizedRequestsHandler(Configuration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _authentication = new Authentication(_configuration);
            AuthorizationToken = _authentication.AuthorizationToken;
        }

        public AuthorizationToken AuthorizationToken { get; }

        public async Task<TResponse> GetOrExceptionAsync<TResponse>(string urlSuffix)
        {
            await _authentication.AuthenticateIfExpired();
            using (var client = new HttpClient())
            {
                AddDefaultRequestHeaders(client);
                var response = await client.GetAsync($"{_configuration.ApiBaseUrl}{urlSuffix}");
                return await response.GetContentObjectOrExceptionAsync<TResponse>();
            }
        }

        public async Task<TResponse> PostOrExceptionAsync<TResponse>(string urlSuffix, HttpContent content)
        {
            await _authentication.AuthenticateIfExpired();
            using (var client = new HttpClient())
            {
                AddDefaultRequestHeaders(client);
                var response = await client.PostAsync($"{_configuration.ApiBaseUrl}{urlSuffix}", content);

                return await response.GetContentObjectOrExceptionAsync<TResponse>();
            }
        }

        private void AddDefaultRequestHeaders(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Add("Authorization", Authorization);
            httpClient.DefaultRequestHeaders.Add("API-Key", _configuration.ApiKey);
        }

        private string Authorization => $"{_authentication.AuthorizationToken.TokenType} {_authentication.AuthorizationToken.AccessToken}";
    }
}
