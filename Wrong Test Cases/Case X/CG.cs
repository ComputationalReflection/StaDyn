using System;
using System.Collections;


namespace JG
{
    public class A
    {
        public void ma(dynamic param)
        {			
            Console.WriteLine("{0}",param);
        }				
    }
	
    public class B
    {
        public dynamic attr;        
		
        public void mb(dynamic a)
        {   
			this.attr = a;
			this.attr.ma(1);  			
        }  

        public void mb(dynamic a) var21 = A
        {   
			this.attr = a; 
			this.attr.ma(a);  
        }  		
    }
    
    public class Program
    {
        public static void Main(string[] args)
        {            
            dynamic b = new B(); 	
			dynamic a = new A();		
			b.mb(a);					
        }
    }
}