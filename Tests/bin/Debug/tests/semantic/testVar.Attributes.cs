using System;
using Figures;

namespace Testing.Var {

    class Class {

        var attribute;

        public void setAttribute(var p) {
            this.attribute = p;
        }
        
        public var getAttribute() {
            return this.attribute;
        }
       
        public void multipleAttributeAssignment() { 
            this.attribute = new Class();
            this.attribute.attribute = new Class();
            this.attribute.attribute.attribute = "hello";
        }

        public static void testMultipleAttributeAssignment() {
            Class obj = new Class();
            obj.multipleAttributeAssignment();
            string n = obj.attribute.attribute.attribute;
            // * Indirect access
            Class c = obj.attribute.attribute;
            string m = c.attribute;
        }
                 
        
        public void multipleMethodCall() {
            this.setAttribute(new Class());
            this.getAttribute().setAttribute(new Class());
            this.getAttribute().getAttribute().setAttribute("hello");
        }
        
        public static void testMultipleMethodCall() {
            Class obj = new Class();
            obj.multipleMethodCall();
            string n = obj.getAttribute().getAttribute().getAttribute();
            // * Indirect access
            Class c = obj.getAttribute().getAttribute();
            string m = c.getAttribute();
        }

        public static void Main() {
        }

    }

}