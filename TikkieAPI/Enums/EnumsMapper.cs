using System;

namespace TikkieAPI.Enums
{
    /// <summary>
    /// Class with extension methods for mapping the custom Tikkie API enum to its corresponding string and vice versa.
    /// </summary>
    internal static class EnumsMapper
    {
        /// <summary>
        /// Maps the <see cref="PlatformUsage"/> to its corresponding Tikkie API string.
        /// </summary>
        /// <param name="platformUsage">The platform usage as enum.</param>
        /// <returns>The platform usage in string format.</returns>
        /// <exception cref="ArgumentException">When the platform usage enum is not recognized.</exception>
        public static string MapToString(this PlatformUsage platformUsage)
        {
            switch (platformUsage)
            {
                case PlatformUsage.PaymentRequestForMyself: return "PAYMENT_REQUEST_FOR_MYSELF";
                case PlatformUsage.PaymentRequestForOthers: return "PAYMENT_REQUEST_FOR_OTHERS";
                default: throw new ArgumentException($"Not recognized {nameof(platformUsage)}");
            }
        }

        /// <summary>
        /// Maps the Platform Usage property from Tikkie API string to the correct <see cref="PlatformUsage"/> enum.
        /// </summary>
        /// <param name="platformUsageString">The platform usage as string.</param>
        /// <returns>The platform usage in enum format.</returns>
        /// <exception cref="ArgumentException">When the platform usage string is not recognized.</exception>
        public static PlatformUsage MapToPlatformUsageEnum(this string platformUsageString)
        {
            switch (platformUsageString)
            {
                case "PAYMENT_REQUEST_FOR_MYSELF": return PlatformUsage.PaymentRequestForMyself;
                case "PAYMENT_REQUEST_FOR_OTHERS": return PlatformUsage.PaymentRequestForOthers;
                default: throw new ArgumentException($"Not recognized {nameof(platformUsageString)}");
            }
        }

        /// <summary>
        /// Maps the <see cref="PlatformStatus"/> to its corresponding Tikkie API string.
        /// </summary>
        /// <param name="platformStatus">The platform status as enum.</param>
        /// <returns>The platform usage in string format.</returns>
        /// <exception cref="ArgumentException">When the platform status enum is not recognized.</exception>
        public static string MapToString(this PlatformStatus platformStatus)
        {
            switch (platformStatus)
            {
                case PlatformStatus.Active: return "ACTIVE";
                case PlatformStatus.Inactive: return "INACTIVE";
                default: throw new ArgumentException($"Not recognized {nameof(platformStatus)}");
            }
        }

        /// <summary>
        /// Maps the Platform Status property from Tikkie API string to the correct <see cref="PlatformStatus"/> enum.
        /// </summary>
        /// <param name="platformStatusString">The platform status as string.</param>
        /// <returns>The platform status in enum format.</returns>
        /// <exception cref="ArgumentException">When the platform status string is not recognized.</exception>
        public static PlatformStatus MapToPlatformStatusEnum(this string platformStatusString)
        {
            switch (platformStatusString)
            {
                case "ACTIVE": return PlatformStatus.Active;
                case "INACTIVE": return PlatformStatus.Inactive;
                default: throw new ArgumentException($"Not recognized {nameof(platformStatusString)}");
            }
        }

        /// <summary>
        /// Maps the <see cref="PaymentRequestStatus"/> to its corresponding Tikkie API string.
        /// </summary>
        /// <param name="paymentRequestStatus">The payment request status as enum.</param>
        /// <returns>The payment request status in string format.</returns>
        /// <exception cref="ArgumentException">When the payment request status enum is not recognized.</exception>
        public static string MapToString(this PaymentRequestStatus paymentRequestStatus)
        {
            switch (paymentRequestStatus)
            {
                case PaymentRequestStatus.Open: return "OPEN";
                case PaymentRequestStatus.Closed: return "CLOSED";
                case PaymentRequestStatus.Expired: return "EXPIRED";
                case PaymentRequestStatus.MaxYieldReached: return "MAX_YIELD_REACHED";
                case PaymentRequestStatus.MaxSuccessfulPaymentsReached: return "MAX_SUCCESSFUL_PAYMENTS_REACHED";
                default: throw new ArgumentException($"Not recognized {nameof(paymentRequestStatus)}");
            }
        }

        /// <summary>
        /// Maps the Payment Request Status property from Tikkie API string to the correct <see cref="PaymentRequestStatus"/> enum.
        /// </summary>
        /// <param name="paymentRequestStatusString">The payment request status as string.</param>
        /// <returns>The payment request status in enum format.</returns>
        /// <exception cref="ArgumentException">When the payment request status string is not recognized.</exception>
        public static PaymentRequestStatus MapToPaymentRequestStatusEnum(this string paymentRequestStatusString)
        {
            switch (paymentRequestStatusString)
            {
                case "OPEN": return PaymentRequestStatus.Open;
                case "CLOSED": return PaymentRequestStatus.Closed;
                case "EXPIRED": return PaymentRequestStatus.Expired;
                case "MAX_YIELD_REACHED": return PaymentRequestStatus.MaxYieldReached;
                case "MAX_SUCCESSFUL_PAYMENTS_REACHED": return PaymentRequestStatus.MaxSuccessfulPaymentsReached;
                default: throw new ArgumentException($"Not recognized {nameof(paymentRequestStatusString)}");
            }
        }

        /// <summary>
        /// Maps the <see cref="OnlinePaymentStatus"/> to its corresponding Tikkie API string.
        /// </summary>
        /// <param name="onlinePaymentStatus">The online payment status as enum.</param>
        /// <returns>The online payment status in string format.</returns>
        /// <exception cref="ArgumentException">When the online payment status enum is not recognized.</exception>
        public static string MapToString(this OnlinePaymentStatus onlinePaymentStatus)
        {
            switch (onlinePaymentStatus)
            {
                case OnlinePaymentStatus.New: return "NEW";
                case OnlinePaymentStatus.Pending: return "PENDING";
                case OnlinePaymentStatus.Paid: return "PAID";
                case OnlinePaymentStatus.NotPaid: return "NOT_PAID";
                default: throw new ArgumentException($"Not recognized {nameof(onlinePaymentStatus)}");
            }
        }

        /// <summary>
        /// Maps the Online Payment Status property from Tikkie API string to the correct <see cref="OnlinePaymentStatus"/> enum.
        /// </summary>
        /// <param name="onlinePaymentStatusString">The online payment status as string.</param>
        /// <returns>The online payment status in enum format.</returns>
        /// <exception cref="ArgumentException">When the online payment status string is not recognized.</exception>
        public static OnlinePaymentStatus MapToOnlinePaymentStatusEnum(this string onlinePaymentStatusString)
        {
            switch (onlinePaymentStatusString)
            {
                case "NEW": return OnlinePaymentStatus.New;
                case "PENDING": return OnlinePaymentStatus.Pending;
                case "PAID": return OnlinePaymentStatus.Paid;
                case "NOT_PAID": return OnlinePaymentStatus.NotPaid;
                default: throw new ArgumentException($"Not recognized {nameof(onlinePaymentStatusString)}");
            }
        }
    }
}
