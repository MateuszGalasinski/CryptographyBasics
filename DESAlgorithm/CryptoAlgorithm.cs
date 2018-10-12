using System;
using System.Collections;
using System.Collections.Generic;
using DESAlgorithm.Models;
using DESAlgorithm.PaddingStrategies;

namespace DES
{
    public class CryptoAlgorithm : ICryptoAlgorithm
    {
        private List<IDataTransformation> _encryptSteps { get; }
        private List<IDataTransformation> _decryptSteps { get; }
        private IPaddingStrategy _paddingStrategy { get; }

        public CryptoAlgorithm(
            List<IDataTransformation> encryptSteps,
            List<IDataTransformation> decryptSteps,
            IPaddingStrategy paddingStrategy)
        {
            _encryptSteps = encryptSteps;
            _decryptSteps = decryptSteps;
            _paddingStrategy = paddingStrategy;
        }

        public bool[] Decrypt(bool[] data)
        {
            bool[] unpaddedData = _paddingStrategy.RemovePadding(data);

            return TrasformateWithBlockSeparation(_encryptSteps, unpaddedData);
        }

        public bool[] Encrypt(bool[] data)
        {
            bool[] paddedData = _paddingStrategy.AddPadding(data);

            return TrasformateWithBlockSeparation(_encryptSteps, paddedData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transformations"></param>
        /// <param name="data">Padded data</param>
        /// <returns></returns>
        private bool[] TrasformateWithBlockSeparation(List<IDataTransformation> transformations, bool[] data)
        {
            List<DataSet> encryptedBlocks = new List<DataSet>();
            for (int i = 0; i < data.Length / 64; i++)
            {
                bool[] leftHalfBlock = new bool[32];
                Array.Copy(data, i * 64, leftHalfBlock, 0, 32);
                bool[] rightHalfBlock = new bool[32];
                Array.Copy(data, i * 64 + 32, leftHalfBlock, 0, 32);

                DataSet dataSet = new DataSet()
                {
                    Left = new BitArray(leftHalfBlock),
                    Right = new BitArray(rightHalfBlock)
                };

                //encrypt
                foreach (IDataTransformation transformation in _encryptSteps)
                {
                    dataSet = transformation.Transform(dataSet);
                }
                encryptedBlocks.Add(dataSet);
            }

            bool[] encryptedData = new bool[encryptedBlocks.Count * 64];
            int blocksCounter = 0;
            foreach (DataSet encryptedBlock in encryptedBlocks)
            {
                encryptedBlock.Left.CopyTo(encryptedData, blocksCounter * 64);
                encryptedBlock.Right.CopyTo(encryptedData, blocksCounter * 64 + 32);
            }

            return encryptedData;
        }
    }
}
