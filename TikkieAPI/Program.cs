﻿using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using TikkiePaymentRequestAPI;
using TikkiePaymentRequestAPI.Exceptions;

namespace TikkieAPI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new TikkieClient(new Configuration("NDIejJkCWtO46nOM34m7z5ExJ1djRbOe", "private_rsa.pem", true));

            try
            {
                var platforms = await client.GetPlatformsAsync();

                //var result = await client.CreatePlatformAsync(new TikkiePaymentRequestAPI.Models.PlatformRequest
                //{
                //    Name = "Test platform",
                //    Email = "client@platform.com",
                //    PhoneNumber = "0617386240",
                //    Usage = TikkiePaymentRequestAPI.Enums.PlatformUsage.PaymentRequestForMyself
                //});
                Console.WriteLine("Platforms:");
                Console.WriteLine(JsonConvert.SerializeObject(platforms, Formatting.Indented));

                //var user = await client.CreateUserAsync(platforms.First().PlatformToken, new TikkiePaymentRequestAPI.Models.UserRequest
                //{
                //    Name = "Chus Second",
                //    PhoneNumber = "0617386240",
                //    IBAN = "NL22INGB0689972458",
                //    BankAccountLabel = "Personal account"
                //});
                //Console.WriteLine("Created user:");
                //Console.WriteLine(JsonConvert.SerializeObject(user, Formatting.Indented));

                var users = await client.GetUsersAsync(platforms.First().PlatformToken);
                Console.WriteLine("Users:");
                Console.WriteLine(JsonConvert.SerializeObject(users, Formatting.Indented));
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
