using System.Collections;
using System;

class CGSArithmeticUnionAndLiteral
{
    
    static void Main() {
      var x;
	  if (false) 
			x = 'a';
	  else 
		x = 2;
		
	  Calc(x);
	  
	  var y;
	  if (true) 
			y = 'b';
	  else 
		y = 7;
	  Calc(y);
    }
	static void Calc(var x) {  
	  Console.WriteLine("U + Char");
      Console.WriteLine("'a'\\/2 + 's' = " + (x + 's') );
	  
	  Console.WriteLine("U + Int");
      Console.WriteLine(" 'a'\\/2 + 8 = " + (x + 8) );
	  
	  Console.WriteLine("U + Double");
      Console.WriteLine(" 'a'\\/2 + 0.6 = " + (x + 0.6) );
	  
	  Console.WriteLine("U + String");
      Console.WriteLine(" 'a'\\/2 + \"hello\" = " + (x + "hello") );
	  
	  Console.WriteLine("Char + U");
      Console.WriteLine("'s' + 'a'\\/2 = " + ('s' + x));
	  
	  Console.WriteLine("Int + U");
      Console.WriteLine("8 + 'a'\\/2 = " + (8 + x) );
	  
	  Console.WriteLine("Double + U");
      Console.WriteLine("0.6 + 'a'\\/2 = " + (0.6 + x) );
	  
	  Console.WriteLine("String + U");
      Console.WriteLine("\"hello\" + 'a'\\/2 = " + ("hello" + x) );
		  
   }
}