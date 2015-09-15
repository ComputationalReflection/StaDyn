using System;
namespace CG.Excepcion 
{
	public class CGException5
	{
		public static void TestThrow(){
			try {
			  throw new Exception("Try thrown");
			} catch (OverflowException e) {
					System.Console.WriteLine("OverflowException:Catch-" + e.Message);
			} catch (System.Exception e1) {
					System.Console.WriteLine("Exception:Catch-" + e1.Message);
					throw;
			} finally {
					System.Console.WriteLine("Finally");
			}		
		}
		public static void Main() 
		{
			try {
				CGException5.TestThrow();
			} catch (Exception e) {
				System.Console.WriteLine("Catch-" + e.Message);
			}
		}
    }
}