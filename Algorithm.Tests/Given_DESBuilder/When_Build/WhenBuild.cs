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

            Then_EcnryptedShouldBe(new BitArray(new byte[] { 0b_0111_1111 }));
        }

        public void Then_EcnryptedShouldBe(BitArray correctData)
        {
            _data.Should().Equal(correctData);
        }
    }
}
