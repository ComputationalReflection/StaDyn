using System;

namespace CG.VarAbstractInheritance {

    abstract class Parent {
        public abstract void m(var v);
		public abstract int mplus(var v);		
    }
    class Child : Parent {
	  
        public Child() {
		}
		
		public override void m(var v) {
			Console.WriteLine("Child: " + v);
		}
		
		public override int mplus(var v) {
			Console.WriteLine("Child: " + (v + 2));
			return (int)(v + 2);
		}			
	}
	
	class Test {		
	
        public static void Main() {
			Parent p1 = new Child();		   
			p1.m("hola");
			p1.m(35.5);
			p1.mplus(35.5);
			p1.mplus(5);
			p1.mplus('a');	  
		}
    }
}