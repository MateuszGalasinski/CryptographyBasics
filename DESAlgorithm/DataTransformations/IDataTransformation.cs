using System;
using System.Collections.Generic;
using System.Text;

namespace DES
{
    public interface IDataTransformation
    {
        void Transform(byte[] data);
    }
}
