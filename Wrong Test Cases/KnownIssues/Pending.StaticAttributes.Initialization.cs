using System;
namespace StaticAttributes.Initialization
{    
    public class Program
    {
        public char c;
        public int i;
        public double d;
        public Type[] myTypes = new Type[] { c.GetType(), i.GetType(), d.GetType() };

        public static char cStatic;
        public static int iStatic;
        public static double dStatic;
        //Internal Error durring code generation
        public static Type[] myTypesStatic = new Type[] { cStatic.GetType(), iStatic.GetType(), dStatic.GetType() };

        public static void Main(string[] args)
        {
            //Ok. No error
            Program p = new Program();
            Console.WriteLine(p.myTypes);
        }
    }
}