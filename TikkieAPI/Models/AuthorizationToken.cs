using System;

namespace TikkieAPI.Models
{
    /// <summary>
    /// Contains information about the authorization token.
    /// </summary>
    public class AuthorizationToken
    {
        /// <summary>
        /// The access token in use.
        /// </summary>
        public string AccessToken { get; internal set; }

        /// <summary>
        /// Token expiration date, if any.
        /// </summary>
        public DateTime? TokenExpirationDate { get; internal set; }

        /// <summary>
        /// The scope retrieved by the authentication request.
        /// </summary>
        public string Scope { get; internal set; }

        /// <summary>
        /// The token type.
        /// </summary>
        public string TokenType { get; internal set; }

        /// <summary>
        /// True if the token has expired or directly does not have an expiration date.
        /// </summary>
        public bool IsTokenExpired => !TokenExpirationDate.HasValue || TokenExpirationDate.Value <= DateTime.Now;
    }
}
