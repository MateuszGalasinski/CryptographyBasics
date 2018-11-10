using FluentAssertions;
using NUnit.Framework;
using RSA;

namespace RSATests
{
    public class PaddingTests
    {
        [TestCase(2,
            new byte[] { 0x0A },
            new byte[] { 0x0, 0x0A })]
        [TestCase(4,
            new byte[] { 0x0A },
            new byte[] { 0x0, 0x0, 0x0, 0x0A })]
        [TestCase(4,
            new byte[] { 0x0, 0x0A },
            new byte[] { 0x0, 0x0, 0x0, 0x0A })]
        [TestCase(4,
            new byte[] { 0x0, 0xD, 0x0A, 0x0 },
            new byte[] { 0x0, 0xD, 0x0A, 0x0 })]
        [TestCase(2,
            new byte[] { 0x0, 0xD, 0x0A, 0x0 },
            new byte[] { 0x0, 0xD, 0x0A, 0x0 })]
        [TestCase(2,
            new byte[] { 0x0, 0xD, 0x0A, 0x0 },
            new byte[] { 0x0, 0xD, 0x0A, 0x0 })]
        [TestCase(3,
            new byte[] { 0x0, 0xD, 0x0A, 0x0 },
            new byte[] { 0x0, 0xD, 0x0A, 0x0, 0x0, 0x0 })]
        [TestCase(3,
            new byte[] { 0x0, 0xD, 0x0A, 0xC },
            new byte[] { 0x0, 0xD, 0x0A, 0x0, 0x0, 0xC })]
        public void PaddingTest(int keyLength, byte[] data, byte[] correctlyPadded)
        {
            byte[] padded = Padding.AddPadding(data, keyLength);

            padded.Should().BeEquivalentTo(correctlyPadded);
        }
    }
}
