using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DESAlgorithm.Models;

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

        public DataSet Decrypt(DataSet data)
        {
            foreach (IDataTransformation transformation in _decryptSteps)
            {
                data = transformation.Transform(data);
            }

            return data;
        }

        public DataSet Encrypt(DataSet data)
        {
            foreach (IDataTransformation transformation in _encryptSteps)
            {
                data = transformation.Transform(data);
            }

            return data;
        }
    }
}
