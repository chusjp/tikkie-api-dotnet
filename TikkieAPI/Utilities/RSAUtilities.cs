using System.IO;
using System.Security.Cryptography;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;

namespace TikkieAPI.Utilities
{
    /// <summary>
    /// RSA Encryption related utilities.
    /// </summary>
    internal class RSAUtilities
    {
        /// <summary>
        /// Gets the private key as a valid readable format for JWT encoding.
        /// </summary>
        /// <param name="privateKeyPemFilePath">The directory path where the RSA private key PEM file can be found.</param>
        /// <returns>The RSA key</returns>
        /// <exception cref="FileNotFoundException">If the private key pem file doesn't exist in the specified location.</exception>
        /// <exception cref="InvalidDataException">If the RSA private key PEM file cannot be read.</exception>
        public static RSACryptoServiceProvider GetPrivateKeyFromPemFile(string privateKeyPemFilePath)
        {
            if (!File.Exists(privateKeyPemFilePath))
            {
                throw new FileNotFoundException("The private key pem file doesn't exist in the specified location");
            }
            var privateKey = File.ReadAllText(privateKeyPemFilePath);
            using (TextReader privateKeyTextReader = new StringReader(privateKey))
            {
                var pemReader = new PemReader(privateKeyTextReader);
                var asymmetricKeyPair = new PemReader(privateKeyTextReader).ReadObject() as AsymmetricCipherKeyPair;
                if (asymmetricKeyPair == null)
                {
                    throw new InvalidDataException("Could not read the specified file as RSA private key");
                }

                var privateKeyParams = asymmetricKeyPair.Private as RsaPrivateCrtKeyParameters;

                var cryptoServiceProvider = new RSACryptoServiceProvider();
                var rsaParameters = ToRSAParameters(privateKeyParams);
                cryptoServiceProvider.ImportParameters(rsaParameters);

                return cryptoServiceProvider;
            }
        }

        /// <summary>
        /// Transforms the RSA private certificate key parameters from BouncyCastle libraty into the System's RSA parameters.
        /// </summary>
        /// <param name="privateKey">The RSA private certificate key parameters</param>
        /// <returns>The RSA parameters</returns>
        private static RSAParameters ToRSAParameters(RsaPrivateCrtKeyParameters privateKey)
        {
            return new RSAParameters
            {
                Modulus = privateKey.Modulus.ToByteArrayUnsigned(),
                Exponent = privateKey.PublicExponent.ToByteArrayUnsigned(),
                P = privateKey.P.ToByteArrayUnsigned(),
                Q = privateKey.Q.ToByteArrayUnsigned(),
                D = privateKey.Exponent.ToByteArrayUnsigned(),
                DP = privateKey.DP.ToByteArrayUnsigned(),
                DQ = privateKey.DQ.ToByteArrayUnsigned(),
                InverseQ = privateKey.QInv.ToByteArrayUnsigned()
            };
        }
    }
}
