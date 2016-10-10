using System;
namespace Boxing.CastExpression
{    
    public class Program
    {
        public object attr; 
        public static void Main(string[] args)
        {
            Program p = new Program();
            p.attr = 220;
            //Causes a silent error. Condition is false ('unbox.any int32' missing after pushing the object attr.)
            //Fixed at revision 1596:
            //Added a Promotion in VisitorILCodeGeneration::Visit(CastExpression) when the CastType is a valueType
            //and the expression to cast is interanally an object. Is a more specific condition needed??
            int myInteger = (int)p.attr;
            if(myInteger == 220)
                Console.WriteLine("OK");
            else Console.WriteLine("Test failed");
        }
    }
}