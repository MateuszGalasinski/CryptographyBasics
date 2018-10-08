using System;
using System.Collections.Generic;
using System.Text;

namespace DESAlgorithm.Extensions
{
    public static class IntExtensions
    {
        public static int SumModuloTwo(this int first, int second)
        {
            return (first + second) % 2;
        }
    }
}
