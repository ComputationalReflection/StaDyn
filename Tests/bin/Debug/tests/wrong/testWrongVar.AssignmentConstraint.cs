namespace TestVar.Wrong.New {

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

        public void wrong() {
            int n;
            Class withParameter = new Class(1);
            withParameter.setAttribute("true");
            n = withParameter.getAttribute(); // * Error
            n = withParameter.baseAttribute; // * Error

            Class initialized = new Class();
            n = initialized.getInitializedAttribute();
            initialized.setInitializedAttribute(true);
            n = initialized.getInitializedAttribute(); // * Error
        }

    }

    public class Run {
        public static void Main() {
        }
    }

}

