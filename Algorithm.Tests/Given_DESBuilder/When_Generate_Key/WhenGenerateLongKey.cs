﻿using System;
using System.Collections;
using System.Threading.Tasks;
using CryptoTests.Given_DESBuilder;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithm.Tests.Given_DESBuilder.When_Generate_Key
{
    class WhenGenerateLongKey : GivenDESBuilder
    {
        private BitArray _resultData;

        public void When_Generate_Long_Key(BitArray key, int cycle)
        {
            try
            {
                Task.Run(() => { _resultData = context.GenerateLongKeyForCycle(key, cycle); }).Wait();
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
                true, true, false, true, false, false, false, false,
                true, false, true, true, true, true, false, true,
                true, true, true, true, false, true, false, false,
                false, false, true, true, false, false, false, false,
                true, true, false, true, false, true, false, false,
                true, true, true, true, false, true, true, true,
                true, false, true, false, true, false, true, false
            });
            int cycle = 1;

            When_Generate_Long_Key(key, cycle);

            BitArray result_long = new BitArray(new[]
            {
                true, false, true, false, false, false, false, true,
                false, true, true, true, true, false, true, true,
                true, true, true, false, true, false, false, false,
                false, true, true, true, false, false, false, true,
                true, false, true, false, true, false, false, true,
                true, true, true, false, true, true, true, true,
                false, true, false, true, false, true, false, false
            });

            Then_KeyShouldBe(result_long);
        }
    }
}