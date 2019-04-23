using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TikkieAPI.Constants;
using TikkieAPI.Models;

namespace TikkieAPI.RequestsHandlers
{
    internal class PlatformRequestsHandler
    {
        private AuthorizedRequestsHandler _authorizedRequestsHandler;

        public PlatformRequestsHandler(AuthorizedRequestsHandler authorizedRequestsHandler)
        {
            _authorizedRequestsHandler = authorizedRequestsHandler ?? throw new ArgumentNullException(nameof(authorizedRequestsHandler));
        }

        public async Task<PlatformResponse[]> GetPlatformsAsync()
        {
            return await _authorizedRequestsHandler
                .GetOrExceptionAsync<PlatformResponse[]>(Urls.PlatformUrlSuffix);
        }

        public async Task<PlatformResponse> CreatePlatformAsync(PlatformRequest request)
        {
            var jsonRequest = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            return await _authorizedRequestsHandler
                .PostOrExceptionAsync<PlatformResponse>(Urls.PlatformUrlSuffix, content);
        }
    }
}
