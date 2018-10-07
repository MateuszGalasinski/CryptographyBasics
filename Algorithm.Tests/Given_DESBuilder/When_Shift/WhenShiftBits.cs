using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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

        [Test]
        public void And_DataToShift()
        {
            BitArray dataToShift = new BitArray(new bool[] {false, false, false, true , false, true , true, true });
            int translation = 2;
            Direction direction = Direction.Left;

            When_ShiftBits( dataToShift,  translation,  direction);

            Then_ShiftShouldBe(new BitArray(new bool[] {  false, true, false, true, true, true, false, false }));
        }

        public void Then_ShiftShouldBe(BitArray correctData)
        {
            _resultData.Should().Equal(correctData);
        }


    }
}
