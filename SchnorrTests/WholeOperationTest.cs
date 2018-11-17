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
            int howManyShouldBeOk = 3;
            int OkCounter = 0;

            for (int i = 0; i < howManyShouldBeOk; i++)
            {
                KeyGenerator keyGen = new KeyGenerator();

                SystemKeys keyPair = keyGen.Generate(136, 512, 160);

                byte[] message = new byte[]
                {
                    0x0A, 0x0B, 0x0C, 0x0D,
                    0x0A, 0x0B, 0x0C, 0x07,
                    0x0A, 0x0B, 0x0C, 0x0D,
                    0x0A, 0x04, 0x0A, 0x04
                };

                //SchnorrAlgorithm schnorrAlgorithm = new SchnorrAlgorithm();

                UserKeys userKeys = UserKeyGenerator.Generate(keyPair);

                Signature signature = SchnorrAlgorithm.SignMessage(message, keyPair, userKeys);

                bool isValid = SchnorrAlgorithm.Verify(message, keyPair, signature, userKeys.PublicKey);
                if (isValid)
                {
                    OkCounter++;
                }
            }

            Assert.AreEqual(howManyShouldBeOk, OkCounter);
        }

        //[TestMethod]
        //public void GenerateR_Test()
        //{
        //    int howManyShouldBeOk = 10;
        //    int OkCounter = 0;

        //    for (int i = 0; i < howManyShouldBeOk; i++)
        //    {
        //        var byteLength = (KeyGenerator.QLengthBits - 8) / 8;
        //        var r = SchnorrAlgorithm.GenerateR(new RNGCryptoServiceProvider(), byteLength );

        //        if (r.ToByteArray().Length == byteLength)
        //            OkCounter++;
        //    }

        //    Assert.AreEqual(howManyShouldBeOk, OkCounter);
        //}
    }
}
