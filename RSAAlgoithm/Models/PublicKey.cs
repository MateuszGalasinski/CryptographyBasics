using System.Numerics;

namespace RSAAlgoithm.Models
{
    public class PublicKey
    {
        public BigInteger N { get; set; }
        public BigInteger E { get; set; }
    }
}
