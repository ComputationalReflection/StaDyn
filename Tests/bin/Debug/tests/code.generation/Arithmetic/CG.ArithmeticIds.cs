using System;

class CGArithmeticIds {
    static void Main() {

        char c1 = '1';
        char c2 = '2';
        string s1 = "3";
        string s2 = "4";
        int i1 = 5;
        int i2 = 6;

        double d1 = 5.5;
        double d2 = 6.5;


        /**
        Console.WriteLine("'{0}'({1}) + '{2}'({3}) = {4}", c1, (int)c1, c2, (int)c2, (c1 + c2));
	  
        Console.WriteLine("Char - Char");
        Console.WriteLine(" '{0}'({1}) - '{2}'({3}) = {4}", c1, (int)c1, c2, (int)c2, c1 - c2);
	  
        Console.WriteLine("Char / Char");
        Console.WriteLine(" '{0}'({1}) / '{2}'({3}) = {4}", c1, (int)c1, c2, (int)c2, c1  / c2);
	  
        Console.WriteLine("Char + String");
        Console.WriteLine(" '{0}'({1}) + \"{1}\" = {2}", c1, (int)c1, s, c1 + s);
       TODO: Esto da error semantico unkown member error */


        Console.WriteLine("Char + Char");
        Console.WriteLine("'{0}' + '{1}' = {2}", c1, c2, c1 + c2);

        Console.WriteLine("Char - Char");
        Console.WriteLine("'{0}' - '{1}' = {2}", c1, c2, c1 - c2);

        Console.WriteLine("Char * Char");
        Console.WriteLine("'{0}' * '{1}' = {2}", c1, c2, c1 * c2);

        Console.WriteLine("Char / Char");
        Console.WriteLine("'{0}' / '{1}' = {2}", c1, c2, c1 / c2);

        Console.WriteLine("Char + Int");
        Console.WriteLine("'{0}' + {1} = {2}", c1, i1, c1 + i1);

        Console.WriteLine("Char - Int");
        Console.WriteLine("'{0}' - {1} = {2}", c1, i1, c1 - i1);

        Console.WriteLine("Char * Int");
        Console.WriteLine("'{0}' * {1} = {2}", c1, i1, c1 * i1);

        Console.WriteLine("Char / Int");
        Console.WriteLine("'{0}' / {1} = {2}", c1, i1, c1 / i1);

        Console.WriteLine("Char + Double");
        Console.WriteLine("'{0}' + {1} = {2}", c1, d1, c1 + d1);

        Console.WriteLine("Char - Double");
        Console.WriteLine("'{0}' - {1} = {2}", c1, d1, c1 - d1);

        Console.WriteLine("Char * Double");
        Console.WriteLine("'{0}' * {1} = {2}", c1, d1, c1 * d1);

        Console.WriteLine("Char / Double");
        Console.WriteLine("'{0}' / {1} = {2}", c1, d1, c1 / d1);

        Console.WriteLine("Char + String");
        Console.WriteLine("'{0}' + \"{1}\" = {2}", c1, s1, c1 + s1);


        Console.WriteLine("Int + Char");
        Console.WriteLine("{0} + '{1}' = {2}", i1, c1, i1 + c1);

        Console.WriteLine("Int - Char");
        Console.WriteLine("{0} - '{1}' = {2}", i1, c1, i1 - c1);

        Console.WriteLine("Int * Char");
        Console.WriteLine("{0} * '{1}' = {2}", i1, c1, i1 * c1);

        Console.WriteLine("Int / Char");
        Console.WriteLine("{0} / '{1}' = {2}", i1, c1, i1 / c1);

        Console.WriteLine("Int + Int");
        Console.WriteLine("{0} + {1} = {2}", i1, i2, i1 + i2);

        Console.WriteLine("Int - Int");
        Console.WriteLine("{0} - {1} = {2}", i1, i2, i1 - i2);

        Console.WriteLine("Int * Int");
        Console.WriteLine("{0} * {1} = {2}", i1, i2, i1 * i2);

        Console.WriteLine("Int / Int");
        Console.WriteLine("{0} / {1} = {2}", i1, i2, i1 / i2);

        Console.WriteLine("Int + Double");
        Console.WriteLine("{0} + {1} = {2}", i1, d1, i1 + d1);

        Console.WriteLine("Int - Double");
        Console.WriteLine("{0} - {1} = {2}", i1, d1, i1 - d1);

        Console.WriteLine("Int * Double");
        Console.WriteLine("{0} * {1} = {2}", i1, d1, i1 * d1);

        Console.WriteLine("Int / Double");
        Console.WriteLine("{0} / {1} = {2}", i1, d1, i1 / d1);


        Console.WriteLine("Int + String");
        Console.WriteLine("{0} + \"{1}\" = {2}", i1, s1, i1 + s1);

        Console.WriteLine("Double + Char");
        Console.WriteLine("{0} + '{1}' = {2}", d1, c1, d1 + c1);

        Console.WriteLine("Double - Char");
        Console.WriteLine("{0} - '{1}' = {2}", d1, c1, d1 - c1);

        Console.WriteLine("Double * Char");
        Console.WriteLine("{0} * '{1}' = {2}", d1, c1, d1 * c1);

        Console.WriteLine("Double / Char");
        Console.WriteLine("{0} / '{1}' = {2}", d1, c1, d1 / c1);

        Console.WriteLine("Double + Int");
        Console.WriteLine("{0} + {1} = {2}", d1, i1, d1 + i1);

        Console.WriteLine("Double - Int");
        Console.WriteLine("{0} - {1} = {2}", d1, i1, d1 - i1);

        Console.WriteLine("Double * Int");
        Console.WriteLine("{0} * {1} = {2}", d1, i1, d1 * i1);

        Console.WriteLine("Double / Int");
        Console.WriteLine("{0} / {1} = {2}", d1, i1, d1 / i1);

        Console.WriteLine("Double + Double");
        Console.WriteLine("{0} + {1} = {2}", d1, d2, d1 + d2);

        Console.WriteLine("Double - Double");
        Console.WriteLine("{0} + {1} = {2}", d1, d2, d1 - d2);

        Console.WriteLine("Double * Double");
        Console.WriteLine("{0} + {1} = {2}", d1, d2, d1 * d2);

        Console.WriteLine("Double / Double");
        Console.WriteLine("{0} / {1} = {2}", d1, d2, d1 / d2);


        Console.WriteLine("Double + String");
        Console.WriteLine("{0} + \"{1}\" = {2}", d1, s1, d1 + s1);

        Console.WriteLine("String + Char");
        Console.WriteLine("\"{0}\" + '{1}' = {2}", s1, c1, s1 + c1);

        Console.WriteLine("String + Int");
        Console.WriteLine("\"{0}\" + {1} = {2}", s1, i1, s1 + i1);

        Console.WriteLine("String + Double");
        Console.WriteLine("\"{0}\" + {1} = {2}", s1, d1, s1 + d1);

        Console.WriteLine("String + String");
        Console.WriteLine("\"{0}\" + \"{1}\" = {2}", s1, s2, s1 + s2);

    }
}