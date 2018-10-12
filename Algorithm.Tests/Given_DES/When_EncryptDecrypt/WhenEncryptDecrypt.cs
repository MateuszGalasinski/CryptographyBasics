using System;
using System.Collections;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithm.Tests.Given_DES.When_EncryptDecrypt
{
    public class WhenEncryptDecrypt : GivenDes.GivenDES
    {
        private bool[] _data;

        public void When_Func(bool[] data)
        {
            try
            {
                Task.Run(() => { _data = functionToInvoke(data); }).Wait();
            }
            catch (AggregateException ex)
            {

            }
        }

        [Test]
        public void AndAllFalse_BlockPart_KeyZero()
        {
            With_WholeDES(new BitArray(new byte[]
            {
                0x_0,
                0x_0,
                0x_0,
                0x_0,
                0x_0,
                0x_0,
                0x_0,
                0x_0
            }));

            With_EncryptDecrypt();

            bool[] data = new bool[]
            {
                false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false
            };

            When_Func(data);

            Then_ResultShouldBe(data);
        }

        [Test]
        public void AndAllTrue_BlockPart_KeyZero()
        {
            With_WholeDES(new BitArray(new byte[]
            {
                0x_0,
                0x_0,
                0x_0,
                0x_0,
                0x_0,
                0x_0,
                0x_0,
                0x_0
            }));

            With_EncryptDecrypt();

            bool[] data = new bool[]
            {
                true, true, true, true, true, true, true, true,
                true, true, true, true, true, true, true, true,
                true, true, true, true, true, true, true, true,
                true, true, true, true, true, true, true, true,
                true, true, true, true, true, true, true, true,
                true, true, true, true, true, true, true, true,
                true, true, true, true, true, true, true, true,
                true, true, true, true, true, true, true, true
            };

            When_Func(data);

            Then_ResultShouldBe(data);
        }

        public void Then_ResultShouldBe(bool[] correctData)
        {
            _data.Should().BeEquivalentTo(correctData);
        }
    }
}
