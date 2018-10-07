using DES.DataTransformations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using DES.Constants;
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
                return p.Not();
            }));
        }
    
        public BitArray ShiftBits(BitArray data, int translation, Direction direction)
        {
            int length = data.Length;
            BitArray result = new BitArray(data.Length);

            if (direction == Direction.Left)
            {
                //old front to new end
                result = blockCopy(data, 0 , translation, length , length);

                for (int i = length-translation; i < length ; i++)
                {
                    result.Set(i, data.Get(i-length+translation) );
                }

            }
            else if(direction == Direction.Right)
            {
                //old end to new front
                result = blockCopy(data, translation, 0, length - translation, length);

                for (int i = 0; i < translation; i++)
                {
                    result.Set(i, data.Get(length - translation + i));
                }
            }


           return result;

        }

        private BitArray blockCopy(BitArray data, int destOffSet, int offSet, int count, int arrayLength )
        {
            BitArray result = new BitArray(arrayLength);

            for(int i = 0; i < destOffSet; i++)
            {
                result.Set(i, false);
            }

            for (int j = offSet; j < count; j++)
            {
                result.Set(destOffSet + j - offSet, data.Get(j));
            }

            return result;
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
