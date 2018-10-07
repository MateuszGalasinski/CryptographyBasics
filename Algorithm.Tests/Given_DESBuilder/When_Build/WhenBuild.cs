using System;
using System.Threading.Tasks;
using NUnit.Framework;
using FluentAssertions;

namespace CryptoTests.Given_DESBuilder.When_Build
{
    public class WhenBuild : GivenDESBuilder
    {
        private byte[] _data;

        public void When_Encrypt(byte[] data)
        {
            try
            {
                Task.Run(() =>
                {
                    var algorithm = context.Build();
                    algorithm.Encrypt(data);
                });
            }
            catch (AggregateException)
            {

            }
        }

        [Test]
        public void And_Permutation()
        {
            With_EncryptPermutation();

            _data = new byte[] {0b_1000_0000};
            When_Encrypt(_data);

            Then_EcnryptedShouldBe(new byte[] { 0b_0111_1111 });
        }

        public void Then_EcnryptedShouldBe(byte[] correctData)
        {
            _data.Should().Equal(correctData);
        }
    }
}
