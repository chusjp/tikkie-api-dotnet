using System.Threading.Tasks;
using TikkieAPI.Models;

namespace TikkieAPI.Interfaces
{
    /// <summary>
    /// Handles the Payment related requests to the Tikkie API authenticating automatically if the current token has expired.
    /// Payment requests are described in https://developer.abnamro.com/api/tikkie-v1/technical-details
    /// </summary>
    internal interface IPaymentRequestsHandler
    {
        /// <summary>
        /// Creates a new Payment Request for an existing user using HTTP POST. 
        /// </summary>
        /// <param name="request">The PaymentRequest object</param>
        /// <returns>The PaymentResponse object with information of the created Payment Request</returns>
        Task<PaymentResponse> CreatePaymentRequestAsync(PaymentRequest request);

        /// <summary>
        /// Gets all the Payment Requests from an existing user using HTTP GET.
        /// </summary>
        /// <param name="request">The UserPaymentRequest object</param>
        /// <returns>The UserPaymentResponse object</returns>
        Task<UserPaymentResponse> GetUserPaymentRequestsAsync(UserPaymentRequest request);

        /// <summary>
        /// Gets a Single Payment Request using HTTP GET.
        /// </summary>
        /// <param name="request">The SinglePaymentRequest object</param>
        /// <returns>The SinglePaymentRequestResponse object</returns>
        Task<SinglePaymentRequestResponse> GetPaymentRequestAsync(SinglePaymentRequest request);
    }
}
