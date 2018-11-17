using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

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
                var key = keyGen.Generate(136, 512, 160);

                if (((key.P - BigInteger.One) % key.Q) == BigInteger.Zero)
                    OkCounter++;

            }
            //var key = keyGen.GenerateKeysProbablePrimes_FIPS(136, 512, 160);

            Assert.AreEqual(howManyShouldBeOk, OkCounter);
        }

    }
}