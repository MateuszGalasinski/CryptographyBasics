using SchnorDigitalSign.Model;
using System.Numerics;

namespace SchnorDigitalSign
{
    public class SchnorrAlgorithm
    {
        private UserKeys userKeys;

        public Signature SignMessage(byte[] message, KeyPair keyPair)
        {
            BigInteger r = 0;



            int x = 0;


            return null;
        }

        public bool Verify(byte[] message, KeyPair keyPair, Signature signature, BigInteger SenderPublicKey)
        {
            return false;
        }
    }
}
