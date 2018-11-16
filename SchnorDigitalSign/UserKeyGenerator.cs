using SchnorDigitalSign.Model;
using System.Numerics;
using System.Security.Cryptography;

namespace SchnorDigitalSign
{
    public class UserKeyGenerator
    {
        public static UserKeys Generate(KeyPair keyPair)
        {
            //return new UserKeys()
            //{
            //    PrivateKey = new BigInteger(1),
            //    PublicKey = new BigInteger(3)
            //};
            UserKeys userKeys = new UserKeys();
            RNGCryptoServiceProvider randomProvider = new RNGCryptoServiceProvider();
            int numberSmallerThanQ = (KeyGenerator.QLengthBits - 8)/8;

            byte[] bytePrivateKey = new byte[numberSmallerThanQ];
            randomProvider.GetBytes(bytePrivateKey);
            byte[] bytePrivateKeyZero = new byte[bytePrivateKey.Length + 1];
            bytePrivateKey.CopyTo(bytePrivateKeyZero, 0);
            userKeys.PrivateKey = new BigInteger(bytePrivateKeyZero) % keyPair.q;
            userKeys.PrivateKey = new BigInteger(1);
            userKeys.PublicKey = BigInteger.ModPow(keyPair.a, userKeys.PrivateKey.ModInv(keyPair.p), keyPair.p);

            return userKeys;
        }
    }
}
