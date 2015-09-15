using System;

namespace Testing.Wrong.Dynamics {

    class Arrays {

        public static void wrongTest() {
            // * dyn is a dynamic reference
            var[] dyn = new var[10];
            // * sta is a dynamic reference
            var[] sta = new var[10];
            for (int i = 0; i < dyn.Length; i++)
                if (i % 2 == 0) {
                    dyn[i] = i < 5;
                    sta[i] = i < 5;
                }
                else {
                    dyn[i] = ""+i;
                    sta[i] = i;
                }

            // dyn: Array(string\/bool)
            // sta: Array(int\/bool)
            dyn[0] * 3; // * Error
            sta[0] * 3; // * Error
        }
    }

    public class Run {
        public static void Main() {
        }
    }

}