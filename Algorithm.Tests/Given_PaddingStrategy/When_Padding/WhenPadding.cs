using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm.Tests.Given_IPaddingStrategy;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Algorithm.Tests.Given_PaddingStrategy.When_Padding
{
    [TestFixture()]
    public class WhenPadding : GivenPaddingStrategy
    {
        private BitArray _resultData;

        public void When_Action(BitArray data, Action actionToRun)
        {
            try
            {
                Task.Run(actionToRun).Wait();
            }
            catch (AggregateException)
            {

            }
        }

        [Test]
        public void And_AddPadding()
        {
            With_CMSPaddingStrategy();

            BitArray dataForPadding = new BitArray(
                new bool[]
                {
                    false, false, false, true, false, true, true, true
                });

            When_Action(dataForPadding,
                () => { _resultData = context.AddPadding(dataForPadding); });

            Then_MessageShouldBe(new BitArray(
                new bool[]
                {
                    false, false, false, true, false, true, true, true,

                    false, false, false, false, false, true, true, true,
                    false, false, false, false, false, true, true, true,
                    false, false, false, false, false, true, true, true,
                    false, false, false, false, false, true, true, true,
                    false, false, false, false, false, true, true, true,
                    false, false, false, false, false, true, true, true,
                    false, false, false, false, false, true, true, true,
                }));
        }

        [Test]
        public void And_RemovePadding()
        {
            With_CMSPaddingStrategy();

            BitArray dataForPadding = new BitArray(
                new bool[]
                {
                    false, false, false, true, false, true, true, true,

                    false, false, false, false, false, true, true, true,
                    false, false, false, false, false, true, true, true,
                    false, false, false, false, false, true, true, true,
                    false, false, false, false, false, true, true, true,
                    false, false, false, false, false, true, true, true,
                    false, false, false, false, false, true, true, true,
                    false, false, false, false, false, true, true, true
                });

            When_Action(dataForPadding,
                () => { _resultData = context.RemovePadding(dataForPadding); });

            Then_MessageShouldBe(new BitArray(
                new bool[]
                {
                    false, false, false, true, false, true, true, true
                }));
        }

        public void Then_MessageShouldBe(BitArray correctData)
        {
            _resultData.Should().Equal(correctData);
        }
    }
}
