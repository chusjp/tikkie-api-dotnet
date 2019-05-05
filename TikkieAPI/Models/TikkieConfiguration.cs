using System;
using System.IO;
using System.Security.Cryptography;
using TikkieAPI.Interfaces;
using TikkieAPI.Utilities;

namespace TikkieAPI.Models
{
    /// <summary>
    /// Default class holding the Tikkie API configuration.
    /// </summary>
    internal class TikkieConfiguration : ITikkieConfiguration
    {
        private const double DefaultTokenExpirationInMinutes = 10;
        private const string DefaultIssuerName = "TikkieAPI Dotnet";

        /// <summary>
        /// Initializes a new instance of the Tikkie API configuration.
        /// </summary>
        /// <param name="apiKey">The API key that allows the usage of the Tikkie API</param>
        /// <param name="privateKeyPath">The directory path where the RSA private key PEM file can be found.</param>
        /// <param name="useTestEnvironment">True if the connexion is made to the development/sandbox Tikkie API environment. False by default.</param>
        /// <exception cref="ArgumentException">If the API Key is null or empty.</exception>
        /// <exception cref="FileNotFoundException">If the private key pem file doesn't exist in the specified location.</exception>
        /// <exception cref="InvalidDataException">If the RSA private key PEM file cannot be read.</exception>
        public TikkieConfiguration(string apiKey, string privateKeyPath, bool useTestEnvironment = false)
        {
            ApiKey = string.IsNullOrEmpty(apiKey) ? throw new ArgumentException(nameof(apiKey)) : apiKey;
            RSAKey = RSAUtilities.GetPrivateKeyFromPemFile(privateKeyPath);
            IsTestEnvironment = useTestEnvironment;
        }

        /// <summary>
        /// The API key that allows the usage of the Tikkie API. Used as value for "sub" parameter on the authentication JWT payload 
        /// and as Header for HTTP requests.
        /// </summary>
        public string ApiKey { get; }

        /// <summary>
        /// True if the connection is made to the development/sandbox Tikkie API environment.
        /// </summary>
        public bool IsTestEnvironment { get; set; }

        /// <summary>
        /// The base url used to make request to the Tikkie API. Default values for each environment are:
        /// Sandbox: https://api-sandbox.abnamro.com/v1
        /// Production: https://api.abnamro.com/v1
        /// </summary>
        public string ApiBaseUrl => IsTestEnvironment ? UrlProvider.SandboxApiBaseUrl : UrlProvider.ProductionApiBaseUrl;

        /// <summary>
        /// The OAuth Token url for authentication to the Tikkie API. Used as value for "aud" parameter on the authentication JWT payload.
        /// Default values for each environment are:
        /// Sandbox: https://auth-sandbox.abnamro.com/oauth/token
        /// Production: https://auth.abnamro.com/oauth/token
        /// </summary>
        public string OAuthTokenUrl => IsTestEnvironment ? UrlProvider.SandboxOAuthTokenUrl : UrlProvider.ProductionOAuthTokenUrl;

        /// <summary>
        /// The RSA key to be encoded as JWT.
        /// </summary>
        public RSACryptoServiceProvider RSAKey { get; }

        /// <summary>
        /// The token expiration in minutes after current DateTime. Used as value for "exp" parameter on the authentication JWT payload.
        /// </summary>
        public double TokenExpirationInMinutes { get; set; } = DefaultTokenExpirationInMinutes;

        /// <summary>
        /// The issuer name passed as claim to the authentication payload. Used as value for "iss" parameter on the authentication JWT payload.
        /// </summary>
        public string IssuerName { get; set; } = DefaultIssuerName;
    }
}
