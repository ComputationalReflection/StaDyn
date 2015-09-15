using System;

public class Run {
    public static void Main() {
        int[] arr2 = new int[] { 2, 3, 4 };

        for (int i = 0, j = 2; j <= 4; i++, j++)
            if (arr2[i] != j)
                Environment.Exit(-1);
     }
}