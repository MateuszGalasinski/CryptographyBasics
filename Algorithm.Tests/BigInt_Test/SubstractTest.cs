using FluentAssertions;
using NUnit.Framework;
using RSAAlgoithm;

namespace Algorithm.Tests.BigInt_Test
{
    public class SubstractTest
    {
        [Test]
        public void SubstractExactlySameNumbers_ShouldZero()
        {
            //BigInt first = new BigInt(new int[] { 9, 9, 9 });
            //BigInt second = new BigInt(new int[] { 9, 9, 9 });
            int[] first = new int[] {9, 9, 9};
            int[] second = new int[] { 9, 9, 9 };

            int[] result = BigInt.Substract(first, second);

            result.Should().BeEquivalentTo(new int[] {0});
        }

        [Test]
        public void SubstractSameLengthNumbers_WithNoCarry()
        {
            //BigInt first = new BigInt(new int[] { 9, 9, 9 });
            //BigInt second = new BigInt(new int[] { 9, 9, 9 });
            int[] first = new int[] { 7, 5, 3 };
            int[] second = new int[] { 6, 3, 1 };

            int[] result = BigInt.Substract(first, second);

            result.Should().BeEquivalentTo(new int[] { 1, 2, 2 });
        }

        [Test]
        public void SubstractSameLengthNumbers_WithNoCarry_WithShorterResult()
        {
            //BigInt first = new BigInt(new int[] { 9, 9, 9 });
            //BigInt second = new BigInt(new int[] { 9, 9, 9 });
            int[] first = new int[] { 7, 5, 3 };
            int[] second = new int[] { 7, 3, 1 };

            int[] result = BigInt.Substract(first, second);

            result.Should().BeEquivalentTo(new int[] { 2, 2 });
        }

        [Test]
        public void SubstractSameLengthNumbers_WithCarry_WithShorterResult()
        {
            //BigInt first = new BigInt(new int[] { 9, 9, 9 });
            //BigInt second = new BigInt(new int[] { 9, 9, 9 });
            int[] first = new int[] { 7, 5, 3 };
            int[] second = new int[] { 6, 6, 1 };

            int[] result = BigInt.Substract(first, second);

            result.Should().BeEquivalentTo(new int[] { 9, 2 });
        }

        [Test]
        public void SubstractDifferentLengthNumbers_WithNoCarry()
        {
            //BigInt first = new BigInt(new int[] { 9, 9, 9 });
            //BigInt second = new BigInt(new int[] { 9, 9, 9 });
            int[] first = new int[] { 7, 5, 3 };
            int[] second = new int[] { 4, 1 };

            int[] result = BigInt.Substract(first, second);

            result.Should().BeEquivalentTo(new int[] { 7, 1, 2 });
        }

        [Test]
        public void SubstractDifferentLengthNumbers_WithCarry()
        {
            //BigInt first = new BigInt(new int[] { 9, 9, 9 });
            //BigInt second = new BigInt(new int[] { 9, 9, 9 });
            int[] first = new int[] { 7, 5, 3 };
            int[] second = new int[] { 6, 1 };

            int[] result = BigInt.Substract(first, second);

            result.Should().BeEquivalentTo(new int[] { 6, 9, 2 });
        }
    }
}
