using System;

namespace CG.TestGettersAndSetters {
	public class Wrapper {
		private int int_attr;		
		public int Get_int_attr()
		{
			return int_attr;
		}
		
		public void Set_int_attr(int int_value)
		{
			int_attr = int_value;
		}
				
		private static int st_int_attr;
		public static int Get_st_int_attr()
		{
			return st_int_attr;
		}
		
		public static void Set_st_int_attr(int st_int_value)
		{
			st_int_attr = st_int_value;
		}
				
		private String string_attr;
		public String Get_string_attr()
		{
			return string_attr;
		}
		
		public void Set_string_attr(String string_value)
		{
			string_attr = string_value;
		}
		
		private static String st_string_attr;
		public static String Get_st_string_attr()
		{
			return st_string_attr;
		}
		
		public static void Set_st_string_attr(String st_string_value)
		{
			st_string_attr = st_string_value;
		}
		
		private Object object_attr;
		public Object Get_object_attr()
		{
			return object_attr;
		}
		
		public void Set_object_attr(Object object_value)
		{
			object_attr = object_value;
		}
		
		private static Object st_object_attr;
		public static Object Get_st_object_attr()
		{
			return st_object_attr;
		}
		
		public static void Set_st_object_attr(Object st_object_value)
		{
			st_object_attr = st_object_value;
		}
		
		private var var_attr;
		public var Get_var_attr()
		{
			return var_attr;
		}
		
		public void Set_var_attr(var var_value)
		{
			var_attr = var_value;
		}
		
		private static var st_var_attr;
		public static var Get_st_var_attr()
		{
			return st_var_attr;
		}
		
		public static void Set_st_var_attr(var st_var_value)
		{
			st_var_attr = st_var_value;
		}
    }

    public class Test {

		public void TestAttr(){
			Wrapper w = new Wrapper();
			w.Set_int_attr(4);
			int local_int = w.Get_int_attr();
			if (local_int != 4)
				Environment.Exit(-1);
			
			w.Set_string_attr("Hello");
			String local_string = w.Get_string_attr();
			if (!local_string.Equals("Hello"))
				Environment.Exit(-1);			
			
			w.Set_object_attr(4);
			Object local_object = w.Get_object_attr();
			if (((int)local_object) != 4)
				Environment.Exit(-1);	
			
			w.Set_object_attr("Hello");
			local_object = w.Get_object_attr();
			if (!((String)local_object).Equals("Hello"))
				Environment.Exit(-1);	
				
			w.Set_var_attr(4);
			var local_var = w.Get_var_attr();
			if (local_var != 4)
				Environment.Exit(-1);	
				
			w.Set_var_attr("Hello");
			local_var = w.Get_var_attr();
			if (!local_var.Equals("Hello"))
				Environment.Exit(-1);	
		}
		
		public void TestReflectiveAttr(var w){			
			w.Set_int_attr(4);
			int local_int = w.Get_int_attr();
			if (local_int != 4)
				Environment.Exit(-1);
			
			w.Set_string_attr("Hello");
			String local_string = w.Get_string_attr();
			if (!local_string.Equals("Hello"))
				Environment.Exit(-1);			
			
			w.Set_object_attr(4);
			Object local_object = w.Get_object_attr();
			if (((int)local_object) != 4)
				Environment.Exit(-1);	
			
			w.Set_object_attr("Hello");
			local_object = w.Get_object_attr();
			if (!((String)local_object).Equals("Hello"))
				Environment.Exit(-1);	
				
			w.Set_var_attr("Hello");
			var local_var = w.Get_var_attr();
			if (!local_var.Equals("Hello"))
				Environment.Exit(-1);	
				
			w.Set_var_attr(4);
			local_var = w.Get_var_attr();
			if (local_var != 4)
				Environment.Exit(-1);	
		}
		
		public void TestSTAttr(){			
			Wrapper.Set_st_int_attr(4);
			int local_int = Wrapper.Get_st_int_attr();
			if (local_int != 4)
				Environment.Exit(-1);
			
			Wrapper.Set_st_string_attr("Hello");
			String local_string = Wrapper.Get_st_string_attr();
			if (!local_string.Equals("Hello"))
				Environment.Exit(-1);			
			
			Wrapper.Set_st_object_attr(4);
			Object local_object = Wrapper.Get_st_object_attr();
			if (((int)local_object) != 4)
				Environment.Exit(-1);	
			
			Wrapper.Set_st_object_attr("Hello");
			local_object = Wrapper.Get_st_object_attr();
			if (!((String)local_object).Equals("Hello"))
				Environment.Exit(-1);	
				
			Wrapper.Set_st_var_attr(4);
			var local_var = Wrapper.Get_st_var_attr();
			if (local_var != 4)
				Environment.Exit(-1);	
				
			Wrapper.Set_st_var_attr("Hello");
			local_var = Wrapper.Get_st_var_attr();
			if (!local_var.Equals("Hello"))
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

