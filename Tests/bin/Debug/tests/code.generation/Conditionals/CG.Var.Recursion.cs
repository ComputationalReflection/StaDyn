using System;

public class CGVarRecursion
{
	private static var attribute;	

	public static void Main() 
	{	
		if (true)
			 attribute = "hola";
		else 
			 attribute = 3;
		attribute + 2;
	}
}


