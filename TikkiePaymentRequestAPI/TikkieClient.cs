using System;
using System.Threading.Tasks;
using TikkiePaymentRequestAPI.Models;

namespace TikkiePaymentRequestAPI
{
    public class TikkieClient
    {
        private Platform _platform;
        private User _user;

        public TikkieClient(Configuration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            var authorizedRequestsHandler = new AuthorizedRequestsHandler(Configuration);
            AuthorizationToken = authorizedRequestsHandler.AuthorizationToken;
            _platform = new Platform(authorizedRequestsHandler);
            _user = new User(authorizedRequestsHandler);
        }

        public Configuration Configuration { get; }

        public AuthorizationToken AuthorizationToken { get; }

        public async Task<PlatformResponse[]> GetPlatformsAsync()
        {
            return await _platform.GetPlatformsAsync();
        }

        public async Task<PlatformResponse> CreatePlatformAsync(PlatformRequest request)
        {
            return await _platform.CreatePlatformAsync(request);
        }

        public async Task<UserResponse[]> GetUsersAsync(string platformToken)
        {
            return await _user.GetUsersAsync(platformToken);
        }

        public async Task<UserResponse> CreateUserAsync(string platformToken, UserRequest request)
        {
            return await _user.CreateUserAsync(platformToken, request);
        }
    }
}
