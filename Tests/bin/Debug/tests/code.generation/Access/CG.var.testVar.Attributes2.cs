using System;
using Figures;

namespace Testing.Var {

    public class Klass {

        public var attribute;

        public void setAttribute(var p) {
            this.attribute = p;
        }
        
        public var getAttribute() {
            return this.attribute;
        }
       
        public void multipleAttributeAssignment() { 
            this.attribute = new Klass();
            this.attribute.attribute = new Klass();
            this.attribute.attribute.attribute = 1024;
        }

        public static void testMultipleAttributeAssignment() {
            Klass obj = new Klass();
            obj.multipleAttributeAssignment();
            //int n = obj.attribute.attribute.attribute;
            double n = obj.attribute.attribute.attribute;

            // --------
            Console.WriteLine(n);
            Console.WriteLine(obj.attribute.attribute.attribute);
            // --------

            // * Indirect access
            Klass c = obj.attribute.attribute;
            int m = c.attribute;

            // --------
            Console.WriteLine(m);
            Console.WriteLine(c.attribute);
            // --------
        }
        
        public void multipleMethodCall() {
            this.setAttribute(new Klass());
            this.getAttribute().setAttribute(new Klass());
            this.getAttribute().getAttribute().setAttribute(1024);
        }
        
        public static void testMultipleMethodCall() {
            Klass obj = new Klass();
            obj.multipleMethodCall();
            int n = obj.getAttribute().getAttribute().getAttribute();

            // --------
            Console.WriteLine(n);
            Console.WriteLine(obj.getAttribute().getAttribute().getAttribute());
            // --------

            // * Indirect access
            Klass c = obj.getAttribute().getAttribute();
            int m = c.getAttribute();

            // --------
            Console.WriteLine(m);
            Console.WriteLine(c.getAttribute());
            // --------

        }

       public static void Main()
       {
          Klass.testMultipleAttributeAssignment();
          Klass.testMultipleMethodCall();
		  Console.WriteLine("Successful!!!");	
       }
    }
}