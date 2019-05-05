using System;
using NUnit.Framework;
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

        [TestCase(PlatformStatus.Active, "ACTIVE")]
        [TestCase(PlatformStatus.Inactive, "INACTIVE")]
        public void MapToString_FromPlatformStatusEnum_ExpectedResult(PlatformStatus input, string expectedResult)
        {
            // Arrange + Act
            var result = input.MapToString();

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void MapToPlatformStatusEnum_FromString_NotRecognized_ArgumentExceptionExpected()
        {
            // Arrange
            string unknownPlatformStatus = "UNKNOWN";

            // Act + Assert
            Assert.Throws<ArgumentException>(() => unknownPlatformStatus.MapToPlatformStatusEnum());
        }

        [TestCase("ACTIVE", PlatformStatus.Active)]
        [TestCase("INACTIVE", PlatformStatus.Inactive)]
        public void MapToPlatformStatusEnum_FromString_ExpectedResult(string input, PlatformStatus expectedResult)
        {
            // Arrange + Act
            var result = input.MapToPlatformStatusEnum();

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void MapToString_FromPaymentRequestStatusEnum_NotRecognized_ArgumentExceptionExpected()
        {
            // Arrange
            Enum unknownPaymentRequestStatus = AttributeTargets.Parameter;

            // Act + Assert
            Assert.Throws<ArgumentException>(() => ((PaymentRequestStatus)unknownPaymentRequestStatus).MapToString());
        }

        [TestCase(PaymentRequestStatus.Open, "OPEN")]
        [TestCase(PaymentRequestStatus.Closed, "CLOSED")]
        [TestCase(PaymentRequestStatus.Expired, "EXPIRED")]
        [TestCase(PaymentRequestStatus.MaxYieldReached, "MAX_YIELD_REACHED")]
        [TestCase(PaymentRequestStatus.MaxSuccessfulPaymentsReached, "MAX_SUCCESSFUL_PAYMENTS_REACHED")]
        public void MapToString_FromPaymentRequestStatusEnum_ExpectedResult(PaymentRequestStatus input, string expectedResult)
        {
            // Arrange + Act
            var result = input.MapToString();

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void MapToPaymentRequestStatusEnum_FromString_NotRecognized_ArgumentExceptionExpected()
        {
            // Arrange
            string unknownPaymentRequestStatus = "UNKNOWN";

            // Act + Assert
            Assert.Throws<ArgumentException>(() => unknownPaymentRequestStatus.MapToPaymentRequestStatusEnum());
        }

        [TestCase("OPEN", PaymentRequestStatus.Open)]
        [TestCase("CLOSED", PaymentRequestStatus.Closed)]
        [TestCase("EXPIRED", PaymentRequestStatus.Expired)]
        [TestCase("MAX_YIELD_REACHED", PaymentRequestStatus.MaxYieldReached)]
        [TestCase("MAX_SUCCESSFUL_PAYMENTS_REACHED", PaymentRequestStatus.MaxSuccessfulPaymentsReached)]
        public void MapToPaymentRequestStatusEnum_FromString_ExpectedResult(string input, PaymentRequestStatus expectedResult)
        {
            // Arrange + Act
            var result = input.MapToPaymentRequestStatusEnum();

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void MapToString_FromOnlinePaymentStatusEnum_NotRecognized_ArgumentExceptionExpected()
        {
            // Arrange
            Enum unknownOnlinePaymentStatus = AttributeTargets.Parameter;

            // Act + Assert
            Assert.Throws<ArgumentException>(() => ((OnlinePaymentStatus)unknownOnlinePaymentStatus).MapToString());
        }

        [TestCase(OnlinePaymentStatus.New, "NEW")]
        [TestCase(OnlinePaymentStatus.Pending, "PENDING")]
        [TestCase(OnlinePaymentStatus.Paid, "PAID")]
        [TestCase(OnlinePaymentStatus.NotPaid, "NOT_PAID")]
        public void MapToString_FromOnlinePaymentStatusEnum_ExpectedResult(OnlinePaymentStatus input, string expectedResult)
        {
            // Arrange + Act
            var result = input.MapToString();

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void MapToOnlinePaymentStatusEnum_FromString_NotRecognized_ArgumentExceptionExpected()
        {
            // Arrange
            string unknownOnlinePaymentStatus = "UNKNOWN";

            // Act + Assert
            Assert.Throws<ArgumentException>(() => unknownOnlinePaymentStatus.MapToOnlinePaymentStatusEnum());
        }

        [TestCase("NEW", OnlinePaymentStatus.New)]
        [TestCase("PENDING", OnlinePaymentStatus.Pending)]
        [TestCase("PAID", OnlinePaymentStatus.Paid)]
        [TestCase("NOT_PAID", OnlinePaymentStatus.NotPaid)]
        public void MapToOnlinePaymentStatusEnum_FromString_ExpectedResult(string input, OnlinePaymentStatus expectedResult)
        {
            // Arrange + Act
            var result = input.MapToOnlinePaymentStatusEnum();

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}
