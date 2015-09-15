namespace TestVar.New {

    class Base {
        protected var baseAttribute;
    }

    class Class : Base {
        var attribute;
        var[] arrayAttribute;
        var initializedAttribute = 3;

        Class() { }

        Class(int parameter) {
            baseAttribute = 0.0;
            this.attribute = parameter;
            arrayAttribute = new int[10];
        }

        var instanceMethod(var param) {
            return param;
        }

        static var staticMethod(var param) {
            return param;
        }

        var getAttribute() {
            return attribute;
        }

        void setAttribute(var param) {
            attribute = param;
        }

        var getInitializedAttribute() {
            return initializedAttribute;
        }

        void setInitializedAttribute(var param) {
            initializedAttribute = param;
        }

        public static void testNewAndMethodCall() {
            // * New class reference with fresh variables
            Class klass = new Class(1);
            // * Method invocation by means of unification
            string myString = klass.instanceMethod("hello");

            // * Another class: fresh variables
            Class other;
            // * Assignment means unification
            other = klass;
            char c = other.instanceMethod('a');

            // * Static method invocation
            int n = staticMethod(3);

            // * Unification of local references 
            var local = klass;
            // * Inheritance
            double d = local.baseAttribute;
        }

        public void testAttributes() {
            // * Unification of attributes
            Class freeAttribute = new Class(0);
            freeAttribute.attribute = 3;
            // * Char promotes to int
            freeAttribute.attribute = '3';
        }

        public void assignmentConstraints() {
            int n;
            Class withParameter = new Class(1);
            n = withParameter.getAttribute();

            var withoutParameter = new Class();
            withoutParameter.setAttribute(true);
            bool b = withoutParameter.getAttribute();

            var initialized = new Class();
            n = initialized.getInitializedAttribute();
            //initialized.setInitializedAttribute(true);
            n = initialized.getInitializedAttribute();
        }

        public static void Main() {
        }

    }

}

