using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using RSAAlgorithm;

namespace Algorithm.Tests.BigInt_Test
{
    class AddTest
    {
        [Test]
        public void SameLength_ResultShouldBeOneDigitLonger_WithCarry()
        {
            int[] first = {9, 9, 9 };
            int[] second = {9,9,9};
            
            int[] result = BigInt.Add(first, second);

            Assert.AreEqual(new int[]{8,9,9,1}, result);
        }

        [Test]
        public void SameLength_NoCarry()
        {
            int[] first = { 8, 2, 1 };
            int[] second = { 1, 2, 3 };

            int[] result = BigInt.Add(first, second);

            Assert.AreEqual(new int[] { 9,4,4 }, result);
        }

        [Test]
        public void FirstLonger_WithCarry()
        {
            int[] first = {9, 8, 2, 1 };
            int[] second = { 5, 2, 3 };

            int[] result = BigInt.Add(first, second);

            Assert.AreEqual(new int[] { 4, 1, 6,1 }, result);
        }

        [Test]
        public void FirstLonger_NoCarry()
        {
            int[] first = { 7, 8, 2, 1 };
            int[] second = { 1, 1, 1 };

            int[] result = BigInt.Add(first, second);

            Assert.AreEqual(new int[] { 8, 9, 3, 1 }, result);
        }

        [Test]
        public void SecondLonger_WithCarry()
        {
            int[] second = { 9, 8, 2, 1 };
            int[] first = { 5, 2, 3 };

            int[] result = BigInt.Add(first, second);

            Assert.AreEqual(new int[] { 4, 1, 6, 1 }, result);
        }

        [Test]
        public void SecondLonger_NoCarry()
        {
            int[] second = { 7, 8, 2, 1 };
            int[] first = { 1, 1, 1 };

            int[] result = BigInt.Add(first, second);

            Assert.AreEqual(new int[] { 8, 9, 3, 1 }, result);
        }

        [Test]
        public void SecondspamLonger_NoCarry()
        {
            int[] second = { 0,0,3,6,9 };
            int[] first = { 1,4,7,6 };

            int[] result = BigInt.Add(first, second);

            Assert.AreEqual(new int[] { 1,4,0,3,0,1 }, result);
        }
    }
}
