
namespace TestVar.Cast {

    class Test {
        private var attribute;

        public void setAttribute(var attribute) {
            this.attribute = attribute;
        }

        var castToint() {
            int n = (int)this.attribute;
            return n;
        }

        static void testImplicit() {
            int n;
            Test test = new Test();

            test.setAttribute('a');
            n = test.castToint();

            test.setAttribute('a');
            n = test.castToint();

        }

        public static void Main() {
        }

    }


}

