using System;
using System.Collections.Generic;
using System.Text;
using DES.AlgorithmBuilders;
using NUnit.Framework;

namespace CryptoTests.Given_DESBuilder
{
    [TestFixture]
    public class GivenDESBuilder
    {
        protected DESBuilder context;
        
        [SetUp]
        public void Given()
        {
            context = new DESBuilder();
        }

        public void With_EncryptPermutation()
        {
            context.AddEncryptPermutation();
        }
    }
}
