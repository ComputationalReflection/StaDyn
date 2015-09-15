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

    }

}

