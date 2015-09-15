using System;

namespace CG.TestTernary 
{
    public class Test
	{
        public static void Main() {
			int second, third;
            bool c;
            c = true;
            Console.WriteLine(c ? second : third); // * OK
            Console.WriteLine(!c ? second : 3.4);  // * OK
            Console.WriteLine(c ? 3.4 : third);  // * OK
        }
    }
}