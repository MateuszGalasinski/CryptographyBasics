using System;
using System.Collections;
using DESAlgorithm.Models;

namespace DES.DataTransformations
{
    public class DataTransformationWithKey : IDataTransformation
    {
        public Func<DataSet, BitArray, DataSet> Transformation { get; }
        public BitArray Key { get; }

        public DataTransformationWithKey(Func<DataSet, BitArray, DataSet> transformation, BitArray key)
        {
            Transformation = transformation;
            Key = new BitArray(key);
        }

        public DataSet Transform(DataSet data)
        {
            return Transformation(data, Key);
        }
    }
}
