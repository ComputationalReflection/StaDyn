using System;
using System.Text;
namespace Pybench.Strings
{
	public class Paybench
	{		
		public static void Main()
        {							
			Test test = new ConcatStrings();
			test.test();
			test = new CompareStrings();
			test.test();
			test = new CreateStringsWithConcat();
			test.test();
			test = new CompareSlicing();
			test.test();		
			test = new StringPredicates();
			test.test();
			test = new StringMappings();
			test.test();										
			Console.WriteLine("Pybench completed!!");			
        }	
	}
	
	public abstract class Test 
	{
        public abstract void test();
    }
	
	public class ConcatStrings : Test 
	{
        public override void test() 
		{            
			var s = new StringBuilder();
            for (var i = 0; i < 100; i++)
                s.Append("" + i);
			s = s.ToString();
			
			var t = new StringBuilder();
            for (var i = 1; i < 101; i++)
                t.Append("" + i);
			t = t.ToString();

			var b;
			for (var i = 0; i < 100000; i++)
            {
				b = t + s;
				b = t + s;
				b = t + s;
				b = t + s;
				b = t + s;
				
				b = t + s;
				b = t + s;
				b = t + s;
				b = t + s;
				b = t + s;
				
				b = t + s;
				b = t + s;
				b = t + s;
				b = t + s;
				b = t + s;
				
				b = t + s;
				b = t + s;
				b = t + s;
				b = t + s;
				b = t + s;
				
				b = t + s;
				b = t + s;
				b = t + s;
				b = t + s;
				b = t + s;
				
				b = t + s;
				b = t + s;
				b = t + s;
				b = t + s;
				b = t + s;
				
				b = t + s;
				b = t + s;
				b = t + s;
				b = t + s;
				b = t + s;
				
				b = t + s;
				b = t + s;
				b = t + s;
				b = t + s;
				b = t + s;
				
				b = t + s;
				b = t + s;
				b = t + s;
				b = t + s;
				b = t + s;
				
				b = t + s;
				b = t + s;
				b = t + s;
				b = t + s;
				b = t + s;				
			}
		}
	}
	
	public class CompareStrings : Test 
	{
        public override void test() 
		{            
			var s = new StringBuilder();
            for (var i = 0; i < 10; i++)
                s.Append("" + i);
			s = s.ToString();
			
			var t = new StringBuilder();
            for (var i = 0; i < 10; i++)
                t.Append("" + i);
			t = t.ToString() + "abc";

			var b;
			for (var i = 0; i < 200000; i++)
            {
				b = t.CompareTo(s) < 0;				
				b = t.CompareTo(s) > 0;				
				b = t == s;				
				b = t.CompareTo(s) > 0;				
				b = t.CompareTo(s) < 0;				

				b = t.CompareTo(s) < 0;				
				b = t.CompareTo(s) > 0;				
				b = t == s;				
				b = t.CompareTo(s) > 0;				
				b = t.CompareTo(s) < 0;				

				b = t.CompareTo(s) < 0;				
				b = t.CompareTo(s) > 0;				
				b = t == s;				
				b = t.CompareTo(s) > 0;				
				b = t.CompareTo(s) < 0;				

				b = t.CompareTo(s) < 0;				
				b = t.CompareTo(s) > 0;				
				b = t == s;				
				b = t.CompareTo(s) > 0;				
				b = t.CompareTo(s) < 0;					
				
				b = t.CompareTo(s) < 0;				
				b = t.CompareTo(s) > 0;				
				b = t == s;				
				b = t.CompareTo(s) > 0;				
				b = t.CompareTo(s) < 0;					

				b = t.CompareTo(s) < 0;				
				b = t.CompareTo(s) > 0;				
				b = t == s;				
				b = t.CompareTo(s) > 0;				
				b = t.CompareTo(s) < 0;				

				b = t.CompareTo(s) < 0;				
				b = t.CompareTo(s) > 0;				
				b = t == s;				
				b = t.CompareTo(s) > 0;				
				b = t.CompareTo(s) < 0;				

				b = t.CompareTo(s) < 0;				
				b = t.CompareTo(s) > 0;				
				b = t == s;				
				b = t.CompareTo(s) > 0;				
				b = t.CompareTo(s) < 0;				

				b = t.CompareTo(s) < 0;				
				b = t.CompareTo(s) > 0;				
				b = t == s;				
				b = t.CompareTo(s) > 0;				
				b = t.CompareTo(s) < 0;					
				
				b = t.CompareTo(s) < 0;				
				b = t.CompareTo(s) > 0;				
				b = t == s;				
				b = t.CompareTo(s) > 0;				
				b = t.CompareTo(s) < 0;	
			}			
 		}
	}
	
