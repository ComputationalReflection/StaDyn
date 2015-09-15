/*A port to StaDyn (dynamic) of the pystone benchmark using duck typing.*/

using System;

namespace StaDyn.Dynamic
{
	public class Idents
    {
        public static var Ident1 = 1;
        public static var Ident2 = 2;
        public static var Ident3 = 3;
        public static var Ident4 = 4;
        public static var Ident5 = 5;
    }

	public class Pystone
    {
		public static var IntGlob = 0;
		
		public static var Proc6(var EnumParIn)
        {
            var EnumParOut;

            EnumParOut = EnumParIn;
            if (!Func3(EnumParIn))
                EnumParOut = Idents.Ident4;
            if (EnumParIn == Idents.Ident1)
                EnumParOut = Idents.Ident1;
            else
            {
                if (EnumParIn == Idents.Ident2)
                {
                    if (IntGlob > 100) EnumParOut = Idents.Ident1;
                    else EnumParOut = Idents.Ident4;
                }
                else
                {
                    if (EnumParIn == Idents.Ident3)
                        EnumParOut = Idents.Ident2;
                    else
                    {
                        if (EnumParIn == Idents.Ident4)
                        {
                        }
                        else
                        {
                            if (EnumParIn == Idents.Ident5)
                                EnumParOut = Idents.Ident3;
                        }
                    }
                }
            }
            return EnumParOut;
        }
		
		public static void TestProc6()
		{			
			Pystone.IntGlob = 5;
			var EnumParIn = 3;
			var result = Pystone.Proc6(EnumParIn);			
			if(result != 2)
				Environment.Exit(-1);				
		}
				
		public static var Func3(var EnumParIn)
        {
            var EnumLoc;
            EnumLoc = EnumParIn;
            if (EnumLoc == Idents.Ident3) return true;
            return false;
        }
				
        public static void Main()
        {	
			Pystone.Test();
			Console.WriteLine("Pystone completed!!");		
        }	
		
		public static void Test()
		{			
			Pystone.TestProc6();
			Console.WriteLine("TestProc6 completed!!");		
		}		
    }
}