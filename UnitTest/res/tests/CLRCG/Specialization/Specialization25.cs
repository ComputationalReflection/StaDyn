using System;

namespace ProgramSpecialization
{
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
            var result = program.Method(1);            
            System.Console.WriteLine(result.ToString());
            result = program.Method("1");
            System.Console.WriteLine(result.ToString());
        }
    }
}