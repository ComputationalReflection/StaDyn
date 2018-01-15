using System;

namespace Pybench.Aritmethic
{

    public class A
    {
        private dynamic attr;
        private dynamic attr2;
        public A(dynamic attr, dynamic attr2)
        {
            this.attr = attr;
            this.attr2 = attr2;
        }
        public override string ToString()
        {
            return "A[attr=" + attr.ToString() + ",attr2=" + attr2.ToString() + "]";
        }
    }


    public class Program
    {
        public static void Main(string[] args)
        {

            dynamic i = 1;
            dynamic s = "One";

            dynamic a1 = new A(i, i);
            dynamic a2 = new A(s, i);
            dynamic a3 = new A(i, s);
            dynamic a4 = new A(s, s);

            Console.WriteLine(a1);
            Console.WriteLine(a2);
            Console.WriteLine(a3);
            Console.WriteLine(a4);


        }
    }
}