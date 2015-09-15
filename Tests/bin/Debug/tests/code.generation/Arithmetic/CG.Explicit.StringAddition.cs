using System;

namespace CG.StringAddition {

    class Test {

        public static void testStringAddition() {
            string s;

            s = true + "hello";
            s = '3' + "hello";
            s = 3 + "hello";
            s = 3.3 + "hello";
            s = null + "hello";
            s = "3" + "hello";

            Int32 int32 = 32;
            s = int32 + "hello";
            Boolean boolean = true;
            s = boolean + "hello";
            Char character = '3';
            s = character + "hello";
            Double doub = 33.3;
            s = doub + "hello";
            String myString = "hello";
            s = myString + "hello";
            var variable = "hello";
            s = variable + "hello";

            s = "hello" + true;
            s = "hello" + '3';
            s = "hello" + 3;
            s = "hello" + 3.3;
            s = "hello" + null;
            s = "hello" + "3";

            s = "hello" + int32;
            s = "hello" + boolean;
            s = "hello" + character;
            s = "hello" + doub;
            s = "hello" + myString;
            s = "hello" + variable;
        }

        public static void Main() {
			Test.testStringAddition();
        }
    
    }

}

