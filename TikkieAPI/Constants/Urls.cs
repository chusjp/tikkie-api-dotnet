namespace TikkieAPI.Constants
{
    internal static class Urls
    {
        /// <summary>
        /// Base url for development/sandbox Tikkie API.
        /// </summary>
        public const string SandboxApiBaseUrl = "https://api-sandbox.abnamro.com/v1";

        /// <summary>
        /// Base url for production Tikkie API.
        /// </summary>
        public const string ProductionApiBaseUrl = "https://api.abnamro.com/v1";

        /// <summary>
        /// Token url used for authentication payload in the development/sandbox environment.
        /// </summary>
        public const string SandboxOAuthTokenUrl = "https://auth-sandbox.abnamro.com/oauth/token";

        /// <summary>
        /// Token url used for authentication payload in the production environment.
        /// </summary>
        public const string ProductionOAuthTokenUrl = "https://auth.abnamro.com/oauth/token";

        /// <summary>
        /// The url suffix to be appended to a base url used for Authentication.
        /// </summary>
        public const string AuthenticationUrlSuffix = "/oauth/token";

        /// <summary>
        /// The url suffix to be appended to a base url used for Platform related requests.
        /// </summary>
        public const string PlatformUrlSuffix = "/tikkie/platforms";

        /// <summary>
        /// The url suffix to be appended to a base url used for User related requests.
        /// </summary>
        /// <param name="platformToken">Identifies to which platform the user is enrolled.</param>
        /// <returns>The User url suffix.</returns>
        public static string UserUrlSuffix(string platformToken) 
            => $"/tikkie/platforms/{platformToken}/users";

        /// <summary>
        /// Creates the url suffix to be appended to a base url for Payment Request creations.
        /// </summary>
        /// <param name="platformToken">Identifies to which platform the user is enrolled.</param>
        /// <param name="userToken">Identifies to which user the request is made.</param>
        /// <param name="bankAccountToken">Identifies to which account of the user the request is made.</param>
        /// <returns>The payment creation url suffix<./returns>
        public static string PaymentCreationUrlSuffix(string platformToken, string userToken, string bankAccountToken)
            => $"/tikkie/platforms/{platformToken}/users/{userToken}/bankaccounts/{bankAccountToken}/paymentrequests";

        /// <summary>
        /// Creates the url suffix to be appended to a base url for getting the User Payment Requests.
        /// </summary>
        /// <param name="platformToken">Identifies to which platform the user is enrolled.</param>
        /// <param name="userToken">Identifies to which user the request is made.</param>
        /// <returns>The get user payments url suffix.</returns>
        public static string GetUserPaymentsUrlSuffix(string platformToken, string userToken)
            => $"/tikkie/platforms/{platformToken}/users/{userToken}/paymentrequests";

        /// <summary>
        /// Creates the url suffix to be appended to a base url for getting a Single Payment Request.
        /// </summary>
        /// <param name="platformToken">Identifies to which platform the user is enrolled.</param>
        /// <param name="userToken">Identifies to which user the request is made.</param>
        /// <param name="paymentRequestToken">Identifies the accessed payment request.</param>
        /// <returns>The get single payment url suffix.</returns>
        public static string GetPaymentUrlSuffix(string platformToken, string userToken, string paymentRequestToken)
            => $"/tikkie/platforms/{platformToken}/users/{userToken}/paymentrequests/{paymentRequestToken}";
    }
}
