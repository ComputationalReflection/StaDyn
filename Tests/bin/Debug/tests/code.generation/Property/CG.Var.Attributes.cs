using System;
using Figures;

namespace CG.VarAttributes
{
	public class Klass 
	{
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
            this.attribute.attribute.attribute = "hello";
        }

        public static void testMultipleAttributeAssignment() {
            Klass obj = new Klass();
            obj.multipleAttributeAssignment();
            string n = obj.attribute.attribute.attribute;
            // * Indirect access
            Klass c = obj.attribute.attribute;
            string m = c.attribute;
        }                
        
        public void multipleMethodCall() {
            this.setAttribute(new Klass());
            this.getAttribute().setAttribute(new Klass());
            this.getAttribute().getAttribute().setAttribute("hello");
        }
        
        public static void testMultipleMethodCall() {
            Klass obj = new Klass();
            obj.multipleMethodCall();
            string n = obj.getAttribute().getAttribute().getAttribute();
            // * Indirect access
            Klass c = obj.getAttribute().getAttribute();
            string m = c.getAttribute();
        }

        public static void Main() {
			Klass.testMultipleAttributeAssignment();
			Klass.testMultipleMethodCall();
			Console.WriteLine("Successfull!!");
        }
    }
}