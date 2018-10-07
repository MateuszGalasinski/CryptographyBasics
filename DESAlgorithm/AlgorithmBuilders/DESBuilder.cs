using DES.DataTransformations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DESAlgorithm.Exceptions;

namespace DES.AlgorithmBuilders
{
    public class DESBuilder
    {
        List<IDataTransformation> _encryptSteps = new List<IDataTransformation>();
        List<IDataTransformation> _decryptSteps = new List<IDataTransformation>();

        public CryptoAlgorithm Build()
        {
            return new CryptoAlgorithm(_encryptSteps, _decryptSteps);
        }

        public void AddWholeDES()
        {
            AddEncryptPermutation();
        }

        //1st step
        public void AddEncryptPermutation()
        {
            _encryptSteps.Add(new DataTransformation(p =>
            {
                p.Not();
            }));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataToShuffle">entry data</param>
        /// <param name="permutationTable"> Consists of numbers specifing which bit from data to choose.</param>
        /// <returns></returns>
        public byte[] Shuffle(byte[] dataToShuffle, int[] permutationTable)
        {
            if (permutationTable.Length % 8 != 0)
            {
                throw new ValidationException("Table descripting where to put specific bits needs to have size divisible by 8.");
            }

            if (permutationTable.Max() >= dataToShuffle.Length * 8)
            {
                throw new ValidationException($"Found too big number: {permutationTable.Max()} in permutation table. Data to choose from is too short for it.");

            }

            byte[] bitSetters = new byte[]
            {
                128,
                64,
                32,
                16,
                8,
                4,
                2,
                1
            };

            byte[] resultData = new byte[permutationTable.Length / 8];
            for (int i = 0; i < permutationTable.Length; i++)
            {
                int byteNumber = permutationTable[i] / 8;
                int bitNumberInByte = permutationTable[i] % 8;
                byte bitSetter = (byte)(bitSetters[bitNumberInByte] & dataToShuffle[byteNumber]);
                byte bitResetter = (byte)~bitSetter;

                resultData[i/8] &= bitResetter;
                resultData[i/8] |= bitSetter;
            }

            return resultData;
        }
    }
}
