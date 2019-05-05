using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TikkieAPI.Exceptions;
using TikkieAPI.Interfaces;
using TikkieAPI.Models;
using TikkieAPI.Utilities;

namespace TikkieAPI.RequestsHandlers
{
    /// <summary>
    /// Handles the Payment related requests to the Tikkie API authenticating automatically if the current token has expired.
    /// Payment requests are described in https://developer.abnamro.com/api/tikkie-v1/technical-details
    /// </summary>
    internal class PaymentRequestsHandler : IPaymentRequestsHandler
    {
        private readonly IAuthorizedRequestsHandler _authorizedRequestsHandler;

        /// <summary>
        /// Initializes a new instance of the class with an <see cref="IAuthorizedRequestsHandler"/> as parameter.
        /// </summary>
        /// <param name="authorizedRequestsHandler">The authorized request handler</param>
        /// <exception cref="ArgumentNullException">If the parameter is null</exception>
        public PaymentRequestsHandler(IAuthorizedRequestsHandler authorizedRequestsHandler)
        {
            _authorizedRequestsHandler = authorizedRequestsHandler ?? throw new ArgumentNullException(nameof(authorizedRequestsHandler));
        }

        /// <summary>
        /// Creates a new Payment Request for an existing user using HTTP POST. 
        /// </summary>
        /// <param name="request">The PaymentRequest object</param>
        /// <returns>The PaymentResponse object with information of the created Payment Request</returns>
        /// <exception cref="ArgumentNullException">If the PaymentRequest object is null</exception>
        /// <exception cref="TikkieErrorResponseException">If the Tikkie API returns an error response.</exception>
        public async Task<PaymentResponse> CreatePaymentRequestAsync(PaymentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var jsonRequest = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            return await _authorizedRequestsHandler
                .PostOrExceptionAsync<PaymentResponse>(UrlProvider.PaymentCreationUrlSuffix(request.PlatformToken, request.UserToken, request.BankAccountToken), content);
        }

        /// <summary>
        /// Gets all the Payment Requests from an existing user using HTTP GET.
        /// </summary>
        /// <param name="request">The UserPaymentRequest object</param>
        /// <returns>The UserPaymentResponse object</returns>
        /// <exception cref="ArgumentNullException">If the UserPaymentRequest object is null</exception>
        /// <exception cref="TikkieErrorResponseException">If the Tikkie API returns an error response.</exception>
        public async Task<UserPaymentResponse> GetUserPaymentRequestsAsync(UserPaymentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var queryString = GetUserPaymentRequestQueryString(request);

            return await _authorizedRequestsHandler
                .GetOrExceptionAsync<UserPaymentResponse>($"{UrlProvider.GetUserPaymentsUrlSuffix(request.PlatformToken, request.UserToken)}{queryString}");
        }

        /// <summary>
        /// Gets a Single Payment Request using HTTP GET.
        /// </summary>
        /// <param name="request">The SinglePaymentRequest object</param>
        /// <returns>The SinglePaymentRequestResponse object</returns>
        /// <exception cref="ArgumentNullException">If the SinglePaymentRequest object is null</exception>
        /// <exception cref="TikkieErrorResponseException">If the Tikkie API returns an error response.</exception>
        public async Task<SinglePaymentRequestResponse> GetPaymentRequestAsync(SinglePaymentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return await _authorizedRequestsHandler
                .GetOrExceptionAsync<SinglePaymentRequestResponse>(UrlProvider.GetPaymentUrlSuffix(request.PlatformToken, request.UserToken, request.PaymentRequestToken));
        }

        /// <summary>
        /// Gets query string from the <see cref="UserPaymentRequest"/>.
        /// </summary>
        /// <param name="request">The UserPaymentRequest object</param>
        /// <returns>A string with a combination of mandatory and optional parameters.</returns>
        private string GetUserPaymentRequestQueryString(UserPaymentRequest request)
        {
            var optionalQueryStringBuilder = new StringBuilder();
            AppendParameterIfNotNullOrEmpty(optionalQueryStringBuilder, "fromDate", request.FromDateString);
            AppendParameterIfNotNullOrEmpty(optionalQueryStringBuilder, "toDate", request.ToDateString);

            return $"?offset={request.Offset}&limit={request.Limit}{optionalQueryStringBuilder.ToString()}";
        }

        /// <summary>
        /// Appends the query string value if it is not null or empty.
        /// </summary>
        /// <param name="stringBuilder">The string builder</param>
        /// <param name="variableName">The name of the variable that is going to be appended.</param>
        /// <param name="stringDate">The date as string.</param>
        private void AppendParameterIfNotNullOrEmpty(StringBuilder stringBuilder, string variableName, string stringDate)
        {
            if (!string.IsNullOrEmpty(stringDate))
            {
                stringBuilder.Append($"&{variableName}={stringDate}");
            }
        }
    }
}
