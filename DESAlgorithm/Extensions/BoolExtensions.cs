using System;
using DESAlgorithm.Exceptions;

namespace DESAlgorithm.Extensions
{
    public static class BoolExtensions
    {
        public static int ToInt(this bool value)
        {
            return value == true ? 1 : 0;
        }

        public static int GetByteValue(this bool[] singleByteArray)
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

        public static byte[] ToByteArray(this bool[] boolArray)
        {
            if (boolArray.Length % 8 != 0)
            {
                throw new ValidationException("Bool array must be divisble by 8.");
            }


            byte[] result = new byte[boolArray.Length / 8];
            for (int i = 0, j=7, k=0 ; i< boolArray.Length; i += 8, j += 8, k++)
            {
                result[k] = boolArray.GetByteValue(i, j);
            }
            return result;
        }

        public static byte GetByteValue(this bool[] source, int startIndex, int endIndex)
        {
            byte result = 0;

            for (int i = startIndex, j = 0 ; i <= endIndex; i++, j++)
            {
                if(source[i])
                    result |= (byte)(1 << (7 - j));
            }

            return result;
           
        }
    }
}
