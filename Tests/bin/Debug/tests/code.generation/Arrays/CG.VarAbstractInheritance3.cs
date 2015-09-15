using System;
namespace CG.VaraAbstractInheritance{
     public class Parent {
	    private var attribute;		
		public virtual var getAttribute() { return attribute; }
        public virtual void setAttribute(var attribute){ this.attribute = attribute;}
		public Parent() { }		
		public Parent(var attribute) { 
			this.attribute = attribute;
		}		
    }

    public class Child : Parent {
        public Child(var p){
			base.setAttribute(p);
        }
		
		public override var getAttribute() { 
			Console.WriteLine("Logger child");
			return base.getAttribute(); 
		}
    }
	
	public class Test
	{
		public static void Main() {
			Parent parent = new Parent("Hello parent!!");									
			Console.WriteLine("Parent attribute: " + parent.getAttribute());
			
			Parent child = new Child("Hello child!!");			
			Console.WriteLine("Child attribute: " + child.getAttribute());
		}	
	}
}