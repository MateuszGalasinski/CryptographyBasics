using RSAAlgoithm.Models;
using System.Collections.Generic;
using System.Numerics;

namespace RSAAlgorithm
{
    public class RSABuilder
    {
        private readonly List<IDataTransformation> _decryptSteps = new List<IDataTransformation>();
        private readonly List<IDataTransformation> _encryptSteps = new List<IDataTransformation>();

        public CryptoAlgorithm Build(Key key)
        {
            Key onlyPublicKey = new Key()
            {
                PublicKey = key.PublicKey
            };

            _encryptSteps.Add
            (
                new DataTransformationWithKey(
                    (dataSet, publicKey) =>
                        {
                            DataSet result = new DataSet()
                            {
                                Message = BigInteger.ModPow(dataSet.Message, publicKey.PublicKey.E, publicKey.PublicKey.N)
                            };

                            return result;
                        },
                    onlyPublicKey)
            );

            _decryptSteps.Add
            (
                new DataTransformationWithKey(
                    (dataSet, fullKey) =>
                    {
                        DataSet result = new DataSet()
                        {
                            Message = BigInteger.ModPow(dataSet.Message, fullKey.PrivateKey.D, fullKey.PublicKey.N)
                        };

                        return result;
                    },
                    key)
            );

            return new CryptoAlgorithm(_encryptSteps, _decryptSteps, new CMSPaddingStrategy());
        }
    }
}
