using SchnorDigitalSign.Model;
using System.Numerics;
using System.Security.Cryptography;

namespace SchnorDigitalSign
{
    public class UserKeyGenerator
    {
        public static UserKeys Generate(SystemKeys keyPair)
        {
            UserKeys userKeys = new UserKeys();
            RNGCryptoServiceProvider randomProvider = new RNGCryptoServiceProvider();
            int numberSmallerThanQLength = (KeyGenerator.QLengthBits - 8)/8;

            byte[] bytePrivateKey = new byte[numberSmallerThanQLength];
            randomProvider.GetBytes(bytePrivateKey);
            byte[] bytePrivateKeyZero = new byte[bytePrivateKey.Length + 1];
            bytePrivateKey.CopyTo(bytePrivateKeyZero, 0);
            userKeys.PrivateKey = new BigInteger(bytePrivateKeyZero) % keyPair.Q;
            userKeys.PrivateKey = new BigInteger(1);
            userKeys.PublicKey = BigInteger.ModPow(keyPair.A, userKeys.PrivateKey.ModInv(keyPair.P), keyPair.P);

            return userKeys;
        }
    }
}
