using System;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchnorDigitalSign;
using SchnorDigitalSign.Model;

namespace SchnorrTests
{
    [TestClass]
    public class WholeOperationTest
    {



        [TestMethod]
        public void SignAndVerify_Test()
        {
            KeyGenerator keyGen = new KeyGenerator();

            KeyPair keyPair = keyGen.Generate(136, 512, 160);

            byte[] message = new byte[]
            {
                0x0A, 0x0B, 0x0C, 0x0D, 0x0A, 0x0B, 0x0C,
                0x0A, 0x0B, 0x0C, 0x0D, 0x0A,
            };

            SchnorrAlgorithm schnorrAlgorithm = new SchnorrAlgorithm();

            UserKeys userKeys = UserKeyGenerator.Generate(keyPair);

            Signature signature = schnorrAlgorithm.SignMessage(message, keyPair, userKeys);

            bool isValid = schnorrAlgorithm.Verify(message, keyPair, signature, userKeys.PublicKey);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void GenerateR_Test()
        {
            int howManyShouldBeOk = 10;
            int OkCounter = 0;

            for (int i = 0; i < howManyShouldBeOk; i++)
            {
                var byteLength = (KeyGenerator.QLengthBits - 8) / 8;
                var r = SchnorrAlgorithm.GenerateR(new RNGCryptoServiceProvider(), byteLength );

                if (r.ToByteArray().Length == byteLength)
                    OkCounter++;
            }

            Assert.AreEqual(howManyShouldBeOk, OkCounter);
        }

    }
}
