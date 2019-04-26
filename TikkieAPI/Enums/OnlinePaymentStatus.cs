namespace TikkieAPI.Enums
{
    /// <summary>
    /// Represents the online payment state of the payment from the Tikkie API as an enum. 
    /// This indicates if the counter party paid or not. One of [ NEW, PENDING, PAID, NOT_PAID ]
    /// </summary>
    public enum OnlinePaymentStatus
    {
        New,
        Pending,
        Paid,
        NotPaid
    }
}
