using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DES
{
    interface ICryptoAlgorithm
    {
        BitArray Encrypt(BitArray data);
        BitArray Decrypt(BitArray data);
    }
}
