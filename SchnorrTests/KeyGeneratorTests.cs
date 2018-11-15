using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchnorDigitalSign;
using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
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
            int howManyShouldBeOk = 10;
            int OkCounter = 0;

            for (int i = 0; i < howManyShouldBeOk; i++)
            {
                KeyGenerator keyGen = new KeyGenerator();
                var key = keyGen.GenerateKeysProbablePrimes(136, 512, 160);

                if (((key.p - BigInteger.One) % key.q) == 0)
                    OkCounter++;

            }
            //var key = keyGen.GenerateKeysProbablePrimes_FIPS(136, 512, 160);

            Assert.AreEqual(howManyShouldBeOk, OkCounter);
        }

    }
}