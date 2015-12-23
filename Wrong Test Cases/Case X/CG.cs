using System;

namespace ProgramSpecialization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var i = 0;
            while (i < 10)
            {
                i = i + 1;
            }            
            System.Console.WriteLine("Result {0}", i.ToString());            
        }
    }
}