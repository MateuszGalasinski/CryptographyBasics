using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DES
{
    public interface IDataTransformation
    {
        BitArray Transform(BitArray data);
    }
}
