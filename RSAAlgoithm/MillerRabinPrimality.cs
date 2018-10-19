namespace RSAAlgoithm
{
    public static class MillerRabinPrimality
    {
        public static bool IsProbablePrime(this BigInt source, int certainty)
        {
            if (source?.Value == null || source.Value.Length == 0)
            {
                throw new ValidationException("Number to test primality cannot be null or zero");
            }

            if (source == new[]{2} || source == new[] {3})
                return true;
            if (source.Value[0] < 2 || BigInt.Mod(source.Value, new [] {2}) == new []{0})
                return false;

            BigInt d = source - 1;
            int s = 0;

            while (d % 2 == 0)
            {
                d /= 2;
                s += 1;
            }

            BigInt a;
            for (int i = 0; i < certainty; i++)
            {
                do
                {
                    a = new BigInt(BigInt.GenerateRandom(source.Value.Length));
                }
                while (a < 2 || a >= source - 2);

                BigInt x = BigInt.ModPow(a, d, source);
                if (x == 1 || x == source - 1)
                    continue;

                for (int r = 1; r < s; r++)
                {
                    x = BigInt.ModPow(x, 2, source);
                    if (x == 1)
                        return false;
                    if (x == source - 1)
                        break;
                }

                if (x != source - 1)
                    return false;
            }

            return true;
        }
    }
}
