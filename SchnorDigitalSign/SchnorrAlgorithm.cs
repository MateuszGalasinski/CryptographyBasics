using SchnorDigitalSign.Model;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;

namespace SchnorDigitalSign
{
    public class SchnorrAlgorithm
    {
        public static Signature SignMessage(byte[] message, SystemKeys keyPair, UserKeys userKeys)
        {
            RNGCryptoServiceProvider randomProvider = new RNGCryptoServiceProvider();
            int numberSmallerThanQLength = (KeyGenerator.QLengthBits - 8) / 8;

            BigInteger r = GenerateR(randomProvider, numberSmallerThanQLength);

            BigInteger x = BigInteger.ModPow(keyPair.A, r, keyPair.P);

            byte[] messageWithSalt = message.Concat(x.ToByteArray()).ToArray();

            BigInteger e;
            using (SHA1 sha1 = new SHA1Cng())
            {
                byte[] byteE = sha1.ComputeHash(messageWithSalt);
                byte[] byteEWithZero = new byte[byteE.Length + 1];
                byteE.CopyTo(byteEWithZero, 0);
                e = new BigInteger(byteEWithZero);
            }

            BigInteger y = (r + (userKeys.PrivateKey * e)) % keyPair.Q;

            Signature signature = new Signature()
            {
                e = e,
                y = y
            };

            return signature;
        }

        public static BigInteger GenerateR(RNGCryptoServiceProvider randomProvider,  int numberSmallerThanQLength)
        {
            byte[] byteR = new byte[numberSmallerThanQLength];
            randomProvider.GetBytes(byteR);
            byte[] byteRZero = new byte[byteR.Length + 1];
            byteR.CopyTo(byteRZero, 0);

            return new BigInteger(byteRZero);
        }

        public static bool Verify(byte[] message, SystemKeys keyPair, Signature signature, BigInteger senderPublicKey)
        {
            BigInteger x = BigInteger.ModPow(keyPair.A, signature.y, keyPair.P) * BigInteger.ModPow(senderPublicKey.ModInv(keyPair.P), signature.e, keyPair.P);
            x = x % keyPair.P;

            byte[] messageWithSalt = message.Concat(x.ToByteArray()).ToArray();

            BigInteger resultE;
            using (SHA1 sha1 = new SHA1Cng())
            {
                byte[] byteE = sha1.ComputeHash(messageWithSalt);
                byte[] byteEWithZero = new byte[byteE.Length + 1];
                byteE.CopyTo(byteEWithZero, 0);
                resultE = new BigInteger(byteEWithZero);
            }

            return AreEqual(resultE.ToByteArray(), signature.e.ToByteArray());

        }

        public static bool AreEqual(byte[] first, byte[] second)
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
