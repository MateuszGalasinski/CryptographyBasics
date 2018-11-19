using FluentAssertions;
using NUnit.Framework;
using RSA;
using RSA.Models;
using RSA.PaddingStrategies;
using System;
using System.IO;

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
            decrypted.ShouldBeEquivalentTo(testValue);
        }

        [Test]
        public void EncryptDecryptOperation()
        {
            int blockSize = 7;//RSAAlgorithm.NumberOfBytes - 1;
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
            decrypted.ShouldBeEquivalentTo(testValue);

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

        //[Test]
        //public void FullEncryptDecryptOperation()
        //{
        //    DataChunker chunker = new DataChunker();
        //    int blockSize = RSAAlgorithm.NumberOfBytes;
        //    FullKey key = RSAAlgorithm.GenerateKey();
        //    byte[] byteValues = new byte[]
        //    {
        //        0x0A, 0x0B, 0x0C, 0x0D, 0x0A, 0x0B, 0x0C,
        //        0x0A, 0x0B, 0x0C, 0x0D, 0x0A,
        //    };

        //    var paddedValue = chunker.ChunkData(byteValues, blockSize);

        //    for (int i = 0; i < paddedValue.Length; i++)
        //    {
        //        paddedValue[i] = RSAAlgorithm.Encrypt(paddedValue[i], key.E, key.N);
        //    }

        //    var savedData = chunker.MergeData(paddedValue, blockSize);

        //    var loadedData = chunker.BytesToBigIntegers(savedData, blockSize);

        //    loadedData.Should().BeEquivalentTo(paddedValue);

        //    for (int i = 0; i < loadedData.Length; i++)
        //    {
        //        loadedData[i] = RSAAlgorithm.Decrypt(loadedData[i], key.D, key.N);
        //    }

        //    var decrypted = chunker.MergeDataAndRemovePadding(loadedData, blockSize);

        //    decrypted.Should().BeEquivalentTo(new byte[]
        //    {
        //        0x0A, 0x0B, 0x0C, 0x0D, 0x0A, 0x0B, 0x0C,
        //        0x0A, 0x0B, 0x0C, 0x0D, 0x0A
        //    });
        //}

        [Test]
        public void FullOperationWithoutEncryptDecrypt()
        {
            string dataPath = @"C:\Users\Mateusz\Desktop\tkst.txt";
            string encryptedPath = @"C:\Users\Mateusz\Desktop\en";
            string decryptedPath = @"C:\Users\Mateusz\Desktop\tkst2.txt";
            int howManyOk = 0;
            int howManyTrials = 10;

            for (int j = 0; j < howManyTrials; j++)
            {
                DataChunker chunker = new DataChunker();
                int blockSize = RSAAlgorithm.BlockSize;
                FullKey key = RSAAlgorithm.GenerateKey();

                byte[] byteValues = File.ReadAllBytes(dataPath);

                var paddedValue = chunker.ChunkData(byteValues, blockSize-8);

                for (int i = 0; i < paddedValue.Length; i++)
                {
                    paddedValue[i] = RSAAlgorithm.Encrypt(paddedValue[i], key.E, key.N);
                }

                var savedData = chunker.MergeData(paddedValue, blockSize);

                File.WriteAllBytes(encryptedPath, savedData);
                var loadedDataFromFile = File.ReadAllBytes(encryptedPath);

                var loadedData = chunker.BytesToBigIntegers(loadedDataFromFile, blockSize);

                for (int i = 0; i < loadedData.Length; i++)
                {
                    loadedData[i] = RSAAlgorithm.Decrypt(loadedData[i], key.D, key.N);
                }

                var decrypted = chunker.MergeDataAndRemovePadding(loadedData, blockSize-8);

                File.WriteAllBytes(decryptedPath, decrypted);

                var loadedDecrypt = File.ReadAllBytes(decryptedPath);

                var orginal = File.ReadAllBytes(dataPath);

                bool isOk = true;

                if (orginal.Length != loadedDecrypt.Length)
                {
                    Console.WriteLine("lipa " + key.N + " len:" + key.N.ToString().Length);
                    Console.WriteLine(" Diff " + (orginal.Length - loadedDecrypt.Length));
                    isOk = false;
                }
                else
                {
                    for (int i = 0; i < loadedDecrypt.Length; i++)
                    {
                        if (orginal[i] != loadedDecrypt[i])
                        {
                            Console.WriteLine(i.ToString());
                            Console.WriteLine("lipa " + key.N + " len:" + key.N.ToString().Length);
                            isOk = false;
                            break;
                        }

                    }
                }

                if (isOk)
                {
                    howManyOk++;
                    Console.WriteLine("git " + key.N + " len:" + key.N.ToString().Length);
                }

                //loadedDecrypt.Should().BeEquivalentTo(orginal);
            }

            Assert.AreEqual(howManyTrials, howManyOk);

        }
    }
}
