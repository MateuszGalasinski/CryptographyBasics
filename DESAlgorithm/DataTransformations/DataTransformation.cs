using System;
using DESAlgorithm.Models;

namespace DES.DataTransformations
{
    public class DataTransformation : IDataTransformation
    {
        public Func<DataSet, DataSet> Transformation { get; }

        public DataTransformation(Func<DataSet, DataSet> transformation)
        {
            Transformation = transformation;
        }

        public DataSet Transform(DataSet data)
        {
            return Transformation(data);
        }
    }
}
