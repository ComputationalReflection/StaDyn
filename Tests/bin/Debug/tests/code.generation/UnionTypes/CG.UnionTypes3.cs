using System;

namespace CG.UnionTypes
{
	public class Test 
	{
		public static void Main() { 			 
			var referencia;
			if (false)
				referencia = "dfa";
			else
				referencia = 3;
			Console.WriteLine(referencia.Length);
		}
	}
}
