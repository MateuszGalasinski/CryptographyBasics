using FluentAssertions;
using NUnit.Framework;
using RSA;
using RSA.Models;
using RSA.PaddingStrategies;

namespace RSATests
{
    public class EncryptionTests
    {
        [TestCase(new byte[] {0x0A})]
        public void EncryptDecrypt(byte[] data)
        {
            //int keyLength = 4;
            //var key = RSAAlgorithm.GenerateKey();

            //var paddedData = Padding.AddPadding(data, keyLength);

            //var encrypted = RSAAlgorithm.Encrypt(new BigInteger(paddedData), key.E, key.N);
            //BigInteger decrypted = RSAAlgorithm.Decrypt(new BigInteger(encrypted), key.D, key.N);

            //byte[] decryptedData = Padding.RemoveTrailingZeros(decrypted.GetData());
            //decryptedData.Should().NotBeNull();
            //decryptedData.Should().BeEquivalentTo(data);
        }

        [Test]
        public void SimpleEncryptDecryptOperation()
        {
            BigInteger testValue = 123;
            var key = RSAAlgorithm.GenerateKey();
            BigInteger encrypted = RSAAlgorithm.Encrypt(testValue, key.E, key.N);
            BigInteger decrypted = RSAAlgorithm.Decrypt(encrypted, key.D, key.N);
            decrypted.Should().ShouldBeEquivalentTo(testValue);
        }

        [Test]
        public void EncryptDecryptOperation()
        {
            int blockSize = RSAAlgorithm.NumberOfBytes - 1;
            FullKey key = RSAAlgorithm.GenerateKey();
            var paddingStrategy = new CMSPaddingStrategy();
            byte[] byteValue = new byte[]
            {
                0x0A, 0x0B, 0x0C, 0x0D, 0x0A, 0x0B, 0x0C,
                0x0A, 0x0B, 0x0C, 0x0D, 0x0A, 
            };

            byte[] paddedValue = paddingStrategy.AddPadding(byteValue, blockSize);

            paddedValue.Should().BeEquivalentTo(new byte[]
            {
                0x0A, 0x0B, 0x0C, 0x0D, 0x0A, 0x0B, 0x0C,
                0x0A, 0x0B, 0x0C, 0x0D, 0x0A, 0x02, 0x02 
            });

            BigInteger testValue = new BigInteger(paddedValue);
            BigInteger encrypted = RSAAlgorithm.Encrypt(testValue, key.E, key.N);
            BigInteger decrypted = RSAAlgorithm.Decrypt(encrypted, key.D, key.N);
            decrypted.Should().ShouldBeEquivalentTo(testValue);

            decrypted.getBytes().Should().BeEquivalentTo(new byte[]
            {
                0x0A, 0x0B, 0x0C, 0x0D, 0x0A, 0x0B, 0x0C,
                0x0A, 0x0B, 0x0C, 0x0D, 0x0A, 0x02, 0x02
            });

            paddingStrategy.RemovePadding(decrypted.getBytes(), blockSize).Should().BeEquivalentTo(new byte[]
            {
                0x0A, 0x0B, 0x0C, 0x0D, 0x0A, 0x0B, 0x0C,
                0x0A, 0x0B, 0x0C, 0x0D, 0x0A
            });
        }

        [Test]
        public void FullEncryptDecryptOperation()
        {
            DataChunker chunker = new DataChunker();
            int blockSize = RSAAlgorithm.NumberOfBytes;
            FullKey key = RSAAlgorithm.GenerateKey();
            byte[] byteValues = new byte[]
            {
                0x0A, 0x0B, 0x0C, 0x0D, 0x0A, 0x0B, 0x0C,
                0x0A, 0x0B, 0x0C, 0x0D, 0x0A,
            };

            var paddedValue = chunker.ChunkData(byteValues, blockSize);

            for (int i = 0; i < paddedValue.Length; i++)
            {
                paddedValue[i] = RSAAlgorithm.Encrypt(paddedValue[i], key.E, key.N);
            }

            var savedData = chunker.MergeData(paddedValue, blockSize);

            var loadedData = chunker.BytesToBigIntegers(savedData, blockSize);

            loadedData.Should().BeEquivalentTo(paddedValue);

            for (int i = 0; i < loadedData.Length; i++)
            {
                loadedData[i] = RSAAlgorithm.Decrypt(loadedData[i], key.D, key.N);
            }

            var decrypted = chunker.MergeDataAndRemovePadding(loadedData, blockSize);

            decrypted.Should().BeEquivalentTo(new byte[]
            {
                0x0A, 0x0B, 0x0C, 0x0D, 0x0A, 0x0B, 0x0C,
                0x0A, 0x0B, 0x0C, 0x0D, 0x0A
            });
        }
    }
}
