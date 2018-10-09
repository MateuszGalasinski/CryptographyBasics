using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DESAlgorithm.PaddingStrategies
{
    public interface IPaddingStrategy
    {
        BitArray AddPadding(BitArray message);
        BitArray RemovePadding(BitArray message);
    }
}
