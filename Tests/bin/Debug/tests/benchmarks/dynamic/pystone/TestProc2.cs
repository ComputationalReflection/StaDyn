/*A port to StaDyn (dynamic) of the pystone benchmark using duck typing.*/

using System;

namespace StaDyn.Dynamic
{
	public class Idents
    {
        public static var Ident1 = 1;        
        public static var Ident5 = 5;
    }
   	
	public class Pystone
    {
        public static var IntGlob = 0;       
        public static var Char1Glob = '\0';
       
		public static var Proc2(var IntParIO) {
            var IntLoc;
            var EnumLoc = Idents.Ident5;

            IntLoc = IntParIO + 10;            
            while (true) { //Esto es un while
                if (Pystone.Char1Glob == 'A') {
                    IntLoc = IntLoc - 1;
                    IntParIO = IntLoc - Pystone.IntGlob;
                    EnumLoc = Idents.Ident1;
                }
                if (EnumLoc == Idents.Ident1)
					return IntParIO;
            }
            return IntParIO;
        }
		
		public static void TestProc2()
		{
			var IntParIO = 3;
			Pystone.Char1Glob = 'A';
			Pystone.IntGlob = 5;
			var result = Pystone.Proc2(IntParIO);
			if(result != 7)
				Environment.Exit(-1);
		}	
		
        public static void Main()
        {	
			Pystone.Test();
			Console.WriteLine("Pystone completed!!");		
        }	
		
		public static void Test()
		{			
			Pystone.TestProc2();
			Console.WriteLine("TestProc2 completed!!");		
		}		
    }
}