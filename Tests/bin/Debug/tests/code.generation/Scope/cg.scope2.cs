using System;

    class Test {
        protected const int x = 4;
        public static void Main() {
            const int x = 2;
			if (x!=2)
				Environment.Exit(-1);
        }
  }