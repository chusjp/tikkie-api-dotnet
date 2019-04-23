using System;

namespace TikkieAPI.Enums
{
    internal static class EnumsMapper
    {
        public static string MapToString(this PlatformUsage platformUsage)
        {
            switch (platformUsage)
            {
                case PlatformUsage.PaymentRequestForMyself: return "PAYMENT_REQUEST_FOR_MYSELF";
                case PlatformUsage.PaymentRequestForOthers: return "PAYMENT_REQUEST_FOR_OTHERS";
                default: throw new ArgumentException($"Not recognized {nameof(platformUsage)}");
            }
        }

        public static PlatformUsage MapToPlatformUsageEnum(this string platformUsageString)
        {
            switch (platformUsageString)
            {
                case "PAYMENT_REQUEST_FOR_MYSELF": return PlatformUsage.PaymentRequestForMyself;
                case "PAYMENT_REQUEST_FOR_OTHERS": return PlatformUsage.PaymentRequestForOthers;
                default: throw new ArgumentException($"Not recognized {nameof(platformUsageString)}");
            }
        }

        public static string MapToString(this PlatformStatus platformStatus)
        {
            switch (platformStatus)
            {
                case PlatformStatus.Active: return "ACTIVE";
                case PlatformStatus.Inactive: return "INACTIVE";
                default: throw new ArgumentException($"Not recognized {nameof(platformStatus)}");
            }
        }

        public static PlatformStatus MapToPlatformStatusEnum(this string platformStatusString)
        {
            switch (platformStatusString)
            {
                case "ACTIVE": return PlatformStatus.Active;
                case "INACTIVE": return PlatformStatus.Inactive;
                default: throw new ArgumentException($"Not recognized {nameof(platformStatusString)}");
            }
        }

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
