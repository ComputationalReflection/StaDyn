using System;

namespace ProgramSpecialization
{
    public class A
    {
        private int data;

        public A(int data)
        {
            this.data = data;
        }

        public override String ToString()
        {
            return "A Class";
        }
    }

    public class B
    {
        private int data;

        public B(int data)
        {
            this.data = data;
        }

        public override String ToString()
        {
            return "B Class";
        }
    }

    public class Program
    {
        public var Method(var className, var data)
        {
            var result;
            if (className == 'A')
                result = 1 + data;
            else
                result = 2 + data;
            return result;
        }

        public static void Main()
        {
            Program program = new Program();
            var result = program.Method('A', 1);
            System.Console.WriteLine(result.ToString());
        }
    }
}