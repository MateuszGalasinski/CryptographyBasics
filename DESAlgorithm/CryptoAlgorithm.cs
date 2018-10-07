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

        public void Decrypt(byte[] data)
        {
            throw new NotImplementedException();
        }

        public void Encrypt(byte[] data)
        {
            foreach (var dataTransformation in AlgorithmSteps)
            {
                dataTransformation.Transform(data);
            }
        }
    }
}
