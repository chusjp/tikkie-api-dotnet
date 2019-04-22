using Newtonsoft.Json;
using TikkiePaymentRequestAPI.Enums;

namespace TikkiePaymentRequestAPI.Models
{
    public class UserResponse
    {
        /// <summary>
        /// The token that identifies the new user
        /// </summary>
        [JsonProperty("userToken")]
        public string UserToken { get; set; }

        /// <summary>
        /// The name of the new user.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The current status of the new user as string.
        /// </summary>
        [JsonProperty("status")]
        public string StatusString { get; set; }

        /// <summary>
        /// The current status of the new user as string.
        /// </summary>
        [JsonIgnore]
        public Status Status => StatusString.MapToStatusEnum();

        /// <summary>
        /// An array holding bank account objects, that represent the bank accounts of the user.
        /// </summary>
        [JsonProperty("bankAccounts")]
        public BankAccount[] BankAccounts { get; set; }
    }

    public class BankAccount
    {
        /// <summary>
        /// The bank account token for the bank account
        /// </summary>
        [JsonProperty("bankAccountToken")]
        public string Token { get; set; }

        /// <summary>
        /// The IBAN account number of the bank account
        /// </summary>
        [JsonProperty("iban")]
        public string IBAN { get; set; }

        /// <summary>
        /// The label of the bank account
        /// </summary>
        [JsonProperty("bankAccountLabel")]
        public string Label { get; set; }
    }
}
