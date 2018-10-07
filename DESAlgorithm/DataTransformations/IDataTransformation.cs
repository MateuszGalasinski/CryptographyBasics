using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DES
{
    public interface IDataTransformation
    {
        void Transform(BitArray data);
    }
}
