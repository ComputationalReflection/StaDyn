using System;
namespace CG.Excepcion 
{
	public class CGException2
	{
		public static void Main() 
		{
			try {
				throw new Exception("Try thrown");
			} 
			catch (System.Exception e1) {
				System.Console.WriteLine("Exception:Catch-" + e1.Message);
			}
			finally {
				System.Console.WriteLine("Finally");
			}	
		}
	}
}