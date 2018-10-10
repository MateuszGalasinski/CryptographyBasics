using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DESAlgorithm.Exceptions;

namespace DESAlgorithm.Extensions
{
    public static class BitArrayExtensions
    {
        public static BitArray Revert(this BitArray array)
        {
            BitArray revertedArray = new BitArray(array.Length);
            for (int i = array.Length - 1, r = 0; i >=0; i--, r++)
            {
                revertedArray[r] = array[i];
            }

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
                value += (int)Math.Pow(singleByteArray[7 - i].ToInt() * 2, i);
            }

            return value;
        }
    }
}
