//using RSAAlgoithm.Exceptions;
//using RSAAlgoithm.Extensions;
//using System;
//using System.Collections;

//namespace RSAAlgorithm
//{
//    public class CMSPaddingStrategy : IPaddingStrategy
//    {
//        public bool[] AddPadding(bool[] message, )
//        {
//            if (message.Length % 64 == 0)
//            {
//                bool[] paddedMessage = new bool[message.Length + 64];
//                message.CopyTo(paddedMessage, 0);
//                for (int i = message.Length; i < message.Length + 64; i += 8)
//                {
//                    paddedMessage[i + 4] = true; // set 5th bit in byte to true, so the byte value will equals 8
//                }

//                return paddedMessage;
//            }
//            else
//            {
//                int remainderBits = 64 - message.Length % 64;
//                int remainderBytesNumber = remainderBits / 8;
//                bool[] paddedMessage = new bool[message.Length + remainderBits];

//                message.CopyTo(paddedMessage, 0); //copy message content

//                var binaryRepresentation = new BitArray(new[] { (byte)remainderBytesNumber }).Revert();
//                for (int i = message.Length; i < message.Length + remainderBits; i += 8)
//                {
//                    binaryRepresentation.CopyTo(paddedMessage, i);
//                }

//                return paddedMessage;
//            }
//            return message;
//        }

//        public bool[] RemovePadding(bool[] message)
//        {
//            bool[] lastByte = new bool[8];
//            for (int j = 0, i = message.Length - 8; i < message.Length; i++, j++)
//            {
//                lastByte[j] = message[i];
//            }

//            int byteValue = lastByte.GetByteValue();

//            if (byteValue * 8 > message.Length)
//            {
//                throw new ValidationException("Message is shorter than padding length taken from last byte.");
//            }

//            bool[] messageWithoutPadding = new bool[message.Length - byteValue * 8];
//            Array.Copy(message, 0, messageWithoutPadding, 0, messageWithoutPadding.Length);

//            for (int i = 0; i < messageWithoutPadding.Length; i++)
//            {
//                messageWithoutPadding[i] = message[i];
//            }

//            return messageWithoutPadding;
//            return message;
//        }
//    }
//}