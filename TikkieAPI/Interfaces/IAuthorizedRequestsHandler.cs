using System.Net.Http;
using System.Threading.Tasks;
using TikkieAPI.Models;

namespace TikkieAPI.Interfaces
{
    internal interface IAuthorizedRequestsHandler
    {
        /// <summary>
        /// Carries information about the current authorization token.
        /// </summary>
        AuthorizationToken AuthorizationTokenInfo { get; }

        /// <summary>
        /// Performs an Http Get passing the url suffix as parameter or throws an exception if something went wrong.
        /// </summary>
        /// <typeparam name="TResponse">Response object type.</typeparam>
        /// <param name="urlSuffix">Url suffix where the Get request is going to be performed</param>
        /// <returns>The response object</returns>
        Task<TResponse> GetOrExceptionAsync<TResponse>(string urlSuffix);

        /// <summary>
        /// Performs an Http Post passing the url suffix as parameter or throws an exception if something went wrong.
        /// </summary>
        /// <typeparam name="TResponse">Response object type.</typeparam>
        /// <param name="urlSuffix">Url suffix where the Post request is going to be performed</param>
        /// <param name="content">The request http content</param>
        /// <returns>The response object</returns>
        Task<TResponse> PostOrExceptionAsync<TResponse>(string urlSuffix, HttpContent content);
    }
}
