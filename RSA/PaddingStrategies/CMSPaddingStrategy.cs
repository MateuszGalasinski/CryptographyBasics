using System;
using System.Linq;

namespace RSA.PaddingStrategies
{
    public class CMSPaddingStrategy : IPaddingStrategy
    {
        public byte[] AddPadding(byte[] message, int blockLength)
        {
            if (blockLength > 255)
            {
                throw new ArgumentException($"block length has to be shorter than 255, but was {blockLength}");
            }

            if (message.Length % blockLength == 0)
            {
                byte[] paddedMessage = new byte[message.Length + blockLength];
                message.CopyTo(paddedMessage, 0);
                for (int i = message.Length; i < message.Length + blockLength; i++)
                {
                    paddedMessage[i] = (byte)blockLength; 
                }
                return paddedMessage;
            }
            else
            {
                int remainderBytes = blockLength - (message.Length % blockLength);
                byte[] paddedMessage = new byte[message.Length + remainderBytes];

                message.CopyTo(paddedMessage, 0); //copy message content

                for (int i = message.Length; i < message.Length + remainderBytes; i++)
                {
                    paddedMessage[i] = (byte) remainderBytes;
                }

                return paddedMessage;
            }
        }

        public byte[] RemovePadding(byte[] message, int keyLength)
        {
            //find last byte
            byte lastByte = message.Last();

            if (lastByte > message.Length)
            {
                throw new ArgumentException("Message is shorter than padding length taken from last byte.");
            }

            //remove correct number of bytes (just dont copy them)
            byte[] messageWithoutPadding = new byte[message.Length - lastByte];

            Array.Copy(message, 0, messageWithoutPadding, 0, messageWithoutPadding.Length);

            return messageWithoutPadding;
        }
    }
}
