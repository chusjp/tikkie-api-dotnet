using System.Threading.Tasks;
using TikkieAPI.Models;

namespace TikkieAPI.Interfaces
{
    /// <summary>
    /// Client gateway accessing to all the actions provided by the Tikkie API.
    /// It follows the techical details specified in the official ABN-AMRO documentation.
    /// https://developer.abnamro.com/api/tikkie-v1/technical-details
    /// </summary>
    public interface ITikkieClient
    {
        /// <summary>
        /// Holds the Tikkie API configuration.
        /// </summary>
        ITikkieConfiguration Configuration { get; }

        /// <summary>
        /// Carries information about the current authorization token.
        /// </summary>
        AuthorizationToken AuthorizationTokenInfo { get; }

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

        /// <summary>
        /// Creates a new Payment Request for an existing user using HTTP POST. 
        /// </summary>
        /// <param name="request">The PaymentRequest object</param>
        /// <returns>The PaymentResponse object with information of the created Payment Request</returns>
        Task<PaymentResponse> CreatePaymentRequestAsync(PaymentRequest request);

        /// <summary>
        /// Gets all the Payment Requests from an existing user using HTTP GET.
        /// </summary>
        /// <param name="request">The UserPaymentRequest object</param>
        /// <returns>The UserPaymentResponse object</returns>
        Task<UserPaymentResponse> GetUserPaymentRequestsAsync(UserPaymentRequest request);

        /// <summary>
        /// Gets a Single Payment Request using HTTP GET.
        /// </summary>
        /// <param name="request">The SinglePaymentRequest object</param>
        /// <returns>The SinglePaymentRequestResponse object</returns>
        Task<SinglePaymentRequestResponse> GetPaymentRequestAsync(SinglePaymentRequest request);
    }
}
