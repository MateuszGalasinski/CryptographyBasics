using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DESAlgorithm.Exceptions;

namespace DESAlgorithm.PaddingStrategies
{
    public class CMSPaddingStrategy : IPaddingStrategy
    {
        public BitArray AddPadding(BitArray message)
        {
            if (message.Length % 8 != 0)
            {
                throw new ValidationException($"Message does not represent bytes. {message.Length} is not divisible by 8.");
            }
            if (message.Length % 64 == 0)
            {
                bool[] paddedMessage = new bool[message.Length + 64];
                message.CopyTo(paddedMessage, 0);
                for (int i = message.Length; i < message.Length + 64; i += 8)
                {
                    paddedMessage[i + 4] = true; // set 5th bit in byte to true, so the byte value will equals 8
                }
                return new BitArray(paddedMessage);
            }
            else
            {
                int remainderBits = 64 - (message.Length % 64);
                int remainderBytesNumber = (remainderBits) / 8;
                var binaryRepresentation = new BitArray(new byte[] {(byte)remainderBytesNumber});
                BitArray paddedMessage = new BitArray(new bool[message.Length + remainderBits]);
                message.CopyTo(paddedMessage, 0);
                for (int i = message.Length; i < message.Length + remainderBits; i += 8)
                {
                    binaryRepresentation.CopyTo(paddedMessage, i);
                }
                return new BitArray(paddedMessage);
            }
        }

        public BitArray RemovePadding(BitArray message)
        {
            throw new NotImplementedException();
        }
    }
}
