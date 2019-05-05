namespace TikkieAPI.Enums
{
    /// <summary>
    /// Represents the current status of the payment request matching with values on the Tikkie API. 
    /// One of [ OPEN, CLOSED, EXPIRED, MAX_YIELD_REACHED, MAX_SUCCESSFUL_PAYMENTS_REACHED ]
    /// </summary>
    public enum PaymentRequestStatus
    {
        Open,
        Closed,
        Expired,
        MaxYieldReached,
        MaxSuccessfulPaymentsReached
    }
}
