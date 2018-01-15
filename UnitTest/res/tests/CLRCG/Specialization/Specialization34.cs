using System;
using System.Text;

public class Program
{
    static int ConcreteMethod(int param) { return param; }

    static dynamic Method(dynamic param)
    {
        return Program.ConcreteMethod(param);
    }
    static void Main()
    {
        var union;
        if (true)
            union = 1;
        else
            union = 23.34;
        var result = Program.Method(union);
        Console.WriteLine(union.ToString() + " = " + result.ToString());
    }
}