namespace TikkiePaymentRequestAPI.Models
{
    public class SinglePaymentRequest
    {
        /// <summary>
        /// Identifies to which platform the user is enrolled.
        /// Required.
        /// </summary>
        public string PlatformToken { get; set; }

        /// <summary>
        /// Identifies from which user this payment request is accessed.
        /// Required.
        /// </summary>
        public string UserToken { get; set; }

        /// <summary>
        /// Identifies the accessed payment request.
        /// Required.
        /// </summary>
        public string PaymentRequestToken { get; set; }
    }
}
