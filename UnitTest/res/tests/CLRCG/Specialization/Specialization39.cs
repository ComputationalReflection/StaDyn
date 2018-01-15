using System;

namespace ProgramSpecialization
{
    public class Program
    {
        public static var inc(var x) { return x + 1; }

        public static var param(var x)
        {
            return inc(x);
        }

        public static void Main(string[] args)
        {
            var r = param(1);
            int i = inc(1);
            double d = inc(2.5);
            System.Console.WriteLine("Result {0} ", r);
            System.Console.WriteLine("Result {0} ", i);
            System.Console.WriteLine("Result {0} ", d);
        }
    }
}