using System.IO;
using NUnit.Framework;
using TikkieAPI.Utilities;

namespace TikkieAPI.Tests.Utilities
{
    [TestFixture]
    public class RSAUtilitiesTests
    {
        [Test]
        public void GetPrivateKeyFromPemFile_FileDoesNotExist_ExpectedFileNotFoundException()
        {
            // Arrange + Act + Assert
            Assert.Throws<FileNotFoundException>(() => RSAUtilities.GetPrivateKeyFromPemFile("non/existing/path/file.pem"));
        }

        [Test]
        public void GetPrivateKeyFromPemFile_FileIsNotAnRSAFile_ExpectedInvalidDataException()
        {
            // Arrange + Act + Assert
            Assert.Throws<InvalidDataException>(() => RSAUtilities.GetPrivateKeyFromPemFile("non_rsa.pem"));
        }

        [Test]
        public void GetPrivateKeyFromPemFile_ValidRSAFile_ExpectedNotNullResult()
        {
            // Arrange
            var pemFilePath = "example_rsa.pem";

            // Act
            var result = RSAUtilities.GetPrivateKeyFromPemFile(pemFilePath);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
