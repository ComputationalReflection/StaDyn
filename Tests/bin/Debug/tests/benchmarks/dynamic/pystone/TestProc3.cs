/*A port to StaDyn (dynamic) of the pystone benchmark using duck typing.*/

using System;

namespace StaDyn.Dynamic
{
    public class Idents
    {
        public static var Ident1 = 1;       
        public static var Ident3 = 3;        
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
		
		public static void TestProc3()
		{
			Pystone.IntGlob = 0;
			var PtrParOut = new Record(Idents.Ident1, Idents.Ident3, 5, "DHRYSTONE PROGRAM, SOME STRING");
            PtrParOut.PtrComp = PtrParOut;	
			PtrGlb = PtrParOut;			
			var result = Pystone.Proc3(PtrParOut);
			if(result.Discr != Idents.Ident1)
				Environment.Exit(-1);
			if(result.EnumComp != Idents.Ident3)
				Environment.Exit(-1);
			if(result.IntComp != 12)
				Environment.Exit(-1);
			if(!("DHRYSTONE PROGRAM, SOME STRING".Equals(result.StringComp)))
				Environment.Exit(-1);
		}
				
		public static var Proc7(var IntParI1, var IntParI2)
        {
            var IntParOut;
            var IntLoc;

            IntLoc = IntParI1 + 2;
            IntParOut = IntParI2 + IntLoc;
            return IntParOut;
        }
		
        public static void Main()
        {				
			Pystone.Test();
			Console.WriteLine("Pystone completed!!");		
        }	
		
		public static void Test()
		{
			Pystone.TestProc3();
			Console.WriteLine("TestProc3 completed!!");
		}		
    }
}