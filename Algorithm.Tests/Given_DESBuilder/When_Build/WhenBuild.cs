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

        public void When_Encrypt(DataSet data)
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

            When_Encrypt(new DataSet(){Right = data});

            //    0b_1101_0000,
            //    0b_1011_1101,
            //    0b_1111_0100,
            //    0b_0011_0000,
            //    0b_1101_0100,
            //    0b_1111_0111,
            Then_EncryptedShouldBe(
                new DataSet()
                {
                    Right = new BitArray(new bool[]
                    {
                        true, true, false, true,  false, false, false, false,
                        true, false, true, true,  true, true, false, true,
                        true, true, true, true,  false, true, false, false,
                        false, false, true, true,  false, false, false, false,
                        true, true, false, true,  false, true, false, false,
                        true, true, true, true,  false, true, true, true
                    })
                });
        }

        [Test]
        public void And_Sblocks()
        {
            With_Sblocks();
            DataSet data = new DataSet()
            {
                Right = new BitArray(new bool[]
                {
                    true, true, false, true,  false, false, false, false,
                    true, false, true, true,  true, true, false, true,
                    true, true, true, true,  false, true, false, false,
                    false, false, true, true,  false, false, false, false,
                    true, true, false, true,  false, true, false, false,
                    true, true, true, true,  false, true, true, true
                })
            };

            DataSet result = new DataSet()
            {
                Right = new BitArray(new bool[]
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

            When_Encrypt(data);

            Then_EncryptedShouldBe(result);
        }


        [Test]
        public void And_PblockPermutation()
        {
            DataSet data = new DataSet()
            {
                Right = new BitArray(new bool[]
                {
                    true, false, true, false,  false, true, false, true,
                    true, false, true, true,  true, false, true, false,
                    false, true, true, false,  false, true, true, false,
                    true, false, false, true,  true, false, true, true
                })
            };

            DataSet result = new DataSet()
            {
                Right = new BitArray(new bool[]
                {
                    false, false, false, false, true, true, true, false,
                    true, true, true, false, false, true, true, false,
                    false, true, false, false, true, false, true, true,
                    true, true, false, true, true, true, false, true
                })
            };

            With_PblockPermutation();

            When_Encrypt(data);

            Then_EncryptedShouldBe(result);
        }



        public void Then_EncryptedShouldBe(DataSet correctData)
        {
            _data.Should().Equals(correctData);
        }
    }
}
