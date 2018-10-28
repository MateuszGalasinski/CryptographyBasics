using RSAAlgoithm.Models;
using System;

namespace RSAAlgorithm
{
    public class DataTransformationWithKey : IDataTransformation
    {
        public DataTransformationWithKey(Func<DataSet, Key, DataSet> transformation, Key key)
        {
            Transformation = transformation;
            Key = key;
        }

        public Func<DataSet, Key, DataSet> Transformation { get; }
        public Key Key { get; }

        public DataSet Transform(DataSet data)
        {
            return Transformation(data, Key);
        }
    }
}