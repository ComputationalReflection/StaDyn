using System;
using Figures;

namespace Testing.Wrong.Dynamics {

    class Promotion {

        static Circle toCircle(Circle circle) {
            return circle;
        }

        static Rectangle toRectangle(Rectangle rectangle) {
            return rectangle;
        }

        static var circleOrRectangle(bool condition) {
            if (condition)
                return new Circle(0, 0, 10);
            else 
                return new Rectangle(0, 0, 20, 30);
        }

        static void test() {
            var dynamicRef = circleOrRectangle(true);
            var staticRef = circleOrRectangle(true);

            Circle circle = toCircle(staticRef); // * Error
            Rectangle rectangle = toRectangle(staticRef); // * Error

            circle = staticRef; // * Error
            rectangle = staticRef; // * Error
        }
    }

    public class Run {
        public static void Main() {
        }
    }

}

