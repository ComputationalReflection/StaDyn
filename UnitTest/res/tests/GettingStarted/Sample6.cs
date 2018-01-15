using System;

namespace GettingStarted {

    class Test {

        public static var upper(var parameter) {
            return parameter.ToUpper();
        }

        public static var getString(var parameter) {
            return parameter.ToString();
        }

        public static void Main() {
            string aString = "hello";
            aString=upper("hello");
            Console.WriteLine(aString);

            var reference;
            if (new Random().NextDouble() < 0.5)
                reference = "hello";
            else
                reference = new SystemException("A system exception");
            aString=getString(reference); // * Correct!
            Console.WriteLine(aString);
            aString=upper(reference); // * Compilation error (reference is static)

            dynamic dynamicReference;
            if (new Random().NextDouble() < 1)
                dynamicReference = "hello";
            else
                dynamicReference = new SystemException("A system exception");
            aString=dynamicReference.ToUpper(); // * Correct! (dynamicReference is dynamic)
            Console.WriteLine(aString);

            aString = getString(dynamicReference); // * Correct! (regardles of parameter dynamism)
            aString = upper(dynamicReference); // * Compilation error (correct if we set parameter to dynamic)
            Console.WriteLine(aString);
        }
    }
}