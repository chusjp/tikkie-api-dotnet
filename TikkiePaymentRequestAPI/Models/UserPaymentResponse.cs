using Newtonsoft.Json;

namespace TikkiePaymentRequestAPI.Models
{
    public class UserPaymentResponse
    {
        /// <summary>
        /// Array containing all the payment requests in the requested range.
        /// </summary>
        [JsonProperty("paymentRequests")]
        public SinglePaymentRequestResponse[] PaymentRequests { get; internal set; }

        /// <summary>
        /// Total number of payment requests.
        /// </summary>
        [JsonProperty("totalElements")]
        public int TotalElements { get; internal set; }
    }
}
