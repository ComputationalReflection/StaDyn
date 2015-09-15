using System;
namespace CG.Excepcion 
{
	public class CGException4
	{
		public static void Main() 
		{
			try {
				throw new Exception("Try thrown");
			} catch (OverflowException e) {
				System.Console.WriteLine("Catch-OverFlow-Outer-" + e.Message);
			} catch (System.Exception e1) {
				System.Console.WriteLine("Catch-Exception-Outer-" + e1.Message);
				try {
					throw new Exception("Try thrown-Inner");
				} catch (OverflowException e3) {
					System.Console.WriteLine("Catch-OverFlow-Inner-" + e3.Message);
				} catch (System.Exception e4) {
					System.Console.WriteLine("Catch-Exception-Inner-" + e4 .Message);
				} finally {
					System.Console.WriteLine("Finally-Inner");
				}
			} finally {
				System.Console.WriteLine("Finally");
			}
		}
    }
}