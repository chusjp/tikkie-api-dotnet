using System;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TikkieAPI.Interfaces;
using TikkieAPI.Models;
using TikkieAPI.RequestsHandlers;
using TikkieAPI.Utilities;

namespace TikkieAPI.Tests.RequestHandlers
{
    [TestFixture]
    public class PaymentRequestsHandlerTests
    {
        private Mock<IAuthorizedRequestsHandler> _mockAuthorizedRequestsHandler;

        [SetUp]
        public void Initialize()
        {
            _mockAuthorizedRequestsHandler = new Mock<IAuthorizedRequestsHandler>();
        }

        [Test]
        public void Ctor_NullAuthorizedRequestsHandler_ExpectedArgumentNullException()
        {
            // Arrange + Act + Assert
            Assert.Throws<ArgumentNullException>(() => new PaymentRequestsHandler(null));
        }

        [Test]
        public void Ctor_CanConstruct_ResultNotNull()
        {
            // Arrange + act
            var result = new PaymentRequestsHandler(Mock.Of<IAuthorizedRequestsHandler>());

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void CreatePaymentRequestAsync_NullPaymentRequest_ExpectedArgumentNullException()
        {
            // Arrange
            var sut = CreateSut();

            // Act + Assert
           Assert.ThrowsAsync<ArgumentNullException>(() => sut.CreatePaymentRequestAsync(null));
        }

        [Test]
        public async Task CreatePaymentRequestAsync_ExpectedResult_VerifiesCall()
        {
            // Arrange
            var paymentRequest = new PaymentRequest()
            {
                PlatformToken = "PlatformToken",
                UserToken = "UserToken",
                BankAccountToken = "BankAccountToken",
                AmountInCents = 50,
                Currency = "EUR",
                Description = "Test payment",
                ExternalId = "123"
            };
            var expectedResult = new PaymentResponse();
            var urlSuffix = UrlProvider.PaymentCreationUrlSuffix(paymentRequest.PlatformToken, paymentRequest.UserToken, paymentRequest.BankAccountToken);
            _mockAuthorizedRequestsHandler
                .Setup(m => m.PostOrExceptionAsync<PaymentResponse>(urlSuffix, It.IsAny<StringContent>()))
                .ReturnsAsync(expectedResult)
                .Verifiable();
            var sut = CreateSut();

            // Act
            var result = await sut.CreatePaymentRequestAsync(paymentRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult, result);
            _mockAuthorizedRequestsHandler
                .Verify(m => m.PostOrExceptionAsync<PaymentResponse>(urlSuffix, It.IsAny<StringContent>()), Times.Once);
        }

        [Test]
        public void GetUserPaymentRequestsAsync_NullUserPaymentRequest_ExpectedArgumentNullException()
        {
            // Arrange
            var sut = CreateSut();

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => sut.GetUserPaymentRequestsAsync(null));
        }

        [Test]
        public async Task GetUserPaymentRequestsAsync_ExpectedResult_VerifiesCall_NoOptionalParameters()
        {
            // Arrange
            var userPaymentRequest = new UserPaymentRequest()
            {
                PlatformToken = "PlatformToken",
                UserToken = "UserToken",
                Limit = 100,
                Offset = 1
            };
            var expectedResult = new UserPaymentResponse();
            var expectedCall = $"{UrlProvider.GetUserPaymentsUrlSuffix(userPaymentRequest.PlatformToken, userPaymentRequest.UserToken)}?offset={userPaymentRequest.Offset}&limit={userPaymentRequest.Limit}";
            _mockAuthorizedRequestsHandler
                .Setup(m => m.GetOrExceptionAsync<UserPaymentResponse>(expectedCall))
                .ReturnsAsync(expectedResult)
                .Verifiable();
            var sut = CreateSut();

            // Act
            var result = await sut.GetUserPaymentRequestsAsync(userPaymentRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult, result);
            _mockAuthorizedRequestsHandler
                .Verify(m => m.GetOrExceptionAsync<UserPaymentResponse>(expectedCall), Times.Once);
        }

        [Test]
        public async Task GetUserPaymentRequestsAsync_ExpectedResult_VerifiesCall_WithFromDateParameter()
        {
            // Arrange
            var userPaymentRequest = new UserPaymentRequest()
            {
                PlatformToken = "PlatformToken",
                UserToken = "UserToken",
                Limit = 100,
                Offset = 1,
                FromDate = DateTime.Now
            };
            var expectedResult = new UserPaymentResponse();
            var expectedCall = $"{UrlProvider.GetUserPaymentsUrlSuffix(userPaymentRequest.PlatformToken, userPaymentRequest.UserToken)}?offset={userPaymentRequest.Offset}&limit={userPaymentRequest.Limit}&fromDate={userPaymentRequest.FromDateString}";
            _mockAuthorizedRequestsHandler
                .Setup(m => m.GetOrExceptionAsync<UserPaymentResponse>(expectedCall))
                .ReturnsAsync(expectedResult)
                .Verifiable();
            var sut = CreateSut();

            // Act
            var result = await sut.GetUserPaymentRequestsAsync(userPaymentRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult, result);
            _mockAuthorizedRequestsHandler
                .Verify(m => m.GetOrExceptionAsync<UserPaymentResponse>(expectedCall), Times.Once);
        }

