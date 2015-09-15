using System;
namespace CG.Var.Cast {

    class Test {
        private var attribute;

        public void setAttribute(var attribute) {
            this.attribute = attribute;
        }

        public int castToint() {
            int n = (int)this.attribute;
            return n;
        }

        static void testImplicit() {
            int n;
            Test test = new Test();
            test.setAttribute('a');
            n = test.castToint();
        }

        public static void Main() {
			testImplicit();	
			Console.WriteLine("Successful!!");
        }
    }
}


