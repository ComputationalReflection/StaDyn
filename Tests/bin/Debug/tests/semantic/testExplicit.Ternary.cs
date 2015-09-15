using System;

namespace Testing.TestTernary {

    class Objeto {
        private var atributo;
        public Objeto(var p) {
            int second, third;
            bool c;
            c = true;

            Console.WriteLine(c ? second : third); // * OK
            Console.WriteLine(c ? second : 3.4);  // * OK
            Console.WriteLine(c ? 3.4 : third);  // * OK

        }

        public static void Main() {
        }

    }


}