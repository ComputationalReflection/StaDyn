using System;

namespace TestVar {

    class VarWrap {
        private var attribute;

        public VarWrap(var param) {
            this.attribute = param;
        }
        public VarWrap() { }

        public var get() { 
            return attribute; 
        }

        public void set(var param) {
            attribute = param; 
        }
		
        public static void Main() {
			VarWrap v = new VarWrap(4);
			v.set(2 + v.get());
			v.set("Hello");		
			v.set(v.get() + " world!!");			
			Console.Out.WriteLine(v.get());
        }
    }
}

