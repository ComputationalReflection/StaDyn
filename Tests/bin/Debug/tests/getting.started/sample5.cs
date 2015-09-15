using System;
using System.Reflection;

namespace GettingStarted {
    class Wrapper {
        private var attribute;

        public Wrapper(var attribute) {
            this.attribute = attribute;
        }

        public var get() {
            return attribute;
        }

        public void set(var attribute) {
            this.attribute = attribute;
        }
    }

    class Test {
        public static void Main() {
            string aString;
            int aInt;
            Wrapper wrapper = new Wrapper("Hello");
            aString = wrapper.get();
            aInt = wrapper.get(); // * Compilation error
            Console.WriteLine(aString);

            wrapper.set(3);
            aString = wrapper.get(); // * Compilation error
            aInt = wrapper.get();
            Console.WriteLine(aInt);
        }
    }
}