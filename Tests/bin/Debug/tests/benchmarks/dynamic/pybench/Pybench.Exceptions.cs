using System;

namespace Pybench.Exceptions
{
	public class Pybench
	{		
		public static void Main()
        {				
			Test test = new TryRaiseExcept();
			test.test();
			test = new TryExcept();
			test.test();
			Console.WriteLine("Pybench completed!!");		
        }	
	}
	
	public abstract class Test 
	{
        public abstract void test();
    }

	public class TryRaiseExcept : Test 
	{		
        public static void ThrowException(var message)
		{
			throw new Exception(message);
		}
		
        public override void test() 
		{
            var i = 0;
			while(i < 8000)
			{
                try {
                    throw new Exception("hi");
                }
                catch (Exception exp1) { }
                finally {
                    try {
                        ThrowException("hi");
                    }
                    catch (Exception exp2) { }
                    try {
                        ThrowException("something");
                    }
                    catch (Exception exp3) { }
                    try {
                        ThrowException("something");
                    }
                    catch (Exception exp4) { }
                    try {
                        ThrowException("something");
                    }
                    catch (Exception exp5) { }
                    try {
                        ThrowException("something");
                    }
                    catch (Exception exp6) { }
                    try {
                        ThrowException("something");
                    }
                    catch (Exception exp7) { }
                    try {
                        ThrowException("something");
                    }
                    catch (Exception exp8) { }
                }
				i++;
            }
        }
    }
	
