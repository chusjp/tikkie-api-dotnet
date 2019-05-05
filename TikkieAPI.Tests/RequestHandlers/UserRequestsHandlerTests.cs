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
    public class UserRequestsHandlerTests
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
            Assert.Throws<ArgumentNullException>(() => new UserRequestsHandler(null));
        }

        [Test]
        public void Ctor_CanConstruct_ResultNotNull()
        {
            // Arrange + act
            var result = new UserRequestsHandler(Mock.Of<IAuthorizedRequestsHandler>());

            // Assert
            Assert.IsNotNull(result);
        }

        [TestCase(null)]
        [TestCase("")]
        public void GetUsersAsync_NullOrEmptyPlatformToken_ExpectedArgumentException(string platformToken)
        {
            // Arrange
            var sut = CreateSut();

            // Act + Assert
            Assert.ThrowsAsync<ArgumentException>(() => sut.GetUsersAsync(platformToken));
        }

        [Test]
        public async Task GetUsersAsync_ExpectedResult_VerifiesCall()
        {
            // Arrange
            var platformToken = "platformToken";
            var expectedResult = new[] { new UserResponse() };
            var urlSuffix = UrlProvider.UserUrlSuffix(platformToken);
            _mockAuthorizedRequestsHandler
                .Setup(m => m.GetOrExceptionAsync<UserResponse[]>(urlSuffix))
                .ReturnsAsync(expectedResult)
                .Verifiable();
            var sut = CreateSut();

            // Act
            var result = await sut.GetUsersAsync(platformToken);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult, result);
            _mockAuthorizedRequestsHandler
                .Verify(m => m.GetOrExceptionAsync<UserResponse[]>(urlSuffix), Times.Once);
        }

        [Test]
        public void CreateUserAsync_NullUserRequest_ExpectedArgumentNullException()
        {
            // Arrange
            var sut = CreateSut();

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => sut.CreateUserAsync(null));
        }

        [Test]
        public async Task CreateUserAsync_ExpectedResult_VerifiesCall()
        {
            // Arrange
            var userRequest = new UserRequest()
            {
                PlatformToken = "platformToken"
            };
            var expectedResult = new UserResponse();
            var urlSuffix = UrlProvider.UserUrlSuffix(userRequest.PlatformToken);
            _mockAuthorizedRequestsHandler
                .Setup(m => m.PostOrExceptionAsync<UserResponse>(urlSuffix, It.IsAny<StringContent>()))
                .ReturnsAsync(expectedResult)
                .Verifiable();
            var sut = CreateSut();

            // Act
            var result = await sut.CreateUserAsync(userRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult, result);
            _mockAuthorizedRequestsHandler
                .Verify(m => m.PostOrExceptionAsync<UserResponse>(urlSuffix, It.IsAny<StringContent>()), Times.Once);
        }

        private UserRequestsHandler CreateSut()
            => new UserRequestsHandler(_mockAuthorizedRequestsHandler.Object);
    }
}
