using System;

namespace ProgramSpecialization
{
    public class A
    {
        public override String ToString()
        {
            return "A Class";
        }
    }

    public class B
    {
        public override String ToString()
        {
            return "B Class";
        }
    }

    public class Program
    {
        public var Method(var className)
        {
            var result;
            if (className == 'A')
                result = new A();
            else
                result = new B();
            return result;
        }

        public static void Main()
        {
            Program program = new Program();
            var result = program.Method('A');
            System.Console.WriteLine(result.ToString());
        }
    }
}