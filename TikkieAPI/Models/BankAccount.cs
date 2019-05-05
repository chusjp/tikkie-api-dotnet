using Newtonsoft.Json;

namespace TikkieAPI.Models
{
    public class BankAccount
    {
        /// <summary>
        /// The bank account token for the bank account
        /// </summary>
        [JsonProperty("bankAccountToken")]
        public string Token { get; internal set; }

        /// <summary>
        /// The IBAN account number of the bank account
        /// </summary>
        [JsonProperty("iban")]
        public string IBAN { get; internal set; }

        /// <summary>
        /// The label of the bank account
        /// </summary>
        [JsonProperty("bankAccountLabel")]
        public string Label { get; internal set; }
    }
}
