using RSAAlgoithm.Models;
using System;
using System.Numerics;


namespace RSAAlgorithm
{
    public class DataTransformationWithKey : IDataTransformation
    {
        public DataTransformationWithKey(Func<DataSet, BigInteger, DataSet> transformation, BigInteger key)
        {
            Transformation = transformation;
            Key = key;
        }

        public Func<DataSet, BigInteger, DataSet> Transformation { get; }
        public BigInteger Key { get; }

        public DataSet Transform(DataSet data)
        {
            return Transformation(data, Key);
        }
    }
}