using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TikkieAPI.Constants;
using TikkieAPI.Models;

namespace TikkieAPI.RequestsHandlers
{
    internal class PaymentRequestsHandler
    {
        private AuthorizedRequestsHandler _authorizedRequestsHandler;

        public PaymentRequestsHandler(AuthorizedRequestsHandler authorizedRequestsHandler)
        {
            _authorizedRequestsHandler = authorizedRequestsHandler ?? throw new ArgumentNullException(nameof(authorizedRequestsHandler));
        }

        public async Task<PaymentResponse> CreatePaymentRequestAsync(PaymentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var jsonRequest = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            return await _authorizedRequestsHandler
                .PostOrExceptionAsync<PaymentResponse>(Urls.PaymentCreationUrlSuffix(request.PlatformToken, request.UserToken, request.BankAccountToken), content);
        }

        public async Task<UserPaymentResponse> GetUserPaymentRequestsAsync(UserPaymentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var queryString = GetUserPaymentRequestQueryString(request);

            return await _authorizedRequestsHandler
                .GetOrExceptionAsync<UserPaymentResponse>($"{Urls.GetUserPaymentsUrlSuffix(request.PlatformToken, request.UserToken)}{queryString}");
        }

        public async Task<SinglePaymentRequestResponse> GetPaymentRequestAsync(SinglePaymentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return await _authorizedRequestsHandler
                .GetOrExceptionAsync<SinglePaymentRequestResponse>(Urls.GetPaymentUrlSuffix(request.PlatformToken, request.UserToken, request.PaymentRequestToken));
        }

        private string GetUserPaymentRequestQueryString(UserPaymentRequest request)
        {
            var optionalQueryStringBuilder = new StringBuilder();
            AppendParameterIfNotNull(optionalQueryStringBuilder, "fromDate", request.FromDate);
            AppendParameterIfNotNull(optionalQueryStringBuilder, "toDate", request.ToDate);

            return $"?offset={request.Offset}&limit={request.Limit}{optionalQueryStringBuilder.ToString()}";
        }

        private void AppendParameterIfNotNull(StringBuilder stringBuilder, string variableName, DateTime? nullableDate)
        {
            if (nullableDate.HasValue)
            {
                stringBuilder.Append($"&{variableName}={nullableDate.Value}");
            }
        }
    }
}
