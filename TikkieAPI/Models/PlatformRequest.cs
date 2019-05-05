using Newtonsoft.Json;
using TikkieAPI.Enums;

namespace TikkieAPI.Models
{
    public class PlatformRequest
    {
        /// <summary>
        /// Name of the new platform. Length: 2-100 characters.
        /// Required.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The phone number of a contact person of the platform. Length: 5-100 characters. Example: 0601234567.
        /// Currently only Dutch mobile numbers are supported.
        /// Required.
        /// </summary>
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// The email address of a contact person of the platform. Length: 5-100 characters.
        /// Optional.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Url on which to receive notifications. Length: up to 200 characters.
        /// Optional.
        /// </summary>
        [JsonProperty("notificationUrl")]
        public string NotificationUrl { get; set; }

        /// <summary>
        /// The type of usage for this platform.
        /// Required.
        /// </summary>
        [JsonIgnore]
        public PlatformUsage Usage { get; set; }

        /// <summary>
        /// The type of usage for this platform as string.
        /// Required.
        /// </summary>
        [JsonProperty("platformUsage")]
        public string UsageString => Usage.MapToString();
    }
}