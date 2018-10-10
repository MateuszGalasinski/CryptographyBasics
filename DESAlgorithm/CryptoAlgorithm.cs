using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
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

        public DataSet Decrypt(DataSet data)
        {
            foreach (IDataTransformation transformation in _decryptSteps)
            {
                data = transformation.Transform(data);
            }

            return data;
        }

        public bool[] Encrypt(bool[] data)
        {
            List<DataSet> encryptedBlocks = new List<DataSet>();

            bool[] paddedData = _paddingStrategy.AddPadding(data);

            for (int i = 0; i < paddedData.Length / 64; i++)
            {
                bool[] leftHalfBlock = new bool[32];
                Array.Copy(paddedData, i * 64, leftHalfBlock, 0, 32);
                bool[] rightHalfBlock = new bool[32];
                Array.Copy(paddedData, i * 64 + 32, leftHalfBlock, 0, 32);
                
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

            bool[] encryptedData = new bool[encryptedBlocks.Count*64];
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
