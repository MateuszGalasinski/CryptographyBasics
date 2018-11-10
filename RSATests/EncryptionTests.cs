using FluentAssertions;
using NUnit.Framework;
using RSA;

namespace RSATests
{
    public class EncryptionTests
    {
        [TestCase(new byte[] {0x0A})]
        public void EncryptDecrypt(byte[] data)
        {
            int keyLength = 4;
            var key = RSAAlgorithm.GenerateKey();

            var paddedData = Padding.AddPadding(data, keyLength);

            var encrypted = RSAAlgorithm.Encrypt(new BigInteger(paddedData), key.E, key.N);
            BigInteger decrypted = RSAAlgorithm.Decrypt(new BigInteger(encrypted), key.D, key.N);

            byte[] decryptedData = Padding.RemoveTrailingZeros(decrypted.GetData());
            decryptedData.Should().NotBeNull();
            decryptedData.Should().BeEquivalentTo(data);
        }
    }
}
