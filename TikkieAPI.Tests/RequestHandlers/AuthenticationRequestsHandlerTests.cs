using System;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using RichardSzalay.MockHttp;
using TikkieAPI.Exceptions;
using TikkieAPI.Interfaces;
using TikkieAPI.Models;
using TikkieAPI.RequestsHandlers;
using TikkieAPI.Utilities;

namespace TikkieAPI.Tests.RequestHandlers
{
    [TestFixture]
    public class AuthenticationRequestsHandlerTests
    {
        [Test]
        public void Ctor_NullTikkieConfiguration_ExpectedArgumentNullException()
        {
            // Arrange + Act + Assert
            Assert.Throws<ArgumentNullException>(() => new AuthenticationRequestsHandler(null));
        }

        [Test]
        public void Ctor_CanConstruct_ResultNotNull()
        {
            // Arrange + act
            var result = new AuthenticationRequestsHandler(Mock.Of<ITikkieConfiguration>());

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void InternalCtor_NullParameters_ExpectedArgumentNullException()
        {
            // Arrange + Act + Assert
            Assert.Throws<ArgumentNullException>(() => new AuthenticationRequestsHandler(null, null, null));
        }

        [Test]
        public void InternalCtor_NullHttpClientFactory_ExpectedArgumentNullException()
        {
            // Arrange + Act + Assert
            Assert.Throws<ArgumentNullException>(() => new AuthenticationRequestsHandler(Mock.Of<ITikkieConfiguration>(), null, new AuthorizationToken()));
        }

        [Test]
        public void InternalCtor_NullAuthorizationToken_ExpectedArgumentNullException()
        {
            // Arrange + Act + Assert
            Assert.Throws<ArgumentNullException>(() => new AuthenticationRequestsHandler(Mock.Of<ITikkieConfiguration>(), () => Mock.Of<HttpClient>(), null));
        }

        [Test]
        public void InternalCtor_CanConstruct_ResultNotNull()
        {
            // Arrange + act
            var result = new AuthenticationRequestsHandler(Mock.Of<ITikkieConfiguration>(), () => Mock.Of<HttpClient>(), new AuthorizationToken());

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task AuthenticateIfTokenExpiredAsync_TokenHasNotExpired_AuthorizationTokenDoesNotVary()
        {
            // Arrange
            var initialAccessToken = "1234";
            var initialTokenExpirationDate = DateTime.Now.AddMinutes(1);
            var initialScope = "Tikkie";
            var initialTokenType = "Bearer";
            var authorizationToken = new AuthorizationToken()
            {
                AccessToken = initialAccessToken,
                TokenExpirationDate = initialTokenExpirationDate,
                Scope = initialScope,
                TokenType = initialTokenType
            };
            var sut = CreateSut(Mock.Of<ITikkieConfiguration>(), () => Mock.Of<HttpClient>(), authorizationToken);

            // Act
            await sut.AuthenticateIfTokenExpiredAsync();

            // Assert
            Assert.AreEqual(initialAccessToken, authorizationToken.AccessToken);
            Assert.AreEqual(initialTokenExpirationDate, authorizationToken.TokenExpirationDate);
            Assert.AreEqual(initialScope, authorizationToken.Scope);
            Assert.AreEqual(initialTokenType, authorizationToken.TokenType);
        }

        [Test]
        public async Task AuthenticateIfTokenExpiredAsync_TokenHasExpired_ReturnsExpectedAuthorizationToken()
        {
            // Arrange
            // Mocks Tikkie Configuration
            var mockTikkieConfiguration = new Mock<ITikkieConfiguration>();
            mockTikkieConfiguration
                .Setup(m => m.RSAKey)
                .Returns(new RSACryptoServiceProvider());
            mockTikkieConfiguration
                .Setup(m => m.ApiBaseUrl)
                .Returns("https://tikkie.unittests");

            // Mocks HttpClient
            var expectedAuthenticationResponse = new AuthenticationResponse
            {
                AccessToken = "2334",
                ExpiresInSeconds = 60,
                Scope = "tikkie",
                TokenType = "Bearer"
            };

            var mockHttp = new MockHttpMessageHandler();
            mockHttp
                .When($"{mockTikkieConfiguration.Object.ApiBaseUrl}{UrlProvider.AuthenticationUrlSuffix}")
                .Respond(HttpStatusCode.OK, "application/json", JsonConvert.SerializeObject(expectedAuthenticationResponse));
            var httpClient = mockHttp.ToHttpClient();

            // Sets up authorization token
            var authorizationToken = new AuthorizationToken()
            {
                AccessToken = "1234",
                TokenExpirationDate = DateTime.Now.AddMinutes(-1),
                Scope = "Tikki",
                TokenType = "Something"
            };
            var sut = CreateSut(mockTikkieConfiguration.Object, () => httpClient, authorizationToken);

            // Act
            await sut.AuthenticateIfTokenExpiredAsync();

            // Assert
            Assert.AreEqual(expectedAuthenticationResponse.AccessToken, authorizationToken.AccessToken);
            Assert.GreaterOrEqual(DateTime.Now.AddSeconds(expectedAuthenticationResponse.ExpiresInSeconds), authorizationToken.TokenExpirationDate);
            Assert.AreEqual(expectedAuthenticationResponse.Scope, authorizationToken.Scope);
            Assert.AreEqual(expectedAuthenticationResponse.TokenType, authorizationToken.TokenType);
            Assert.IsTrue(httpClient.DefaultRequestHeaders.Contains("API-Key"));
        }

        [Test]
        public void AuthenticateIfTokenExpiredAsync_TokenHasExpired_ErrorResponse_TikkieErrorResponseExceptionExpected()
        {
            // Arrange
            // Mocks Tikkie Configuration
            var mockTikkieConfiguration = new Mock<ITikkieConfiguration>();
            mockTikkieConfiguration
                .Setup(m => m.RSAKey)
                .Returns(new RSACryptoServiceProvider());
            mockTikkieConfiguration
                .Setup(m => m.ApiBaseUrl)
                .Returns("https://tikkie.unittests");

            // Mocks HttpClient
            var errorResponse = new ErrorResponses()
            {
                ErrorResponseArray = new[] { new ErrorResponse() }
            };
            var mockHttp = new MockHttpMessageHandler();
            mockHttp
                .When($"{mockTikkieConfiguration.Object.ApiBaseUrl}{UrlProvider.AuthenticationUrlSuffix}")
                .Respond(HttpStatusCode.BadRequest, "application/json", JsonConvert.SerializeObject(errorResponse));

            // Sets up authorization token
            var sut = CreateSut(mockTikkieConfiguration.Object, () => mockHttp.ToHttpClient(), new AuthorizationToken());

            // Act + Assert
            Assert.ThrowsAsync<TikkieErrorResponseException>(async () => await sut.AuthenticateIfTokenExpiredAsync());
        }

        private AuthenticationRequestsHandler CreateSut(ITikkieConfiguration configuration, Func<HttpClient> httpClientFactory, AuthorizationToken authorizationToken)
            => new AuthenticationRequestsHandler(configuration, httpClientFactory, authorizationToken);
    }
}
