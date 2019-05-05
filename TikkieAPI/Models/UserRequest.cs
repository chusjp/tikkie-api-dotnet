using Newtonsoft.Json;

namespace TikkieAPI.Models
{
    public class UserRequest
    {
        /// <summary>
        /// Identifies to which platform the new user is enrolled.
        /// </summary>
        [JsonIgnore]
        public string PlatformToken { get; set; }

        /// <summary>
        /// Name of the new user. Length: 2-70 characters.
        /// Required.
        /// </summary> 
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The phone number of the new user. Length: 10 characters. Example: 0601234567. 
        /// Currently only Dutch phone numbers are supported.
        /// Required.
        /// </summary>
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// The IBAN number of the bank account of the new user. Only Dutch IBANs are supported. 
        /// The Tikkie API limits the number of users with the same IBAN account number, so if you use an IBAN that has been used too often, the user creation will fail.
        /// Required.
        /// </summary>
        [JsonProperty("iban")]
        public string IBAN { get; set; }

        /// <summary>
        /// A label to describe the bank account of the new user, e.g. personal account. Length: 2-70 characters.
        /// Required.
        /// </summary>
        [JsonProperty("bankAccountLabel")]
        public string BankAccountLabel { get; set; }
    }
}
