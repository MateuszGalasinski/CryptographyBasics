using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SchnorDigitalSign.Model;

namespace SchnorDigitalSign
{
    public class UserKeyGenerator
    {
        public static UserKeys Generate(KeyPair keyPair)
        {
            UserKeys userKeys = new UserKeys();
            RNGCryptoServiceProvider randomProvider = new RNGCryptoServiceProvider();
            int numberSmallerThanQ = (KeyGenerator.QLengthBits - 8)/8;

            byte[] bytePrivateKey = new byte[numberSmallerThanQ];
            randomProvider.GetBytes(bytePrivateKey);
            byte[] bytePrivateKeyZero = new byte[bytePrivateKey.Length + 1];
            bytePrivateKey.CopyTo(bytePrivateKeyZero, 0);
            userKeys.PrivateKey = new BigInteger(bytePrivateKeyZero);
            BigInteger exponent = BigInteger.ModPow(userKeys.PrivateKey, keyPair.p - 2, keyPair.p);
            userKeys.PublicKey = BigInteger.ModPow(keyPair.a, exponent, keyPair.p);

            return userKeys;
        }
    }
}
