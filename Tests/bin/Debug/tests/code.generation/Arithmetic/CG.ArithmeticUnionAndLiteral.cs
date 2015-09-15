using System.Collections;
using System;

class CGSArithmeticUnionAndLiteral
{
      static string [] PlusUnion = new string [] {"char", "int","double", "string"};
	  static string [] DefaultUnion = new string [] {"char", "int","double" };
     /* static char c1 = 'a';
	  static char c2 = 'b';
	  static int i1=1;
	  static int i2=2;
	  static double d1 = 3.3;
	  static double d2 = 5.7;
	  static string s1 = "pepe";
	  static string s2 = "juan";
	*/
	static void PlusOperation() {
		Console.WriteLine("Plus Operation. Working with \\/{0}", PlusUnion);
		Console.WriteLine("======================================");
		
		for (int i=0; i<4; i++) {
		  Plus(i);
		  Console.WriteLine();
		
		}
		Console.WriteLine("======================================");
	}
	static void MinusOperation() {
		Console.WriteLine("Minus Operation. Working with \\/{0}", DefaultUnion.ToString());
		Console.WriteLine("======================================");
		
		for (int i=0; i<3; i++) {
		  Minus(i);
		  Console.WriteLine();
		
		}
		Console.WriteLine("======================================");
	}
	
	static void MultOperation() {
		Console.WriteLine("Mult Operation. Working with \\/{0}", DefaultUnion.ToString());
		Console.WriteLine("======================================");
		
		for (int i=0; i<3; i++) {
		  Mult(i);
		  Console.WriteLine();
		}
		Console.WriteLine("======================================");
	}
	
	static void ModulusOperation() {
		Console.WriteLine("Modulus Operation. Working with \\/{0}", DefaultUnion.ToString());
		Console.WriteLine("======================================");
		
		for (int i=0; i<3; i++) {
		  Modulus(i);
		  Console.WriteLine();
		}
		Console.WriteLine("======================================");
	}
	
	static void DivOperation() {
		Console.WriteLine("Div Operation. Working with \\/{0}", DefaultUnion.ToString());
		Console.WriteLine("======================================");
		
		for (int i=0; i<3; i++) {
		  Div(i);
		  Console.WriteLine();
		}
		Console.WriteLine("======================================");
	}
		
    static void Main() {
		PlusOperation();
		MinusOperation();
		MultOperation();
		DivOperation();
		ModulusOperation();	
	}
    static void Plus(int n) {
      var x;
	
	 char c1 = 'a';
	 char c2 = 'b';
	 int i1=1;
	 int i2=2;
	 double d1 = 3.3;
	 double d2 = 5.7;
	 string s1 = "pepe";
	 string s2 = "juan";
	  	  
	  switch (n) {
	     case 0: 
			 x= c1;
			 break;
	     case 1: 
			 x= i1;
			 break;
	     case 2: 
			 x= d1;
			 break;
		default:
			x=s1;
		
	}
	Console.WriteLine("Union ---> {0}", PlusUnion[n]);
	Console.WriteLine("{0} + \\/({1}) = {2}", c2, x, c2 + x);
    Console.WriteLine("\\/({0}) + {1} = {2}", x, c2, x + c2);  
	
	Console.WriteLine("{0} + \\/({1}) = {2}", i2, x, i2 + x);
	Console.WriteLine("\\/({0}) + {1} = {2}", x, i2, x + i2);  
	
	Console.WriteLine("{0} + \\/({1}) = {2}", d2, x, d2 + x);
	Console.WriteLine("\\/({0}) + {1} = {2}", x, d2, x + d2); 
	
	Console.WriteLine("{0} + \\/({1}) = {2}", s2, x, s2 + x);
	Console.WriteLine("\\/({0}) + {1} = {2}", x, s2, x + s2);  
   }
   
