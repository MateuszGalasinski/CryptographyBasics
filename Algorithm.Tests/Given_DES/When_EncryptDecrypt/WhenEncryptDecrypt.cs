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

        [Test]
        public void AndSomeText_KeyZero()
        {
            With_WholeDES(new BitArray(
                new byte[]
                {
                    0x01,
                    0x03,

                    0x03,
                    0x04,

                    0x05,
                    0x07,

                    0x07,
                    0x09,

                    0x09,
                    0x0B,

                    0x0B,
                    0x0C,

                    0x0D,
                    0x0F,

                    0x0F,
                    0x01
                }));

            With_EncryptDecrypt();

            BitArray dataArray = new BitArray(new BitArray(
                new byte[]
                {
                    0x00,
                    0x01,

                    0x02,
                    0x03,

                    0x04,
                    0x05,

                    0x06,
                    0x07,

                    0x08,
                    0x09,

                    0x0A,
                    0x0B,

                    0x0C,
                    0x0D,

                    0x0E,
                    0x0F
                }));

            bool[] data = new bool[dataArray.Length];
            dataArray.CopyTo(data, 0);

            When_Func(data);

            Then_ResultShouldBe(data);
        }


        public void Then_ResultShouldBe(bool[] correctData)
        {
            _data.Should().BeEquivalentTo(correctData);
        }
    }
}
