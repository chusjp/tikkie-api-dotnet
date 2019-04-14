namespace TikkiePaymentRequestAPI.Constants
{
    internal static class AuthenticationConstants
    {
        public const string ClientAssertionType = "urn:ietf:params:oauth:client-assertion-type:jwt-bearer";
        public const string GrantType = "client_credentials";
        public const string DefaultScope = "tikkie";

        public const string ExpirationTimeInSecondsClaimName = "exp";
        public const string NotBeforeInSecondsClaimName = "nbf";
        public const string IssuerNameClaimName = "iss";
        public const string SubjectClaimName = "sub";
        public const string AudienceClaimName = "aud";

        public const double DefaultTokenExpirationInMinutes = 10;
        public const double DefaultNotBeforeAcceptanceInMinutes = 1;
        public const string DefaultIssuerName = "TikkiePaymentRequestAPI Dotnet";

        public const string ApiKeyHeaderName = "API-Key";
    }
}
