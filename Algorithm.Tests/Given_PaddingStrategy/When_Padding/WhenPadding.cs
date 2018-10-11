using System;
using System.Threading.Tasks;
using Algorithm.Tests.Given_IPaddingStrategy;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithm.Tests.Given_PaddingStrategy.When_Padding
{
    [TestFixture()]
    public class WhenPadding : GivenPaddingStrategy
    {
        private bool[] _resultData;

        public void When_Action(bool[] data, Action actionToRun)
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

            bool[] dataForPadding = new bool[]
                {
                    false, false, false, true, false, true, true, true
                };

            When_Action(dataForPadding,
                () => { _resultData = context.AddPadding(dataForPadding); });

            Then_MessageShouldBe(new bool[]
                {
                    false, false, false, true, false, true, true, true,

                    false, false, false, false, false, true, true, true,
                    false, false, false, false, false, true, true, true,
                    false, false, false, false, false, true, true, true,
                    false, false, false, false, false, true, true, true,
                    false, false, false, false, false, true, true, true,
                    false, false, false, false, false, true, true, true,
                    false, false, false, false, false, true, true, true,
                });
        }

        [Test]
        public void And_RemovePadding()
        {
            With_CMSPaddingStrategy();

            bool[] dataForPadding = new bool[]
                {
                    false, false, false, true, false, true, true, true,

                    false, false, false, false, false, true, true, true,
                    false, false, false, false, false, true, true, true,
                    false, false, false, false, false, true, true, true,
                    false, false, false, false, false, true, true, true,
                    false, false, false, false, false, true, true, true,
                    false, false, false, false, false, true, true, true,
                    false, false, false, false, false, true, true, true
                };

            When_Action(dataForPadding,
                () => { _resultData = context.RemovePadding(dataForPadding); });

            Then_MessageShouldBe(new bool[]
                {
                    false, false, false, true, false, true, true, true
                });
        }

        public void Then_MessageShouldBe(bool[] correctData)
        {
            _resultData.Should().Equal(correctData);
        }
    }
}
