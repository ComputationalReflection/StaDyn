using System;
namespace Test {

    class VarSimpleTest {

        var attribute;
		
		var GetAttribute() {return this.attribute; }
		void SetAttribute(var attribute) { this.attribute = attribute; }
		void WriteAttribute() {
			String attributeAsString = this.GetAttribute().ToString();
			Console.WriteLine(attributeAsString);
		}
		void Check(var c) {
			String varAsString = this.attribute.ToString();
			if (!c.ToString().Equals(varAsString)) Environment.Exit(-1);
		}
		
		public static void Main(String[] args) {
		  VarSimpleTest vt = new VarSimpleTest();
		  vt.SetAttribute(2);
		  vt.WriteAttribute();
		  vt.Check(2);
		  Console.WriteLine("Successfull!!!!!");
		 	
		}
	}
}

