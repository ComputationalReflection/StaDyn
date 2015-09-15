namespace TestVar.ParametersAndReturn {

    class Class {

        Class() { }

        var m(var param) {
            return param;
        }

        static var staticMethod(var param) {
            return param;
        }

        public static void testOK() {
            // * New class reference with fresh variables
            Class klass = new Class();
            // * Method invocation by means of unification
            string myString = klass.m("hello");

            // * Another class: fresh variables
            Class other;
            // * Assignment means unification
            other = klass;
            char c = other.m('a');

            // * Static method invocation
            int n = staticMethod(3);

            // * Unification of local references 
            var local = klass;
        }

        public static void Main() {
        }

    }

}

