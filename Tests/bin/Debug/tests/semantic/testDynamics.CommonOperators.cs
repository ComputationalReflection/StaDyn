using System;
using Figures;

namespace Testing.Dynamics {

    class CommonOperators {

        public static void Main() {
            // * Local is a dynamic reference
            var local;

            if (true)
                local = true;
            else
                local = 3;

            // local: int\/bool

            // * Binary Arithmetic
            3 + local;
            local * 3;
            '3' - local;
            local * 3;
            3.2 / local;
            local % 2;

            // * Unary Arithmetic
            local = local + 1;// local++; 
            -local;
            +local;
            --local;

            // * Now local is int. To be int\/bool once again...
            if (true)
                local = true;
            else
                local = 3;

            
            // * Logic 
            local && local || !local;

            // * Relational
            3 > local;
            local < 3;
            '3' == local;
            local != 3;
            3.2 >= local;
            local <= 2;

            // * Bitwise
            local & local | local ^ local & ~local;

            // * Shift
            local << 4;
            local >> '3';
        }

    }

}