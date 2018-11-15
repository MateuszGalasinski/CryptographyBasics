using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchnorDigitalSign;
using SchnorDigitalSign.Model;
using System.Numerics;

namespace SchnorrTests
{
    [TestClass]
    public class GeneratorGeneratorTests
    {
        private KeyGenerator _keyGenerator;

        [TestInitialize]
        public void Init()
        {
            _keyGenerator = new KeyGenerator();
        }


        [TestMethod]
        public void GenerateKeysKeyGeneratorTest()
        {
            int howManyShouldBeOk = 50;
            int OkCounter = 0;

            KeyPair baseKeys = _keyGenerator.Generate(136, 512, 160);

            for (int i = 0; i < howManyShouldBeOk; i++)
            {
                BigInteger generator = GeneratorGenerator.Generate(baseKeys.p, baseKeys.q);

                OkCounter += BigInteger.ModPow(generator, baseKeys.q, baseKeys.p) == 1 ? 1 : 0;
            }

            Assert.AreEqual(howManyShouldBeOk, OkCounter);
        }
    }
}
