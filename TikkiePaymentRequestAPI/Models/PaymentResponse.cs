using Newtonsoft.Json;

namespace TikkiePaymentRequestAPI.Models
{
    public class PaymentResponse
    {
        /// <summary>
        /// The URL that directs the counter party to a payment page.
        /// </summary>
        [JsonProperty("paymentRequestUrl")]
        public string PaymentRequestUrl { get; internal set; }

        /// <summary>
        /// The token that identifies the payment request, for future access.
        /// </summary>
        [JsonProperty("paymentRequestToken")]
        public string PaymentRequestToken { get; internal set; }

        /// <summary>
        /// An external identifier for this payment request, if provided.
        /// </summary>
        [JsonProperty("externalId")]
        public string ExternalId { get; internal set; }
    }
}
