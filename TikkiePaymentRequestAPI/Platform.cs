using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TikkiePaymentRequestAPI.Models;

namespace TikkiePaymentRequestAPI
{
    internal class Platform
    {
        private const string PlatformUrlSuffix = "/tikkie/platforms";

        private AuthorizedRequestsHandler _authorizedRequestsHandler;

        public Platform(AuthorizedRequestsHandler authorizedRequestsHandler)
        {
            _authorizedRequestsHandler = authorizedRequestsHandler ?? throw new ArgumentNullException(nameof(authorizedRequestsHandler));
        }

        public async Task<PlatformResponse[]> GetPlatformsAsync()
        {
            return await _authorizedRequestsHandler
                .GetOrExceptionAsync<PlatformResponse[]>(PlatformUrlSuffix);
        }

        public async Task<PlatformResponse> CreatePlatformAsync(PlatformRequest request)
        {
            var jsonRequest = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            return await _authorizedRequestsHandler
                .PostOrExceptionAsync<PlatformResponse>(PlatformUrlSuffix, content);
        }
    }
}
