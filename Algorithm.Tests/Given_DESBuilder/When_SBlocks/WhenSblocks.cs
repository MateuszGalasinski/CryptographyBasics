using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoTests.Given_DESBuilder;
using DES.Constants;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithm.Tests.Given_DESBuilder.When_SBlocks
{
    class WhenSblocks : GivenDESBuilder
    {
        private BitArray _resultData;

        public void When_DataToSBlock(BitArray dataToSBlock)
        {
            try
            {
                Task.Run(() => { _resultData = context.SBlocks(dataToSBlock); }).Wait();
            }
            catch (AggregateException)
            {

            }
        }

        [Test]
        public void And_DataToSBlock()
        {
           BitArray data =  new BitArray(new bool[]
            {
                true, true, false, true,  false, false, false, false,
                true, false, true, true,  true, true, false, true,
                true, true, true, true,  false, true, false, false,
                false, false, true, true,  false, false, false, false,
                true, true, false, true,  false, true, false, false,
                true, true, true, true,  false, true, true, true
            });

            BitArray result = new BitArray(new bool[]
            {
                true, false, false, true,
                false, false, true, false,
                false, false, true, true,
                false, false, true, true,
                true, false, true, true,
                true, false, false, true,
                false, false, true, true,
                false, false, false, false
            });

            //BitArray dataToSBlock = new BitArray(data);

            When_DataToSBlock(data);

            Then_SblockShouldBe(result);
        }

        public void Then_SblockShouldBe(BitArray correctData)
        {
            _resultData.Should().Equal(correctData);
        }
    }
}

