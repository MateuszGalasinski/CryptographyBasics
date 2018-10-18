using NUnit.Framework;
using RSAAlgoithm;

namespace Algorithm.Tests.BigInt_Test
{
    class Multiply_Test
    {
        [Test]
        public void MultiplyTest()
        {
            int[] first = {9, 9, 9};
            int[] second = {9, 9, 9};
            int[] result = BigInt.Multiply(first, second);
        }
    }
}