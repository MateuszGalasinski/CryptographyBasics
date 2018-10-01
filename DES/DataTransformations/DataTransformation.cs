using System;
using System.Collections.Generic;
using System.Text;

namespace DES.DataTransformations
{
    public class DataTransformation : IDataTransformation
    {
        public Action<byte[]> Transformation { get; }

        public DataTransformation(Action<byte[]> transormation)
        {
            Transformation = transormation;
        }

        public void Transform(byte[] data)
        {
            Transformation(data);
        }
    }
}
