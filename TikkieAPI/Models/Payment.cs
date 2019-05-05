using System;
using Newtonsoft.Json;
using TikkieAPI.Enums;

namespace TikkieAPI.Models
{
    public class Payment
    {
        /// <summary>
        /// The token for this payment.
        /// </summary>
        [JsonProperty("paymentToken")]
        public string PaymentToken { get; internal set; }

        /// <summary>
        /// The name of the counter party (the person paying this payment).
        /// </summary>
        [JsonProperty("counterPartyName")]
        public string CounterPartyName { get; internal set; }

        /// <summary>
        /// The amount that was paid in cents.
        /// </summary>
        [JsonProperty("amountInCents")]
        public decimal? AmountInCents { get; internal set; }

        /// <summary>
        /// The currency of the amount that was paid.
        /// </summary>
        [JsonProperty("amountCurrency")]
        public string AmountCurrency { get; internal set; }

        /// <summary>
        /// Description for this payment.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; internal set; }

        /// <summary>
        /// The date and time on which this payment was created.
        /// </summary>
        [JsonProperty("created")]
        public DateTime? Created { get; internal set; }

        /// <summary>
        /// The online payment state of this payment as string. This indicates if the counter party paid or not.
        /// </summary>
        [JsonProperty("onlinePaymentStatus")]
        public string OnlinePaymentStatusString { get; internal set; }

        /// <summary>
        /// The online payment state of this payment as string. This indicates if the counter party paid or not.
        /// </summary>
        [JsonIgnore]
        public OnlinePaymentStatus OnlinePaymentStatus => OnlinePaymentStatusString.MapToOnlinePaymentStatusEnum();
    }
}
