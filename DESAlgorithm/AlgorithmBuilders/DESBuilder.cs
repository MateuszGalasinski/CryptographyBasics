using DES.DataTransformations;
using System;
using System.Collections.Generic;
using System.Text;

namespace DES.AlgorithmBuilders
{
    public class DESBuilder
    {
        List<IDataTransformation> _encryptSteps = new List<IDataTransformation>();
        List<IDataTransformation> _decryptSteps = new List<IDataTransformation>();

        public CryptoAlgorithm Build()
        {
            return new CryptoAlgorithm(_encryptSteps, _decryptSteps);
        }

        public void AddWholeDES()
        {
            AddEncryptPermutation();
        }

        //1st step
        public void AddEncryptPermutation()
        {
            _encryptSteps.Add(new DataTransformation(p =>
            {
                p[0] = unchecked((byte)~p[0]);
            }));
        }
    }
}
