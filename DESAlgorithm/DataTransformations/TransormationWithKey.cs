using System;
using System.Collections.Generic;
using System.Text;

namespace DES.DataTransformations
{
    public class TransformationWithKey : IDataTransformation
    {
        public Action<byte[], byte[]> Transformation { get; }
        public byte[] Key { get; }

        public TransformationWithKey(Action<byte[], byte[]> transformation, byte[] key)
        {
            Transformation = transformation;
            Key = key;

        }

        public void Transform(byte[] data)
        {
            Transformation(data, Key);
        }
    }
}
