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
            int i = program.MainMethod(1);			
            String s = program.MainMethod("1");            
        }
    }
}