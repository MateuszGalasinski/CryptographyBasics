using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SchnorDigitalSign.Model;

namespace SchnorDigitalSign
{
    public class SchnorrAlgorithm
    {
        private UserKeys userKeys;

        public Signature SignMessage(byte[] message, KeyPair keyPair)
        {
            RNGCryptoServiceProvider randomProvider = new RNGCryptoServiceProvider();
            int numberSmallerThanQ = KeyGenerator.QLengthBits - 8;

            byte[] byteR = new byte[numberSmallerThanQ];
            randomProvider.GetBytes(byteR);
            BigInteger r  = new BigInteger(byteR);

            BigInteger a = new BigInteger(); // implement

            BigInteger x = BigInteger.ModPow(a, r, keyPair.p);

            BigInteger e;

            byte[] messageWithSalt = message.Concat(x.ToByteArray()).ToArray();

            using (SHA1 sha1 = new SHA1Cng())
            {
                byte[] byteE = sha1.ComputeHash(messageWithSalt);
                byte[] byteEWithZero = new byte[byteE.Length + 1];
                byteE.CopyTo(byteEWithZero, 0);
                e = new BigInteger(byteEWithZero);
            }

            // public and private key
            byte[] bytePrivateKey = new byte[numberSmallerThanQ];
            randomProvider.GetBytes(bytePrivateKey);

            userKeys.PrivateKey = new BigInteger(bytePrivateKey);
            userKeys.PublicKey = BigInteger.ModPow(a, BigInteger.MinusOne * userKeys.PrivateKey, keyPair.p);

            BigInteger y = (r + (userKeys.PrivateKey * e)) % keyPair.q;

            Signature signature = new Signature()
            {
                e = e,
                y = y
            };

            return signature;
        }

        public bool Verify(byte[] message, KeyPair keyPair, Signature signature, BigInteger SenderPublicKey)
        {
            return false;
        }
    }
}
