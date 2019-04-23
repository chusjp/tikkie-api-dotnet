using System;

namespace TikkieAPI.Models
{
    public class AuthorizationToken
    {
        public string AccessToken { get; internal set; }

        public DateTime? TokenExpirationDate { get; internal set; }

        public string Scope { get; internal set; }

        public string TokenType { get; internal set; }

        public bool IsTokenExpired => !TokenExpirationDate.HasValue || TokenExpirationDate.Value >= DateTime.Now;
    }
}
