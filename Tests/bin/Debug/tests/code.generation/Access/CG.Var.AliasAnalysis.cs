using TestVar;

namespace CG.Var.AliasAnalysis {

	class VarWrap {
        private var attribute;

        public VarWrap(var param) {
            this.attribute = param;
        }
        public VarWrap() { }

        public var get() { 
            return attribute; 
        }

        public void set(var param) {
            attribute = param; 
        }
	}
	
    class Test {

        public static void modify(var theObject, var param) {
            theObject.set(param);
        }

        public static void testInterProcLocalAlias() {
            var obj1, obj2;
            obj1 = obj2 = new VarWrap();
            modify(obj1, "value");
            // * Correct!
            string s= obj2.get();
            // * Wrong!
            //int n = obj2.get(); // * Error
        }

        private var testField;

        public void setField(var param) {
            this.testField = param;
        }

        public var getField() {
            return this.testField;
        }

        public static void testInterProcGlobalAlias() {
            var boolObj = new VarWrap();
            var test = new Test();
            test.setField(boolObj);
            boolObj.set(true);
            // * Correct!
            bool b = test.getField().get();
            // * Wrong!
            //string s = test.getField().get(); // * Error
        }

        public static void Main() {
			Test.testInterProcLocalAlias();
			Test.testInterProcGlobalAlias();
        }
    }
}

