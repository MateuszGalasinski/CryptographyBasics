using RSAAlgoithm.Constants;
using RSAAlgoithm.Models;
using RSAAlgorithm;
using System.Numerics;
using System.Security.Cryptography;

namespace RSAAlgoithm
{
    public class KeyGenerator
    {
        //private const int keyLengthInBytes = 64;
        public Key Generate()
        {
            BigInteger p, q;
            BigInteger eulerResult;

            Key key = new Key();

            do
            {
                p = generateBigNumber();
            }while (MillerRabinPrimality.IsProbablePrime(p, 40)) ;

            do
            {
                q = generateBigNumber();
            } while (MillerRabinPrimality.IsProbablePrime(p, 40));

            key.PublicKey.N = p * q;

            eulerResult = EulerFunction(p,q);

            do
            {
                key.PublicKey.E = generateBigNumber();

            } while (NWD(key.PublicKey.E, eulerResult));

            key.PrivateKey.D = (1 / key.PublicKey.E) % eulerResult;

            return key;

        }

        public BigInteger EulerFunction(BigInteger p, BigInteger q)          //euler function: f(n) = (p-1)(q-1)
        {
            BigInteger m = p - 1;
            BigInteger n = q - 1;
            return m * n;
        }

        private BigInteger generateBigNumber()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] randomNumber = new byte[KeyConstants.KeySize];
            rng.GetBytes(randomNumber);
            //BigInteger result = new BigInteger(randomNumber);
            return BigInteger.Abs(new BigInteger(randomNumber));
        }

        public bool NWD(BigInteger a, BigInteger b)
        {
            BigInteger c = 0;
            while (b != 0)
            {
                c = b;
                b = a % b;
                a = c;
            }
            
            if(a == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
