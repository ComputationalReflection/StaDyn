using System;
using Figures;

namespace Testing.Dynamics {

    class Parameters {

        static void testDynamic(var dynParameter) {
            // * dynParameter is a dynamic reference
            // dynParameter: Circle\/Rectangle
            dynParameter.getRadius() * dynParameter.getWidth();
        }

        static void testStatic(var staParameter) {
            // * staParameter is a static reference
            // staParameter: Circle\/Rectangle
            staParameter.getX() * staParameter.getY();
        }

        static var circleOrRectangle(bool condition) {
            if (condition)
                return new Circle(0, 0, 10);
            else 
                return new Rectangle(0, 0, 20, 30);
        }

        static void Main() {
            testDynamic(circleOrRectangle(true));
            testStatic(circleOrRectangle(false));
        }
    }
}

