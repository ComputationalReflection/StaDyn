using System;
using Figures;

namespace Testing.Wrong.Attributes {

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
       
        public static void wrongTestMultipleAttributeAssignment() {
            Class obj = new Class();
            obj.multipleAttributeAssignment();
            int n = obj.attribute.attribute.attribute; // * Error
            // * Indirect access
            Class c = obj.attribute.attribute;
            int  m = c.attribute; // * Error
        }        
        
        public void multipleMethodCall() {
            this.setAttribute(new Class());
            this.getAttribute().setAttribute(new Class());
            this.getAttribute().getAttribute().setAttribute("hello");
        }
        
        public static void wrongTestMultipleMethodCall() {
            Class obj = new Class();
            obj.multipleMethodCall();
            int n = obj.getAttribute().getAttribute().getAttribute(); // * Error
            // * Indirect access
            Class c = obj.getAttribute().getAttribute();
            int m = c.getAttribute(); // * Error
        }
    }

    public class Run {
        public static void Main() {
        }
    }

}