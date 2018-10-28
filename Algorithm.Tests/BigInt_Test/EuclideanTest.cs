using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using RSAAlgorithm;

namespace Algorithm.Tests.BigInt_Test
{
    class EuclideanTest
    {
        [Test]
        public void NWD_TwoFirstNumbers_ShouldBeOk()
        {
            int[] first = {5, 4};
            int[] second = {0, 6};
            int[] expected = {5, 1};
            EuclideanAlgorithm.NWD(first, second).Should().BeEquivalentTo(expected);
        }
    }
}