   static void Minus(int n) {
      var x;
     char c1 = 'a';
	 char c2 = 'b';
	 int i1=1;
	 int i2=2;
	 double d1 = 3.3;
	 double d2 = 5.7;	  
	  switch (n) {
	     case 0: 
			 x= c1;
			 break;
	     case 1: 
			 x= i1;
			 break;
	     default: 
			 x= d1;
	}
	Console.WriteLine("Union ---> {0}", DefaultUnion[n]);
	Console.WriteLine("{0} - \\/({1}) = {2}", c2, x, c2 - x);
    Console.WriteLine("\\/({0}) - {1} = {2}", x, c2, x - c2);  
	
	Console.WriteLine("{0} - \\/({1}) = {2}", i2, x, i2 - x);
	Console.WriteLine("\\/({0}) - {1} = {2}", x, i2, x - i2);  
	
	Console.WriteLine("{0} - \\/({1}) = {2}", d2, x, d2 - x);
	Console.WriteLine("\\/({0}) - {1} = {2}", x, d2, x - d2); 
	  
   }
   
    static void Div(int n) {
      var x;
     char c1 = 'a';
	 char c2 = 'b';
	 int i1=1;
	 int i2=2;
	 double d1 = 3.3;
	 double d2 = 5.7;	  
	  switch (n) {
	     case 0: 
			 x= c1;
			 break;
	     case 1: 
			 x= i1;
			 break;
	     default: 
			 x= d1;
	}
	Console.WriteLine("Union ---> {0}", DefaultUnion[n]);
	Console.WriteLine("{0} / \\/({1}) = {2}", c2, x, c2 / x);
    Console.WriteLine("\\/({0}) / {1} = {2}", x, c2, x / c2);  
	
	Console.WriteLine("{0} / \\/({1}) = {2}", i2, x, i2 / x);
	Console.WriteLine("\\/({0}) / {1} = {2}", x, i2, x / i2);  
	
	Console.WriteLine("{0} / \\/({1}) = {2}", d2, x, d2 / x);
	Console.WriteLine("\\/({0}) / {1} = {2}", x, d2, x / d2); 
	  
   }
   
    static void Mult(int n) {
      var x;
     char c1 = 'a';
	 char c2 = 'b';
	 int i1=1;
	 int i2=2;
	 double d1 = 3.3;
	 double d2 = 5.7;	  
	  switch (n) {
	     case 0: 
			 x= c1;
			 break;
	     case 1: 
			 x= i1;
			 break;
	     default: 
			 x= d1;
	}
	Console.WriteLine("Union ---> {0}", DefaultUnion[n]);
	Console.WriteLine("{0} * \\/({1}) = {2}", c2, x, c2 * x);
    Console.WriteLine("\\/({0}) * {1} = {2}", x, c2, x * c2);  
	
	Console.WriteLine("{0} * \\/({1}) = {2}", i2, x, i2 * x);
	Console.WriteLine("\\/({0}) * {1} = {2}", x, i2, x * i2);  
	
	Console.WriteLine("{0} * \\/({1}) = {2}", d2, x, d2 * x);
	Console.WriteLine("\\/({0}) * {1} = {2}", x, d2, x * d2); 
	  
   }
   
    static void Modulus(int n) {
      var x;
     char c1 = 'a';
	 char c2 = 'b';
	 int i1=1;
	 int i2=2;
	 double d1 = 3.3;
	 double d2 = 5.7;	  
	  switch (n) {
	     case 0: 
			 x= c1;
			 break;
	     case 1: 
			 x= i1;
			 break;
	     default: 
			 x= d1;
	}
	Console.WriteLine("Union ---> {0}", DefaultUnion[n]);
	Console.WriteLine("{0} % \\/({1}) = {2}", c2, x, c2 % x);
    Console.WriteLine("\\/({0}) % {1} = {2}", x, c2, x % c2);  
	
	Console.WriteLine("{0} % \\/({1}) = {2}", i2, x, i2 % x);
	Console.WriteLine("\\/({0}) % {1} = {2}", x, i2, x % i2);  
	
	Console.WriteLine("{0} % \\/({1}) = {2}", d2, x, d2 % x);
	Console.WriteLine("\\/({0}) % {1} = {2}", x, d2, x % d2); 
	  
   }
}
