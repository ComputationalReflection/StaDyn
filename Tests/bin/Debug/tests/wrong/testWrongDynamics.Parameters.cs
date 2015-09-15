using System;
using Figures;

namespace Testing.Wrong.Dynamics {

    class Parameters {

        static void testDynamic(var dynParameter) {
            // * dynParameter is a dynamic reference
            // dynParameter: Circle\/Rectangle
            dynParameter.getUnknown(); // * Error
        }

        static void testStatic(var staParameter) {
            // * staParameter is a static reference
            // staParameter: Circle\/Rectangle
            staParameter.getWidth();  // * Error
            staParameter.getRadius(); // * Error
        }

        static var circleOrRectangle(bool condition) {
            if (condition)
                return new Circle(0, 0, 10);
            else 
                return new Rectangle(0, 0, 20, 30);
        }

        static void test() {
            testDynamic(circleOrRectangle(true));
            testStatic(circleOrRectangle(false));
        }
    }

    public class Run {
        public static void Main() {
        }
    }


}

