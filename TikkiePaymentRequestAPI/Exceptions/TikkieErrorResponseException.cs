using System;
using TikkiePaymentRequestAPI.Models;

namespace TikkiePaymentRequestAPI.Exceptions
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
