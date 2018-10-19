using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using RSAAlgoithm;

namespace Algorithm.Tests.BigInt_Test
{
    class OperatorTest
    {
        [Test]
        public void EqualsOperator_EqualNumbers()
        {
            BigInt first = new BigInt(new int[] { 9, 9, 9 });
            BigInt second = new BigInt(new int[] { 9, 9, 9 });

            (first == second).Should().Be(true);
        }

        [Test]
        public void EqualsOperator_DifferentSize()
        {
            BigInt first = new BigInt(new int[] { 9, 9, 9 });
            BigInt second = new BigInt(new int[] { 9 });

            (first == second).Should().Be(false);
        }

        [Test]
        public void EqualsOperator_BothZero()
        {
            BigInt first = new BigInt(new int[] {0});
            BigInt second = new BigInt(new int[] {0});

            (first == second).Should().Be(true);
        }

        [Test]
        public void EqualsOperator_NotEqualNumbers()
        {
            BigInt first = new BigInt(new int[] { 9, 9, 9 });
            BigInt second = new BigInt(new int[] { 9, 1, 9 });

            (first == second).Should().Be(false);
        }

        [Test]
        public void UnEqualsOperator_EqualNumbers()
        {
            BigInt first = new BigInt(new int[] { 9, 9, 9 });
            BigInt second = new BigInt(new int[] { 9, 9, 9 });

            (first != second).Should().Be(false);
        }

        [Test]
        public void UnEqualsOperator_DifferentSize()
        {
            BigInt first = new BigInt(new int[] { 9, 9, 9 });
            BigInt second = new BigInt(new int[] { 9 });

            (first != second).Should().Be(true);
        }

        [Test]
        public void UnEqualsOperator_BothZero()
        {
            BigInt first = new BigInt(new int[] { 0 });
            BigInt second = new BigInt(new int[] { 0 });

            (first != second).Should().Be(false);
        }

        [Test]
        public void UnEqualsOperator_NotEqualNumbers()
        {
            BigInt first = new BigInt(new int[] { 9, 9, 9 });
            BigInt second = new BigInt(new int[] { 9, 1, 9 });

            (first != second).Should().Be(true);
        }
    }
}
