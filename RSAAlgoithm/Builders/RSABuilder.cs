using System.Collections.Generic;

namespace RSAAlgoithm
{
    public class RSABuilder
    {
        readonly List<IDataTransformation> _decryptSteps = new List<IDataTransformation>();
        readonly List<IDataTransformation> _encryptSteps = new List<IDataTransformation>();

        public CryptoAlgorithm Build()
        {
            return new CryptoAlgorithm(_encryptSteps, _decryptSteps, new CMSPaddingStrategy());
        }

        public void AddWholeRSA()
        {

        }
    }
}
