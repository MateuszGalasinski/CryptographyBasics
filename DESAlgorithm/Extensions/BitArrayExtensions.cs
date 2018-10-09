using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

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
    }
}
