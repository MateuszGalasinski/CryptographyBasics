using System;
using System.Linq;

namespace RSA
{
    public static class Padding
    {
        public static byte[] AddPadding(byte[] data, int keyLength)
        {
            byte[] result;

            if (data.Length % keyLength == 0)
            {
                result = new byte[data.Length];
                data.CopyTo(result, 0);
            }
            else
            {
                int numberOfBlocksInSource = data.Length / keyLength;
                int additionalLength = ((numberOfBlocksInSource + 1) * keyLength) - data.Length;
                result = new byte[data.Length + additionalLength];
                data.CopyTo(result, 0);

                for (int i = 0; i < additionalLength; i++)
                {
                    result[numberOfBlocksInSource * keyLength + i] = 0;
                }

                Array.Copy(data,
                    numberOfBlocksInSource * keyLength,
                    result,
                    numberOfBlocksInSource * keyLength + additionalLength,
                    keyLength - additionalLength);
            }

            return result;
        }

        public static byte[] RemoveTrailingZeros(uint[] data)
        {
            //TODO: implement correctly
            byte[] resultData = data.Where(p => p != 0).SelectMany(BitConverter.GetBytes).ToArray();
            return resultData.Where(p => p != 0).ToArray();
            //return data.SelectMany(BitConverter.GetBytes).ToArray();
        }
    }
}
