using System.Security.Cryptography;

namespace TikkieAPI.Interfaces
{
    /// <summary>
    /// Holds the Tikkie API configuration.
    /// </summary>
    public interface ITikkieConfiguration
    {
        /// <summary>
        /// The API key that allows the usage of the Tikkie API. Used as value for "sub" parameter on the authentication JWT payload 
        /// and as Header for HTTP requests.
        /// </summary>
        string ApiKey { get; }

        /// <summary>
        /// True if the connection is made to the development/sandbox Tikkie API environment.
        /// </summary>
        bool IsTestEnvironment { get; set; }

        /// <summary>
        /// The base url used to make request to the Tikkie API.
        /// </summary>
        string ApiBaseUrl { get; }

        /// <summary>
        /// The OAuth Token url for authentication to the Tikkie API. Used as value for "aud" parameter on the authentication JWT payload.
        /// </summary>
        string OAuthTokenUrl { get; }

        /// <summary>
        /// The RSA key to be encoded as JWT.
        /// </summary>
        RSACryptoServiceProvider RSAKey { get; }

        /// <summary>
        /// The token expiration in minutes after current DateTime. Used as value for "exp" parameter on the authentication JWT payload.
        /// </summary>
        double TokenExpirationInMinutes { get; set; }

        /// <summary>
        /// The issuer name passed as claim to the authentication payload. Used as value for "iss" parameter on the authentication JWT payload.
        /// </summary>
        string IssuerName { get; set; }
    }
}
