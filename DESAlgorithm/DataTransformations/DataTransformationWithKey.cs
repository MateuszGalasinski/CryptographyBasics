using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DES.DataTransformations
{
    public class DataTransformationWithKey : IDataTransformation
    {
        public Func<BitArray, BitArray, BitArray> Transformation { get; }
        public BitArray Key { get; }

        public DataTransformationWithKey(Func<BitArray, BitArray, BitArray> transformation, BitArray key)
        {
            Transformation = transformation;
            Key = key;
        }

        public BitArray Transform(BitArray data)
        {
            return Transformation(data, Key);
        }
    }
}
