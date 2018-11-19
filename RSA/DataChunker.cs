using RSA.PaddingStrategies;
using System;
using System.Collections.Generic;

namespace RSA
{
    public class DataChunker
    {
        private IPaddingStrategy _paddingStrategy = new CMSPaddingStrategy();

        public BigInteger[] ChunkData(byte[] inputData, int blockSize)
        {
            var paddedData = _paddingStrategy.AddPadding(inputData, blockSize);
            BigInteger[] blocks = new BigInteger[paddedData.Length / blockSize];

            for (int i = 0; i < paddedData.Length / blockSize; i++)
            {
                byte[] block = new byte[blockSize];
                Array.Copy(paddedData, i * blockSize, block, 0, blockSize);
                blocks[i] = new BigInteger(block);
            }

            return blocks;
        }

        public BigInteger[] BytesToBigIntegers(byte[] encryptedBytes, int blockSize)
        {
            if (encryptedBytes.Length % blockSize != 0)
                throw new Exception("Encrypted bytes should be divisible by block size");

            BigInteger[] blocks = new BigInteger[encryptedBytes.Length / blockSize];

            for (int i = 0; i < encryptedBytes.Length / blockSize; i++)
            {
                byte[] block = new byte[blockSize];
                Array.Copy(encryptedBytes, i * blockSize, block, 0, blockSize);
               
                blocks[i] = new BigInteger(block);
            }

            return blocks;
        }

        public byte[] MergeDataAndRemovePadding(BigInteger[] decryptedValues, int blockSize)
        {
            byte[] result = new byte[decryptedValues.Length * blockSize];

            for (int i = 0; i < decryptedValues.Length; i++)
            {
                byte[] padded =
                    AddPadding(decryptedValues[i].ToByteArray(), blockSize);
                padded.CopyTo(result, i * blockSize);
            }

            result = _paddingStrategy.RemovePadding(result,blockSize);
            return result;
        }

        public byte[] MergeData(BigInteger[] decryptedValues, int blockSize)
        {
            byte[] result = new byte[decryptedValues.Length * blockSize];

            for (int i = 0; i < decryptedValues.Length; i++)
            {
                byte[] padded =
                    AddPadding(decryptedValues[i].ToByteArray(), blockSize);
                padded.CopyTo(result, i * blockSize);
            }

            return result;
        }

        public BigInteger[] ChunkBytesData(byte[] decryptedValues, int blockSize)
        {
            List<BigInteger> encryptedValues = new List<BigInteger>();

            for (int i = 0; i < decryptedValues.Length / blockSize; i++)
            {
                byte[] newBlock = new byte[blockSize];
                Array.Copy(decryptedValues, i * blockSize, newBlock, 0, blockSize);
                encryptedValues.Add(new BigInteger(newBlock));
            }

            return encryptedValues.ToArray();
        }

        private byte[] AddPadding(byte[] data, int bytesInBlock)
        {
            int diff = data.Length % bytesInBlock;
            int bytesToPadd = diff == 0 ? 0 : bytesInBlock - diff;
            byte[] paddedData = new byte[data.Length + bytesToPadd];
            data.CopyTo(paddedData, bytesToPadd);
            return paddedData;
        }
    }
}
