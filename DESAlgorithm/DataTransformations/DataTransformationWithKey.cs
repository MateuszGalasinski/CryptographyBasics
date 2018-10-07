using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DES.DataTransformations
{
    public class DataTransformationWithKey : IDataTransformation
    {
        public Action<BitArray, BitArray> Transformation { get; }
        public BitArray Key { get; }

        public DataTransformationWithKey(Action<BitArray, BitArray> transformation, BitArray key)
        {
            Transformation = transformation;
            Key = key;
        }

        public void Transform(BitArray data)
        {
            Transformation(data, Key);
        }
    }
}
