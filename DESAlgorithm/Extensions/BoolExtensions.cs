using System;
using System.Collections.Generic;
using System.Text;

namespace DESAlgorithm.Extensions
{
    public static class BoolExtensions
    {
        public static int ToInt(this bool value)
        {
            return value == true ? 1 : 0;
        }
    }
}
