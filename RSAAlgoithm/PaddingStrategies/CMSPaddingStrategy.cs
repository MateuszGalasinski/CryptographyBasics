using System;
using System.Collections;


namespace RSAAlgoithm
{
    public class CMSPaddingStrategy : IPaddingStrategy
    {
        public bool[] AddPadding(bool[] message)
        {
            //if (message.Length % 8 != 0)
            //{
            //    throw new ValidationException(
            //        $"Message does not represent bytes. {message.Length} is not divisible by 8.");
            //}

            //if (message.Length % 64 == 0)
            //{
            //    bool[] paddedMessage = new bool[message.Length + 64];
            //    message.CopyTo(paddedMessage, 0);
            //    for (int i = message.Length; i < message.Length + 64; i += 8)
            //    {
            //        paddedMessage[i + 4] = true; // set 5th bit in byte to true, so the byte value will equals 8
            //    }

            //    return paddedMessage;
            //}
            //else
            //{
            //    int remainderBits = 64 - message.Length % 64;
            //    int remainderBytesNumber = remainderBits / 8;
            //    bool[] paddedMessage = new bool[message.Length + remainderBits];

            //    message.CopyTo(paddedMessage, 0); //copy message content

            //    //add padding bytes
            //    var binaryRepresentation = new BitArray(new[] {(byte) remainderBytesNumber}).Revert();
            //    for (int i = message.Length; i < message.Length + remainderBits; i += 8)
            //    {
            //        binaryRepresentation.CopyTo(paddedMessage, i);
            //    }

            //    return paddedMessage;
            //}
            return message;
        }

        public bool[] RemovePadding(bool[] message)
        {
            ////find last byte
            //bool[] lastByte = new bool[8];
            //for (int j = 0, i = message.Length - 8; i < message.Length; i++, j++)
            //{
            //    lastByte[j] = message[i];
            //}

            ////gain its value
            //int byteValue = lastByte.GetByteValue();

            //if (byteValue * 8 > message.Length)
            //{
            //    throw new ValidationException("Message is shorter than padding length taken from last byte.");
            //}

            ////remove correct number of bytes (just dont copy them)
            //bool[] messageWithoutPadding = new bool[message.Length - byteValue * 8];
            //Array.Copy(message, 0, messageWithoutPadding, 0, messageWithoutPadding.Length);

            ////for (int i = 0; i < messageWithoutPadding.Length; i++)
            ////{
            ////    messageWithoutPadding[i] = message[i];
            ////}

            //return messageWithoutPadding;
            return message;
        }
    }
}