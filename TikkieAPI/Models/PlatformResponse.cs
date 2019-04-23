using System;
using Newtonsoft.Json;
using TikkieAPI.Enums;

namespace TikkieAPI.Models
{
    public class PlatformResponse
    {
        /// <summary>
        /// The name of the new platform.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; internal set; }

        /// <summary>
        /// The token that identifies the new platform.
        /// </summary>
        [JsonProperty("platformToken")]
        public string PlatformToken { get; internal set; }

        /// <summary>
        /// The phone number of a contact person of the platform.
        /// </summary>
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; internal set; }

        /// <summary>
        /// The email address of a contact person of the platform.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; internal set; }

        /// <summary>
        /// Optional url on which to receive notifications.
        /// </summary>
        [JsonProperty("notificationUrl")]
        public string NotificationUrl { get; internal set; }

        /// <summary>
        /// The current status of the new platform as string.
        /// </summary>
        [JsonProperty("status")]
        public string StatusString { get; internal set; }

        /// <summary>
        /// The current status of the new platform.
        /// </summary>
        [JsonIgnore]
        public PlatformStatus Status => StatusString.MapToPlatformStatusEnum();

        /// <summary>
        /// The type of usage for this platform as string.
        /// </summary>
        [JsonProperty("platformUsage")]
        public string UsageString { get; internal set; }

        /// <summary>
        /// The type of usage for this platform.
        /// </summary>
        [JsonIgnore]
        public PlatformUsage Usage => UsageString.MapToPlatformUsageEnum();
    }
}
