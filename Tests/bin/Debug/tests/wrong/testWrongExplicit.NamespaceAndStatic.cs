using System;

namespace Testing.Wrong.TestNameSpace {

    class Base {
        public void errors() {
            // * Wrong attribute
            Base.unknown; // * Error
            // * Wrong method
            Testing.Wrong.TestNameSpace.Objeto.wrongMethod(); // * Error
            // * Wrong class
            Testing.TestNameSpace.WrongClass.attribute = 3; // * Error
            // * Wrong class
            Testing.Wrong.TestNameSpace.WrongClass.method(); ; // * Error
        }

    }

    public class Run {
        public static void Main() {
        }
    }

}