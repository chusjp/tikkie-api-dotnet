using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TikkieAPI.Exceptions;
using TikkieAPI.Interfaces;
using TikkieAPI.Models;
using TikkieAPI.Utilities;

namespace TikkieAPI.RequestsHandlers
{
    /// <summary>
    /// Handles the Platform related requests to the Tikkie API authenticating automatically if the current token has expired.
    /// Platform requests are described in https://developer.abnamro.com/api/tikkie-v1/technical-details
    /// </summary>
    internal class PlatformRequestsHandler : IPlatformRequestsHandler
    {
        private readonly IAuthorizedRequestsHandler _authorizedRequestsHandler;

        /// <summary>
        /// Initializes a new instance of the class with an <see cref="IAuthorizedRequestsHandler"/> as parameter.
        /// </summary>
        /// <param name="authorizedRequestsHandler">The authorized request handler</param>
        /// <exception cref="ArgumentNullException">If the parameter is null</exception>
        public PlatformRequestsHandler(IAuthorizedRequestsHandler authorizedRequestsHandler)
        {
            _authorizedRequestsHandler = authorizedRequestsHandler ?? throw new ArgumentNullException(nameof(authorizedRequestsHandler));
        }

        /// <summary>
        /// Gets all the existing Platforms created for a certain API consumer using HTTP GET.
        /// </summary>
        /// <returns>An array of PlatformResponse object.</returns>
        /// <exception cref="TikkieErrorResponseException">If the Tikkie API returns an error response.</exception>
        public async Task<PlatformResponse[]> GetPlatformsAsync()
        {
            return await _authorizedRequestsHandler
                .GetOrExceptionAsync<PlatformResponse[]>(UrlProvider.PlatformUrlSuffix);
        }

        /// <summary>
        /// Enrolls a new platform using HTTP POST.
        /// </summary>
        /// <param name="request">The PlatformRequest object</param>
        /// <returns>The PlatformResponse object with information of the newly created platform</returns>
        /// <exception cref="ArgumentNullException">If the PlatformRequest object is null</exception>
        /// <exception cref="TikkieErrorResponseException">If the Tikkie API returns an error response.</exception>
        public async Task<PlatformResponse> CreatePlatformAsync(PlatformRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var jsonRequest = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            return await _authorizedRequestsHandler
                .PostOrExceptionAsync<PlatformResponse>(UrlProvider.PlatformUrlSuffix, content);
        }
    }
}
