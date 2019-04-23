using Newtonsoft.Json;
using TikkieAPI.Enums;

namespace TikkieAPI.Models
{
    public class UserResponse
    {
        /// <summary>
        /// The token that identifies the new user
        /// </summary>
        [JsonProperty("userToken")]
        public string UserToken { get; internal set; }

        /// <summary>
        /// The name of the new user.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; internal set; }

        /// <summary>
        /// The current status of the new user as string.
        /// </summary>
        [JsonProperty("status")]
        public string StatusString { get; internal set; }

        /// <summary>
        /// The current status of the new user as string.
        /// </summary>
        [JsonIgnore]
        public PlatformStatus Status => StatusString.MapToPlatformStatusEnum();

        /// <summary>
        /// An array holding bank account objects, that represent the bank accounts of the user.
        /// </summary>
        [JsonProperty("bankAccounts")]
        public BankAccount[] BankAccounts { get; internal set; }
    }
}
