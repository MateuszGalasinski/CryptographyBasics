using RSA.Models;
using System;

namespace RSA
{
    public static class RSAAlgorithm
    {
        public const int NumberOfBits = 1024;
        public const int Confidence = 40;
        public static readonly BigInteger BigOne = new BigInteger(1);

        public static int NumberOfBytes => NumberOfBits / 8;
        public static int BlockSize => NumberOfBytes;

        public static BigInteger Encrypt(BigInteger data, BigInteger e, BigInteger n) => data.modPow(e, n);

        public static BigInteger Decrypt(BigInteger data, BigInteger d, BigInteger n) => data.modPow(d, n);

        public static FullKey GenerateKey()
        {
            Random rand = new Random();
            BigInteger p = BigInteger.genPseudoPrime(NumberOfBits/2, Confidence, rand);
            BigInteger q = BigInteger.genPseudoPrime(NumberOfBits/2, Confidence, rand);
            BigInteger modulus = (p - BigOne) * (q - BigOne);

            BigInteger e = (modulus).genCoPrime(NumberOfBits - 8, rand);

            while (e % 2 == 0 || e >= modulus)
            {
                e = (modulus).genCoPrime(NumberOfBits - 8, rand);
            }

            BigInteger d = e.modInverse(modulus);

            BigInteger n = p * q;

            return new FullKey()
            {
                E = e,
                D = d,
                N = n
            };
        }
    }
}
