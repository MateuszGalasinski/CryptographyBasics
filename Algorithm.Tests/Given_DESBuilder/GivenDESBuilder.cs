using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DES.AlgorithmBuilders;
using DES.Constants;
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

        public void With_ExtendingPermutation()
        {
            context.AddExtendingPermutation();
        }

        public void With_SBlocks()
        {
            context.AddSBlocks();
        }

        public void With_PBlockPermutation()
        {
            context.AddPblockPermutation();
        }

        public void With_WholeDES(BitArray key)
        {
            context.AddWholeDES(key);
        }
    }
}
