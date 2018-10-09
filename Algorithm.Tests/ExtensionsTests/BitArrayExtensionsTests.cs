using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DESAlgorithm.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithm.Tests.ExtensionsTests
{
    public class BitArrayExtensionsTests
    {
        [Test]
        public void RevertSingleByteArray()
        {
            BitArray array = new BitArray(new bool[]
            {
                false, false, true, false, true, true, false, true
            });

            array.Revert().Should().BeEquivalentTo(new BitArray(new bool[]
            {
                true, false, true, true, false, true, false, false
            }));
        }
    }
}
