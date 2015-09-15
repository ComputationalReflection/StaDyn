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

    public class Record
    {
        public var PtrComp;        
        public var Discr;
		public var EnumComp;
		public var IntComp;
		public var StringComp;

        public Record(var Discr, var EnumComp, var IntComp, var StringComp)
        {            
            this.Discr = Discr;  
			this.EnumComp = EnumComp;
			this.IntComp = IntComp;
			this.StringComp = StringComp;
        }        
		
		public var copy()
        {
            var record = new Record(this.Discr, this.EnumComp, this.IntComp, this.StringComp);
			record.PtrComp = this.PtrComp;
			return record;
        }
    }
	
	public class Pystone
    {
        public static var IntGlob = 0;        
        public static var PtrGlb;
      		
		public static var Proc1(var PtrParIn)
        {
            var NextRecord = PtrGlb.copy();
            PtrParIn.PtrComp = NextRecord;
            PtrParIn.IntComp = 5;
            NextRecord.IntComp = PtrParIn.IntComp;
            NextRecord.PtrComp = PtrParIn.PtrComp;
            NextRecord.PtrComp = Proc3(NextRecord.PtrComp);
            if (NextRecord.Discr == Idents.Ident1)
            {
                NextRecord.IntComp = 6;
                NextRecord.EnumComp = Proc6(PtrParIn.EnumComp);
                NextRecord.PtrComp = PtrGlb.PtrComp;
                NextRecord.IntComp = Proc7(NextRecord.IntComp, 10);
            }
            else PtrParIn = NextRecord.copy();
            NextRecord.PtrComp = null;
            return PtrParIn;
        }
		
		public static void TestProc1()
		{			
			var PtrParIn = new Record(Idents.Ident1, Idents.Ident3, 40, "DHRYSTONE PROGRAM, SOME STRING");			
            Pystone.PtrGlb = PtrParIn;			
			var result = Pystone.Proc1(PtrParIn);
			if(result.Discr != Idents.Ident1)
				Environment.Exit(-1);
			if(result.EnumComp != Idents.Ident3)
				Environment.Exit(-1);
			if(result.IntComp != 12)
				Environment.Exit(-1);
			if(!("DHRYSTONE PROGRAM, SOME STRING".Equals(result.StringComp)))
				Environment.Exit(-1);
		}	
						
		public static var Proc3(var PtrParOut)
        {
            if (PtrGlb != null)
            {
                PtrParOut = PtrGlb.PtrComp;
            }
            else IntGlob = 100;
            PtrGlb.IntComp = Proc7(10, IntGlob);
            return PtrParOut;
        }
		
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
		
		public static var Proc7(var IntParI1, var IntParI2)
        {
            var IntParOut;
            var IntLoc;

            IntLoc = IntParI1 + 2;
            IntParOut = IntParI2 + IntLoc;
            return IntParOut;
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
			Pystone.TestProc1();
			Console.WriteLine("TestProc1 completed!!");			
		}		
    }
}