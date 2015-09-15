using System;

namespace CG.Var.SSA {

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
	
	public class Test {
        public var attribute;

        public void setAttribute(var p) {
            this.attribute = p;
        }

        public var getAttribute() {
            return this.attribute;
        }


        /**************** For ***************/

        public void testFor() {
          var reference;
            int i, n;
            for (reference = new Circle(1, 1, 10); n < 10; reference = new Rectangle(1, 1, 20, 10)) 						
				n = n + reference.getY() + reference.getX();
            reference = new Circle(0, 0, 10);
            for (int i = 0; i < 10; i++) {
                reference.getX();
                reference = new Rectangle(0, 0, 10, 20);
                reference.getWidth();
            }
            n = reference.getY() + reference.getX();
        }
		
		public void testForAttributes() {
            Test obj = new Test();
            int i, n;

            for (obj.attribute = new Circle(1, 1, 10); n < 10; obj.setAttribute(new Rectangle(1, 1, 20, 10)))
				n = n + obj.getAttribute().getY() + obj.attribute.getX();
			
			obj.setAttribute(new Circle(0, 0, 10));
            for (int i = 0; i < 10; i++) {
                obj.attribute.getX();
                obj.attribute = new Rectangle(0, 0, 10, 20);
                obj.getAttribute().getX();
            }
            n = obj.getAttribute().getY() + obj.attribute.getX();
        }

      
		public void thisForAttributesSimple() {
            int i, n;
            for (this.attribute = new Circle(1, 1, 10); n < 10; this.setAttribute(new Rectangle(1, 1, 20, 10)))
				n = n + this.getAttribute().getY() + this.attribute.getX();
            this.setAttribute(new Circle(0, 0, 10));
            for (int i = 0; i < 10; i++) {
                this.attribute.getX();
                this.setAttribute(new Rectangle(0, 0, 10, 20));
                this.getAttribute().getY();
            }
            n = this.getAttribute().getY() + this.attribute.getX();
        }
		
		 public void thisForAttributesMultiple() {
            this.attribute = new Test();
            int i, n;
            for (this.attribute.attribute = new Circle(1, 1, 10); n < 10; this.getAttribute().setAttribute(new Rectangle(1, 1, 20, 10)))
                n = n + attribute.getAttribute().getX() * this.getAttribute().attribute.getY();

            this.getAttribute().setAttribute(new Circle(0, 0, 10));
            for (int i = 0; i < 10; i++) {
                this.attribute.attribute.getX();
                this.getAttribute().setAttribute(new Rectangle(0, 0, 10, 20));
                this.getAttribute().getAttribute().getY();
            }
            n = this.getAttribute().getAttribute().getY() + this.attribute.attribute.getX();
        }

		public static void testThisForAttributesSimple() {
            Test obj = new Test();
            obj.thisForAttributesSimple();
            int n = obj.getAttribute().getX() + obj.attribute.getY();
        }
		
        public static void Main() {
			Test t = new Test();
			t.testFor();
			t.testForAttributes();
			t.thisForAttributesSimple();
			Test.testThisForAttributesSimple();
			Console.WriteLine("Sucessfull!!");
        }
    }
}

