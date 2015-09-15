using System;

namespace CG.TestFields {
	public class Wrapper {
		public int int_attr;		
		public static int st_int_attr;
		
		public String string_attr;
		public static String st_string_attr;
		
		public Object object_attr;
		public static Object st_object_attr;
		
		public var var_attr;
		public static var st_var_attr;
    }

    public class Test {

		public void TestAttr(){
			Wrapper w = new Wrapper();
			w.int_attr = 4;
			int local_int = w.int_attr;
			if (local_int != 4)
				Environment.Exit(-1);
			
			w.string_attr = "Hello";
			String local_string = w.string_attr;
			if (!local_string.Equals("Hello"))
				Environment.Exit(-1);			
			
			w.object_attr = 4;
			Object local_object = w.object_attr;
			if (((int)local_object) != 4)
				Environment.Exit(-1);	
			
			w.object_attr = "Hello";
			local_object = w.object_attr;
			if (!((String)local_object).Equals("Hello"))
				Environment.Exit(-1);	

			w.var_attr = 4;
			var local_var = w.var_attr;
			if (local_var != 4)
				Environment.Exit(-1);	
							 
			w.var_attr = "Hello";
			local_var = w.var_attr;
			if (!local_var.Equals("Hello"))
				Environment.Exit(-1);								
		}
		
		public void TestReflectiveAttr(var w){			
			w.int_attr = 4;
			int local_int = w.int_attr;
			if (local_int != 4)
				Environment.Exit(-1);
			
			w.string_attr = "Hello";
			String local_string = w.string_attr;
			if (!local_string.Equals("Hello"))
				Environment.Exit(-1);			
			
			w.object_attr = 4;
			Object local_object = w.object_attr;
			if (((int)local_object) != 4)
				Environment.Exit(-1);	
			
			w.object_attr = "Hello";
			local_object = w.object_attr;
			if (!((String)local_object).Equals("Hello"))
				Environment.Exit(-1);	

			w.var_attr = 4;
			var local_var = w.var_attr;
			if (local_var != 4)
				Environment.Exit(-1);	
							 
			w.var_attr = "Hello";
			local_var = w.var_attr;
			if (!local_var.Equals("Hello"))
				Environment.Exit(-1);								
		}
		
		public void TestSTAttr(){			
			Wrapper.st_int_attr = 4;
			int local_int = Wrapper.st_int_attr;
			if (local_int != 4)
				Environment.Exit(-1);
			
			Wrapper.st_string_attr = "Hello";
			String local_string = Wrapper.st_string_attr;
			if (!local_string.Equals("Hello"))
				Environment.Exit(-1);			
			
			Wrapper.st_object_attr = 4;
			Object local_object = Wrapper.st_object_attr;
			if (((int)local_object) != 4)
				Environment.Exit(-1);	
			
			Wrapper.st_object_attr = "Hello";
			local_object = Wrapper.st_object_attr;
			if (!((String)local_object).Equals("Hello"))
				Environment.Exit(-1);	

			Wrapper.st_var_attr = 4;
			var local_var = Wrapper.st_var_attr;
			if (local_var != 4)
				Environment.Exit(-1);								
		}
		       

        public static void Main() {			
			Test t = new Test();
            t.TestAttr();
			t.TestReflectiveAttr(new Wrapper());
			t.TestSTAttr();  			
			Console.WriteLine("Successful!!");
        }
    }
}