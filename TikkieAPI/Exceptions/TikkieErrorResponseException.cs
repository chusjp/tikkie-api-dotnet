using System;
using TikkieAPI.Models;

namespace TikkieAPI.Exceptions
{
    public class TikkieErrorResponseException : Exception
    {
        public TikkieErrorResponseException(string message, ErrorResponse[] errorResponses) : base(message)
        {
            ErrorResponses = errorResponses;
        }

        public ErrorResponse[] ErrorResponses { get; }
    }
}
