using System;
using Figures;

namespace Testing.Dynamics {

    class Attributes {

        var dynAttribute;
        var staAttribute;

        void set(bool condition, var param1, var param2) {
            
            if (condition) {
                this.dynAttribute = param1;
                this.staAttribute = param1;
            }
            else {
                this.dynAttribute = param2;
                this.staAttribute = param2;
            }   
        }

        // * The return reference is dynamic
        var getDynAttribute() {
            return dynAttribute;
        }

        // * The return reference is static
        var getStaAttribute() {
            return staAttribute;
        }


        public static void Main() {
            Attributes obj = new Attributes();
            obj.set(true, new Circle(0, 0, 10), new Rectangle(0, 0, 10, 20));

            obj.dynAttribute.getRadius();
            obj.staAttribute.getX();

            obj.getDynAttribute().getRadius();
            obj.getStaAttribute().getX();
        }

    }

}