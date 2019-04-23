using Newtonsoft.Json;
using TikkiePaymentRequestAPI.Enums;

namespace TikkiePaymentRequestAPI.Models
{
    public class PaymentRequest
    {
        /// <summary>
        /// Identifies to which platform the user is enrolled.
        /// Required.
        /// </summary>
        [JsonIgnore]
        public string PlatformToken { get; set; }

        /// <summary>
        /// Identifies to which user the request is made.
        /// Required.
        /// </summary>
        [JsonIgnore]
        public string UserToken { get; set; }

        /// <summary>
        /// Identifies to which account of the user the request is made.
        /// Required.
        /// </summary>
        [JsonIgnore]
        public string BankAccountToken { get; set; }

        /// <summary>
        /// The amount to be payed, in cents. When left empty, the payment request will automatically 
        /// becomes a payment request with open amount, where the payer can decide on the amount when paying. 
        /// Length: 1-6 characters.
        /// Optional.
        /// </summary>
        [JsonIgnore]
        public decimal? AmountInCents { get; set; }

        /// <summary>
        /// The amount to be payed, in cents as string. When left empty, the payment request will automatically 
        /// becomes a payment request with open amount, where the payer can decide on the amount when paying. 
        /// Length: 1-6 characters.
        /// Optional.
        /// </summary>
        [JsonProperty("amountInCents")]
        public string AmountInCentsString => AmountInCents?.ToString();

        /// <summary>
        /// The currency in which the amount has to be payed. Length: 3 characters.
        /// Required.
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Description of the (reason for) the request. Length: 1-35 characters.
        /// Required.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// An external identifier for this payment request, e.g. an invoice number. 
        /// Required when PlatformUsage is set to <see cref="PlatformUsage.PaymentRequestForMyself"/>. Length: 1-35 characters.
        /// </summary>
        [JsonProperty("externalId")]
        public string ExternalId { get; set; }
    }
}
