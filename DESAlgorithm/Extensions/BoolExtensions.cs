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
    }
}
