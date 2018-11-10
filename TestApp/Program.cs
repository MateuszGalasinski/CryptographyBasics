using System;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("\nGenerating 512-bits random pseudoprime. . .");
            Random rand = new Random();
            BigInteger prime = BigInteger.genPseudoPrime(512, 5, rand);
            Console.WriteLine("\n" + prime);

            int dwStart = System.Environment.TickCount;
            BigInteger.MulDivTest(1);
            //BigInteger.RSATest(10);
            //BigInteger.RSATest2(10);
            Console.WriteLine(System.Environment.TickCount - dwStart);
            Console.Read();
        }
    }
}
