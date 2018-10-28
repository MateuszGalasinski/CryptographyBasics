using RSAAlgoithm.Models;
using System.Collections.Generic;

namespace RSAAlgorithm
{
    public class RSABuilder
    {
        private readonly List<IDataTransformation> _decryptSteps = new List<IDataTransformation>();
        private readonly List<IDataTransformation> _encryptSteps = new List<IDataTransformation>();

        public CryptoAlgorithm Build(Key key)
        {


            return new CryptoAlgorithm(_encryptSteps, _decryptSteps, new CMSPaddingStrategy());
        }
    }
}
