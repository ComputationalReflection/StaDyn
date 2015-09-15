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
				
		public static var Func2(var StrParI1, var StrParI2)
        {
			var IntLoc;
            var CharLoc = ' ';

            IntLoc = 1;
            while(IntLoc <= 1)
            {
                if (Func1(StrParI1.ToCharArray()[IntLoc], StrParI2.ToCharArray()[IntLoc + 1]) == Idents.Ident1)
                {
                    CharLoc = 'A';
                    IntLoc = IntLoc + 1;
                }
            }
            if ((CharLoc >= 'W') && (CharLoc <= 'Z'))
                IntLoc = 7;
            if (CharLoc == 'X')
                return true;
            else
            {
                if (StrParI1.CompareTo(StrParI2) > 0)
                {
                    IntLoc = IntLoc + 7;
                    return true;
                }
                else return false;
            }         
        }
		
		public static void TestFunc2()
		{			
			var StrParI1 = "DHRYSTONE PROGRAM, 1'ST STRING";
			var StrParI2 = "DHRYSTONE PROGRAM, 2'ND STRING";
			var result = Pystone.Func2(StrParI1,StrParI2);			
			if(result != false)
				Environment.Exit(-1);					
		}	
		
        public static void Main()
        {	
			Pystone.Test();
			Console.WriteLine("Pystone completed!!");		
        }	

		public static void Test()
		{
			
			Pystone.TestFunc2(); 
			Console.WriteLine("TestFunc2 completed!!");			
		}		
    }
}