using System;

class CGArithmeticLiterals {
    static void Main() {
        Console.WriteLine("Char + Char");
        Console.WriteLine(" '2' + '3' = {0}", '2' + '3');

        Console.WriteLine("Char + String");
        Console.WriteLine(" '2' + \"3\" = {0}", '2' + "3");

        Console.WriteLine("Char + Int");
        Console.WriteLine(" '2' + 3 = {0}", '2' + 3);

        Console.WriteLine("Char + Double");
        Console.WriteLine(" '2' + 5.5 = {0}", '2' + 5.5);


        Console.WriteLine("Int + Char");
        Console.WriteLine("4 + '2' = {0}", 4 + '2');


        Console.WriteLine("Int + String");
        Console.WriteLine("4 + \"3\" = {0}", 4 + "3");

        Console.WriteLine("Int + Int");
        Console.WriteLine("4 + 3 = {0}", 4 + 3);

        Console.WriteLine("Int + Double");
        Console.WriteLine("4 + 5.5 = {0}", 4 + 5.5);

        Console.WriteLine("Double + Char");
        Console.WriteLine("4.3 + '2' = {0}", 4.3 + '2');

        Console.WriteLine("Double + String");
        Console.WriteLine("4.3 + \"3\" = {0}", 4.3 + "3");

        Console.WriteLine("Double + Int");
        Console.WriteLine("4.3 + 2 = {0}", 4.3 + 2);

        Console.WriteLine("Double + Double");
        Console.WriteLine("4.3 + 5.7 = {0}", 4.3 + 5.7);

        Console.WriteLine("String + Char");
        Console.WriteLine("\"{0}\" + '{1}' = {2}", "3", '2', "3" + '2');

        Console.WriteLine("String + Int");
        Console.WriteLine("\"{0}\" + {1} = {2}", "3", 8, "3" + 8);

        Console.WriteLine("String + Double");
        Console.WriteLine("\"{0}\" + {1} = {2}", "3", 4.3, "3" + 4.3);

        Console.WriteLine("String + String");
        Console.WriteLine("\"{0}\" + \"{1}\" = {2}", "3", "5", "3" + "5");
    }
}