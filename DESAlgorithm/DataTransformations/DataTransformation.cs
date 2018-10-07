using System;
using System.Collections.Generic;
using System.Text;

namespace DES.DataTransformations
{
    public class DataTransformation : IDataTransformation
    {
        public Action<byte[]> Transformation { get; }

        public DataTransformation(Action<byte[]> transformation)
        {
            Transformation = transformation;
        }

        public void Transform(byte[] data)
        {
            Transformation(data);
        }
    }
}
