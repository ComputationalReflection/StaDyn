using System;
using System.Text;

public class Program
{
    static int ConcreteMethod(int param) { return param; }
    static string ConcreteMethod(string param) { return param; }

    static var Method(var param)
    {
        return Program.ConcreteMethod(param);
    }
    static void Main()
    {
        var union = 1;
        var result = Method(union);
        Console.WriteLine(union.ToString() + " = " + result.ToString());
    }
}