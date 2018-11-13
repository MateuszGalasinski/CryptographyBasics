using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchnorDigitalSign;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchnorDigitalSign.Tests
{
    [TestClass()]
    public class KeyGeneratorTests
    {
        [TestMethod()]
        public void GenerateKeysKeyGeneratorTest()
        {
            KeyGenerator keyGen = new KeyGenerator();
            var key = keyGen.GenerateKeys();

            var c = 1;
            Debug.WriteLine(key.p.ToString());
            Debug.WriteLine(key.q.ToString());

        }

        [TestMethod()]
        public void GeneratePrimeNumberKeyGeneratorTest()
        {
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void GenerateRandomBigIntegerKeyGeneratorTest()
        {
            Assert.IsTrue(true);
        }
    }
}