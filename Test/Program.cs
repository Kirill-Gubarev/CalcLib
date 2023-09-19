using CalcLib;
using System;
namespace Test
{
    internal class Program
    {
        static void Main()
        {
            //Function f = new Function("",('x',5));
            //Console.WriteLine(f.Calc());
            while(true)
            {
                Console.WriteLine(Calculator.Calc(Console.ReadLine()));
            }

            Console.ReadLine();
        }
    }
}
