﻿using Newtonsoft.Json;

namespace TikkiePaymentRequestAPI.Models
{
    public class ErrorResponse
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("traceId")]
        public string TraceId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }
    }

    public class ErrorResponses
    {
        [JsonProperty("errors")]
        public ErrorResponse[] ErrorResponseArray { get; set; }
    }
}
