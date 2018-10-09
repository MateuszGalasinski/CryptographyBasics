using System;
using System.Collections;
using System.Threading.Tasks;
using CryptoTests.Given_DESBuilder;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithm.Tests.Given_DESBuilder.When_RemoveParityBits
{
    public class WhenRemoveParityBits : GivenDESBuilder
    {
        private BitArray _resultKey;

        public void When_RemoveParityBits(BitArray keyToReduce)
        {
            try
            {
                Task.Run(() =>
                {
                    _resultKey = context.RemoveParityBits(keyToReduce);
                }).Wait();
            }
            catch (AggregateException)
            {

            }
        }

        [Test]
        public void And_KeyToReduce()
        {
            BitArray keyToReduce = new BitArray(
                new bool[]
                {
                    false, false, false, true, false, true, true, true,
                    false, false, true, true, true, true, true, true,
                    false, false, false, true, false, true, false, false,
                    true, true, true, true, true, true, true, true,
                    false, false, false, false, false, false, false, false,
                    false, true, false, true, false, true, false, true,
                    false, false, false, true, true, true, true, false,
                    false, false, false, true, false, true, false, false
                });

            When_RemoveParityBits(keyToReduce);

            Then_KeyShouldBe
            (
                new BitArray
                (
                    new bool[]
                    {
                        false, false, false, true, false, true, true,
                        false, false, true, true, true, true, true,
                        false, false, false, true, false, true, false,
                        true, true, true, true, true, true, true,
                        false, false, false, false, false, false, false,
                        false, true, false, true, false, true, false,
                        false, false, false, true, true, true, true,
                        false, false, false, true, false, true, false
                    }
                )
            );
        }

        public void Then_KeyShouldBe(BitArray correctData)
        {
            _resultKey.Should().Equal(correctData);
        }
    }
}
