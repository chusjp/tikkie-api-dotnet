using System;

namespace TikkiePaymentRequestAPI.Enums
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

        public static PlatformUsage MapToPlatFormUsageEnum(this string platformUsageString)
        {
            switch (platformUsageString)
            {
                case "PAYMENT_REQUEST_FOR_MYSELF": return PlatformUsage.PaymentRequestForMyself;
                case "PAYMENT_REQUEST_FOR_OTHERS": return PlatformUsage.PaymentRequestForOthers;
                default: throw new ArgumentException($"Not recognized {nameof(platformUsageString)}");
            }
        }

        public static string MapToString(this Status status)
        {
            switch (status)
            {
                case Status.Active: return "ACTIVE";
                case Status.Inactive: return "INACTIVE";
                default: throw new ArgumentException($"Not recognized {nameof(status)}");
            }
        }

        public static Status MapToStatusEnum(this string statusString)
        {
            switch (statusString)
            {
                case "ACTIVE": return Status.Active;
                case "INACTIVE": return Status.Inactive;
                default: throw new ArgumentException($"Not recognized {nameof(statusString)}");
            }
        }
    }
}
