using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DESAlgorithm.Models;

namespace DES
{
    public interface ICryptoAlgorithm
    {
        DataSet Encrypt(DataSet data);
        DataSet Decrypt(DataSet data);
    }
}
