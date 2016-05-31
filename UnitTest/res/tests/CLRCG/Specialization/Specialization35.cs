using System;
using System.Text;

public class Program
{
    static int ConcreteMethod(int param) { return param; }
    static string ConcreteMethod(string param) { return param; }

    static var Method(var param)
    {
        return ConcreteMethod(param);
    }
    static void Main()
    {
        var union;
        if (true)
            union = 1;
        else
            union = 23.34;
        var result = Method(union);
        Console.WriteLine(union.ToString() + " = " + result.ToString());
    }
}