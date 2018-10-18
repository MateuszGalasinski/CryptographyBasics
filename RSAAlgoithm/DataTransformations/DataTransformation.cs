﻿using System;


namespace RSAAlgoithm
{
    public class DataTransformation : IDataTransformation
    {
        public DataTransformation(Func<DataSet, DataSet> transformation)
        {
            Transformation = transformation;
        }

        public Func<DataSet, DataSet> Transformation { get; }

        public DataSet Transform(DataSet data)
        {
            return Transformation(data);
        }

    }
}