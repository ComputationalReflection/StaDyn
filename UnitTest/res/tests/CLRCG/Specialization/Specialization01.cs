using System;

namespace Specialization
{	
    public class Program
    {
		public static var inc(var x) { return x+1; }
				
        public static void Main(string[] args)
        {			
            int i = inc(1);
			System.Console.WriteLine("Result {0} ",i);            
        }
	}
}