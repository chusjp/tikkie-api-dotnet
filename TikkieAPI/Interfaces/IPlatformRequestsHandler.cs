using System.Threading.Tasks;
using TikkieAPI.Models;

namespace TikkieAPI.Interfaces
{
    /// <summary>
    /// Handles the Platform related requests to the Tikkie API authenticating automatically if the current token has expired.
    /// Platform requests are described in https://developer.abnamro.com/api/tikkie-v1/technical-details
    /// </summary>
    internal interface IPlatformRequestsHandler
    {
        /// <summary>
        /// Gets all the existing Platforms created for a certain API consumer using HTTP GET.
        /// </summary>
        /// <returns>An array of PlatformResponse object.</returns>
        Task<PlatformResponse[]> GetPlatformsAsync();

        /// <summary>
        /// Enrolls a new platform using HTTP POST.
        /// </summary>
        /// <param name="request">The PlatformRequest object</param>
        /// <returns>The PlatformResponse object with information of the newly created platform</returns>
        Task<PlatformResponse> CreatePlatformAsync(PlatformRequest request);
    }
}
