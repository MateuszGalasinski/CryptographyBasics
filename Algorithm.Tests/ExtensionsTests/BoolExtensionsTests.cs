using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DESAlgorithm.Extensions;
using FluentAssertions;

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
    }
}
