using System;
using System.Net.Http;
using System.Threading.Tasks;
using TikkieAPI.Models;
using TikkieAPI.Utilities;

namespace TikkieAPI.RequestsHandlers
{
    internal class AuthorizedRequestsHandler
    {
        private TikkieConfiguration _configuration;
        private AuthenticationRequestsHandler _authenticationRequestsHandler;

        public AuthorizedRequestsHandler(TikkieConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _authenticationRequestsHandler = new AuthenticationRequestsHandler(_configuration);
            AuthorizationToken = _authenticationRequestsHandler.AuthorizationToken;
        }

        public AuthorizationToken AuthorizationToken { get; }

        public async Task<TResponse> GetOrExceptionAsync<TResponse>(string urlSuffix)
        {
            await _authenticationRequestsHandler.AuthenticateIfTokenExpiredAsync();
            using (var client = new HttpClient())
            {
                AddDefaultRequestHeaders(client);
                var response = await client.GetAsync($"{_configuration.ApiBaseUrl}{urlSuffix}");
                return await response.GetContentObjectOrExceptionAsync<TResponse>();
            }
        }

        public async Task<TResponse> PostOrExceptionAsync<TResponse>(string urlSuffix, HttpContent content)
        {
            await _authenticationRequestsHandler.AuthenticateIfTokenExpiredAsync();
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

        private string Authorization => $"{_authenticationRequestsHandler.AuthorizationToken.TokenType} {_authenticationRequestsHandler.AuthorizationToken.AccessToken}";
    }
}
