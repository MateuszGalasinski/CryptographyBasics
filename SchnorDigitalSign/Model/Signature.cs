using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SchnorDigitalSign.Model
{
    public class Signature
    {
        public BigInteger e { get; set; }
        public BigInteger y { get; set; }
    }
}
