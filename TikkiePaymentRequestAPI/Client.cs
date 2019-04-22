using System;
using TikkiePaymentRequestAPI.Models;

namespace TikkiePaymentRequestAPI
{
    public class Client
    {
        public Client(Configuration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public Configuration Configuration { get; set; }

        public PlatformResponse CreatePlatform(PlatformRequest request)
        {
            return new PlatformResponse();
        }
    }
}
