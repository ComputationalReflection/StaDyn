namespace TestVar.Wrong.ParametersAndReturn {

    class Class {

        Class() { }

        var m(var param) {
            return param;
        }

        static var staticMethod(var param) {
            return param;
        }

        public static void testWrong() {
            Class klass = new Class();
            string myString = klass.m(3); // * Error

            Class other;
            other = klass;
            char c = other.m(3); // * Error

            int n= staticMethod(3.3); // * Error
        }

    }

    public class Run {
        public static void Main() {
        }
    }

}

