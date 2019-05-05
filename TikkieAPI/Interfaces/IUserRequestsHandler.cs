using System.Threading.Tasks;
using TikkieAPI.Models;

namespace TikkieAPI.Interfaces
{
    /// <summary>
    /// Handles the User related requests to the Tikkie API authenticating automatically if the current token has expired.
    /// User requests are described in https://developer.abnamro.com/api/tikkie-v1/technical-details
    /// </summary>
    internal interface IUserRequestsHandler
    {
        /// <summary>
        /// Gets all users from an existing platform of a certain API consumer using HTTP GET.
        /// </summary>
        /// <param name="platformToken">The platform token to fetch the users from</param>
        /// <returns>An array of UserResponse object.</returns>
        Task<UserResponse[]> GetUsersAsync(string platformToken);

        /// <summary>
        /// Enrolls a new user into an existing platform payment using HTTP POST.
        /// </summary>
        /// <param name="request">The UserRequest object</param>
        /// <returns>The UserResponse object with information of the newly created platform</returns>
        Task<UserResponse> CreateUserAsync(UserRequest request);
    }
}
