using RSAAlgoithm.Models;
using System;
using System.Collections;
using System.Collections.Generic;

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

        public bool[] Decrypt(bool[] data)
        {
            bool[] decryptedData = TrasformateWithBlockSeparation(_decryptSteps, data);

            return _paddingStrategy.RemovePadding(decryptedData);
        }

        public bool[] Encrypt(bool[] data)
        {
            bool[] paddedData = _paddingStrategy.AddPadding(data);

            return TrasformateWithBlockSeparation(_encryptSteps, paddedData);
        }

        /// <summary>
        /// </summary>
        /// <param name="transformations"></param>
        /// <param name="data">Padded data</param>
        /// <returns></returns>
        private bool[] TrasformateWithBlockSeparation(List<IDataTransformation> transformations, bool[] data)
        {
            List<DataSet> transformedBlocks = new List<DataSet>();
            for (int i = 0; i < data.Length / 64; i++)
            {
                bool[] leftHalfBlock = new bool[32];
                Array.Copy(data, i * 64, leftHalfBlock, 0, 32);
                bool[] rightHalfBlock = new bool[32];
                Array.Copy(data, i * 64 + 32, rightHalfBlock, 0, 32);

                DataSet dataSet = new DataSet
                {
                    Left = new BitArray(leftHalfBlock),
                    Right = new BitArray(rightHalfBlock)
                };

                //encrypt
                foreach (IDataTransformation transformation in transformations)
                {
                    dataSet = transformation.Transform(dataSet);
                }

                transformedBlocks.Add(dataSet);
            }

            bool[] transformedData = new bool[transformedBlocks.Count * 64];
            int blocksCounter = 0;
            foreach (DataSet transformedBlock in transformedBlocks)
            {
                transformedBlock.Left.CopyTo(transformedData, blocksCounter * 64);
                transformedBlock.Right.CopyTo(transformedData, blocksCounter * 64 + 32);
                blocksCounter++;
            }

            return transformedData;
        }
    }
}