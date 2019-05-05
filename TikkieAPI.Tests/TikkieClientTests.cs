using System;
using System.IO;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TikkieAPI.Interfaces;
using TikkieAPI.Models;
using TikkieAPI.Utilities;

namespace TikkieAPI.Tests
{
    [TestFixture]
    public class TikkieClientTests
    {
        private const string ValidApiKey = "apiKey";
        private const string ValidPrivateKeyPath = "example_rsa.pem";

        [TestCase(null)]
        [TestCase("")]
        public void Ctor_NullOrEmptyApiKey_ExpectedArgumentException(string apiKey)
        {
            // Arrange + Act + Assert
            Assert.Throws<ArgumentException>(() => new TikkieClient(apiKey, "file.pem"));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("non/existing/location/file.pem")]
        public void Ctor_NonExistingPrivateKeyFile_ExpectedFileNotFoundException(string privateKeyPath)
        {
            // Arrange + Act + Assert
            Assert.Throws<FileNotFoundException>(() => new TikkieClient(ValidApiKey, privateKeyPath));
        }

        [Test]
        public void Ctor_PrivateKeyFileCannotBeRead_ExpectedInvalidDataException()
        {
            // Arrange + Act + Assert
            Assert.Throws<InvalidDataException>(() => new TikkieClient(ValidApiKey, "non_rsa.pem"));
        }

        [Test]
        public void Ctor_CanConstruct_ResultNotNull()
        {
            // Arrange + Act
            var result = new TikkieClient(ValidApiKey, ValidPrivateKeyPath, true);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Configuration);
            Assert.IsNotNull(result.AuthorizationTokenInfo);
        }

        [Test]
        public void InternalCtor_NullPlatformRequestsHandler_ExpectedArgumentNullException()
        {
            // Arrange + Act + Assert
            Assert.Throws<ArgumentNullException>(() => new TikkieClient((IPlatformRequestsHandler)null, null, null));
        }

        [Test]
        public void InternalCtor_NullUserRequestsHandler_ExpectedArgumentNullException()
        {
            // Arrange + Act + Assert
            Assert.Throws<ArgumentNullException>(() => new TikkieClient(Mock.Of<IPlatformRequestsHandler>(), null, null));
        }

        [Test]
        public void InternalCtor_NullPaymentRequestsHandler_ExpectedArgumentNullException()
        {
            // Arrange + Act + Assert
            Assert.Throws<ArgumentNullException>(() => new TikkieClient(Mock.Of<IPlatformRequestsHandler>(), Mock.Of<IUserRequestsHandler>(), null));
        }

        [Test]
        public void InternalCtor_CanConstruct_ResultNotNull()
        {
            // Arrange + Act
            var result = new TikkieClient(Mock.Of<IPlatformRequestsHandler>(), Mock.Of<IUserRequestsHandler>(), Mock.Of<IPaymentRequestsHandler>());

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Configuration_UseTestEnvironmentIsTrue_ExpectedConfigurationUrlParameters()
        {
            // Arrange + Act
            var sut = new TikkieClient(ValidApiKey, ValidPrivateKeyPath, useTestEnvironment: true);

            // Assert
            Assert.IsNotNull(sut.Configuration);
            Assert.AreEqual(UrlProvider.SandboxApiBaseUrl, sut.Configuration.ApiBaseUrl);
            Assert.AreEqual(UrlProvider.SandboxOAuthTokenUrl, sut.Configuration.OAuthTokenUrl);
        }

        [Test]
        public void Configuration_UseTestEnvironmentIsTrue_AndChangesToFalse_ExpectedConfigurationUrlParameters()
        {
            // Arrange
            var sut = new TikkieClient(ValidApiKey, ValidPrivateKeyPath, useTestEnvironment: true);

            // Act
            sut.Configuration.IsTestEnvironment = false;

            // Assert
            Assert.AreEqual(UrlProvider.ProductionApiBaseUrl, sut.Configuration.ApiBaseUrl);
            Assert.AreEqual(UrlProvider.ProductionOAuthTokenUrl, sut.Configuration.OAuthTokenUrl);
        }

        [Test]
        public async Task GetPlatformsAsync_ExpectedResponse_VerifiesCall()
        {
            // Arrange
            var expectedResult = new[] { new PlatformResponse(), new PlatformResponse() };
            var mockPlatformRequestsHandler = new Mock<IPlatformRequestsHandler>();
            mockPlatformRequestsHandler
                .Setup(m => m.GetPlatformsAsync())
                .ReturnsAsync(expectedResult)
                .Verifiable();

            var sut = new TikkieClient(mockPlatformRequestsHandler.Object, Mock.Of<IUserRequestsHandler>(), Mock.Of<IPaymentRequestsHandler>());

            // Act
            var result = await sut.GetPlatformsAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult, result);
            mockPlatformRequestsHandler
                .Verify(m => m.GetPlatformsAsync(), Times.Once);
        }

