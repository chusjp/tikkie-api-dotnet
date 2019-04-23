﻿using System;

namespace TikkiePaymentRequestAPI.Models
{
    public class UserPaymentRequest
    {
        /// <summary>
        /// Identifies to which platform the user is enrolled.
        /// Required.
        /// </summary>
        public string PlatformToken { get; set; }

        /// <summary>
        /// Identifies from which user the payment requests are accessed.
        /// Required.
        /// </summary>
        public string UserToken { get; set; }

        /// <summary>
        /// Pagination: zero based index of the records range to return.
        /// Required.
        /// </summary>
        public uint Offset { get; set; }

        /// <summary>
        /// Pagination: the number of records to return.
        /// Required.
        /// </summary>
        public uint Limit { get; set; }

        /// <summary>
        /// Filtering: only include payment requests created after this date/time.
        /// Optional.
        /// </summary>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Filtering: only include payment requests created after this date/time (in ISO-8601 format). Example: 2017-05-31T23:59:59Z.
        /// Optional.
        /// </summary>
        public string FromDateString => FromDate?.ToString("yyyy-MM-ddTHH:mm:ssZ");

        /// <summary>
        /// Filtering: only include payment requests created before this date/time.
        /// Optional.
        /// </summary>
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// Filtering: only include payment requests created before this date/time (in ISO-8601 format). Example: 2017-05-31T23:59:59Z.
        /// Optional.
        /// </summary>
        public string ToDateString => ToDate?.ToString("yyyy-MM-ddTHH:mm:ssZ");
    }
}
