using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public int QLengthBits = 136;
        public int PLengthBits = 512;
        
        public int QLengthBytes;
        public int PLengthBytes;

        private int alfa;

        private static RNGCryptoServiceProvider rndProvider = new RNGCryptoServiceProvider();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="N">The desired length of the prime q</param>
        /// <param name="L">The desired length of the prime p</param>
        /// <param name="seedlen">The desired length of the domain parameter seed</param>
        /// <returns></returns>
        public KeyPair GenerateKeysProbablePrimes(int N, int L, int seedlen)
        {
            //N and L have to be acceptable

            if (seedlen < N)
                throw new ArgumentException("Seedlen cannot be less then N");
            
            //Bit length of the output block of choosen hash function, shall be equal to or greater than N
            int outlen = 160;

            int n = (int)Math.Ceiling((double)L / outlen) - 1;

            int b = L - 1 - (n * outlen);

            int remainingBits = seedlen % 8;
            byte[] domain_parameter_seed;
            if (remainingBits == 0)
            {
                domain_parameter_seed = new byte[seedlen / 8];
            }
            else
            {
                domain_parameter_seed = new byte[(seedlen / 8) + 1];
            }

            BigInteger q;

            while(true)
            {
                do
                {
                    rndProvider.GetBytes(domain_parameter_seed);

                    if (remainingBits > 0)
                    {
                        byte mask = 0;
                        for (int i = 0; i < remainingBits; i++)
                        {
                            mask |= (byte)(1 << i);
                        }

                        domain_parameter_seed[domain_parameter_seed.Length - 1] &= mask;
                    }

                    domain_parameter_seed[domain_parameter_seed.Length - 1] |= (byte)(1 << (remainingBits > 0 ? remainingBits : 8));

                    //set last bit of domain parameter seed to 1

                    byte[] hashedDomain;
                    using (SHA1Managed sha1 = new SHA1Managed())
                    {
                        hashedDomain = sha1.ComputeHash(domain_parameter_seed);
                    }

                    BigInteger hashedDomainInt = new BigInteger(hashedDomain);

                    BigInteger mod = BigInteger.Pow(2, N - 1);

                    BigInteger U = hashedDomainInt % mod;

                    q = mod + U + 1 - (U % 2);

                } while (MillerRabin(q, 20) == 1);

                int offset = 1;

                BigInteger[] V = new BigInteger[n+1];

                BigInteger mod2 = BigInteger.Pow(2, seedlen);

                //loop 11
                for (int i = 0; i <= (4 * L - 1); i++)
                {
                    for (int j = 0; j <= n; j++)
                    {
                        using (SHA1Managed sha1 = new SHA1Managed())
                        {
                            BigInteger temp = new BigInteger(domain_parameter_seed) + offset + j;
                            V[j] = new BigInteger(sha1.ComputeHash((temp % mod2).ToByteArray()));
                        }
                    }

                    BigInteger W = new BigInteger();

                    for (int j = 0; j < n; j++)
                    {
                        BigInteger exp = BigInteger.Pow(2, j * outlen);
                        W += V[j] * exp;
                    }

                    W += (V[n] % BigInteger.Pow(2, b)) * BigInteger.Pow(2, n * outlen);

                    BigInteger X = new BigInteger();
                    X = W + BigInteger.Pow(2, L - 1);
                    BigInteger c = X % (2 * q);
                    BigInteger p = X - (c - 1);
                    if (p < BigInteger.Pow(2, L - 1))
                    {
                        offset = offset + n + 1;
                    }
                    else
                    {
                        if (MillerRabin(p, 20) == 1)
                        {
                            KeyPair result = new KeyPair()
                            {
                                q = q,
                                p = p
                            };
                            return result;
                        }
                    }
                }
            } 

            //BigInteger domain_parameter_seed = new BigInteger();


        }



        public KeyPair GenerateKeys()
        {
            QLengthBytes = QLengthBits / 8;
            PLengthBytes = PLengthBits / 8;

            alfa = PLengthBits - QLengthBits;

            BigInteger tempP;// = GeneratePrimeNumber(PLengthBytes);
            BigInteger tempQ = GeneratePrimeNumber(QLengthBytes);


            int counter = 0;
            do
            {
                tempQ = GeneratePrimeNumber(QLengthBytes);
                tempP = (alfa * tempQ) + 1;
                
                counter++;

            } while (MillerRabin(tempP, 23) != 1);

            var length = tempP.ToByteArray().Length;

            KeyPair keyPair = new KeyPair()
            {
                q = tempQ,
                p = tempP
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
            y = BigInteger.ModPow(210, nm1, n);
            if (y != 1) return 0; /// n is not prime.

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
