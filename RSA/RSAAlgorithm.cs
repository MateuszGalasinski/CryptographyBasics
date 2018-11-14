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
        public static int /*BlockSize {get; set;}*/ BlockSize => NumberOfBytes/2;

        public static BigInteger Encrypt(BigInteger data, BigInteger e, BigInteger n) => data.modPow(e, n);

        public static BigInteger Decrypt(BigInteger data, BigInteger d, BigInteger n) => data.modPow(d, n);

        public static FullKey GenerateKey()
        {
            Random rand;
            BigInteger p;
            BigInteger q;
            BigInteger modulus;

            BigInteger e;

            BigInteger d;

            BigInteger n;

            int NLength;

            //do
            //{
                rand = new Random();
                 p = BigInteger.genPseudoPrime(NumberOfBits / 2, Confidence, rand);
                 q = BigInteger.genPseudoPrime(NumberOfBits / 2, Confidence, rand);
                 modulus = (p - BigOne) * (q - BigOne);

                 e = (modulus).genCoPrime(NumberOfBits, rand);

                 d = e.modInverse(modulus);

                 n = p * q;

                NLength = n.ToString().Length;
              //  if (NLength == 308)
                //    Console.WriteLine();

            //} while (NLength != 309);
            

            return new FullKey()
            {
                E = e,
                D = d,
                N = n
            };

            //BlockSize = n.dataLength;
        }
    }
}
