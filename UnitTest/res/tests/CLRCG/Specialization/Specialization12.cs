using System;

namespace ProgramSpecialization
{	
	public class Program
    {		
		public static var inc(var x) 
		{ 					
			var inta = new int[1];
			inta[0] = 1;
			return x + inta[0];
		}
				
        public static void Main(string[] args)
        {		
			var result = inc(1);
			System.Console.WriteLine("Result {0} ",result.ToString());					
        }
	}
}