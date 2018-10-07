using DES.DataTransformations;
using System;
using System.Collections.Generic;
using System.Text;

namespace DES.AlgorithmBuilders
{
    public class DESBuilder
    {
        List<IDataTransformation> steps = new List<IDataTransformation>();

        public CryptoAlgorithm Build()
        {
            return new CryptoAlgorithm(steps);
        }

        //1st step
        public void AddPermutation()
        {
            steps.Add(new DataTransformation(p =>
            {
                p[0] = unchecked((byte)~p[0]);
            }));
        }
    }
}
