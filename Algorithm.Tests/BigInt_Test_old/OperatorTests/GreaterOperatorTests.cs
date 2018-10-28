using FluentAssertions;
using NUnit.Framework;
using RSAAlgorithm;

namespace Algorithm.Tests.BigInt_Test.OperatorTests
{
    class GreaterLessOperatorTests
    {
        [Test]
        public void GreaterLessOperator_EqualNumbers()
        {
            BigInt first = new BigInt(new int[] { 9, 9, 9 });
            BigInt second = new BigInt(new int[] { 9, 9, 9 });

            (first > second).Should().Be(false);
            (first >= second).Should().Be(true);
            (first <= second).Should().Be(true);
            (first < second).Should().Be(false);
        }

        [Test]
        public void GreaterLessOperator_LongerSize()
        {
            BigInt first = new BigInt(new int[] { 9, 9, 9 });
            BigInt second = new BigInt(new int[] { 9 });

            (first > second).Should().Be(true);
            (first >= second).Should().Be(true);
            (first <= second).Should().Be(false);
            (first < second).Should().Be(false);
        }


        [Test]
        public void GreaterLessOperator_ShorterSize()
        {
            BigInt first = new BigInt(new int[] { 9, 9, 9 });
            BigInt second = new BigInt(new int[] { 0, 0, 0, 1 });

            (first > second).Should().Be(false);
            (first >= second).Should().Be(false);
            (first <= second).Should().Be(true);
            (first < second).Should().Be(true);
        }

        [Test]
        public void GreaterLessOperator_BothZero()
        {
            BigInt first = new BigInt(new int[] { 0 });
            BigInt second = new BigInt(new int[] { 0 });

            (first > second).Should().Be(false);
            (first >= second).Should().Be(true);
            (first <= second).Should().Be(true);
            (first < second).Should().Be(false);
        }

        [Test]
        public void GreaterLessOperator_FirstDigitGreater()
        {
            BigInt first = new BigInt(new int[] { 7, 9, 7 });
            BigInt second = new BigInt(new int[] { 2, 1, 9 });

            (first > second).Should().Be(false);
            (first >= second).Should().Be(false);
            (first <= second).Should().Be(true);
            (first < second).Should().Be(true);
        }

        [Test]
        public void GreaterLessOperator_LastDigitGreater()
        {
            BigInt first = new BigInt(new int[] { 7, 9, 9 });
            BigInt second = new BigInt(new int[] { 9, 1, 7 });

            (first > second).Should().Be(true);
            (first >= second).Should().Be(true);
            (first <= second).Should().Be(false);
            (first < second).Should().Be(false);
        }
    }
}
