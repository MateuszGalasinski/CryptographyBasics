using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoTests.Given_DESBuilder;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithm.Tests.Given_DESBuilder.When_Shuffle
{
    public class WhenShuffle : GivenDESBuilder
    {
        private byte[] _resultData;

        public void When_Shuffle(byte[] dataToShuffle, int[] permutationTable)
        {
            try
            {
                Task.Run(() => { _resultData = context.Shuffle(dataToShuffle, permutationTable); });
            }
            catch (AggregateException)
            {

            }
        }

        [Test]
        public void And_Permutation()
        {
            byte[] data = new byte[] { 0b_1111_1111, 0b_0000_0000 };
            int[] permutationTable = new int[]
            {
                1, 8, 1, 8,  1, 8, 1, 8
            };

            When_Shuffle(data, permutationTable);
            
            Then_ShuffledShouldBe(new byte[] { 0b_1010_1010 });
        }

        public void Then_ShuffledShouldBe(byte[] correctData)
        {
            _resultData.Should().Equal(correctData);
        }
    }
}
