using System;

namespace Testing.BCLBuiltInTypes {

    class MyString {
        public void test() {
            char[] chars = "hello".ToCharArray();
            string myString = new System.String(chars);
            for (int i = 0; i < myString.Length; i++) {
                char ch = myString.ToCharArray()[i];
            }
            // * Inheritance
            IComparable myComparable = myString;
            Object myObject = myString;
            String otherOne = myString;
        }
    }


    class MyInt {
        public void test() {
            Int32 myInt = 3;
            int other = myInt;
            other.GetHashCode();
            IComparable myComparable = other;
            Object myObject = other;
        }
    }

    class MyArray {
        public void test() {
            MyArray[] v1 = new MyArray[10];
            for (int i = 0; i < v1.Length; i++)
                v1[i].test();

            int[] v2 = new System.Int32[10];
            Int32[] v3 = v2;
            int c = v3.GetLength(0);
            System.Array array = v2;
        }
    }


    class MyObject {
        public void test() {
            object obj = new MyObject();
            obj.GetHashCode();
            Object anotherOne = obj;
        }
    }

    class MyChar {
        public void test() {
            Char myChar = 'a';
            char other = myChar;
            other.GetHashCode();
            System.IConvertible myConvertible = other;
            Object myObject = other;
        }
    }

    class MyBool {
        public void test() {
            Boolean myBool = true;
            bool other = myBool;
            other.GetHashCode();
            System.IConvertible myConvertible = other;
            Object myObject = other;
        }
    }

    class MyDouble {
        public void test() {
            Double myDouble = 3;
            double other = myDouble;
            other.GetHashCode();
            System.IConvertible myConvertible = other;
            Object myObject = other;
        }
    }


    class Test {
        public static void Main() { }
    }


}