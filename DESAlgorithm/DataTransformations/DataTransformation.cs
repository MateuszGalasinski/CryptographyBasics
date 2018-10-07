using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DES.DataTransformations
{
    public class DataTransformation : IDataTransformation
    {
        public Func<BitArray, BitArray> Transformation { get; }

        public DataTransformation(Func<BitArray, BitArray> transformation)
        {
            Transformation = transformation;
        }

        public BitArray Transform(BitArray data)
        {
            return Transformation(data);
        }
    }
}
