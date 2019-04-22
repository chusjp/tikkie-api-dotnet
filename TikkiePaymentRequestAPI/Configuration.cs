using System;
using System.Security.Cryptography;
using TikkiePaymentRequestAPI.Utilities;

namespace TikkiePaymentRequestAPI
{
    public class Configuration
    {
        // Environments
        private const string SandboxApiBaseUrl = "https://api-sandbox.abnamro.com/v1";
        private const string ProductionApiBaseUrl = "https://api.abnamro.com/v1";
        private const string SandboxOAuthTokenUrl = "https://auth-sandbox.abnamro.com/oauth/token";
        private const string ProductionOAuthTokenUrl = "https://auth.abnamro.com/oauth/token";

        private const double DefaultTokenExpirationInMinutes = 10;
        private const string DefaultIssuerName = "TikkieAPI Dotnet";

        public Configuration(string apiKey, string privateKeyPath, bool useTestEnvironment = false)
        {
            ApiKey = string.IsNullOrEmpty(apiKey) ? throw new ArgumentNullException(nameof(apiKey)) : apiKey;
            RSAKey = RSAFromPemFile.PrivateKeyFromPemFile(privateKeyPath);
            IsTestEnvironment = useTestEnvironment;
        }

        public string ApiKey { get; }

        public bool IsTestEnvironment { get; set; }

        public string ApiBaseUrl => IsTestEnvironment ? SandboxApiBaseUrl : ProductionApiBaseUrl;

        public string OAuthTokenUrl => IsTestEnvironment ? SandboxOAuthTokenUrl : ProductionOAuthTokenUrl;

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
