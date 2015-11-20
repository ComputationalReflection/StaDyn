using System;
using System.Reflection;

namespace GettingStarted {
    class Test {

        public static void Main() {
            UnknownType(4);
        }
		
		 public static void UnknownType(var exception) {            
            Random random = new Random();
            switch (random.Next(1, 5)) {
                case 1:
                    exception = new ApplicationException("An application exception.");
                    break;
                case 2:
                    exception = new SystemException("A system exception");
                    break;
                case 3:
                    exception = "This is not an exception";
                    break;
            }
            Console.WriteLine(exception.ToString());            
			//Console.WriteLine(exception.Message); // Compilation error?
        }
    }
}