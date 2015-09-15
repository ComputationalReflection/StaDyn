using System;

namespace Testing.Dynamics {

    class Arrays {

        public static void Main() {
            // * Local is a dynamic reference
            var[] vector = new var[10];
            for (int i = 0; i < vector.Length; i++)
                if (i % 2 == 0)
                    vector[i] = i < 5;
                else
                    vector[i] = i;

            // vector: Array(int\/bool)
            int n = new Random().Next(Int32.MaxValue);
            vector[n] > n && vector[n + 1];
        }

    }

}