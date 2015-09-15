using System;
using Figures;

namespace Testing.Wrong.Dynamics {

    class CommonOperators {


        public static void wrongTest() {
            // * Local is a static reference
            var local;

            if (true)
                local = true;
            else
                local = 3;

            // local: int\/bool
			// * Error
            // * Binary Arithmetic
            3 + local; // * Error
            local * 3; // * Error
            '3' - local; // * Error
            local * 3; // * Error
            3.2 / local; // * Error
            local % 2; // * Error

            // * Unary Arithmetic
            local++; // * Error

            // * Now local is int. To be int\/bool once again...
            if (true)
               local = true;
            else
               local = 3;

            -local; // * Error
            +local; // * Error
            --local; // * Error

            // * Now local is int. To be int\/bool once again...
            if (true)
               local = true;
            else
               local = 3;

            // * Logic 
            local && local || !local; // * Error

            // * Relational
            3 > local; // * Error
            local < 3; // * Error
            '3' == local; // * Error
            local != 3; // * Error
            3.2 >= local; // * Error
            local <= 2; // * Error

            // * Bitwise
            local & local | local ^ local & ~local;

            // * Shift
            local << 4;      // * Error
            local >> '3';    // * Error
        }
    }

    public class Run {
        public static void Main() {
        }
    }

}