using System;
using System.Collections.Generic;
using System.Text;

namespace DES
{
    interface ICryptoAlgorithm
    {
        void Encrypt(byte[] data);
        void Decrypt(byte[] data);
    }
}
