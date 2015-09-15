using System;

    public class Run {
        public static void Main() {
			 int x = 3;
			 int y =4;
			 int [] v = new int [x+y+4];
			 v[x+y+3] = 5;
			 
			 if (v[x+y+3]!=5)
					Environment.Exit(-1);
		 
		 }
}