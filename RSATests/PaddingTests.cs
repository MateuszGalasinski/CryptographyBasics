using FluentAssertions;
using NUnit.Framework;
using RSA;
using RSA.PaddingStrategies;

namespace RSATests
{
    public class PaddingTests
    {
        private IPaddingStrategy _paddingStrategy = new CMSPaddingStrategy();

        [TestCase(new byte[]
            {
                0x03, 0x02, 0x05, 0x06
            },
            new byte[]
            {
                0x03, 0x02, 0x05, 0x06, 0x02, 0x02
            }, 2)]
        [TestCase(new byte[]
            {
                0x03, 0x02, 0x05,
            },
            new byte[]
            {
                0x03, 0x02, 0x05, 0x01
            }, 2)]
        public void AddPaddingTest(byte[] dataToPadd, byte[] correctData, int blockLength)
        {
            byte[] paddedData = _paddingStrategy.AddPadding(dataToPadd, blockLength);

            paddedData.Should().BeEquivalentTo(correctData);
        }

        [Test]
        public void AddBigIntegerPaddingTest()
        {
            int blockSize = 4;
            var chunker = new DataChunker();
            var data = new BigInteger[]
            {
                new BigInteger(new byte[]
                {
                    0x03, 0x02, 0x01,
                }),
                new BigInteger(new byte[]
                {
                    0x03, 0x02, 0x01,
                })
            };

            var padded = chunker.MergeDataAndRemovePadding(data, blockSize);

            padded.ShouldBeEquivalentTo(new byte[]
            {
                0x0, 0x03, 0x02, 0x01, 0x0, 0x03, 0x02
            });
        }
    }
}
