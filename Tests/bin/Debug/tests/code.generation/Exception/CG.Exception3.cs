using System;
namespace CG.Excepcion 
{
	public class CGException3
	{
		public static void Main() 
		{
			try {
				 throw new OverflowException("Try thrown");			
			} catch (OverflowException e) {
				try{
					System.Console.WriteLine("Overflow:Catch-" + e.Message);					
					throw new Exception("Re-throw: Catched exception",e);
				} catch (System.Exception e1) {
					System.Console.WriteLine("Catch-" + e1.Message);
				}
			}
		}			
	}
}