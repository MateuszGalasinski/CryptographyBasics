using System;
using System.Collections;
using System.Threading.Tasks;
using NUnit.Framework;
using FluentAssertions;

namespace CryptoTests.Given_DESBuilder.When_Build
{
    public class WhenBuild : GivenDESBuilder
    {
        private BitArray _data;

        public void When_Encrypt(BitArray data)
        {
            try
            {
                Task.Run(() =>
                {
                    var algorithm = context.Build();
                    _data = algorithm.Encrypt(data);
                }).Wait();
            }
            catch (AggregateException)
            {

            }
        }

        [Test]
        public void And_Permutation()
        {
            With_EncryptPermutation();

            BitArray data = new BitArray(new byte[] {0b_1000_0000});
            When_Encrypt(data);

            Then_EncryptedShouldBe(new BitArray(new byte[] { 0b_0111_1111 }));
        }

        [Test]
        public void And_ExtendingPermutation()
        {
           With_ExtendingPermutation();

            //0b_1010_0101,
            //0b_1011_1010,
            //0b_0110_0110,
            //0b_1001_1011
            BitArray data = new BitArray(new bool[]
            {
                true, false, true, false,  false, true, false, true,
                true, false, true, true,  true, false, true, false,
                false, true, true, false,  false, true, true, false,
                true, false, false, true,  true, false, true, true
            });

            When_Encrypt(data);

            //    0b_1101_0000,
            //    0b_1011_1101,
            //    0b_1111_0100,
            //    0b_0011_0000,
            //    0b_1101_0100,
            //    0b_1111_0111,
            Then_EncryptedShouldBe(
                new BitArray( new bool[]
                    {
                        true, true, false, true,  false, false, false, false,
                        true, false, true, true,  true, true, false, true,
                        true, true, true, true,  false, true, false, false,
                        false, false, true, true,  false, false, false, false,
                        true, true, false, true,  false, true, false, false,
                        true, true, true, true,  false, true, true, true
                    }));
        }

        public void Then_EncryptedShouldBe(BitArray correctData)
        {
            _data.Should().Equal(correctData);
        }
    }
}
