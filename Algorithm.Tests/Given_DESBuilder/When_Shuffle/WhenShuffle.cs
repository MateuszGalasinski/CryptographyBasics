using System;
using System.Collections;
using System.Threading.Tasks;
using CryptoTests.Given_DESBuilder;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithm.Tests.Given_DESBuilder.When_Shuffle
{
    public class WhenShuffle : GivenDESBuilder
    {
        private BitArray _resultData;

        public void When_Shuffle(BitArray dataToShuffle, int[] permutationTable)
        {
            try
            {
                Task.Run(() => { _resultData = context.Shuffle(dataToShuffle, permutationTable); }).Wait();
            }
            catch (AggregateException)
            {

            }
        }

        [Test]
        public void And_Shuffle()
        {
            BitArray data = new BitArray(new byte[] { 0b_1111_1111, 0b_0000_0000 });
            int[] permutationTable = new int[]
            {
                1, 9, 1, 9,  1, 9, 1, 9
            };

            When_Shuffle(data, permutationTable);
            
            Then_ShuffledShouldBe(new BitArray(new byte[] { 0b_0101_0101 }));
        }

        public void Then_ShuffledShouldBe(BitArray correctData)
        {
            _resultData.Should().Equal(correctData);
        }
    }
}
