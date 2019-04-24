using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using System;
using System.IO;
using System.Security.Cryptography;

namespace TikkieAPI.Utilities
{
    internal class RSAUtilities
    {
        public static RSACryptoServiceProvider GetPrivateKeyFromPemFile(string privateKeyPemFilePath)
        {
            var privateKey = File.ReadAllText(privateKeyPemFilePath);
            using (TextReader privateKeyTextReader = new StringReader(privateKey))
            {
                var pemReader = new PemReader(privateKeyTextReader);
                var asymmetricKeyPair = new PemReader(privateKeyTextReader).ReadObject() as AsymmetricCipherKeyPair;
                if (asymmetricKeyPair == null)
                {
                    throw new Exception("Could not read RSA private key");
                }

                var privateKeyParams = asymmetricKeyPair.Private as RsaPrivateCrtKeyParameters;

                var cryptoServiceProvider = new RSACryptoServiceProvider();
                var rsaParameters = ToRSAParameters(privateKeyParams);
                cryptoServiceProvider.ImportParameters(rsaParameters);

                return cryptoServiceProvider;
            }
        }

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
