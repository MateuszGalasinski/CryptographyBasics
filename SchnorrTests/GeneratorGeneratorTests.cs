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
            int howManyShouldBeOk = 10;
            int OkCounter = 0;

            SystemKeys baseKeys = _keyGenerator.Generate(136, 512, 160);

            for (int i = 0; i < howManyShouldBeOk; i++)
            {
                BigInteger generator = GeneratorGenerator.Generate(baseKeys.P, baseKeys.Q);

                OkCounter += BigInteger.ModPow(generator, baseKeys.Q, baseKeys.P) == BigInteger.One ? 1 : 0;
            }

            Assert.AreEqual(howManyShouldBeOk, OkCounter);
        }
    }
}
