using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TikkieAPI.Exceptions;
using TikkieAPI.Models;

namespace TikkieAPI.Utilities
{
    /// <summary>
    /// Helper extension methods for parsing Json responses.
    /// </summary>
    internal static class JsonResponseParser
    {
        /// <summary>
        /// Gets the content object or throws an exception in case the response is not successful.
        /// </summary>
        /// <typeparam name="TResponse">The response object type. It should be deserializable as Json</typeparam>
        /// <param name="response">The HttpResponseMessage object.</param>
        /// <returns>The response object or exception for non successful responses.</returns>
        /// <exception cref="TikkieErrorResponseException">If the response message has a non-successful status code.</exception>
        public static async Task<TResponse> GetContentObjectOrExceptionAsync<TResponse>(this HttpResponseMessage response)
        {
            var contentStream = await response.Content.ReadAsStreamAsync();
            using (var streamReader = new StreamReader(contentStream))
            using (var jsonTextReader = new JsonTextReader(streamReader))
            {
                var serializer = new JsonSerializer();
                if (!response.IsSuccessStatusCode)
                {
                    var errorResponses = serializer.Deserialize<ErrorResponses>(jsonTextReader);
                    throw new TikkieErrorResponseException($"Server status code: {response.StatusCode}", errorResponses.ErrorResponseArray);
                }
                
                return serializer.Deserialize<TResponse>(jsonTextReader);
            }
        }
    }
}
