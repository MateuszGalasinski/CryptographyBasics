using SchnorDigitalSign.Model;
using System;
using System.Numerics;
using System.Security.Cryptography;

namespace SchnorDigitalSign
{
    public class KeyGenerator
    {
        public static int QLengthBits { get; set; } = 136;
        public static int PLengthBits { get; set; } = 512;

        private int QLengthBytes;
        private int PLengthBytes;

        private int alfa;

        private static RNGCryptoServiceProvider rndProvider = new RNGCryptoServiceProvider();

        public KeyPair Generate(int N, int L, int seedlen)
        {
            KeyPair key = GenerateKeysProbablePrimes(N, L, seedlen);
            while ((key.p - 1) % key.q != 1) // try again
            {
                key = GenerateKeysProbablePrimes(N, L, seedlen);
            }

            return key;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="N">The desired length of the prime q</param>
        /// <param name="L">The desired length of the prime p</param>
        /// <param name="seedlen">The desired length of the domain parameter seed</param>
        /// <returns></returns>
        private KeyPair GenerateKeysProbablePrimes(int N, int L, int seedlen)
        {
            //N and L have to be acceptable

            if (seedlen < N)
                throw new ArgumentException("Seedlen cannot be less then N");

            //Bit length of the output block of choosen hash function, shall be equal to or greater than N
            int outlen = 160;
            BigInteger outlenPow = BigInteger.Pow(2, seedlen);


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

            bool backToStep1 = true;
            bool backToStep7 = true;

            do
            {
                do
                {
                    q = SetQ(outlenPow, remainingBits, domain_parameter_seed);

                } while (MillerRabin(q, 20) == 1);

                int counter = 0, offset = 2;
                do
                {
                    BigInteger p = SetP(L, outlen, outlenPow, n, b, domain_parameter_seed, q, ref counter, ref offset);

                    if (p < BigInteger.Pow(2, L - 1))
                    {
                        counter++;
                        offset = offset + n + 1;
                        if (counter == 4096)
                        {
                            backToStep1 = true;
                            backToStep7 = false;
                        }
                        else
                        {
                            backToStep1 = false;
                            backToStep7 = true;
                        }
                    }
                    else
                    {
                        if (MillerRabin(p, 20) == 1)
                        {
                            return new KeyPair() {p = p, q = q};
                        }
                        else
                        {
                            counter++;
                            offset = offset + n + 1;
                            if (counter == 4096)
                            {
                                backToStep1 = true;
                                backToStep7 = false;
                            }
                            else
                            {
                                backToStep1 = false;
                                backToStep7 = true;
                            }
                        }
                    }
                } while (backToStep7);

            } while (backToStep1);

            return null;

            //BigInteger domain_parameter_seed = new BigInteger();

        }

        private BigInteger SetQ(BigInteger outlenPow, int remainingBits, byte[] domain_parameter_seed)
        {
            BigInteger q;
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

            domain_parameter_seed[domain_parameter_seed.Length - 1] |= (byte)(1 << (remainingBits > 0 ? remainingBits : 7));

            //set last bit of domain parameter seed to 1

            byte[] hashedDomain;
            byte[] hashedMod;

            byte[] Utmp;
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                hashedDomain = sha1.ComputeHash(domain_parameter_seed);
                byte[] domain_parameter_seed_with_zero = new byte[domain_parameter_seed.Length + 1];
                domain_parameter_seed.CopyTo(domain_parameter_seed_with_zero, 0);
                BigInteger temporary = new BigInteger(domain_parameter_seed_with_zero) + BigInteger.One;
                hashedMod = sha1.ComputeHash((temporary % outlenPow).ToByteArray());

                Utmp = ByteArrayXOR(hashedDomain, hashedMod);
            }

            Utmp[0] |= 1;
            Utmp[Utmp.Length - 1] |= (byte)(1 << 7);
            byte[] U = new byte[Utmp.Length + 1];
            Utmp.CopyTo(U, 0);

            q = new BigInteger(U);
            return q;
        }

        private static BigInteger SetP(int L, int outlen, BigInteger outlenPow, int n, int b, byte[] domain_parameter_seed, BigInteger q, ref int counter, ref int offset)
        {
            BigInteger[] V = new BigInteger[n + 1];


            for (int j = 0; j <= n; j++)
            {
                using (SHA1Managed sha1 = new SHA1Managed())
                {
                    BigInteger temp = new BigInteger(domain_parameter_seed) + new BigInteger(offset) + new BigInteger(j);
                    V[j] = new BigInteger(sha1.ComputeHash((temp % outlenPow).ToByteArray()));
                }
            }

            BigInteger W = new BigInteger();

            for (int j = 0; j < n; j++)
            {
                BigInteger exp = BigInteger.Pow(2, j * outlen);
                W += V[j] * exp;
            }

            W += (V[n] % BigInteger.Pow(2, b)) * BigInteger.Pow(2, n * outlen);

            BigInteger X = W + BigInteger.Pow(2, L - 1);

            BigInteger c = X % (new BigInteger(2) * q);

           return (X - (c - 1));
        }

        public byte[] ByteArrayXOR(byte[] first, byte[] second)
        {
            if(first.Length != second.Length)
                throw new ArgumentException("Arrays should have the same length");

            byte[] result = new byte[first.Length];

            for (int i = 0; i < first.Length; i++)
            {
                result[i] = (byte)(first[i] ^ second[i]);
            }

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
