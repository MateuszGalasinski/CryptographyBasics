using NUnit.Framework;
using RSAAlgoithm;

namespace Algorithm.Tests.BigInt_Test
{
    class AddTest
    {
        [Test]
        public void ThreeNines_Test()
        {
            int[] first = {9, 9, 9};
            int[] second = {9, 9, 9};

            int[] result = BigInt.Add(first, second);

            Assert.AreEqual(new[] {1, 9, 9}, result);
        }
    }
}