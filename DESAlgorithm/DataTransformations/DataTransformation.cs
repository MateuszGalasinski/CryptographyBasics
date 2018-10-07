using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DES.DataTransformations
{
    public class DataTransformation : IDataTransformation
    {
        public Action<BitArray> Transformation { get; }

        public DataTransformation(Action<BitArray> transformation)
        {
            Transformation = transformation;
        }

        public void Transform(BitArray data)
        {
            Transformation(data);
        }
    }
}
