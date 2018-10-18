using DESAlgorithm.PaddingStrategies;
using NUnit.Framework;

namespace Algorithm.Tests.Given_IPaddingStrategy
{
    [TestFixture]
    public class GivenPaddingStrategy
    {
        protected IPaddingStrategy context;

        public void With_CMSPaddingStrategy()
        {
            context = new CMSPaddingStrategy();
        }
    }
}