//namespace RSAAlgoithm
//{
//    public static class MillerRabinPrimality
//    {
//        private static BigInt _zero = new BigInt(new int[] { 0 });
//        private static BigInt _one = new BigInt(new int[] { 1 });
//        private static BigInt _two = new BigInt(new int[] { 2 });
//        private static BigInt _three = new BigInt(new int[] { 3 });

//        public static bool IsProbablePrime(this BigInt source, int certainty)
//        {
//            //helper variables (ups!)
//            BigInt sourceMinusOne = source.Copy();
//            sourceMinusOne.Substract(_one);
//            BigInt sourceMinusTwo = source.Copy();
//            sourceMinusOne.Substract(_two);
//            //

//            if (source?.Value == null || source.Value.Length == 0)
//            {
//                throw new ValidationException("Number to test primality cannot be null or zero");
//            }

//            if (source == _two || source == _three)
//                return true;
//            if (source.Value[0] < 2 || BigInt.Mod(source.Value, new[] { 2 }) == new[] { 0 })
//                return false;

//            BigInt d = source.Copy();
//            d.Substract(_one); //d-=1
//            int s = 0;

//            while (d.Mod(_two) == _zero)
//            {
//                d /= _two;
//                s += 1;
//            }

//            BigInt a;
//            for (int i = 0; i < certainty; i++)
//            {
//                do
//                {
//                    a = new BigInt(BigInt.GenerateRandom(source.Value.Length));
//                }
//                while (a < _two || a >= sourceMinusTwo);

//                BigInt x = BigInt.ModPow(a, d, source);
//                if (x == _one || x == sourceMinusOne)
//                    continue;

//                for (int r = 1; r < s; r++)
//                {
//                    x = BigInt.ModPow(x, 2, source);
//                    if (x == _one)
//                        return false;
//                    if (x == sourceMinusOne)
//                        break;
//                }

//                if (x != sourceMinusOne)
//                    return false;
//            }

//            return true;
//        }
//    }
//}
