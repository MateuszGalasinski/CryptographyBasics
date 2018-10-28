using NUnit.Framework;
using RSAAlgorithm;

namespace Algorithm.Tests.BigInt_Test
{
    class ModTest
    {
        [Test]
        public void ShouldBeNightyNine_Test()
        {
            int[] first = { 9, 9, 9, 9, 9 };
            int[] second = { 9, 9, 9 };

            int[] result = BigInt.Mod(first, second);

            Assert.AreEqual(new int[] { 9,9 }, result);
        }

        [Test]
        public void ShouldBeNine_Test()
        {
            int[] first = {  9, 9, 9, 9,9,9,9 };
            int[] second = { 9, 9, 9 };

            int[] result = BigInt.Mod(first, second);

            Assert.AreEqual(new int[] { 9 }, result);
        }

        [Test]
        public void SameNumbers_ShouldBeZero()
        {
            int[] first = { 9, 9, 9 };
            int[] second = { 9, 9, 9 };

            int[] result = BigInt.Mod(first, second);

            Assert.AreEqual(new int[] { 0 }, result);
        }

        [Test]
        public void FirstShorter_ShouldEqualFirst()
        {
            int[] first = { 9, 9, 9 };
            int[] second = { 9, 9, 9, 9 };

            int[] result = BigInt.Mod(first, second);

            Assert.AreEqual(new int[] { 9,9,9 }, result);
        }

        [Test]
        public void RandomNumbers_Test()
        {
            int[] first = {7,6,5,4,3,2,1};
            int[] second = { 4,3,2,1};

            int[] result = BigInt.Mod(first, second);

            Assert.AreEqual(new int[] { 7,6,5}, result);
        }

        [Test]
        public void ModTwo_Test()
        {
            int[] first = { 7, 6, 5, 4, 3, 2, 1, 2, 1, 1, 1, 1,1 };
            int[] second = { 2 };

            int[] result = BigInt.Mod(first, second);

            Assert.AreEqual(new int[] { 7, 6, 5 }, result);
        }


    }
}