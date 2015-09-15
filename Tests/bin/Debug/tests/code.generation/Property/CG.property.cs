using System;

namespace CG.Property 
{
	public class Test 
	{
		public static void Main() {
			var referencia;
			if (true)
				referencia = "Hello world!!";
			else
				referencia = 3;
			int length = referencia.Length;
			Console.WriteLine(length);
		}
	}
}


