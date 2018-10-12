using DESAlgorithm.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithm.Tests.ExtensionsTests
{
    public class BoolExtensionsTests
    {
        [TestCase(false, 0)]
        [TestCase(true, 1)]
        public void ToInt_Test(bool value, int expectedValue)
        {
            value.ToInt().Should().Be(expectedValue);
        }


        [Test]
        public void GetByteValue()
        {
            bool[] array = new bool[]
            {
                true, false, true, false, true, true, false, true
            };

            array.GetByteValue().Should().Be(173);
        }
    }
}
