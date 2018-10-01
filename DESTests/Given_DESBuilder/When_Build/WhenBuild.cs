using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DES;
using NUnit.Framework;
using FluentAssertions;

namespace CryptoTests.Given_DESBuilder.When_Build
{
    public class WhenBuild : GivenDESBuilder
    {
        Task<byte[]> when;

        public void When_Encrypt(byte[] data)
        {
            try
            {
                when = Task.Run(() =>
                {
                    var algorithm = context.Build();
                    return algorithm.Encrypt(data);
                });
            }
            catch (AggregateException)
            {

            }
        }

        [Test]
        public void And_Permutation()
        {
            With_Permutation();

            When_Encrypt(new byte[] { 0b_1000_0000 });

            Then_EcnryptedShouldBe(new byte[] { 0b_0111_1111 });
        }

        public void Then_EcnryptedShouldBe(byte[] correctData)
        {
            when.Result.Should().Equal(correctData);
        }
    }
}
