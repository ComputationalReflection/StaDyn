using System;
using System.Reflection;

namespace GettingStarted {
    class Test {

        public static void Main() {
            var reference;
            if (new Random().NextDouble() < 0.5)
                reference = "String";
            else
                reference = 3;
            Console.WriteLine(reference.Message); // * Compilation Error
        }

    }
}