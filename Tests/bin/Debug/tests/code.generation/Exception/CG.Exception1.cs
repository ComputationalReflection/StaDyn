using System;
namespace CG.Excepcion 
{
	public class CGException1 
	{
		public static void Main() 
		{
			try {
			  throw new OverflowException("Try thrown");
			} catch (OverflowException e) {
					System.Console.WriteLine("Overflow:Catch-" + e.Message);
			} catch (System.Exception e1) {
					System.Console.WriteLine("Exception:Catch-" + e1.Message);
			} finally {
					System.Console.WriteLine("Finally");
			}		
		}
	}
}