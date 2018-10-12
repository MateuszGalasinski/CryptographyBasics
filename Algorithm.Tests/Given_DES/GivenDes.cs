using System;
using System.Collections;
using DES;
using DES.AlgorithmBuilders;
using NUnit.Framework;

namespace Algorithm.Tests.Given_DES
{
    public class GivenDes
    {
        [TestFixture]
        public class GivenDES
        {
            protected DESBuilder context;
            protected Func<bool[], bool[]> functionToInvoke;

            [SetUp]
            public void Given()
            {
                context = new DESBuilder();
            }

            public void With_WholeDES(BitArray key)
            {
                context.AddWholeDES(key);
            }

            public void With_EncryptDecrypt()
            {
                CryptoAlgorithm algorithm = context.Build();
                functionToInvoke = (data) =>
                {
                    var encrypted = algorithm.Encrypt(data);
                    return algorithm.Decrypt(encrypted);
                };
            }
        }
    }
}
