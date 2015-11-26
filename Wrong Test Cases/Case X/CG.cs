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
            var result;
			result = 1 + program.MainMethod(1);  			
            System.Console.WriteLine("Result {0}", result); 
            result = program.MainMethod("1");
            System.Console.WriteLine("Result {0}", result);
        }
    }
}