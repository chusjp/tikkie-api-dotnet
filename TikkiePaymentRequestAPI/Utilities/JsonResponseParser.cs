using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using TikkiePaymentRequestAPI.Exceptions;
using TikkiePaymentRequestAPI.Models;

namespace TikkiePaymentRequestAPI.Utilities
{
    internal static class JsonResponseParser
    {
        public static async Task<TResponse> GetContentObjectOrExceptionAsync<TResponse>(this HttpResponseMessage response)
        {
            var contentString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var errorResponses = JsonConvert.DeserializeObject<ErrorResponses>(contentString);
                throw new TikkieErrorResponseException($"Server status code: {response.StatusCode}", errorResponses.ErrorResponseArray);
            }

            return JsonConvert.DeserializeObject<TResponse>(contentString);
        }
    }
}
