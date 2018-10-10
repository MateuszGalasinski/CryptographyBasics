using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DESAlgorithm.Exceptions;

namespace DESAlgorithm.Extensions
{
    public static class BitArrayExtensions
    {

        //Reverts whole sequence
        public static BitArray Revert(this BitArray array)
        {
            BitArray revertedArray = new BitArray(array.Length);
            for (int i = array.Length - 1, r = 0; i >= 0; i--, r++)
            {
                revertedArray[r] = array[i];
            }

            return revertedArray;
        }

        public static BitArray RevertEveryByte(this BitArray array)
        {
            BitArray revertedArray;
            BitArray tmpArray = new BitArray(8);

            for (int i = 0 ; i < array.Length / 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    tmpArray[j] = array[i * 8 + j];
                }

               tmpArray = tmpArray.Revert();

                for (int j = 0; j < 8; j++)
                {
                    array[j + i * 8] = tmpArray[j];
                }
            }

            revertedArray = array;

            return revertedArray;
        }


        public static int GetByteValue(this BitArray singleByteArray)
        {
            if (singleByteArray.Length != 8)
            {
                throw new ValidationException("BitArray representing byte needs to has length == 8");
            }

            int value = 0;
            for (int i = 0; i < 8; i++)
            {
                value += (int)Math.Pow(2, i) * singleByteArray[7 - i].ToInt();
            }

            return value;
        }
    }
}
