using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using RichardSzalay.MockHttp;
using TikkieAPI.Exceptions;
using TikkieAPI.Interfaces;
using TikkieAPI.Models;
using TikkieAPI.RequestsHandlers;

namespace TikkieAPI.Tests.RequestHandlers
{
    [TestFixture]
    public class AuthorizedRequestsHandlerTests
    {
        [Test]
        public void Ctor_NullITikkieConfiguration_ExpectedArgumentNullException()
        {
            // Arrange + Act + Assert
            Assert.Throws<ArgumentNullException>(() => new AuthorizedRequestsHandler(null));
        }

        [Test]
        public void Ctor_CanConstruct_ResultNotNull()
        {
            // Arrange + act
            var result = new AuthorizedRequestsHandler(Mock.Of<ITikkieConfiguration>());

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void InternalCtor_NullParameters_ExpectedArgumentNullException()
        {
            // Arrange + Act + Assert
            Assert.Throws<ArgumentNullException>(() => new AuthorizedRequestsHandler(null, null, null));
        }

        [Test]
        public void InternalCtor_NullAuthenticationRequestsHandler_ExpectedArgumentNullException()
        {
            // Arrange + Act + Assert
            Assert.Throws<ArgumentNullException>(() => new AuthorizedRequestsHandler(Mock.Of<ITikkieConfiguration>(), null, null));
        }

        [Test]
        public void InternalCtor_NullHttpClientFactory_ExpectedArgumentNullException()
        {
            // Arrange + Act + Assert
            Assert.Throws<ArgumentNullException>(() => new AuthorizedRequestsHandler(Mock.Of<ITikkieConfiguration>(), Mock.Of<IAuthenticationRequestsHandler>(), null));
        }

        [Test]
        public async Task GetOrExceptionAsync_OkResponse_CallsAuthentication_AddsHeaders_GetsExpectedResponseValues()
        {
            // Arrange
            var requestUrlSuffix = "/test/get";
            var expectedResponse = new TestResponse
            {
                OneProperty = 12,
                AnotherProperty = "Another property value"
            };

            // Mock Tikkie Configuration
            var mockTikkieConfiguration = new Mock<ITikkieConfiguration>();
            mockTikkieConfiguration
                .Setup(m => m.ApiBaseUrl)
                .Returns("https://tikkie.unittests");
            mockTikkieConfiguration
                .Setup(m => m.ApiKey)
                .Returns("AnAPIKey");

            // Mock Authentication Requests Handler
            var mockAuthenticationRequestsHandler = new Mock<IAuthenticationRequestsHandler>();
            mockAuthenticationRequestsHandler
                .Setup(m => m.AuthorizationTokenInfo)
                .Returns(new AuthorizationToken()
                {
                    TokenType = "Bearer",
                    AccessToken = "tHeToKen"
                });

            // Mock Http Client
            var mockHttp = new MockHttpMessageHandler();
            mockHttp
                .When($"{mockTikkieConfiguration.Object.ApiBaseUrl}{requestUrlSuffix}")
                .Respond(HttpStatusCode.OK, "application/json", JsonConvert.SerializeObject(expectedResponse));
            var httpClient = mockHttp.ToHttpClient();

            var sut = CreateSut(mockTikkieConfiguration.Object, mockAuthenticationRequestsHandler.Object, () => httpClient);

            // Act
            var result = await sut.GetOrExceptionAsync<TestResponse>(requestUrlSuffix);

            // Assert
            Assert.AreEqual(expectedResponse.OneProperty, result.OneProperty);
            Assert.AreEqual(expectedResponse.AnotherProperty, result.AnotherProperty);
            mockAuthenticationRequestsHandler.Verify(m => m.AuthenticateIfTokenExpiredAsync(), Times.Once);
            Assert.IsTrue(httpClient.DefaultRequestHeaders.Contains("Authorization"));
            Assert.IsTrue(httpClient.DefaultRequestHeaders.Contains("API-Key"));
        }

        [Test]
        public void GetOrExceptionAsync_ErrorResponse_CallsAuthentication_AddsHeaders_TikkieErrorResponseExceptionExpected()
        {
            // Arrange
            var requestUrlSuffix = "/test/get";

            // Mock Tikkie Configuration
            var mockTikkieConfiguration = new Mock<ITikkieConfiguration>();
            mockTikkieConfiguration
                .Setup(m => m.ApiBaseUrl)
                .Returns("https://tikkie.unittests");
            mockTikkieConfiguration
                .Setup(m => m.ApiKey)
                .Returns("AnAPIKey");

            // Mock Authentication Requests Handler
            var mockAuthenticationRequestsHandler = new Mock<IAuthenticationRequestsHandler>();
            mockAuthenticationRequestsHandler
                .Setup(m => m.AuthorizationTokenInfo)
                .Returns(new AuthorizationToken()
                {
                    TokenType = "Bearer",
                    AccessToken = "tHeToKen"
                });

            // Mock Http Client
            var errorResponse = new ErrorResponses()
            {
                ErrorResponseArray = new[] { new ErrorResponse() }
            };
            var mockHttp = new MockHttpMessageHandler();
            mockHttp
                .When($"{mockTikkieConfiguration.Object.ApiBaseUrl}{requestUrlSuffix}")
                .Respond(HttpStatusCode.BadRequest, "application/json", JsonConvert.SerializeObject(errorResponse));
            var httpClient = mockHttp.ToHttpClient();

            var sut = CreateSut(mockTikkieConfiguration.Object, mockAuthenticationRequestsHandler.Object, () => httpClient);

            // Act + Assert
            Assert.ThrowsAsync<TikkieErrorResponseException>(async () => await sut.GetOrExceptionAsync<TestResponse>(requestUrlSuffix));
            mockAuthenticationRequestsHandler.Verify(m => m.AuthenticateIfTokenExpiredAsync(), Times.Once);
            Assert.IsTrue(httpClient.DefaultRequestHeaders.Contains("Authorization"));
            Assert.IsTrue(httpClient.DefaultRequestHeaders.Contains("API-Key"));
        }

        [Test]
        public async Task PostOrExceptionAsync_OkResponse_CallsAuthentication_AddsHeaders_GetsExpectedResponseValues()
        {
            // Arrange
            var requestUrlSuffix = "/test/post";
            var expectedResponse = new TestResponse
            {
                OneProperty = 12,
                AnotherProperty = "Another property value"
            };
            var jsonRequest = JsonConvert.SerializeObject(new TestRequest());
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            // Mock Tikkie Configuration
            var mockTikkieConfiguration = new Mock<ITikkieConfiguration>();
            mockTikkieConfiguration
                .Setup(m => m.ApiBaseUrl)
                .Returns("https://tikkie.unittests");
            mockTikkieConfiguration
                .Setup(m => m.ApiKey)
                .Returns("AnAPIKey");

            // Mock Authentication Requests Handler
            var mockAuthenticationRequestsHandler = new Mock<IAuthenticationRequestsHandler>();
            mockAuthenticationRequestsHandler
                .Setup(m => m.AuthorizationTokenInfo)
                .Returns(new AuthorizationToken()
                {
                    TokenType = "Bearer",
                    AccessToken = "tHeToKen"
                });

            // Mock Http Client
            var mockHttp = new MockHttpMessageHandler();
            mockHttp
                .When($"{mockTikkieConfiguration.Object.ApiBaseUrl}{requestUrlSuffix}")
                .Respond(HttpStatusCode.OK, "application/json", JsonConvert.SerializeObject(expectedResponse));
            var httpClient = mockHttp.ToHttpClient();

            var sut = CreateSut(mockTikkieConfiguration.Object, mockAuthenticationRequestsHandler.Object, () => httpClient);

            // Act
            var result = await sut.PostOrExceptionAsync<TestResponse>(requestUrlSuffix, content);

            // Assert
            Assert.AreEqual(expectedResponse.OneProperty, result.OneProperty);
            Assert.AreEqual(expectedResponse.AnotherProperty, result.AnotherProperty);
            mockAuthenticationRequestsHandler.Verify(m => m.AuthenticateIfTokenExpiredAsync(), Times.Once);
            Assert.IsTrue(httpClient.DefaultRequestHeaders.Contains("Authorization"));
            Assert.IsTrue(httpClient.DefaultRequestHeaders.Contains("API-Key"));
        }

        [Test]
        public void PostOrExceptionAsync_ErrorResponse_CallsAuthentication_AddsHeaders_TikkieErrorResponseExceptionExpected()
        {
            // Arrange
            var requestUrlSuffix = "/test/post";
            var jsonRequest = JsonConvert.SerializeObject(new TestRequest());
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            // Mock Tikkie Configuration
            var mockTikkieConfiguration = new Mock<ITikkieConfiguration>();
            mockTikkieConfiguration
                .Setup(m => m.ApiBaseUrl)
                .Returns("https://tikkie.unittests");
            mockTikkieConfiguration
                .Setup(m => m.ApiKey)
                .Returns("AnAPIKey");

            // Mock Authentication Requests Handler
            var mockAuthenticationRequestsHandler = new Mock<IAuthenticationRequestsHandler>();
            mockAuthenticationRequestsHandler
                .Setup(m => m.AuthorizationTokenInfo)
                .Returns(new AuthorizationToken()
                {
                    TokenType = "Bearer",
                    AccessToken = "tHeToKen"
                });

            // Mock Http Client
            var errorResponse = new ErrorResponses()
            {
                ErrorResponseArray = new[] { new ErrorResponse() }
            };
            var mockHttp = new MockHttpMessageHandler();
            mockHttp
                .When($"{mockTikkieConfiguration.Object.ApiBaseUrl}{requestUrlSuffix}")
                .Respond(HttpStatusCode.BadRequest, "application/json", JsonConvert.SerializeObject(errorResponse));
            var httpClient = mockHttp.ToHttpClient();

            var sut = CreateSut(mockTikkieConfiguration.Object, mockAuthenticationRequestsHandler.Object, () => httpClient);

            // Act + Assert
            Assert.ThrowsAsync<TikkieErrorResponseException>(async () => await sut.PostOrExceptionAsync<TestResponse>(requestUrlSuffix, content));
            mockAuthenticationRequestsHandler.Verify(m => m.AuthenticateIfTokenExpiredAsync(), Times.Once);
            Assert.IsTrue(httpClient.DefaultRequestHeaders.Contains("Authorization"));
            Assert.IsTrue(httpClient.DefaultRequestHeaders.Contains("API-Key"));
        }

        private AuthorizedRequestsHandler CreateSut(ITikkieConfiguration configuration, IAuthenticationRequestsHandler authenticationRequestsHandler, Func<HttpClient> httpClientFactory)
            => new AuthorizedRequestsHandler(configuration, authenticationRequestsHandler, httpClientFactory);

        private class TestRequest
        {
            public int Id { get; set; }
        }

        private class TestResponse
        {
            public int OneProperty { get; set; }
            public string AnotherProperty { get; set; }
        }
    }
}
