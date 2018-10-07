using System;
using System.Collections;
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

        public BitArray Decrypt(BitArray data)
        {
            foreach (IDataTransformation transformation in _decryptSteps)
            {
                data = transformation.Transform(data);
            }

            return data;
        }

        public BitArray Encrypt(BitArray data)
        {
            foreach (IDataTransformation transformation in _encryptSteps)
            {
                data = transformation.Transform(data);
            }

            return data;
        }
    }
}
