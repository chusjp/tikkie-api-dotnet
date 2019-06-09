using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TikkieAPI.Exceptions;
using TikkieAPI.Interfaces;
using TikkieAPI.Models;
using TikkieAPI.RequestsHandlers;

[assembly: InternalsVisibleTo("TikkieAPI.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace TikkieAPI
{
    /// <summary>
    /// Client gateway accessing to all the actions provided by the Tikkie API.
    /// It follows the techical details specified in the official ABN-AMRO documentation.
    /// https://developer.abnamro.com/api/tikkie-v1/technical-details
    /// </summary>
    public class TikkieClient : ITikkieClient
    {
        private readonly IPlatformRequestsHandler _platformRequestsHandler;
        private readonly IUserRequestsHandler _userRequestsHandler;
        private readonly IPaymentRequestsHandler _paymentRequestsHandler;

        /// <summary>
        /// Initializes a new instance of the Tikkie API Client.
        /// </summary>
        /// <param name="apiKey">The API key that allows the usage of the Tikkie API.</param>
        /// <param name="privateKeyPath">The directory path where the RSA private key PEM file can be found.</param>
        /// <param name="useTestEnvironment">True if the connexion is made to the development/sandbox Tikkie API environment, false if it uses the production environment. False by default.</param>
        /// <exception cref="ArgumentException">If the API Key is null or empty.</exception>
        /// <exception cref="FileNotFoundException">If the private key pem file doesn't exist in the specified location.</exception>
        /// <exception cref="InvalidDataException">If the RSA private key PEM file cannot be read.</exception>
        public TikkieClient(
            string apiKey, 
            string privateKeyPath, 
            bool useTestEnvironment = false)
        {
            Configuration = new TikkieConfiguration(apiKey, privateKeyPath, useTestEnvironment);
            var authorizedRequestsHandler = new AuthorizedRequestsHandler(Configuration);
            AuthorizationTokenInfo = authorizedRequestsHandler.AuthorizationTokenInfo;
            _platformRequestsHandler = new PlatformRequestsHandler(authorizedRequestsHandler);
            _userRequestsHandler = new UserRequestsHandler(authorizedRequestsHandler);
            _paymentRequestsHandler = new PaymentRequestsHandler(authorizedRequestsHandler);
        }

        /// <summary>
        /// Internal constructor used for unit tests that initializes a new instance of the Tikkie API Client injecting the needed dependencies.
        /// Note that with this constructor the values of <see cref="Configuration"/> and <see cref="AuthorizationTokenInfo"/> will remain null.
        /// </summary>
        /// <param name="platformRequestsHandler">Used for injecting the PlatformRequestHandler</param>
        /// <param name="userRequestsHandler">Used for injecting the UserRequestHandler</param>
        /// <param name="paymentRequestsHandler">Used for injecting the PaymentRequestHandler</param>
        internal TikkieClient(
            IPlatformRequestsHandler platformRequestsHandler, 
            IUserRequestsHandler userRequestsHandler, 
            IPaymentRequestsHandler paymentRequestsHandler)
        {
            _platformRequestsHandler = platformRequestsHandler ?? throw new ArgumentNullException(nameof(platformRequestsHandler));
            _userRequestsHandler = userRequestsHandler ?? throw new ArgumentNullException(nameof(userRequestsHandler));
            _paymentRequestsHandler = paymentRequestsHandler ?? throw new ArgumentNullException(nameof(paymentRequestsHandler));
        }

        /// <summary>
        /// Holds the Tikkie API configuration.
        /// </summary>
        public ITikkieConfiguration Configuration { get; }

        /// <summary>
        /// Carries information about the current authorization token.
        /// </summary>
        public AuthorizationToken AuthorizationTokenInfo { get; }

        /// <summary>
        /// Gets all the existing Platforms created for a certain API consumer using HTTP GET.
        /// </summary>
        /// <returns>An array of PlatformResponse object.</returns>
        /// <exception cref="TikkieErrorResponseException">If the Tikkie API returns an error response.</exception>
        public async Task<PlatformResponse[]> GetPlatformsAsync()
        {
            return await _platformRequestsHandler.GetPlatformsAsync();
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
            return await _platformRequestsHandler.CreatePlatformAsync(request);
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
            return await _userRequestsHandler.GetUsersAsync(platformToken);
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
            return await _userRequestsHandler.CreateUserAsync(request);
        }

        /// <summary>
        /// Creates a new Payment Request for an existing user using HTTP POST. 
        /// </summary>
        /// <param name="request">The PaymentRequest object</param>
        /// <returns>The PaymentResponse object with information of the created Payment Request</returns>
        /// <exception cref="ArgumentNullException">If the PaymentRequest object is null</exception>
        /// <exception cref="TikkieErrorResponseException">If the Tikkie API returns an error response.</exception>
        public async Task<PaymentResponse> CreatePaymentRequestAsync(PaymentRequest request)
        {
            return await _paymentRequestsHandler.CreatePaymentRequestAsync(request);
        }

        /// <summary>
        /// Gets all the Payment Requests from an existing user using HTTP GET.
        /// </summary>
        /// <param name="request">The UserPaymentRequest object</param>
        /// <returns>The UserPaymentResponse object</returns>
        /// <exception cref="ArgumentNullException">If the UserPaymentRequest object is null</exception>
        /// <exception cref="TikkieErrorResponseException">If the Tikkie API returns an error response.</exception>
        public async Task<UserPaymentResponse> GetUserPaymentRequestsAsync(UserPaymentRequest request)
        {
            return await _paymentRequestsHandler.GetUserPaymentRequestsAsync(request);
        }

        /// <summary>
        /// Gets a Single Payment Request using HTTP GET.
        /// </summary>
        /// <param name="request">The SinglePaymentRequest object</param>
        /// <returns>The SinglePaymentRequestResponse object</returns>
        /// <exception cref="ArgumentNullException">If the SinglePaymentRequest object is null</exception>
        /// <exception cref="TikkieErrorResponseException">If the Tikkie API returns an error response.</exception>
        public async Task<SinglePaymentRequestResponse> GetPaymentRequestAsync(SinglePaymentRequest request)
        {
            return await _paymentRequestsHandler.GetPaymentRequestAsync(request);
        }
    }
}
