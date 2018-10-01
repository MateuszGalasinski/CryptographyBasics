using System;
using System.Collections.Generic;
using System.Text;

namespace DES
{
    public class CryptoAlgorithm : ICryptoAlgorithm
    {
        public List<IDataTransformation> AlgorithmSteps { get; } = new List<IDataTransformation>();

        public CryptoAlgorithm(List<IDataTransformation> algorithmSteps)
        {
            AlgorithmSteps = algorithmSteps;
        }

        public byte[] Decrypt(byte[] data)
        {
            throw new NotImplementedException();
        }

        public byte[] Encrypt(byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}