	public class CreateStringsWithConcat : Test 
	{
        public override void test() 
		{
            var s;            
            for (var i = 0; i < 200000; i++)
            {
				s = "om";
				s = s + "xbx";
				s = s + "xcx";
				s = s + "xdx";
				s = s + "xex";
                
				s = s + "xax";
				s = s + "xbx";
				s = s + "xcx";
				s = s + "xdx";
				s = s + "xex";
				
				s = s + "xax";
				s = s + "xbx";
				s = s + "xcx";
				s = s + "xdx";
				s = s + "xex";
				
				s = s + "xax";
				s = s + "xbx";
				s = s + "xcx";
				s = s + "xdx";
				s = s + "xex";
				
				s = s + "xax";
				s = s + "xbx";
				s = s + "xcx";
				s = s + "xdx";
				s = s + "xex";
				
				s = s + "xax";
				s = s + "xbx";
				s = s + "xcx";
				s = s + "xdx";
				s = s + "xex";
				
				s = s + "xax";
				s = s + "xbx";
				s = s + "xcx";
				s = s + "xdx";
				s = s + "xex";
				
				s = s + "xax";
				s = s + "xbx";
				s = s + "xcx";
				s = s + "xdx";
				s = s + "xex";
				
				s = s + "xax";
				s = s + "xbx";
				s = s + "xcx";
				s = s + "xdx";
				s = s + "xex";
				
				s = s + "xax";
				s = s + "xbx";
				s = s + "xcx";
				s = s + "xdx";
				s = s + "xex";
            }
        }
    }

	public class CompareSlicing : Test 
	{
        public override void test() 
		{            
			var s = new StringBuilder();
            for (var i = 0; i < 100; i++)
                s.Append("" + i);
			s = s.ToString();
			
			var b;		
			
			var zero = 0;
			var one = 1;
			var two = 2;
			var five = 5;
			var eleven = 11;
			var twentyfive = 25;
			var fifty = 50;
			
			for (var i = 0; i < 160000 ; i++)
            {	
				b = s.Substring(fifty,s.Length - fifty);
				b = s.Substring(zero,twentyfive);
				b = s.Substring(fifty,five);
				b = s.Substring(s.Length - one, one);
				b = s.Substring(zero,one);
				b = s.Substring(two,s.Length - two);
				b = s.Substring(eleven,s.Length - eleven - eleven);
				
				b = s.Substring(fifty,s.Length - fifty);
				b = s.Substring(zero,twentyfive);
				b = s.Substring(fifty,five);
				b = s.Substring(s.Length - one, one);
				b = s.Substring(zero,one);
				b = s.Substring(two,s.Length - two);
				b = s.Substring(eleven,s.Length - eleven - eleven);
				
				b = s.Substring(fifty,s.Length - fifty);
				b = s.Substring(zero,twentyfive);
				b = s.Substring(fifty,five);
				b = s.Substring(s.Length - one, one);
				b = s.Substring(zero,one);
				b = s.Substring(two,s.Length - two);
				b = s.Substring(eleven,s.Length - eleven - eleven);
				
				b = s.Substring(fifty,s.Length - fifty);
				b = s.Substring(zero,twentyfive);
				b = s.Substring(fifty,five);
				b = s.Substring(s.Length - one, one);
				b = s.Substring(zero,one);
				b = s.Substring(two,s.Length - two);
				b = s.Substring(eleven,s.Length - eleven - eleven);
				
				b = s.Substring(fifty,s.Length - fifty);
				b = s.Substring(zero,twentyfive);
				b = s.Substring(fifty,five);
				b = s.Substring(s.Length - one, one);
				b = s.Substring(zero,one);
				b = s.Substring(two,s.Length - two);
				b = s.Substring(eleven,s.Length - eleven - eleven);			
			}
		}
	}

