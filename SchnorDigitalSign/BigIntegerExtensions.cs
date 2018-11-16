using System.Numerics;

namespace SchnorDigitalSign
{
    public static class BigIntegerExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expBase"></param>
        /// <param name="modulus"> Has to be prime number</param>
        public static BigInteger ModInv(this BigInteger expBase, BigInteger modulus)
        {
            return BigInteger.ModPow(expBase, modulus - 2, modulus);
        }
    }
}
