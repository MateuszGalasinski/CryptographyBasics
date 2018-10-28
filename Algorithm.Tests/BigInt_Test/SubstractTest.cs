using FluentAssertions;
using NUnit.Framework;
using RSAAlgorithm;

namespace Algorithm.Tests.BigInt_Test
{
    public class SubstractTest
    {
        //[Test]
        //public void SubstractExactlySameNumbers_ShouldZero()
        //{
        //    int[] first = {9, 9, 9};
        //    int[] second = {9, 9, 9};

        //    int[] result = BigInt.Substract(first, second);

        //    result.Should().Equal(new[] {0});
        //}

        [Test]
        public void SubstractExactlySameBigInts_ShouldZero()
        {
            BigInt first = new BigInt( new int[] { 9, 9, 9 });
            BigInt second = new BigInt(new int[] { 9, 9, 9 });

            (first - second).Value.Should().Equal(new[] { 0 });

            first.Substract(second);

            first.Value.Should().Equal(new[] { 0 });
        }

        //[Test]
        //public void SubstractSameLengthNumbers_WithNoCarry()
        //{
        //    int[] first = {7, 5, 3};
        //    int[] second = {6, 3, 1};

        //    int[] result = BigInt.Substract(first, second);

        //    result.Should().Equal(new[] {1, 2, 2});
        //}

        [Test]
        public void SubstractSameLengthBigInts_WithNoCarry()
        {
            BigInt first = new BigInt(new int[] { 3, 5, 7 }); // 753
            BigInt second = new BigInt(new int[] { 1, 3, 6 }); // 631

            (first - second).Value.Should().Equal(new[] { 2, 2, 1 });

            first.Substract(second);

            first.Value.Should().Equal(new[] { 2, 2, 1 });
        }

        //[Test]
        //public void SubstractSameLengthNumbers_WithNoCarry_WithShorterResult()
        //{
        //    int[] first = {7, 5, 3};
        //    int[] second = {7, 3, 1};

        //    int[] result = BigInt.Substract(first, second);

        //    result.Should().Equal(new[] {2, 2});
        //}

        [Test]
        public void SubstractSameLengthBigInts_WithNoCarry_WithShorterResult()
        {
            BigInt first = new BigInt(new int[] { 3, 5, 7 }); // 753
            BigInt second = new BigInt(new int[] { 1, 3, 7 }); // 731

            first.Substract(second);

            first.Value.Should().Equal(new[] { 2, 2});
        }

        //[Test]
        //public void SubstractSameLengthNumbers_WithCarry_WithShorterResult()
        //{
        //    int[] first = {7, 5, 3};
        //    int[] second = {6, 6, 1};

        //    int[] result = BigInt.Substract(first, second);

        //    result.Should().BeEquivalentTo(new[] {9, 2});
        //}

        [Test]
        public void SubstractSameLengthBigInts_WithCarry_WithShorterResult()
        {
            BigInt first = new BigInt(new int[] { 3, 5, 7 }); // 753
            BigInt second = new BigInt(new int[] { 1, 6, 6 }); // 661

            first.Substract(second);

            first.Value.Should().Equal(new[] { 2, 9 });
        }

        //[Test]
        //public void SubstractDifferentLengthNumbers_WithNoCarry()
        //{
        //    int[] first = {7, 5, 3};
        //    int[] second = {4, 1};

        //    int[] result = BigInt.Substract(first, second);

        //    result.Should().Equal(new[] {7, 1, 2});
        //}


        [Test]
        public void SubstractDifferentLengthBigInts_WithNoCarry()
        {
            BigInt first = new BigInt(new int[] { 3, 5, 7 }); // 753
            BigInt second = new BigInt(new int[] { 1, 4 }); // 41

            first.Substract(second);

            first.Value.Should().Equal(new[] { 2, 1, 7 });
        }

        //[Test]
        //public void SubstractDifferentLengthNumbers_WithCarry()
        //{
        //    int[] first = {7, 5, 3};
        //    int[] second = {6, 1};

        //    int[] result = BigInt.Substract(first, second);

        //    result.Should().Equal(new[] {6, 9, 2});
        //}

        [Test]
        public void SubstractDifferentLengthBigInts_WithCarry()
        {
            BigInt first = new BigInt(new int[] { 3, 5, 7 }); // 753
            BigInt second = new BigInt(new int[] { 1, 6 }); // 41

            first.Substract(second);

            first.Value.Should().Equal(new[] { 2, 9, 6 });
        }
    }
}