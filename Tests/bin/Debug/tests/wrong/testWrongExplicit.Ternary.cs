using System;

namespace Testing.Wrong.TestTernary {

    class Objeto {
        private var atributo;
        public Objeto(var p) {
            int second, third;
            bool c;
            c = true;

            (c ? second : third) = 3; // * Error

            Console.WriteLine((c ? second : "third")); // * Error
        }
    }


    public class Run {
        public static void Main() {
        }
    }

}