using System;

namespace CG.VarAbstractInheritance {

    abstract class Parent {
        public abstract void m(var v);
    }
	
    class Child : Parent {	  
        public Child() {
		}
		
		public override void m(var v) {
			String s = "Child: " + v.ToString();
			Console.WriteLine(s);
		}		
	}
	
	class Test {
        public static void Main() {
           Parent p1 = new Child();
		   p1.m("Hello");
		   p1.m(3);
		   p1.m(3.6);
		   p1.m('a');
		   p1.m(p1);		   	   
		}
    }
}