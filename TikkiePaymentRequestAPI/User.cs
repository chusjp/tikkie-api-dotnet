using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TikkiePaymentRequestAPI.Models;

namespace TikkiePaymentRequestAPI
{
    internal class User
    {
        private AuthorizedRequestsHandler _authorizedRequestsHandler;

        public User(AuthorizedRequestsHandler authorizedRequestsHandler)
        {
            _authorizedRequestsHandler = authorizedRequestsHandler ?? throw new ArgumentNullException(nameof(authorizedRequestsHandler));
        }

        public async Task<UserResponse[]> GetUsersAsync(string platformToken)
        {
            return await _authorizedRequestsHandler
                .GetOrExceptionAsync<UserResponse[]>(UserUrlSuffix(platformToken));
        }

        public async Task<UserResponse> CreateUserAsync(string platformToken, UserRequest request)
        {
            var jsonRequest = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            return await _authorizedRequestsHandler
                .PostOrExceptionAsync<UserResponse>(UserUrlSuffix(platformToken), content);
        }

        private string UserUrlSuffix(string platformToken) => $"/tikkie/platforms/{platformToken}/users";
    }
}
