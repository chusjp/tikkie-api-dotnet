using System;
using System.Security.Cryptography;
using TikkieAPI.Utilities;

namespace TikkieAPI
{
    public class TikkieConfiguration
    {
        private const double DefaultTokenExpirationInMinutes = 10;
        private const string DefaultIssuerName = "TikkieAPI Dotnet";

        public TikkieConfiguration(string apiKey, string privateKeyPath, bool useTestEnvironment = false)
        {
            ApiKey = string.IsNullOrEmpty(apiKey) ? throw new ArgumentNullException(nameof(apiKey)) : apiKey;
            RSAKey = RSAUtilities.GetPrivateKeyFromPemFile(privateKeyPath);
            IsTestEnvironment = useTestEnvironment;
        }

        public string ApiKey { get; }

        public bool IsTestEnvironment { get; set; }

        public string ApiBaseUrl => IsTestEnvironment ? UrlProvider.SandboxApiBaseUrl : UrlProvider.ProductionApiBaseUrl;

        public string OAuthTokenUrl => IsTestEnvironment ? UrlProvider.SandboxOAuthTokenUrl : UrlProvider.ProductionOAuthTokenUrl;

        public RSACryptoServiceProvider RSAKey { get; }

        /// <summary>
        /// The token expiration in minutes after current DateTime.
        /// </summary>
        public double TokenExpirationInMinutes { get; set; } = DefaultTokenExpirationInMinutes;

        /// <summary>
        /// The issuer name passed as claim to the authentication payload.
        /// </summary>
        public string IssuerName { get; set; } = DefaultIssuerName;
    }
}
