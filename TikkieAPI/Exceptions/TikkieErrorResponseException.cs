using System;
using TikkieAPI.Models;

namespace TikkieAPI.Exceptions
{
    /// <summary>
    /// Exception containing the Tikkie API error responses.
    /// Official documentation: https://developer.abnamro.com/api/tikkie-v1/technical-details#error-response-code
    /// Error codes: https://developer.abnamro.com/get-started#error-codes
    /// </summary>
    public class TikkieErrorResponseException : Exception
    {
        public TikkieErrorResponseException(string message, ErrorResponse[] errorResponses) : base(message)
        {
            ErrorResponses = errorResponses;
        }

        /// <summary>
        /// Array of error responses.
        /// </summary>
        public ErrorResponse[] ErrorResponses { get; }
    }
}
