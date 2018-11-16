using SchnorDigitalSign.Model;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;

namespace SchnorDigitalSign
{
    public class SchnorrAlgorithm
    {
        //Big Integer problem with byte array length

        public Signature SignMessage(byte[] message, KeyPair keyPair, UserKeys userKeys)
        {
            RNGCryptoServiceProvider randomProvider = new RNGCryptoServiceProvider();
            int numberSmallerThanQLength = (KeyGenerator.QLengthBits - 8) / 8;

            BigInteger r = GenerateR(randomProvider, numberSmallerThanQLength);

            BigInteger x = BigInteger.ModPow(keyPair.a, r, keyPair.p);

            byte[] messageWithSalt = message.Concat(x.ToByteArray()).ToArray();

            //if (messageWithSalt.Length != message.Length + x.ToByteArray().Length)
            //{
            //    throw new Exception("Message with salt length = " + messageWithSalt.Length);
            //}

            BigInteger e;
            using (SHA1 sha1 = new SHA1Cng())
            {
                byte[] byteE = sha1.ComputeHash(messageWithSalt);
                byte[] byteEWithZero = new byte[byteE.Length + 1];
                byteE.CopyTo(byteEWithZero, 0);
                e = new BigInteger(byteEWithZero);
            }

            BigInteger y = (r + (userKeys.PrivateKey * e)) % keyPair.q;

            Signature signature = new Signature()
            {
                e = e,
                y = y
            };

            return signature;
        }

        public static BigInteger GenerateR(RNGCryptoServiceProvider randomProvider,  int numberSmallerThanQLength)
        {
            //return new BigInteger(1);
            byte[] byteR = new byte[numberSmallerThanQLength];
            randomProvider.GetBytes(byteR);
            byte[] byteRZero = new byte[byteR.Length + 1];
            byteR.CopyTo(byteRZero, 0);

            return new BigInteger(byteRZero);
        }

        public bool Verify(byte[] message, KeyPair keyPair, Signature signature, BigInteger senderPublicKey)
        {
            BigInteger x = BigInteger.ModPow(keyPair.a, signature.y, keyPair.p) * BigInteger.ModPow(senderPublicKey.ModInv(keyPair.p), signature.e, keyPair.p);
            x = x % keyPair.p;

            byte[] messageWithSalt = message.Concat(x.ToByteArray()).ToArray();

            BigInteger resultE;
            using (SHA1 sha1 = new SHA1Cng())
            {
                byte[] byteE = sha1.ComputeHash(messageWithSalt);
                byte[] byteEWithZero = new byte[byteE.Length + 1];
                byteE.CopyTo(byteEWithZero, 0);
                resultE = new BigInteger(byteEWithZero);
            }

            if (AreEqual(resultE.ToByteArray(), signature.e.ToByteArray()))
                return true;
            else
                return false;

        }

        public bool AreEqual(byte[] first, byte[] second)
        {
            if (first.Length != second.Length)
                return false;

            for (int i = 0; i < first.Length; i++)
            {
                if (first[i] != second[i])
                    return false;
            }

            return true;
        }

    }
}
