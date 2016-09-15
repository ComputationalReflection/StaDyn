using System;

namespace ProgramSpecialization
{
    public class Program
    {
        private static var Param(var x)
        {
            return Inc(x) + Inc(x / 2);
        }

        private static var Inc(var x)
        {
            return x + 1;
        }

        public static void Main(string[] args)
        {
            var result = Program.Param(2);
            System.Console.WriteLine("Result: {0} ", result);
        }
    }
}