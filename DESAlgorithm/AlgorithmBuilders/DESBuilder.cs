using DES.DataTransformations;
using System;
using System.Collections;
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
        public BitArray Shuffle(BitArray dataToShuffle, int[] permutationTable)
        {
            if (permutationTable.Length % 8 != 0)
            {
                throw new ValidationException("Table descripting where to put specific bits needs to have size divisible by 8.");
            }

            if (permutationTable.Max() >= dataToShuffle.Length)
            {
                throw new ValidationException($"Found too big number: {permutationTable.Max()} in permutation table. Data to choose from is too short for it.");

            }
            
            BitArray resultData = new BitArray(permutationTable.Length);

            for (int i = 0; i < permutationTable.Length; i++)
            {
                int dataIndex = permutationTable[i] - 1; ;
                resultData[i] = dataToShuffle[dataIndex];
            }

            return resultData;
        }
    }
}
