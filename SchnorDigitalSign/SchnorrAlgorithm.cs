using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using SchnorDigitalSign.Model;

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