	public class TryExcept : Test 
	{		
        public override void test() 
		{
            var i = 0;
			while(i < 150000)
			{
				try { }
                catch (Exception ex0) { }
                try { }
                catch (Exception ex1) { }
                try { }
                catch (Exception ex2) { }
                try { }
                catch (Exception ex3) { }
                try { }
                catch (Exception ex4) { }
                try { }
                catch (Exception ex5) { }
                try { }
                catch (Exception ex6) { }
                try { }
                catch (Exception ex7) { }
                try { }
                catch (Exception ex8) { }
                try { }
                catch (Exception ex9) { }
                
				try { }
                catch (Exception ex10) { }
				try { }
                catch (Exception ex11) { }
                try { }
                catch (Exception ex12) { }
                try { }
                catch (Exception ex13) { }
                try { }
                catch (Exception ex14) { }
                try { }
                catch (Exception ex15) { }
                try { }
                catch (Exception ex16) { }
                try { }
                catch (Exception ex17) { }
                try { }
                catch (Exception ex18) { }
                try { }
                catch (Exception ex19) { }
                
				try { }
                catch (Exception ex20) { }
				try { }
                catch (Exception ex21) { }
                try { }
                catch (Exception ex22) { }
                try { }
                catch (Exception ex23) { }
                try { }
                catch (Exception ex24) { }
                try { }
                catch (Exception ex25) { }
                try { }
                catch (Exception ex26) { }
                try { }
                catch (Exception ex27) { }
                try { }
                catch (Exception ex28) { }
                try { }
                catch (Exception ex29) { }
				
				try { }
                catch (Exception ex30) { }
                try { }
                catch (Exception ex31) { }
                try { }
                catch (Exception ex32) { }
                try { }
                catch (Exception ex33) { }
                try { }
                catch (Exception ex34) { }
                try { }
                catch (Exception ex35) { }
                try { }
                catch (Exception ex36) { }
                try { }
                catch (Exception ex37) { }
                try { }
                catch (Exception ex38) { }
                try { }
                catch (Exception ex39) { }
                
				try { }
                catch (Exception ex40) { }
                try { }
                catch (Exception ex41) { }
                try { }
                catch (Exception ex42) { }
                try { }
                catch (Exception ex43) { }
                try { }
                catch (Exception ex44) { }
                try { }
                catch (Exception ex45) { }
                try { }
                catch (Exception ex46) { }
                try { }
                catch (Exception ex47) { }
                try { }
                catch (Exception ex48) { }
                try { }
                catch (Exception ex49) { }
				
				try { }                
				catch (Exception ex50) { }
                try { }
                catch (Exception ex51) { }
                try { }
                catch (Exception ex52) { }
                try { }
                catch (Exception ex53) { }
                try { }
                catch (Exception ex54) { }
                try { }
                catch (Exception ex55) { }
                try { }
                catch (Exception ex56) { }
                try { }
                catch (Exception ex57) { }
                try { }
                catch (Exception ex58) { }
                try { }
                catch (Exception ex59) { }
				
				try { }
                catch (Exception ex60) { }
                try { }
                catch (Exception ex61) { }
                try { }
                catch (Exception ex62) { }
                try { }
                catch (Exception ex63) { }
                try { }
                catch (Exception ex64) { }
                try { }
                catch (Exception ex65) { }
                try { }
                catch (Exception ex66) { }
                try { }
                catch (Exception ex67) { }
                try { }
                catch (Exception ex68) { }
                try { }
                catch (Exception ex69) { }
								
				try { }                
				catch (Exception ex70) { }
                try { }
                catch (Exception ex71) { }
                try { }
                catch (Exception ex72) { }
                try { }
                catch (Exception ex73) { }
                try { }
                catch (Exception ex74) { }
                try { }
                catch (Exception ex75) { }
                try { }
                catch (Exception ex76) { }
                try { }
                catch (Exception ex77) { }
                try { }
                catch (Exception ex78) { }
                try { }
                catch (Exception ex79) { }
				
				try { }                
				catch (Exception ex80) { }
                try { }
                catch (Exception ex81) { }
                try { }
                catch (Exception ex82) { }
                try { }
                catch (Exception ex83) { }
                try { }
                catch (Exception ex84) { }
                try { }
                catch (Exception ex85) { }
                try { }
                catch (Exception ex86) { }
                try { }
                catch (Exception ex87) { }
                try { }
                catch (Exception ex88) { }
                try { }
                catch (Exception ex89) { }
				
				try { }                
				catch (Exception ex90) { }
                try { }
                catch (Exception ex91) { }
                try { }
                catch (Exception ex92) { }
                try { }
                catch (Exception ex93) { }
                try { }
                catch (Exception ex94) { }
                try { }
                catch (Exception ex95) { }
                try { }
                catch (Exception ex96) { }
                try { }
                catch (Exception ex97) { }
                try { }
                catch (Exception ex98) { }
                try { }
                catch (Exception ex99) { }
				
				try { }
                catch (Exception ex100) { }
                try { }
                catch (Exception ex101) { }
                try { }
                catch (Exception ex102) { }
                try { }
                catch (Exception ex103) { }
                try { }
                catch (Exception ex104) { }
                try { }
                catch (Exception ex105) { }
                try { }
                catch (Exception ex106) { }
                try { }
                catch (Exception ex107) { }
                try { }
                catch (Exception ex108) { }
                try { }
                catch (Exception ex109) { }
								
				try { }
                catch (Exception ex110) { }
                try { }
                catch (Exception ex111) { }
                try { }
                catch (Exception ex112) { }
                try { }
                catch (Exception ex113) { }
                try { }
                catch (Exception ex114) { }
                try { }
                catch (Exception ex115) { }
                try { }
                catch (Exception ex116) { }
                try { }
                catch (Exception ex117) { }
                try { }
                catch (Exception ex118) { }
                try { }
                catch (Exception ex119) { }
				  
				try { }				  
				catch (Exception ex120) { }
                try { }
                catch (Exception ex121) { }
                try { }
                catch (Exception ex122) { }
                try { }
                catch (Exception ex123) { }
                try { }
                catch (Exception ex124) { }
                try { }
                catch (Exception ex125) { }
                try { }
                catch (Exception ex126) { }
                try { }
                catch (Exception ex127) { }
                try { }
                catch (Exception ex128) { }
                try { }
                catch (Exception ex129) { }
				
				try { }
                catch (Exception ex130) { }
                try { }
                catch (Exception ex131) { }
                try { }
                catch (Exception ex132) { }
                try { }
                catch (Exception ex133) { }
                try { }
                catch (Exception ex134) { }
                try { }
                catch (Exception ex135) { }
                try { }
                catch (Exception ex136) { }
                try { }
                catch (Exception ex137) { }
                try { }
                catch (Exception ex138) { }
                try { }
                catch (Exception ex139) { }
				
				try { }                
				catch (Exception ex140) { }
                try { }
                catch (Exception ex141) { }
                try { }
                catch (Exception ex142) { }
                try { }
                catch (Exception ex143) { }
                try { }
                catch (Exception ex144) { }
                try { }
                catch (Exception ex145) { }
                try { }
                catch (Exception ex146) { }
                try { }
                catch (Exception ex147) { }
                try { }
                catch (Exception ex148) { }
                try { }
                catch (Exception ex149) { }
								
				i++;
            }
        }
    }
}