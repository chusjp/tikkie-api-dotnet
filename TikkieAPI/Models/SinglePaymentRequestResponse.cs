using Newtonsoft.Json;
using System;
using TikkieAPI.Enums;

namespace TikkieAPI.Models
{
    public class SinglePaymentRequestResponse
    {
        /// <summary>
        /// The token that identifies the payment request.
        /// </summary>
        [JsonProperty("paymentRequestToken")]
        public string PaymentRequestToken { get; internal set; }

        /// <summary>
        /// The requested amount, in cents as string.
        /// </summary>
        [JsonProperty("amountInCents")]
        public string AmountInCentsString { get; internal set; }

        /// <summary>
        /// The requested amount, in cents.
        /// </summary>
        [JsonIgnore]
        public decimal? AmountInCents => !string.IsNullOrEmpty(AmountInCentsString) ? Convert.ToDecimal(AmountInCentsString) : (decimal?)null;

        /// <summary>
        /// The currency of the requested amount.
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; internal set; }

        /// <summary>
        /// Description of the (reason for) the request.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; internal set; }

        /// <summary>
        /// The date and time on which this payment request was created.
        /// </summary>
        [JsonProperty("created")]
        public DateTime? Created { get; internal set; }

        /// <summary>
        /// The date and time on which this payment request was expired.
        /// </summary>
        [JsonProperty("expired")]
        public DateTime? Expired { get; internal set; }

        /// <summary>
        /// The current status of the payment request as tring.
        /// </summary>
        [JsonProperty("status")]
        public string PaymentRequestStatusString { get; internal set; }

        /// <summary>
        /// The current status of the payment request as tring.
        /// </summary>
        [JsonIgnore]
        public PaymentRequestStatus PaymentRequestStatus => PaymentRequestStatusString.MapToPaymentRequestStatusEnum();

        /// <summary>
        /// If true, the bank account linked to this payment request is temporarily blocked due to exceeding the configured maximum yield per day.
        /// </summary>
        [JsonProperty("bankAccountYieldedTooFast")]
        public bool BankAccountYieldedTooFast { get; internal set; }

        /// <summary>
        /// An external identifier for this payment request, if provided.
        /// </summary>
        [JsonProperty("externalId")]
        public string ExternalId { get; internal set; }

        /// <summary>
        /// Array of all the payments that have been done to fulfill this request.
        /// </summary>
        [JsonProperty("payments")]
        public Payment[] Payments { get; internal set; }
    }
}
