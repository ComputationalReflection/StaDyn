using System;

namespace ProgramSpecialization
{	
    public class Program
    {
		public static var inc(var x) { return x+1; }
		public static var inc(int x) { return x+1; }
				
        public static void Main(string[] args)
        {			
            int i = inc(1);
			double d = inc(2.5);
			System.Console.WriteLine("Result {0} ",i);					
			System.Console.WriteLine("Result {0} ",d);					
        }
	}
}