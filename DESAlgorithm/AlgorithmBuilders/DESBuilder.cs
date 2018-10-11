using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DES.Constants;
using DES.DataTransformations;
using DESAlgorithm.Exceptions;
using DESAlgorithm.Extensions;
using DESAlgorithm.PaddingStrategies;

namespace DES.AlgorithmBuilders
{
    public class DESBuilder
    {
        List<IDataTransformation> _encryptSteps = new List<IDataTransformation>();
        List<IDataTransformation> _decryptSteps = new List<IDataTransformation>();

        public CryptoAlgorithm Build()
        {
            return new CryptoAlgorithm(_encryptSteps, _decryptSteps, new CMSPaddingStrategy());
        }

        public void AddWholeDES(BitArray key)
        {
            if (key.Length != 64)
            {
                throw new ValidationException("Starting key has to 64 bits long.");
            }

            BitArray reducedKey = RemoveParityBits(key);

            for (int cycleNumber = 0; cycleNumber < 16; cycleNumber++)
            {
                //prepare key
                reducedKey = GenerateLongKeyForCycle(reducedKey, cycleNumber);
                BitArray shortKey = GenerateShortKeyfromLongKey(reducedKey);

                AddExtendingPermutation();
                _encryptSteps.Add(new DataTransformation(dataSet =>
                {
                    dataSet.Right = SumModuloTwo(shortKey, dataSet.Right);
                    return dataSet;
                }));

                //addSBlocks
                //add last permutation

                //merge 'halfs'
                _encryptSteps.Add(new DataTransformation(dataSet =>
                {
                    BitArray oldLeft = dataSet.Left;
                    dataSet.Left = SumModuloTwo(dataSet.Right, dataSet.Left);
                    dataSet.Right = oldLeft;
                    return dataSet;
                }));
            }
        }

        public void AddExtendingPermutation()
        {
            _encryptSteps.Add(new DataTransformation(dataSet =>
            {
                BitArray data = dataSet.Right;
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

                dataSet.Right = extendedData;
                return dataSet;
            }));
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

        private BitArray blockCopy(BitArray data, int destOffSet, int offSet, int count, int arrayLength )
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


        public void AddSBlocks()
        {
            _encryptSteps.Add(new DataTransformation(dataSet =>
            {
                BitArray data = dataSet.Right;

                BitArray result = new BitArray(32);
                BitArray OneBlockOutputData = new BitArray(4);

                int[] oneBlockInputData = new int[6];
                int[] rowBinNumber = new int[2];

                int rowDecNumber = new int();
                int[] columnBinNumber = new int[4];
                int columnDecNumber = new int();
                int numberInBlock = new int();

                int[,] blocks = {{ 14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7 },
                             { 0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8 },
                             { 4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0 },
                             { 15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13 },      //S1

                             { 15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10 },
                             { 3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5 },
                             { 0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15 },
                             { 13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9 },      //S2

                             { 10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8 },
                             { 13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1 },
                             { 13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7 },
                             { 1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12 },      //S3

                             { 7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15 },
                             { 13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9 },
                             { 10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4 },
                             { 3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14 },      //S4

                             { 2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9 },
                             { 14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6 },
                             { 4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14 },
                             { 11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3 },      //S5

                             { 12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11 },
                             { 10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8 },
                             { 9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6 },
                             { 4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13 },      //S6

                             { 4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1 },
                             { 13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6 },
                             { 1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2 },
                             { 6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12 },      //S7

                             { 13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7 },
                             { 1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2 },
                             { 7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8 },
                             { 2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11 }       //S8
            };

                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        oneBlockInputData[j] = BoolExtensions.ToInt(data[j + 6 * i]);
                    }

                    rowBinNumber[0] = oneBlockInputData.First();
                    rowBinNumber[1] = oneBlockInputData.Last();

                    for (int j = 1; j <= 4; j++)
                    {
                        columnBinNumber[j - 1] = oneBlockInputData[j];
                    }


                    rowDecNumber = BinaryToDecimal(rowBinNumber);
                    columnDecNumber = BinaryToDecimal(columnBinNumber);
                    OneBlockOutputData = new BitArray(new int[] { blocks[rowDecNumber + i * 4, columnDecNumber] });
                    for (int j = 0; j < 4; j++)
                    {
                        result[j + 4 * i] = OneBlockOutputData[3 - j];
                    }
                }

                dataSet.Right = result;
                return dataSet;
            }));

            
        }

        internal int BinaryToDecimal(int[] binaryNumber)
        {
            int result = new int();
            for (int i = 0; i < binaryNumber.Length; i++)
            {
                result += (int)Math.Pow(2,binaryNumber.Length - 1 - i)  * binaryNumber[i];
                
            }
            return result;
        }

        internal int[] DecimalToBinary(int decimalNumber)
        {
            int[] result = new int[4];
            for (int i = 3; i < 0; i++)
            {
                result[i] = decimalNumber % 2;
                decimalNumber = decimalNumber / 2;
            }

            return result;
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

        private BitArray[] SplitBitArrayInHalf(BitArray arrayToSplit)
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

        private BitArray JoinBitArraysFromHalfs(BitArray[] splitedArrays)
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

        public BitArray SumModuloTwo(BitArray key, BitArray key2)
        {
            return key.Xor(key2);
        }

        public void AddPblockPermutation()
        {
            _encryptSteps.Add(new DataTransformation(dataSet =>
            {
                BitArray data = dataSet.Right;
                if (data.Length != 32)
                {
                    throw new ValidationException("Only 32 bits array is accepted form P block permutation.");
                }

                int[] permutationTable = new int[]
                {
                    16, 7, 20, 21, 29, 12, 28, 17, 1, 15, 23, 26, 5, 18, 31, 10,
                    2, 8, 24, 14, 32, 27, 3, 9, 19, 13, 30, 6, 22, 11, 4, 25
                };

                BitArray extendedData = Shuffle(data, permutationTable);
                dataSet.Right = extendedData;
                return dataSet;
            }));
        }

        public BitArray RemoveParityBits(BitArray key)
        {
            BitArray resultKey = new BitArray(56);
            int currentBitsSum = 0, resultKeyIndex = 0, bitsCounter = 1;
            for (int i = 0; i < key.Length; i++)
            {
                if (bitsCounter % 8 != 0)
                {
                    resultKey[resultKeyIndex] = key[i];
                    resultKeyIndex++;

                    currentBitsSum = currentBitsSum.SumModuloTwo(key[i].ToInt());
                    bitsCounter++;
                }
                else
                {
                    if (currentBitsSum != key[i].ToInt())
                    {
                        throw new ValidationException("Key is broken: parity bits does not match key value.");
                    }

                    currentBitsSum = 0; //clear current sum
                    bitsCounter = 1;
                }
            }

            return resultKey;
        }
    }
}
