using System.Linq;

namespace RSAAlgoithm
{
    public class EuclideanAlgorithm
    {
        public static int[] NWD(int[] first, int[] second)
        {
            int[] tmp;
            while (second.Sum() != 0)
            {
                tmp = BigInt.Mod(first, second);
                first = second;
                second = tmp;
            }

            return first;
        }
    }
}