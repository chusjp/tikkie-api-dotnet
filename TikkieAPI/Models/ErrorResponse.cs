using System;
using Newtonsoft.Json;

namespace TikkieAPI.Models
{
    public class ErrorResponse
    {
        /// <summary>
        /// Error codes reference: https://developer.abnamro.com/get-started#error-codes
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; internal set; }

        [JsonProperty("message")]
        public string Message { get; internal set; }

        [JsonProperty("reference")]
        public string Reference { get; internal set; }

        [JsonProperty("traceId")]
        public Guid TraceId { get; internal set; }

        [JsonProperty("status")]
        public string Status { get; internal set; }

        [JsonProperty("category")]
        public string Category { get; internal set; }
    }

    public class ErrorResponses
    {
        [JsonProperty("errors")]
        public ErrorResponse[] ErrorResponseArray { get; internal set; }
    }
}
