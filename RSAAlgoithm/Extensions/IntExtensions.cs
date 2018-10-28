namespace RSAAlgoithm.Extensions
{
    public static class IntExtensions
    {
        public static int SumModuloTwo(this int first, int second)
        {
            return (first + second) % 2;
        }
    }
}