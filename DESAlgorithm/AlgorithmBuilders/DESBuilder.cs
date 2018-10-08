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

        public void AddExtendingPermutation()
        {
            _encryptSteps.Add(new DataTransformation(data =>
            {
                if (data.Length != 32)
                {
                    throw new ValidationException("Only 32 bits array is accepted to be extended to 48 bits.");
                }

                int[] permutationTable = new int[]
                {
                    32, 1, 2, 3, 4, 5, 4, 5, 6, 7, 8, 9,
                    8, 9, 10, 11, 12, 13, 12, 13, 14, 15, 16, 17,
                    16, 17, 18, 19, 20, 21, 20, 21, 22, 23, 24, 25,
                    24, 25, 26, 27, 28, 29, 28, 29, 30, 31, 32, 1
                };

                BitArray extendedData = Shuffle(data, permutationTable);
                return extendedData;
            }));
        }

        public BitArray GenerateLongKeyForCycle(BitArray previousKey, int cycle)
        {
            Int16[] shiftInCycle = {0,1,1,2,2,2,2,2,2,1,2,2,2,2,2,2,1};

            if (previousKey.Length != 56)
            {
                throw new ValidationException("Previous key must have 56 bits");
            }

            BitArray[] keyInHalfs = SplitBitArrayInHalf(previousKey);

            keyInHalfs[0] = ShiftBits(keyInHalfs[0], shiftInCycle[cycle - 1], Direction.Left);
            keyInHalfs[1] = ShiftBits(keyInHalfs[1], shiftInCycle[cycle - 1], Direction.Left);

            return JoinBitArraysFromHalfs(keyInHalfs);

        }

        public BitArray GenerateShortKeyfromLongKey(BitArray key)
        {
            if (key.Length != 56)
            {
                throw new ValidationException("Long key must have 56 bits");
            }

            int[] permutationTable =
            {
                14, 17, 11, 24, 1, 5, 3, 28, 15, 6, 21, 10,
                23, 19, 12, 4, 26, 8, 16, 7, 27, 20, 13, 2,
                41, 52, 31, 37, 47, 55, 30, 40, 51, 45, 33, 48,
                44, 49, 39, 56, 34, 53, 46, 42, 50, 36, 29, 32
            };

            return Shuffle(key, permutationTable);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">entry data</param>
        /// <param name="translation"> Bits to shift number.</param>
        /// /// <param name="direction"> Direction of translation.</param>
        /// <returns></returns>
        public BitArray ShiftBits(BitArray data, int translation, Direction direction)
        {
            int length = data.Length;
            BitArray result = new BitArray(data.Length);

            if (direction == Direction.Left)
            {
                //old front to new end
                result = blockCopy(data, 0 , translation, length , length);

                //for (int i = length-translation; i < length ; i++)
                //{
                //    result[i] =  data.Get(i-length+translation) ;
                //}

                for (int i = 0; i < translation; i++)
                {
                    result[length - translation + i] = data.Get(i);
                }

            }
            else if(direction == Direction.Right)
            {
                //old end to new front
                result = blockCopy(data, translation, 0, length - translation, length);

                for (int i = 0; i < translation; i++)
                {
                    result[i] = data.Get(length - translation + i);
                }
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

            if (permutationTable.Max() > dataToShuffle.Length)
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

        public BitArray SumModuloTwo(BitArray key, BitArray key2)
        {
            return key.Xor(key2);
        }

        internal BitArray blockCopy(BitArray data, int destOffSet, int offSet, int count, int arrayLength )
        {
            BitArray result = new BitArray(arrayLength);

            for(int i = 0; i < destOffSet; i++)
            {
                result[i] = false;
            }

            for (int j = offSet; j < count; j++)
            {
                result[destOffSet + j - offSet] = data.Get(j);
            }

            return result;
        }

        internal BitArray[] SplitBitArrayInHalf(BitArray arrayToSplit)
        {
            if (arrayToSplit.Length % 2 != 0)
            {
                throw new ValidationException("Number of bits (array size) to split must be even");
            }

            int halfOfArrayCount = arrayToSplit.Length / 2;

            BitArray firstHalf = new BitArray(halfOfArrayCount);
            BitArray secondHalf = new BitArray(halfOfArrayCount);

            for (int i = 0; i < halfOfArrayCount; i++)
            {
                firstHalf[i] = arrayToSplit.Get(i);
            }

            for (int i = 0; i < halfOfArrayCount; i++)
            {
                secondHalf[i] = arrayToSplit.Get(i + halfOfArrayCount);
            }

            return new BitArray[]{firstHalf, secondHalf};
        }

        internal BitArray JoinBitArraysFromHalfs(BitArray[] splitedArrays)
        {
            int halfOfArrayCount = splitedArrays[0].Length;
            BitArray result = new BitArray(halfOfArrayCount * 2);

            for (int i = 0; i < halfOfArrayCount; i++)
            {
                result[i] = splitedArrays[0].Get(i);
                result[i + halfOfArrayCount] = splitedArrays[1].Get(i);
            }

            return result;
        }
    }
}
