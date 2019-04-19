using System;
using System.Threading.Tasks;
using TikkiePaymentRequestAPI;
using TikkiePaymentRequestAPI.Exceptions;

namespace TikkieAPI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var authenticate = new Authentication(new Configuration("NDIejJkCWtO46nOM34m7z5ExJ1djRbOe", "private_rsa.pem", true));

            try
            {
                var result = await authenticate.AuthenticateAsync();

                Console.WriteLine($"Token: {result.AccessToken}");
                Console.WriteLine($"Expires in: {result.ExpiresInSeconds}");
                Console.WriteLine($"Scope: {result.Scope}");
                Console.WriteLine($"Token Type: {result.TokenType}");
            }
            catch (TikkieErrorResponseException ex)
            {
                var errorResponses = ex.ErrorResponses;
                if (errorResponses != null)
                {
                    for(int i = 0; i < errorResponses.Length; i++)
                    {
                        Console.WriteLine($"Error number {i+1}");
                        var error = errorResponses[i];
                        Console.WriteLine($"Code: {error.Code}");
                        Console.WriteLine($"Message: {error.Message}");
                        Console.WriteLine($"Reference: {error.Reference}");
                        Console.WriteLine($"Trace Id: {error.TraceId}");
                        Console.WriteLine($"Status: {error.Status}");
                        Console.WriteLine($"Category: {error.Category}");
                    }
                }
            }
            
        }
    }
}
