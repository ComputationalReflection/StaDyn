
namespace Testing.Wrong.Cast {


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
            Test test = new Test();
            test.setAttribute(false);
            test.castToint(); // * Error
        }
    }


    public class Run {
        public static void Main() {
        }
    }


}