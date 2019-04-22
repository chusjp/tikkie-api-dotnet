﻿using System;
using Newtonsoft.Json;
using TikkiePaymentRequestAPI.Enums;

namespace TikkiePaymentRequestAPI.Models
{
    public class PlatformResponse
    {
        /// <summary>
        /// Unique id generated for every request.
        /// </summary>
        public Guid TraceId { get; set; }

        /// <summary>
        /// The name of the new platform.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The token that identifies the new platform.
        /// </summary>
        [JsonProperty("platformToken")]
        public string PlatformToken { get; set; }

        /// <summary>
        /// The phone number of a contact person of the platform.
        /// </summary>
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// The email address of a contact person of the platform.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Optional url on which to receive notifications.
        /// </summary>
        [JsonProperty("notificationUrl")]
        public string NotificationUrl { get; set; }

        /// <summary>
        /// The current status of the new platform as string.
        /// </summary>
        [JsonProperty("status")]
        public string StatusString { get; set; }

        /// <summary>
        /// The current status of the new platform.
        /// </summary>
        public Status Status => StatusString.MapToStatusEnum();


        /// <summary>
        /// The type of usage for this platform.
        /// </summary>
        public PlatformUsage Usage { get; set; }
    }
}
