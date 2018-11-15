using System.Numerics;

namespace SchnorDigitalSign
{
    public static class GeneratorGenerator
    {
        public static BigInteger Generate(BigInteger p, BigInteger q)
        {
            BigInteger a = new BigInteger();
            for (BigInteger h = 1; h < q; h++)
            {
                BigInteger exponent = (p - 1) / q;
                a = BigInteger.ModPow(h, exponent, p);
                if (a != 1)
                {
                    break;
                }
            }

            return a;
        }
    }
}
