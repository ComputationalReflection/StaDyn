using System;

namespace ProgramSpecialization
{
    public class A
    {
        public var data;

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
        public var data;

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
        public var Method(var dataClass)
        {
            return dataClass.data;
        }

        public static void Main()
        {
            Program program = new Program();
            var resultA = program.Method(new A(1));
            System.Console.WriteLine(resultA.ToString());
			var resultB = program.Method(new B(1));
            System.Console.WriteLine(resultB.ToString());
        }
    }
}