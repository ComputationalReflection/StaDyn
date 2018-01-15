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
        public var MainMethod(var param)
        {
            return NestedMethod(param);
        }

        public var NestedMethod(var param)
        {
            return param;
        }

        public static void Main()
        {
            Program program = new Program();
            var result;
            result = program.MainMethod(new A());
            System.Console.WriteLine("Result {0}", result);
            result = program.MainMethod(new B());
            System.Console.WriteLine("Result {0}", result);
            result = program.MainMethod(program.NestedMethod(1));
            System.Console.WriteLine("Result {0}", result);
            result = program.MainMethod(program.NestedMethod("1"));
            System.Console.WriteLine("Result {0}", result);
        }
    }
}