using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchnorDigitalSign;
using SchnorDigitalSign.Model;
using System.Numerics;

namespace SchnorrTests
{
    [TestClass]
    public class BigIntegerExtensionsTest
    {
        private KeyGenerator _keyGenerator;

        [TestInitialize]
        public void Init()
        {
            _keyGenerator = new KeyGenerator();
        }

        [TestMethod]
        public void ModInvTest()
        {
            int howManyShouldBeOk = 10;
            int OkCounter = 0;


            for (int i = 0; i < howManyShouldBeOk; i++)
            {
                SystemKeys keys = _keyGenerator.Generate(136, 512, 160);
                BigInteger inverse = keys.A.ModInv(keys.P);
                BigInteger result = (inverse * keys.A) % keys.P;
                if (result == BigInteger.One)
                {
                    OkCounter++;
                }
            }

            Assert.AreEqual(howManyShouldBeOk, OkCounter);
        }
    }
}
