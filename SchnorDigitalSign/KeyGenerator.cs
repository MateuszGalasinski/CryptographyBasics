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
    public class KeyGenerator
    {
        public int QLength = 136/8;
        public int PLength = 512/8;

        BigInteger newInt = new BigInteger();


        public KeyPair GenerateKeys()
        {

            BigInteger tempP = GeneratePrimeNumber(PLength);
            BigInteger tempQ;
            do
            {
                tempQ = GeneratePrimeNumber(QLength);

            } while ((tempP - 1) % tempQ != 0);

            KeyPair keyPair = new KeyPair()
            {
                q = GeneratePrimeNumber(QLength),
                p = GeneratePrimeNumber(PLength)
            };

            return keyPair;
        }

        public BigInteger GeneratePrimeNumber(int numberLength)
        {
            BigInteger result;

            do
            {
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                byte[] randomNumber = new byte[numberLength];
                rng.GetBytes(randomNumber);
                result = new BigInteger(randomNumber);
                result = BigInteger.Abs(result);
            } while (MillerRabin(result, 20) != 1);


            return result;
        }


        private int MillerRabin(BigInteger n, int reps)
        {
            BigInteger k, q, x, y;
            BigInteger nm1 = n - 1;

            /// Perform a Fermat test. 210 = 2*3*5*7, magic ???
           // y = BigInteger.ModPow(210, nm1, n);
            //if (y != 1) return 0; /// n is not prime.

            /// Find q and k, where q is odd and n - 1 = 2^k * q.
            for (k = 0, q = nm1; q.IsEven; k++, q >>= 1) ;

            int is_prime = 1;
            for (int r = 0; (r < reps) && (is_prime > 0); r++)
            {
                x = GenerateRandomBigInteger(1, nm1);
                is_prime = MillerRabinInner(n, x, q, k);
            }
            return is_prime;
        }

        private int MillerRabinInner(BigInteger n, BigInteger x, BigInteger q, BigInteger k)
        {
            BigInteger nm1 = n - 1;

            BigInteger y = BigInteger.ModPow(x, q, n);

            if (y == 1 || y == nm1)
                return 1;

            for (BigInteger i = 1; i < k; i++)
            {
                y = BigInteger.ModPow(y, 2, n);
                if (y == nm1)
                    return 1;
                if (y == 1)
                    return 0;
            }
            return 0;
        }

        public static BigInteger GenerateRandomBigInteger(BigInteger min, BigInteger max)
        {
            Random random = new Random();
            byte[] bytes = max.ToByteArray();

            random.NextBytes(bytes);
            bytes[bytes.Length - 1] &= 0x7F; /// force sign bit to positive
            BigInteger value = new BigInteger(bytes);
            BigInteger result = (value % (max - min + 1)) + min;

            return result;
        }


    }
}
