using System;
using System.Collections;
using System.Threading.Tasks;
using DESAlgorithm.Models;
using FluentAssertions;
using NUnit.Framework;

namespace CryptoTests.Given_DESBuilder.When_Build
{
    public class WhenBuild : GivenDESBuilder
    {
        private DataSet _data;

        public void When_Func(DataSet data)
        {
            try
            {
                Task.Run(() => { _data = functionToInvoke(data); }).Wait();
            }
            catch (AggregateException)
            {
            }
        }

        public void And_WholeDES()
        {
        }

        public void Then_EncryptedShouldBe(DataSet correctData)
        {
            _data.Should().BeEquivalentTo(correctData);
        }

        [Test]
        public void And_ExtendingPermutation()
        {
            With_ExtendingPermutation();

            //0b_1010_0101,
            //0b_1011_1010,
            //0b_0110_0110,
            //0b_1001_1011
            BitArray data = new BitArray(new[]
            {
                true, false, true, false, false, true, false, true,
                true, false, true, true, true, false, true, false,
                false, true, true, false, false, true, true, false,
                true, false, false, true, true, false, true, true
            });

            When_Func(new DataSet {Right = data});

            //    0b_1101_0000,
            //    0b_1011_1101,
            //    0b_1111_0100,
            //    0b_0011_0000,
            //    0b_1101_0100,
            //    0b_1111_0111,
            Then_EncryptedShouldBe(
                new DataSet
                {
                    Right = new BitArray(new[]
                    {
                        true, true, false, true, false, false, false, false,
                        true, false, true, true, true, true, false, true,
                        true, true, true, true, false, true, false, false,
                        false, false, true, true, false, false, false, false,
                        true, true, false, true, false, true, false, false,
                        true, true, true, true, false, true, true, true
                    })
                });
        }

        [Test]
        public void And_PblockPermutation()
        {
            DataSet data = new DataSet
            {
                Right = new BitArray(new[]
                {
                    true, false, true, false, false, true, false, true,
                    true, false, true, true, true, false, true, false,
                    false, true, true, false, false, true, true, false,
                    true, false, false, true, true, false, true, true
                })
            };

            DataSet result = new DataSet
            {
                Right = new BitArray(new[]
                {
                    false, false, false, false, true, true, true, false,
                    true, true, true, false, false, true, true, false,
                    false, true, false, false, true, false, true, true,
                    true, true, false, true, true, true, false, true
                })
            };

            With_PBlockPermutation();

            When_Func(data);

            Then_EncryptedShouldBe(result);
        }

        [Test]
        public void And_Sblocks()
        {
            With_SBlocks();
            DataSet data = new DataSet
            {
                Right = new BitArray(new[]
                {
                    true, true, false, true, false, false, false, false,
                    true, false, true, true, true, true, false, true,
                    true, true, true, true, false, true, false, false,
                    false, false, true, true, false, false, false, false,
                    true, true, false, true, false, true, false, false,
                    true, true, true, true, false, true, true, true
                })
            };

            DataSet result = new DataSet
            {
                Right = new BitArray(new[]
                {
                    true, false, false, true,
                    false, false, true, false,
                    false, false, true, true,
                    false, false, true, true,
                    true, false, true, true,
                    true, false, false, true,
                    false, false, true, true,
                    false, false, false, false
                })
            };

            When_Func(data);

            Then_EncryptedShouldBe(result);
        }
    }
}