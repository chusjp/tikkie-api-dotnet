using System.Threading.Tasks;
using TikkieAPI.Models;

namespace TikkieAPI.Interfaces
{
    /// <summary>
    /// Handles the authentication requests to the Tikkie API.
    /// </summary>
    internal interface IAuthenticationRequestsHandler
    {
        /// <summary>
        /// Carries information about the current authorization token.
        /// </summary>
        AuthorizationToken AuthorizationTokenInfo { get; }

        /// <summary>
        /// Authenticates when the current token has expired.
        /// </summary>
        Task AuthenticateIfTokenExpiredAsync();
    }
}
