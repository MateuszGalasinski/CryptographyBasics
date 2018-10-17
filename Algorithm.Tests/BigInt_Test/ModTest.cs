using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using RSAAlgoithm;

namespace Algorithm.Tests.BigInt_Test
{
    class ModTest
    {
        [Test]
        public void ThreeNines_Test()
        {
            int[] first = { 9, 9, 9, 9, 9, 9, 9 };
            int[] second = { 9, 9, 9 };

            int[] result = BigInt.Mod(first, second);

            Assert.AreEqual(new int[] { 1 }, result);
        }
    }
}
