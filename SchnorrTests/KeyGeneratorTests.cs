using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchnorDigitalSign;
using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
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
            var key = keyGen.GenerateKeysProbablePrimes_Book(136, 512, 160);
            //var key = keyGen.GenerateKeysProbablePrimes_FIPS(136, 512, 160);

            var plen = key.p;
            var qlen = key.q;

            var c = 1;
            //Debug.WriteLine(key.p.ToString());
            //Debug.WriteLine(key.q.ToString());

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