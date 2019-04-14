using System;
using System.Security.Cryptography;
using TikkiePaymentRequestAPI.Constants;
using TikkiePaymentRequestAPI.Helpers;

namespace TikkiePaymentRequestAPI
{
    public class Configuration
    {
        public Configuration(string apiKey, string privateKeyPath, bool useTestEnvironment = false)
        {
            ApiKey = string.IsNullOrEmpty(apiKey) ? throw new ArgumentNullException(nameof(apiKey)) : apiKey;
            RSAKey = RSAFromPemFile.PrivateKeyFromPemFile(privateKeyPath);
            IsTestEnvironment = useTestEnvironment;
        }

        public string ApiKey { get; }

        public bool IsTestEnvironment { get; }

        public string ApiBaseUrl => IsTestEnvironment ? Environments.SandboxApiBaseUrl : Environments.ProductionApiBaseUrl;

        public string OAuthTokenUrl => IsTestEnvironment ? Environments.SandboxOAuthTokenUrl : Environments.ProductionOAuthTokenUrl;

        public RSACryptoServiceProvider RSAKey { get; }

        /// <summary>
        /// The token expiration in minutes after current DateTime.
        /// </summary>
        public double TokenExpirationInMinutes { get; set; } = AuthenticationConstants.DefaultTokenExpirationInMinutes;

        /// <summary>
        /// The issuer name passed as claim to the authentication payload.
        /// </summary>
        public string IssuerName { get; set; } = AuthenticationConstants.DefaultIssuerName;
    }
}
