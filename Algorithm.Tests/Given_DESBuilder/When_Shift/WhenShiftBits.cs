using System;
using System.Collections;
using System.Threading.Tasks;
using CryptoTests.Given_DESBuilder;
using DES.Constants;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithm.Tests.Given_DESBuilder.When_Shift
{
    class WhenShiftBits : GivenDESBuilder
    {
        private BitArray _resultData;

        public void When_ShiftBits(BitArray dataToShift, int translation, Direction direction)
        {
            try
            {
                Task.Run(() => { _resultData = context.ShiftBits(dataToShift, translation, direction); }).Wait();
            }
            catch (AggregateException)
            {
            }
        }

        public void Then_ShiftShouldBe(BitArray correctData)
        {
            _resultData.Should().Equal(correctData);
        }

        [Test]
        public void And_DataToShift()
        {
            BitArray dataToShift = new BitArray(new[] {false, false, false, true, false, true, true, true});
            int translation = 2;
            Direction direction = Direction.Left;

            When_ShiftBits(dataToShift, translation, direction);

            Then_ShiftShouldBe(new BitArray(new[] {false, true, false, true, true, true, false, false}));
        }
    }
}