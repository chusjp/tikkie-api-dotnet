using System;
using System.Net.Http;
using System.Threading.Tasks;
using TikkieAPI.Exceptions;
using TikkieAPI.Interfaces;
using TikkieAPI.Models;
using TikkieAPI.Utilities;

namespace TikkieAPI.RequestsHandlers
{
    /// <summary>
    /// Handles the requests to the Tikkie API authenticating automatically if the current token has expired.
    /// It is the class that should use all the requests handlers that require authentication.
    /// </summary>
    internal class AuthorizedRequestsHandler : IAuthorizedRequestsHandler
    {
        private readonly ITikkieConfiguration _configuration;
        private readonly IAuthenticationRequestsHandler _authenticationRequestsHandler;
        private readonly Func<HttpClient> _httpClientFactory;

        /// <summary>
        /// Initializes a new instance of the class with an <see cref="ITikkieConfiguration"/> as parameter.
        /// </summary>
        /// <param name="configuration">The Tikkie API configuration</param>
        /// <exception cref="ArgumentNullException">If the parameter is null</exception>
        public AuthorizedRequestsHandler(ITikkieConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _authenticationRequestsHandler = new AuthenticationRequestsHandler(_configuration);
            AuthorizationTokenInfo = _authenticationRequestsHandler.AuthorizationTokenInfo;
            _httpClientFactory = () => new HttpClient();
        }

        /// <summary>
        /// Internal constructor used for unit tests. 
        /// Initializes a new instance of the class with an <see cref="ITikkieConfiguration"/>, <see cref="IAuthenticationRequestsHandler"/> 
        /// and a factory of <see cref="HttpClient"/> as parameters.
        /// </summary>
        /// <param name="configuration">The Tikkie API configuration</param>
        /// <param name="authenticationRequestsHandler">The authentication requests handler</param>
        /// <param name="httpClientFactory">Factory to inject the HttpClient</param>
        /// <exception cref="ArgumentNullException">If any of the parameters are null</exception>
        internal AuthorizedRequestsHandler(
            ITikkieConfiguration configuration, 
            IAuthenticationRequestsHandler authenticationRequestsHandler, 
            Func<HttpClient> httpClientFactory)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _authenticationRequestsHandler = authenticationRequestsHandler ?? throw new ArgumentNullException(nameof(authenticationRequestsHandler));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        /// <summary>
        /// Carries information about the current authorization token.
        /// </summary>
        public AuthorizationToken AuthorizationTokenInfo { get; }

        /// <summary>
        /// Performs an Http Get using as a base of the url the value of <see cref="ITikkieConfiguration.ApiBaseUrl"/>
        /// and the parameter as a suffix.
        /// </summary>
        /// <typeparam name="TResponse">Response object type deserializable as Json</typeparam>
        /// <param name="urlSuffix">Url suffix where the Get request is going to be performed</param>
        /// <returns>The response object</returns>
        /// <exception cref="TikkieErrorResponseException">If the Tikkie API returns an error response.</exception>
        public async Task<TResponse> GetOrExceptionAsync<TResponse>(string urlSuffix)
        {
            return await PerformAuthenticatedRequestOrExceptionAsync<TResponse>
                (httpClient => httpClient.GetAsync($"{_configuration.ApiBaseUrl}{urlSuffix}"));
        }

        /// <summary>
        /// Performs an Http Post using as a base of the url the value of <see cref="ITikkieConfiguration.ApiBaseUrl"/>
        /// and the parameter as a suffix.
        /// </summary>
        /// <typeparam name="TResponse">Response object type deserializable as Json</typeparam>
        /// <param name="urlSuffix">Url suffix where the Post request is going to be performed</param>
        /// <param name="content">The request http content</param>
        /// <returns>The response object</returns>
        /// <exception cref="TikkieErrorResponseException">If the Tikkie API returns an error response.</exception>
        public async Task<TResponse> PostOrExceptionAsync<TResponse>(string urlSuffix, HttpContent content)
        {
            return await PerformAuthenticatedRequestOrExceptionAsync<TResponse>
                (httpClient => httpClient.PostAsync($"{_configuration.ApiBaseUrl}{urlSuffix}", content));
        }

        /// <summary>
        /// Authenticated HTTP action performer to be reused with any HTTP request type.
        /// </summary>
        /// <typeparam name="TResponse">Response object type deserializable as Json</typeparam>
        /// <param name="responseFunction">Function that performs the HTTP request returning an HttpResponseMessage</param>
        /// <returns>The response object</returns>
        /// <exception cref="TikkieErrorResponseException">If the Tikkie API returns an error response.</exception>
        private async Task<TResponse> PerformAuthenticatedRequestOrExceptionAsync<TResponse>(Func<HttpClient, Task<HttpResponseMessage>> responseFunction)
        {
            await _authenticationRequestsHandler.AuthenticateIfTokenExpiredAsync();
            using (var httpClient = _httpClientFactory())
            {
                AddDefaultRequestHeaders(httpClient);
                var response = await responseFunction(httpClient);
                return await response.GetContentObjectOrExceptionAsync<TResponse>();
            }
        }

        /// <summary>
        /// Adds the default header to the http client object.
        /// </summary>
        /// <param name="httpClient">The http client object</param>
        private void AddDefaultRequestHeaders(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Add("Authorization", AuthorizationValue);
            httpClient.DefaultRequestHeaders.Add("API-Key", _configuration.ApiKey);
        }

        /// <summary>
        /// Gets the authorization parameter value composed by the token type value followed by the access token.
        /// E.g. Bearer duGFHdkmak12mksm
        /// </summary>
        private string AuthorizationValue => $"{_authenticationRequestsHandler.AuthorizationTokenInfo.TokenType} {_authenticationRequestsHandler.AuthorizationTokenInfo.AccessToken}";
    }
}
