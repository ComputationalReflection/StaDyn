using System;

namespace ProgramSpecialization
{
    public class A
    {
        private var data;

        public A(var data)
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
        private var data;

        public B(var data)
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
                result = new A(data);
            else
                result = new B(data);
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
