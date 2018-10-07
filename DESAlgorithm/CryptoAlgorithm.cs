using System;
using System.Collections.Generic;
using System.Text;

namespace DES
{
    public class CryptoAlgorithm : ICryptoAlgorithm
    {
        private List<IDataTransformation> _encryptSteps { get; }
        private List<IDataTransformation> _decryptSteps { get; }

        public CryptoAlgorithm(List<IDataTransformation> encryptSteps, List<IDataTransformation> decryptSteps)
        {
            _encryptSteps = encryptSteps;
            _decryptSteps = decryptSteps;
        }

        public void Decrypt(byte[] data)
        {
            _decryptSteps.ForEach(p => p.Transform(data));
        }

        public void Encrypt(byte[] data)
        {
            _encryptSteps.ForEach(p => p.Transform(data));
        }
    }
}
