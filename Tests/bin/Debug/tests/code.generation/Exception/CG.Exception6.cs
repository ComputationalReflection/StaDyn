using System;
namespace CG.Excepcion 
{
	public class CGException6
	{
		public static void Main() 
		{
			try {
				System.Console.WriteLine("Try"); 
			} catch (OverflowException e) {
					System.Console.WriteLine("Overflow:Catch-" + e.Message);
			} catch (System.Exception e1) {
					System.Console.WriteLine("System:Catch-" + e1.Message);
			} finally {
					System.Console.WriteLine("Finally");
			}		
		}
    }
}