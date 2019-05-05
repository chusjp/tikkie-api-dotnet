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
    public class PlatformRequestsHandlerTests
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
            Assert.Throws<ArgumentNullException>(() => new PlatformRequestsHandler(null));
        }

        [Test]
        public void Ctor_CanConstruct_ResultNotNull()
        {
            // Arrange + act
            var result = new PlatformRequestsHandler(Mock.Of<IAuthorizedRequestsHandler>());

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetPlatformsAsync_ExpectedResult_VerifiesCall()
        {
            // Arrange
            var expectedResult = new[] { new PlatformResponse() };
            var expectedCall = UrlProvider.PlatformUrlSuffix;
            _mockAuthorizedRequestsHandler
                .Setup(m => m.GetOrExceptionAsync<PlatformResponse[]>(expectedCall))
                .ReturnsAsync(expectedResult)
                .Verifiable();
            var sut = CreateSut();

            // Act
            var result = await sut.GetPlatformsAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult, result);
            _mockAuthorizedRequestsHandler
                .Verify(m => m.GetOrExceptionAsync<PlatformResponse[]>(expectedCall), Times.Once);
        }

        [Test]
        public void CreatePlatformAsync_NullPlatformRequest_ExpectedArgumentNullException()
        {
            // Arrange
            var sut = CreateSut();

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => sut.CreatePlatformAsync(null));
        }

        [Test]
        public async Task CreatePlatformAsync_ExpectedResult_VerifiesCall()
        {
            // Arrange
            var platformRequest = new PlatformRequest();
            var expectedResult = new PlatformResponse();
            var urlSuffix = UrlProvider.PlatformUrlSuffix;
            _mockAuthorizedRequestsHandler
                .Setup(m => m.PostOrExceptionAsync<PlatformResponse>(urlSuffix, It.IsAny<StringContent>()))
                .ReturnsAsync(expectedResult)
                .Verifiable();
            var sut = CreateSut();

            // Act
            var result = await sut.CreatePlatformAsync(platformRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult, result);
            _mockAuthorizedRequestsHandler
                .Verify(m => m.PostOrExceptionAsync<PlatformResponse>(urlSuffix, It.IsAny<StringContent>()), Times.Once);
        }

        private PlatformRequestsHandler CreateSut()
            => new PlatformRequestsHandler(_mockAuthorizedRequestsHandler.Object);
    }
}
