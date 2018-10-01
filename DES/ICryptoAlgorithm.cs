using System;
using System.Collections.Generic;
using System.Text;

namespace DES
{
    interface ICryptoAlgorithm
    {
        byte[] Encrypt(byte[] data);
        byte[] Decrypt(byte[] data);
    }
}
