using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DES
{
    interface ICryptoAlgorithm
    {
        void Encrypt(BitArray data);
        void Decrypt(BitArray data);
    }
}