        [Test]
        public async Task CreatePlatformAsync_ExpectedResponse_VerifiesCall()
        {
            // Arrange
            var platformRequest = new PlatformRequest();
            var expectedResult = new PlatformResponse();
            var mockPlatformRequestsHandler = new Mock<IPlatformRequestsHandler>();
            mockPlatformRequestsHandler
                .Setup(m => m.CreatePlatformAsync(platformRequest))
                .ReturnsAsync(expectedResult)
                .Verifiable();

            var sut = new TikkieClient(mockPlatformRequestsHandler.Object, Mock.Of<IUserRequestsHandler>(), Mock.Of<IPaymentRequestsHandler>());

            // Act
            var result = await sut.CreatePlatformAsync(platformRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult, result);
            mockPlatformRequestsHandler
                .Verify(m => m.CreatePlatformAsync(platformRequest), Times.Once);
        }

        [Test]
        public async Task GetUsersAsync_ExpectedResponse_VerifiesCall()
        {
            // Arrange
            var platformToken = "platformToken";
            var expectedResult = new[] { new UserResponse(), new UserResponse() };
            var mockUserRequestsHandler = new Mock<IUserRequestsHandler>();
            mockUserRequestsHandler
                .Setup(m => m.GetUsersAsync(platformToken))
                .ReturnsAsync(expectedResult)
                .Verifiable();

            var sut = new TikkieClient(Mock.Of<IPlatformRequestsHandler>(), mockUserRequestsHandler.Object, Mock.Of<IPaymentRequestsHandler>());

            // Act
            var result = await sut.GetUsersAsync(platformToken);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult, result);
            mockUserRequestsHandler
                .Verify(m => m.GetUsersAsync(platformToken), Times.Once);
        }

        [Test]
        public async Task CreateUserAsync_ExpectedResponse_VerifiesCall()
        {
            // Arrange
            var userRequest = new UserRequest();
            var expectedResult =new UserResponse();
            var mockUserRequestsHandler = new Mock<IUserRequestsHandler>();
            mockUserRequestsHandler
                .Setup(m => m.CreateUserAsync(userRequest))
                .ReturnsAsync(expectedResult)
                .Verifiable();

            var sut = new TikkieClient(Mock.Of<IPlatformRequestsHandler>(), mockUserRequestsHandler.Object, Mock.Of<IPaymentRequestsHandler>());

            // Act
            var result = await sut.CreateUserAsync(userRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult, result);
            mockUserRequestsHandler
                .Verify(m => m.CreateUserAsync(userRequest), Times.Once);
        }

        [Test]
        public async Task CreatePaymentRequestAsync_ExpectedResponse_VerifiesCall()
        {
            // Arrange
            var paymentRequest = new PaymentRequest();
            var expectedResult = new PaymentResponse();
            var mockPaymentRequestsHandler = new Mock<IPaymentRequestsHandler>();
            mockPaymentRequestsHandler
                .Setup(m => m.CreatePaymentRequestAsync(paymentRequest))
                .ReturnsAsync(expectedResult)
                .Verifiable();

            var sut = new TikkieClient(Mock.Of<IPlatformRequestsHandler>(), Mock.Of<IUserRequestsHandler>(), mockPaymentRequestsHandler.Object);

            // Act
            var result = await sut.CreatePaymentRequestAsync(paymentRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult, result);
            mockPaymentRequestsHandler
                .Verify(m => m.CreatePaymentRequestAsync(paymentRequest), Times.Once);
        }

        [Test]
        public async Task GetUserPaymentRequestsAsync_ExpectedResponse_VerifiesCall()
        {
            // Arrange
            var userPaymentRequest = new UserPaymentRequest();
            var expectedResult = new UserPaymentResponse();
            var mockPaymentRequestsHandler = new Mock<IPaymentRequestsHandler>();
            mockPaymentRequestsHandler
                .Setup(m => m.GetUserPaymentRequestsAsync(userPaymentRequest))
                .ReturnsAsync(expectedResult)
                .Verifiable();

            var sut = new TikkieClient(Mock.Of<IPlatformRequestsHandler>(), Mock.Of<IUserRequestsHandler>(), mockPaymentRequestsHandler.Object);

            // Act
            var result = await sut.GetUserPaymentRequestsAsync(userPaymentRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult, result);
            mockPaymentRequestsHandler
                .Verify(m => m.GetUserPaymentRequestsAsync(userPaymentRequest), Times.Once);
        }

        [Test]
        public async Task GetPaymentRequestAsync_ExpectedResponse_VerifiesCall()
        {
            // Arrange
            var singlePaymentRequest = new SinglePaymentRequest();
            var expectedResult = new SinglePaymentRequestResponse();
            var mockPaymentRequestsHandler = new Mock<IPaymentRequestsHandler>();
            mockPaymentRequestsHandler
                .Setup(m => m.GetPaymentRequestAsync(singlePaymentRequest))
                .ReturnsAsync(expectedResult)
                .Verifiable();

            var sut = new TikkieClient(Mock.Of<IPlatformRequestsHandler>(), Mock.Of<IUserRequestsHandler>(), mockPaymentRequestsHandler.Object);

            // Act
            var result = await sut.GetPaymentRequestAsync(singlePaymentRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult, result);
            mockPaymentRequestsHandler
                .Verify(m => m.GetPaymentRequestAsync(singlePaymentRequest), Times.Once);
        }
    }
}
