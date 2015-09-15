/*A port to StaDyn (dynamic) of the pystone benchmark using duck typing.*/

using System;

namespace StaDyn.Dynamic
{
	public class Idents
    {
        public static var Ident1 = 1;
        public static var Ident2 = 2;
    }

	public class Pystone
    {      
		public static var Func1(var CharPar1, var CharPar2)
        {			
            var CharLoc1;
            var CharLoc2;
            CharLoc1 = CharPar1;
            CharLoc2 = CharLoc1;
			if (CharLoc2 != CharPar2)
                return Idents.Ident1;
            else 
				return Idents.Ident2;
        }
		
		public static void TestFunc1()
		{			
			var CharPar1 = 'H';
			var CharPar2 = 'R';			
			var result = Pystone.Func1(CharPar1,CharPar2);			
			if(result != Idents.Ident1)
				Environment.Exit(-1);					
		}

        public static void Main()
        {	
			Pystone.Test();
			Console.WriteLine("Pystone completed!!");		
        }	

		public static void Test()
		{		
			Pystone.TestFunc1();
			Console.WriteLine("TestFunc1 completed!!");		
		}		
    }
}