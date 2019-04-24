using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TikkieAPI.Models;
using TikkieAPI.RequestsHandlers;

[assembly: InternalsVisibleTo("TikkieAPI.Tests")]
namespace TikkieAPI
{
    public class TikkieClient
    {
        private PlatformRequestsHandler _platformRequestsHandler;
        private UserRequestsHandler _userRequestsHandler;
        private PaymentRequestsHandler _paymentRequestsHandler;

        public TikkieClient(TikkieConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            var authorizedRequestsHandler = new AuthorizedRequestsHandler(Configuration);
            AuthorizationToken = authorizedRequestsHandler.AuthorizationToken;
            _platformRequestsHandler = new PlatformRequestsHandler(authorizedRequestsHandler);
            _userRequestsHandler = new UserRequestsHandler(authorizedRequestsHandler);
            _paymentRequestsHandler = new PaymentRequestsHandler(authorizedRequestsHandler);
        }

        public TikkieConfiguration Configuration { get; }

        public AuthorizationToken AuthorizationToken { get; }

        public async Task<PlatformResponse[]> GetPlatformsAsync()
        {
            return await _platformRequestsHandler.GetPlatformsAsync();
        }

        public async Task<PlatformResponse> CreatePlatformAsync(PlatformRequest request)
        {
            return await _platformRequestsHandler.CreatePlatformAsync(request);
        }

        public async Task<UserResponse[]> GetUsersAsync(string platformToken)
        {
            return await _userRequestsHandler.GetUsersAsync(platformToken);
        }

        public async Task<UserResponse> CreateUserAsync(UserRequest request)
        {
            return await _userRequestsHandler.CreateUserAsync(request);
        }

        public async Task<PaymentResponse> CreatePaymentRequestAsync(PaymentRequest request)
        {
            return await _paymentRequestsHandler.CreatePaymentRequestAsync(request);
        }

        public async Task<UserPaymentResponse> GetUserPaymentRequestsAsync(UserPaymentRequest request)
        {
            return await _paymentRequestsHandler.GetUserPaymentRequestsAsync(request);
        }

        public async Task<SinglePaymentRequestResponse> GetPaymentRequestAsync(SinglePaymentRequest request)
        {
            return await _paymentRequestsHandler.GetPaymentRequestAsync(request);
        }
    }
}
