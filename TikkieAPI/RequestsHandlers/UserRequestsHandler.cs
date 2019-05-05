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
    /// Handles the User related requests to the Tikkie API authenticating automatically if the current token has expired.
    /// User requests are described in https://developer.abnamro.com/api/tikkie-v1/technical-details
    /// </summary>
    internal class UserRequestsHandler : IUserRequestsHandler
    {
        private readonly IAuthorizedRequestsHandler _authorizedRequestsHandler;

        /// <summary>
        /// Initializes a new instance of the class with an <see cref="IAuthorizedRequestsHandler"/> as parameter.
        /// </summary>
        /// <param name="authorizedRequestsHandler">The authorized request handler</param>
        /// <exception cref="ArgumentNullException">If the parameter is null</exception>
        public UserRequestsHandler(IAuthorizedRequestsHandler authorizedRequestsHandler)
        {
            _authorizedRequestsHandler = authorizedRequestsHandler ?? throw new ArgumentNullException(nameof(authorizedRequestsHandler));
        }

        /// <summary>
        /// Gets all users from an existing platform of a certain API consumer using HTTP GET.
        /// </summary>
        /// <param name="platformToken">The platform token to fetch the users from</param>
        /// <returns>An array of UserResponse object.</returns>
        /// <exception cref="ArgumentException">If the argument is null or empty.</exception>
        /// <exception cref="TikkieErrorResponseException">If the Tikkie API returns an error response.</exception>
        public async Task<UserResponse[]> GetUsersAsync(string platformToken)
        {
            if (string.IsNullOrEmpty(platformToken))
            {
                throw new ArgumentException(nameof(platformToken));
            }

            return await _authorizedRequestsHandler
                .GetOrExceptionAsync<UserResponse[]>(UrlProvider.UserUrlSuffix(platformToken));
        }

        /// <summary>
        /// Enrolls a new user into an existing platform payment using HTTP POST.
        /// </summary>
        /// <param name="request">The UserRequest object</param>
        /// <returns>The UserResponse object with information of the newly created platform</returns>
        /// <exception cref="ArgumentNullException">If the UserRequest object is null</exception>
        /// <exception cref="TikkieErrorResponseException">If the Tikkie API returns an error response.</exception>
        public async Task<UserResponse> CreateUserAsync(UserRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var jsonRequest = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            return await _authorizedRequestsHandler
                .PostOrExceptionAsync<UserResponse>(UrlProvider.UserUrlSuffix(request.PlatformToken), content);
        }
    }
}
