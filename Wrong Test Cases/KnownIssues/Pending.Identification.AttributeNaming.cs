using System;
namespace Identification.Attributes
{    
    public class Program
    {
        public static void Main(string[] args)
        {
            //Assemble Error: array is a keyword in IL. Cannot be used as variable name
            int[] array = new int[] {10, 12, 14, 16};
        }
    }
}