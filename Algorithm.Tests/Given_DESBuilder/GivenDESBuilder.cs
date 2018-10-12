using System;
using DES.AlgorithmBuilders;
using DESAlgorithm.Models;
using NUnit.Framework;

namespace CryptoTests.Given_DESBuilder
{
    [TestFixture]
    public class GivenDESBuilder
    {
        protected DESBuilder context;
        protected Func<DataSet, DataSet> functionToInvoke;

        [SetUp]
        public void Given()
        {
            context = new DESBuilder();
        }

        public void With_ExtendingPermutation()
        {
            functionToInvoke = context.ExtendingPermutation;
        }

        public void With_SBlocks()
        {
            functionToInvoke = context.SBlocks;
        }

        public void With_PBlockPermutation()
        {
            functionToInvoke = context.PblockPermutation;
        }
    }
}
