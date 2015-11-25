using System;

namespace ProgramSpecialization
{	
	public class Program 
	{
		public static var addition(var x, int y){ return x + y; }
		public static void Main(string[] args) 
		{
			int i = addition(3,4);		
			double d = addition(3.5,4);	
			System.Console.WriteLine("Result {0} ",i);					
			System.Console.WriteLine("Result {0} ",d);				
		}
    }
}