	public class StringPredicates : Test 
	{
        public override void test() 
		{            
			var s = "abc123-10 ABC";
			var b;
			var c;
			for (var i = 0; i < 100000; i++)
            {
				c = s.ToCharArray()[i % s.Length];
				
				b = System.Char.IsNumber(c);
				b = System.Char.IsLetter(c);
				b = System.Char.IsDigit(c);
				b = System.Char.IsLower(c);
				b = System.Char.IsSeparator(c);            
				b = System.Char.IsUpper(c);
				
				b = System.Char.IsNumber(c);
				b = System.Char.IsLetter(c);
				b = System.Char.IsDigit(c);
				b = System.Char.IsLower(c);
				b = System.Char.IsSeparator(c);            
				b = System.Char.IsUpper(c);
				
				b = System.Char.IsNumber(c);
				b = System.Char.IsLetter(c);
				b = System.Char.IsDigit(c);
				b = System.Char.IsLower(c);
				b = System.Char.IsSeparator(c);            
				b = System.Char.IsUpper(c);
				
				b = System.Char.IsNumber(c);
				b = System.Char.IsLetter(c);
				b = System.Char.IsDigit(c);
				b = System.Char.IsLower(c);
				b = System.Char.IsSeparator(c);            
				b = System.Char.IsUpper(c);
				
				b = System.Char.IsNumber(c);
				b = System.Char.IsLetter(c);
				b = System.Char.IsDigit(c);
				b = System.Char.IsLower(c);
				b = System.Char.IsSeparator(c);            
				b = System.Char.IsUpper(c);
				
				b = System.Char.IsNumber(c);
				b = System.Char.IsLetter(c);
				b = System.Char.IsDigit(c);
				b = System.Char.IsLower(c);
				b = System.Char.IsSeparator(c);            
				b = System.Char.IsUpper(c);
				
				b = System.Char.IsNumber(c);
				b = System.Char.IsLetter(c);
				b = System.Char.IsDigit(c);
				b = System.Char.IsLower(c);
				b = System.Char.IsSeparator(c);            
				b = System.Char.IsUpper(c);
				
				b = System.Char.IsNumber(c);
				b = System.Char.IsLetter(c);
				b = System.Char.IsDigit(c);
				b = System.Char.IsLower(c);
				b = System.Char.IsSeparator(c);            
				b = System.Char.IsUpper(c);
				
				b = System.Char.IsNumber(c);
				b = System.Char.IsLetter(c);
				b = System.Char.IsDigit(c);
				b = System.Char.IsLower(c);
				b = System.Char.IsSeparator(c);            
				b = System.Char.IsUpper(c);
				
				b = System.Char.IsNumber(c);
				b = System.Char.IsLetter(c);
				b = System.Char.IsDigit(c);
				b = System.Char.IsLower(c);
				b = System.Char.IsSeparator(c);            
				b = System.Char.IsUpper(c);
			}
		}
	}			
	
	public class StringMappings : Test 
	{
        public override void test() 
		{            
			var s = new StringBuilder();
            for (var i = 0; i < 20; i++)
                s.Append("" + i);
			s = s.ToString();
			
			var t = new StringBuilder();
            for (var i = 0; i < 50; i++)
                t.Append("" + i);
			t = t.ToString();
			
			var u = new StringBuilder();
            for (var i = 0; i < 100; i++)
                u.Append("" + i);
			u = u.ToString();
			
			var v = new StringBuilder();
            for (var i = 0; i < 256; i++)
                v.Append("" + i);
			v = v.ToString();

			var b;
			for (var i = 0; i < 70000; i++)
            {
				b = s.ToLower();		
				b = s.ToLower();		
				b = s.ToLower();		
				b = s.ToLower();		
				b = s.ToLower();		
				
				b = s.ToUpper();
				b = s.ToUpper();
				b = s.ToUpper();
				b = s.ToUpper();
				b = s.ToUpper();
				
				b = t.ToLower();		
				b = t.ToLower();		
				b = t.ToLower();		
				b = t.ToLower();		
				
				b = t.ToUpper();
				b = t.ToUpper();
				b = t.ToUpper();
				b = t.ToUpper();
				
				b = u.ToLower();		
				b = u.ToLower();		
				
				b = u.ToUpper();
				b = u.ToUpper();
				
				b = v.ToLower();		
				
				b = v.ToUpper();				
			}
		}
	}
}