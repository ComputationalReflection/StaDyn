using System;

namespace Pybench.Aritmethic
{

    public class A
    {
        private dynamic attr;        
        public A(dynamic attr)
        {
            this.attr = attr;            
        }
        public override string ToString()
        {
            return "A[attr=" + attr.ToString() + "]";
        }
    }


    public class Program
    {
        public static void Main(string[] args)
        {

            dynamic attr;
			if(true)
				attr = 1;
			else
				attr = "One";

            dynamic a = new A(attr);
            
            Console.WriteLine(a);           
        }
    }
}