using NUnit.Framework;
using System;
using TikkieAPI.Enums;

namespace TikkieAPI.Tests.Enums
{
    [TestFixture]
    public class EnumsMapperTests
    {
        [Test]
        public void MapToString_FromPlatformUsageEnum_NotRecognized_ArgumentExceptionExpected()
        {
            // Arrange
            Enum unknownPlatformUsageEnum = UriComponents.Host;

            // Act + Assert
            Assert.Throws<ArgumentException>(() => ((PlatformUsage)unknownPlatformUsageEnum).MapToString());
        }

        [TestCase(PlatformUsage.PaymentRequestForMyself, "PAYMENT_REQUEST_FOR_MYSELF")]
        [TestCase(PlatformUsage.PaymentRequestForOthers, "PAYMENT_REQUEST_FOR_OTHERS")]
        public void MapToString_FromPlatformUsageEnum_ExpectedResult(PlatformUsage input, string expectedResult)
        {
            // Arrange + Act
            var result = input.MapToString();

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void MapToPlatformUsageEnum_FromString_NotRecognized_ArgumentExceptionExpected()
        {
            // Arrange
            string unknownPlatformUsage = "UNKNOWN";

            // Act + Assert
            Assert.Throws<ArgumentException>(() => unknownPlatformUsage.MapToPlatformUsageEnum());
        }

        [TestCase("PAYMENT_REQUEST_FOR_MYSELF", PlatformUsage.PaymentRequestForMyself)]
        [TestCase("PAYMENT_REQUEST_FOR_OTHERS", PlatformUsage.PaymentRequestForOthers)]
        public void MapToPlatformUsageEnum_FromString_ExpectedResult(string input, PlatformUsage expectedResult)
        {
            // Arrange + Act
            var result = input.MapToPlatformUsageEnum();

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void MapToString_FromPlatformStatusEnum_NotRecognized_ArgumentExceptionExpected()
        {
            // Arrange
            Enum unknownPlatformStatusEnum = UriComponents.Host;

            // Act + Assert
            Assert.Throws<ArgumentException>(() => ((PlatformStatus)unknownPlatformStatusEnum).MapToString());
        }

        [TestCase(PlatformUsage.PaymentRequestForMyself, "PAYMENT_REQUEST_FOR_MYSELF")]
        [TestCase(PlatformUsage.PaymentRequestForOthers, "PAYMENT_REQUEST_FOR_OTHERS")]
        public void MapToString_FromPlatformStatusEnum_ExpectedResult(PlatformUsage input, string expectedResult)
        {
            // Arrange + Act
            var result = input.MapToString();

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}
