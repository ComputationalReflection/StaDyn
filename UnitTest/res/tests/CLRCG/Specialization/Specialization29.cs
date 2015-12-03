using System;

namespace ProgramSpecialization
{
    public class Program
    {
        public var Method(var node)
        {
            return node;
        }

        public static void Main(string[] args)
        {
            var data;
            var result;
            if (true)
            {
                data = 1;
                result = Method(data);
                System.Console.WriteLine("Result {0}", result);
            }
            else
            {
                data = "1";
                result = Method(data);
                System.Console.WriteLine("Result {0}", result);
            }
            result = Method(data);
            System.Console.WriteLine("Result {0}", result);
            Console.WriteLine("Successful!!");
        }
    }
}