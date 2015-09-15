using System;

namespace Testing.Wrong.BCLBuiltInTypes {
    
    class MyString {
        public void testWrong() {
            string myString = new System.String(3);  // * Error
            for (int i = 0; i < myString.Length; i++) {
                char ch = myString.ToCharArray(); // * Error
            }
            // * Inheritance
            MyString myType = myString; // * Error
            object o;
            myString = o; // * Error
        }
    }

    public class Run {
        public static void Main() {
        }
    }


}