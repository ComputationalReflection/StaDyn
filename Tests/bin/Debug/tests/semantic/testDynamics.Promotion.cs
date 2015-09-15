using System;
using Figures;

namespace Testing.Dynamics {

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

        static void Main() {
            var dynamicRef = circleOrRectangle(true);
            var staticRef = circleOrRectangle(true);

            Circle circle = toCircle(dynamicRef);
            Rectangle rectangle = toRectangle(dynamicRef);

            circle = dynamicRef;
            rectangle = dynamicRef;
        }

    }
}