        [Test]
        public async Task GetUserPaymentRequestsAsync_ExpectedResult_VerifiesCall_WithToDateParameter()
        {
            // Arrange
            var userPaymentRequest = new UserPaymentRequest()
            {
                PlatformToken = "PlatformToken",
                UserToken = "UserToken",
                Limit = 100,
                Offset = 1,
                ToDate = DateTime.Now
            };
            var expectedResult = new UserPaymentResponse();
            var expectedCall = $"{UrlProvider.GetUserPaymentsUrlSuffix(userPaymentRequest.PlatformToken, userPaymentRequest.UserToken)}?offset={userPaymentRequest.Offset}&limit={userPaymentRequest.Limit}&toDate={userPaymentRequest.ToDateString}";
            _mockAuthorizedRequestsHandler
                .Setup(m => m.GetOrExceptionAsync<UserPaymentResponse>(expectedCall))
                .ReturnsAsync(expectedResult)
                .Verifiable();
            var sut = CreateSut();

            // Act
            var result = await sut.GetUserPaymentRequestsAsync(userPaymentRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult, result);
            _mockAuthorizedRequestsHandler
                .Verify(m => m.GetOrExceptionAsync<UserPaymentResponse>(expectedCall), Times.Once);
        }

        [Test]
        public async Task GetUserPaymentRequestsAsync_ExpectedResult_VerifiesCall_WithOptionalParameters()
        {
            // Arrange
            var userPaymentRequest = new UserPaymentRequest()
            {
                PlatformToken = "PlatformToken",
                UserToken = "UserToken",
                Limit = 100,
                Offset = 1,
                FromDate = DateTime.Now.AddDays(-1),
                ToDate = DateTime.Now
            };
            var expectedResult = new UserPaymentResponse();
            var expectedCall = $"{UrlProvider.GetUserPaymentsUrlSuffix(userPaymentRequest.PlatformToken, userPaymentRequest.UserToken)}?offset={userPaymentRequest.Offset}&limit={userPaymentRequest.Limit}&fromDate={userPaymentRequest.FromDateString}&toDate={userPaymentRequest.ToDateString}";
            _mockAuthorizedRequestsHandler
                .Setup(m => m.GetOrExceptionAsync<UserPaymentResponse>(expectedCall))
                .ReturnsAsync(expectedResult)
                .Verifiable();
            var sut = CreateSut();

            // Act
            var result = await sut.GetUserPaymentRequestsAsync(userPaymentRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult, result);
            _mockAuthorizedRequestsHandler
                .Verify(m => m.GetOrExceptionAsync<UserPaymentResponse>(expectedCall), Times.Once);
        }

        [Test]
        public void GetPaymentRequestAsync_NullSinglePaymentRequest_ExpectedArgumentNullException()
        {
            // Arrange
            var sut = CreateSut();

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => sut.GetPaymentRequestAsync(null));
        }

        [Test]
        public async Task GetPaymentRequestAsync_ExpectedResult_VerifiesCall()
        {
            // Arrange
            var singlePaymentRequest = new SinglePaymentRequest()
            {
                PlatformToken = "PlatformToken",
                UserToken = "UserToken",
                PaymentRequestToken = "PaymentRequestToken"
            };
            var expectedResult = new SinglePaymentRequestResponse();
            var urlSuffix = UrlProvider.GetPaymentUrlSuffix(singlePaymentRequest.PlatformToken, singlePaymentRequest.UserToken, singlePaymentRequest.PaymentRequestToken);
            _mockAuthorizedRequestsHandler
                .Setup(m => m.GetOrExceptionAsync<SinglePaymentRequestResponse>(urlSuffix))
                .ReturnsAsync(expectedResult)
                .Verifiable();
            var sut = CreateSut();

            // Act
            var result = await sut.GetPaymentRequestAsync(singlePaymentRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult, result);
            _mockAuthorizedRequestsHandler
                .Verify(m => m.GetOrExceptionAsync<SinglePaymentRequestResponse>(urlSuffix), Times.Once);
        }

        private PaymentRequestsHandler CreateSut()
            => new PaymentRequestsHandler(_mockAuthorizedRequestsHandler.Object);
    }
}
