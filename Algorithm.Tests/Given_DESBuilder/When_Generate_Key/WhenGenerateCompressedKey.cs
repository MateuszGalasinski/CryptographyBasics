using System;
using System.Collections;
using System.Threading.Tasks;
using CryptoTests.Given_DESBuilder;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithm.Tests.Given_DESBuilder.When_Generate_Key
{
    class WhenGenerateCompressedKey : GivenDESBuilder
    {
        private BitArray _resultData;

        public void When_Generate_Short_Key(BitArray key)
        {
            try
            {
                Task.Run(() => { _resultData = context.GenerateShortKeyfromLongKey(key); }).Wait();
            }
            catch (AggregateException)
            {
            }
        }

        public void Then_KeyShouldBe(BitArray correctData)
        {
            _resultData.Should().Equal(correctData);
        }

        [Test]
        public void And_KeyToGenerate()
        {
            BitArray key = new BitArray(new[]
            {
                true, false, true, false, false, false, false, true,
                false, true, true, true, true, false, true, true,
                true, true, true, false, true, false, false, false,
                false, true, true, true, false, false, false, true,
                true, false, true, false, true, false, false, true,
                true, true, true, false, true, true, true, true,
                false, true, false, true, false, true, false, false
            });

            When_Generate_Short_Key(key);

            BitArray result_short = new BitArray(new[]
            {
                false, true, true, false, true, false, true, true, true, false, true, true,
                false, true, true, false, true, true, true, false, true, false, true, false,
                true, true, false, true, true, false, false, true, false, true, true, true,
                false, false, false, false, false, false, true, true, true, false, false, true
            });

            Then_KeyShouldBe(result_short);
        }
    }
}