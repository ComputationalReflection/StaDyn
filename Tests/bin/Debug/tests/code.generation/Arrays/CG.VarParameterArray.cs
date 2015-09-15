using System;

namespace CG.VarParameterArray {
	public class Dummy
	{
		public override string ToString()
		{
			return "Dummy object";
		}
	}

	public class Test
	{						
		public static var GetElement(var source, int index)
		{
			return source[index];
		}
		
		public static void SetElement(var source, var value, int index)
		{
			source[index] = value;
		}
		
        public static void Main() {			
			var sourceDouble = new double[10];
			for (int i = 0; i < sourceDouble.Length; i++)
                Test.SetElement(sourceDouble,(double)i,i);
			double d = Test.GetElement(sourceDouble,0);
			System.Console.WriteLine("double d " + d.ToString());
						
			var sourceInt = new int[10];
            for (int i = 0; i < sourceInt.Length; i++)
                Test.SetElement(sourceInt,i,i);
			int i = Test.GetElement(sourceInt,0);
			System.Console.WriteLine("int i " + i.ToString());			
			
			var sourceString = new string[10];
			for (int i = 0; i < sourceString.Length; i++)
				Test.SetElement(sourceString,"String " + i,i);                
			string s = Test.GetElement(sourceString,0);
			System.Console.WriteLine("String s " + s.ToString());			
			
			var sourceDummy = new Dummy[10];
			for (int i = 0; i < sourceDummy.Length; i++)
				Test.SetElement(sourceDummy,new Dummy(),i);                                
			Dummy du = Test.GetElement(sourceDummy,0);
			System.Console.WriteLine("Dummy du " + du.ToString());			
			
			var sourceObject = new object[4];
			Test.SetElement(sourceObject,1,0);                                
			Test.SetElement(sourceObject,1.0,1);
			Test.SetElement(sourceObject,"1",2);                                
			Test.SetElement(sourceObject,new Dummy(),3);
			object o = Test.GetElement(sourceObject,0);
			System.Console.WriteLine("object o " + o.ToString());			
						
			var sourceVar = new var[4];
			Test.SetElement(sourceVar,1,0);                                
			Test.SetElement(sourceVar,new Dummy(),1);
			Test.SetElement(sourceVar,"1",2);                                
			Test.SetElement(sourceVar,1.0,3);
			var v = Test.GetElement(sourceVar,0);
			System.Console.WriteLine("var v " + v.ToString());						
        }
    }
}