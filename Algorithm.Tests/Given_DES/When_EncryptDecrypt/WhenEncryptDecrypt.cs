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


        public void Then_ResultShouldBe(bool[] correctData)
        {
            _data.Should().BeEquivalentTo(correctData);
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

            bool[] data =
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

            bool[] data =
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
            BitArray array = new BitArray(64);
            var values = new[]
            {
                0, 0, 0, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 1, 0, 0,
                0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1,
                1, 0, 0, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 0, 0,
                1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1
            };

            for (int i = 0; i < 64; i++)
            {
                array[i] = values[i] == 1 ? true : false;
            }

            With_WholeDES(array);

            With_EncryptDecrypt();

            var message = new[]
            {
                0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 1, 1,
                0, 1, 0, 0, 0, 1, 0, 1, 0, 1, 1, 0, 0, 1, 1, 1,
                1, 0, 0, 0, 1, 0, 0, 1, 1, 0, 1, 0, 1, 0, 1, 1,
                1, 1, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1
            };

            bool[] data = new bool[64];

            for (int i = 0; i < 64; i++)
            {
                data[i] = message[i] == 1 ? true : false;
            }

            When_Func(data);

            Then_ResultShouldBe(data);
        }
    }
}