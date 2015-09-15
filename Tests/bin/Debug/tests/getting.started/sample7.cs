using System;

namespace GettingStarted {

    class Test {
        private var testField;

        public void setField(var param) {
            this.testField = param;
        }

        public var getField() {
            return this.testField;
        }

        public static void Main() {
            var wrapper = new Wrapper("hi");
            var test = new Test();
            test.setField(wrapper);
            string s = test.getField().get(); // * Correct!

            wrapper.set(true);
            bool b = test.getField().get(); // * Correct!
            s = test.getField().get(); // * Compilation Error
        }
    }

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


}

