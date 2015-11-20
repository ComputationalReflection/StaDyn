using System;
using System.Reflection;

class Test {

    public static void Main() {
        var exception;
        if (new Random().NextDouble()<0.5)
            exception = new ApplicationException("An application exception.");
        else
            exception = new SystemException("A system exception");
        Console.WriteLine(exception.Message);

    }

}
