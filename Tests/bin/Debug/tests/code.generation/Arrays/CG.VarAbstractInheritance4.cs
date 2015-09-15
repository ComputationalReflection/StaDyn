using System;
namespace CG.Var.AbstractInheritance
{
	public abstract class Parent {		
		private int num;
		private var attribute;
		
		public Parent(int num) { 
			this.num = num;				
			this.setAttribute(num); 
		}
		
		public int getX() { 
			return num; 
		}		
				
		public virtual var getAttribute() 
		{ 
			return attribute; 
		}
		
		public virtual void setAttribute(var attribute) 
		{ 
			this.attribute =  attribute; 
		}		
	}
	
	public class Child : Parent {
		public Child(int x):base(x){}	

		public override var getAttribute() { 
			Console.WriteLine("Get" + getX() + base.getAttribute()); 
			return base.getAttribute();			
		}
		
		public override void setAttribute(var attribute) { 
			base.setAttribute(attribute);
			Console.WriteLine("Set" + getX() + base.getAttribute()); 
		}		
	}
	
	public class Test{
		public static void Main() {
			Parent child = new Child(7);			
			int i = child.getAttribute();				
			Console.WriteLine("Attribute: " + i.ToString());
			child.setAttribute(2);
			i = child.getAttribute();				
			Console.WriteLine("Attribute: " + i.ToString());
			Console.WriteLine("Successfull!!");
		}
	}
}