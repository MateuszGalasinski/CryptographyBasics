using RSAAlgoithm.Constants;
using RSAAlgoithm.Models;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace RSAAlgorithm
{
    public class CryptoAlgorithm : ICryptoAlgorithm
    {
        public CryptoAlgorithm(
            List<IDataTransformation> encryptSteps,
            List<IDataTransformation> decryptSteps,
            IPaddingStrategy paddingStrategy)
        {
            _encryptSteps = encryptSteps;
            _decryptSteps = decryptSteps;
            _paddingStrategy = paddingStrategy;
        }

        private List<IDataTransformation> _encryptSteps { get; }
        private List<IDataTransformation> _decryptSteps { get; }
        private IPaddingStrategy _paddingStrategy { get; }

        public byte[] Decrypt(byte[] data)
        {
            byte[] decryptedData = TransformWithBlockSeparation(_decryptSteps, data);

            _paddingStrategy?.RemovePadding(decryptedData);

            return decryptedData;
        }

        public byte[] Encrypt(byte[] data)
        {
            if (_paddingStrategy != null)
            {
                data = _paddingStrategy.AddPadding(data);
            }

            return TransformWithBlockSeparation(_encryptSteps, data);
        }

        /// <summary>
        /// </summary>
        /// <param name="transformations"></param>
        /// <param name="data">Padded data</param>
        /// <returns></returns>
        private byte[] TransformWithBlockSeparation(List<IDataTransformation> transformations, byte[] data)
        {
            const int blockSize = KeyConstants.KeySize;

            List<DataSet> transformedBlocks = new List<DataSet>();
            for (int i = 0; i < data.Length / blockSize; i++)
            {
                byte[] message = new byte[blockSize];
                Array.Copy(data, i * blockSize, message, 0, blockSize);

                DataSet dataSet = new DataSet
                {
                    Message = new BigInteger(message)
                };

                //encrypt
                foreach (IDataTransformation transformation in transformations)
                {
                    dataSet = transformation.Transform(dataSet);
                }

                transformedBlocks.Add(dataSet);
            }

            byte[] transformedData = new byte[transformedBlocks.Count * blockSize];
            int blocksCounter = 0;
            foreach (DataSet transformedBlock in transformedBlocks)
            {
                transformedBlock.Message.ToByteArray().CopyTo(transformedData, blocksCounter * blockSize);
                blocksCounter++;
            }

            return transformedData;
        }
    }
}