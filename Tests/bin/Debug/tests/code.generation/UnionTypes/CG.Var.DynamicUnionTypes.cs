using System;

namespace CG.Var.DynamicUnionTypes
{
	public class Rectangle {
        private int x, y, width, height;
        public Rectangle(int x, int y, int width, int height) {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
        public int getX() { return x; }
        public int getY() { return y; }
        public int getWidth() { return width; }
        public int getHeight() { return height; }
    }

    public class Circle {
        private int x, y, radius;
        public Circle(int x, int y, int radius) {
            this.x = x;
            this.y = y;
            this.radius = radius;
        }
        public int getX() { return x; }
        public int getY() { return y; }
        public int getRadius() { return radius; }
    }
	
	public class A { public void ma() { } }
    public class B : A { void mb() { } }
    public class C : A { void mc() { } }

	public class Test
	{
		public var attribute;

        public Test() { }

        public Test(var parameter) {
            this.attribute = parameter;
        }

        public void setAttribute(var a) { attribute = a; }
        public var getAttribute() { return attribute; }

        public static var m(var param) {
            if (3 > 10) 
				return 10;
            return param;
        }

        public static var m(var param1, var param2) {
            if (3 > 10) return param1;
            return param2;
        }			
		
		public static void testStaticUnionCalls() {
			// * Attribute is int
			Test klass = new Test(1);
			klass.setAttribute(3.3);
			// * Attribute is int \/ double
			double d = klass.getAttribute();
			// * int \/ int
			int n = Test.m(3);
			// * int \/ string
			object obj = Test.m("hello");
			int hashCode = obj.GetHashCode();
			// Rectangle \/ Circle
			var rectangleOrCircle = Test.m(new Rectangle(0, 0, 10, 20), new Circle(0, 0, 30));
			int x = rectangleOrCircle.getX();
			Console.WriteLine("Position: ({0},{1}).", rectangleOrCircle.getX(), rectangleOrCircle.getY());
		}
		
        public static void testStaticUnionArrays() {
            // * Array(int) \/ Array(double)  unifies to  Array(int \/ double) 
            var[] genericArray = Test.m(new int[10], new double[10]);
            // * int \/ double
            double d = genericArray[0];
            // * genericArray: Array(Rectangle \/ Circle)
            var[] anotherArray = new var[10];
            anotherArray[0] = new Rectangle(0, 0, 10, 20);
            anotherArray[1] = new Circle(0, 0, 30);
            int i;
            int n = anotherArray[i].getX();
            anotherArray[i++].GetHashCode();
        }

        public static void testStaticUnionInheritance() {
            // * B \/ C promotes to A
            A a = m(new B(), new C());
            a.ma();
            // * A \/ B \/ C promotes to A
            Test klass = new Test(new A());
            klass.setAttribute(new B());
            klass.setAttribute(new C());
            a = klass.getAttribute();
            a.ma();
        }

        public static void testStaticUnionClasses() {
            // Class(int) \/ Class(double)  unifies to Class( int\/double )
            Test klass = Test.m(new Test(3), new Test(3.3));
            double d = klass.getAttribute();
        }

        public static void testArithmetic() {
            // * int \/ char + int \/ double promotes to double
            double d = Test.m(3, '3') + Test.m(1, 3.3);
            // * string + int \/ string + string \/ char + bool \/ string + string\/ double promotes to string
            string s = " " + Test.m(3, "3") + Test.m("3", '3') + Test.m(true, "3.3") + Test.m("true", 3.3);
            // * int \/ char >= int \/ double is a boolean
            bool b = Test.m(3, '3') >= Test.m(1, 3.3);
            // * bool \/ bool &&  bool \/ bool is a boolean
            b = Test.m(true, false) && Test.m(false, true);
        }		
		
		public static void Main() {
			Test.testStaticUnionCalls();
			Test.testStaticUnionArrays();
			Test.testStaticUnionInheritance();
			Test.testStaticUnionClasses();
			Test.testArithmetic();
			Console.WriteLine("Successfull!!");
        }
	}
}

