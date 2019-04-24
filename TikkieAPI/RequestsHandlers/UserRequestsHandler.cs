using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TikkieAPI.Models;
using TikkieAPI.Utilities;

namespace TikkieAPI.RequestsHandlers
{
    internal class UserRequestsHandler
    {
        private AuthorizedRequestsHandler _authorizedRequestsHandler;

        public UserRequestsHandler(AuthorizedRequestsHandler authorizedRequestsHandler)
        {
            _authorizedRequestsHandler = authorizedRequestsHandler ?? throw new ArgumentNullException(nameof(authorizedRequestsHandler));
        }

        public async Task<UserResponse[]> GetUsersAsync(string platformToken)
        {
            return await _authorizedRequestsHandler
                .GetOrExceptionAsync<UserResponse[]>(UrlProvider.UserUrlSuffix(platformToken));
        }

        public async Task<UserResponse> CreateUserAsync(UserRequest request)
        {
            var jsonRequest = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            return await _authorizedRequestsHandler
                .PostOrExceptionAsync<UserResponse>(UrlProvider.UserUrlSuffix(request.PlatformToken), content);
        }
    }
}
