using DES.DataTransformations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using DES.Constants;

namespace DES.AlgorithmBuilders
{
    public class DESBuilder
    {
        List<IDataTransformation> _encryptSteps = new List<IDataTransformation>();
        List<IDataTransformation> _decryptSteps = new List<IDataTransformation>();

        public CryptoAlgorithm Build()
        {
            return new CryptoAlgorithm(_encryptSteps, _decryptSteps);
        }

        public void AddWholeDES()
        {
            AddEncryptPermutation();
        }

        //1st step
        public void AddEncryptPermutation()
        {
            _encryptSteps.Add(new DataTransformation(p =>
            {
                p.Not();
            }));
        }

        public void MoveBits(BitArray data, int translation, Direction direction)
        {
            int length = data.Length;
        
            //BitArray frontToEnd = new BitArray(64);
            //BitArray endToFront = new BitArray(data.Length);

            BitArray result = new BitArray(data.Length);

            if (direction == Direction.Left)
            {
                
                //for (int i = 0; i < translation; i++)
                //{
                //    frontToEnd.Set(i,data.Get(i));
                //}

                //frontToEnd = data  //.Take(translation).ToArray();

                result = blockCopy(data, 0, translation, length - translation);

                //old front to new end
                for (int i = length-translation; i < length ; i++)
                {
                    result.Set(i, data.Get(i-length+translation) );
                }


                //Buffer.BlockCopy(data, translation, data, 0, length - translation);
            }
            else
            {
                //old end to new front
                result = blockCopy(data, translation, 0, length - translation);

                for (int i = 0; i < translation; i++)
                {
                    result.Set(i, data.Get(length - translation + i);
                }

                //endToFront = data.Skip(data.Length - translation).ToArray();
                //Buffer.BlockCopy(data, 0, endToFront, translation , length - translation);
                //Array.Copy(endToFront, data, length);
            }

            data = result;

        }

        private BitArray blockCopy(BitArray data, int destOffSet, int offSet, int count )
        {
            BitArray result = new BitArray(64);

            for(int i = 0; i < destOffSet; i++)
            {
                result.Set(i, false);
            }

            for (int j = offSet; j < count; j++)
            {
                result.Set(j, data.Get(j));
            }

            return result;
        }
    

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataToShuffle">entry data</param>
        /// <param name="permutationTable"> Consists of numbers specifing which bit from data to choose.</param>
        /// <returns></returns>
        public BitArray Shuffle(BitArray dataToShuffle, int[] permutationTable)
        {
            if (permutationTable.Length % 8 != 0)
            {
                throw new ValidationException("Table descripting where to put specific bits needs to have size divisible by 8.");
            }

            if (permutationTable.Max() >= dataToShuffle.Length)
            {
                throw new ValidationException($"Found too big number: {permutationTable.Max()} in permutation table. Data to choose from is too short for it.");

            }
            
            BitArray resultData = new BitArray(permutationTable.Length);

            for (int i = 0; i < permutationTable.Length; i++)
            {
                int dataIndex = permutationTable[i] - 1; ;
                resultData[i] = dataToShuffle[dataIndex];
            }

            return resultData;
        }
    }
}
