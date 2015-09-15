using System;

public class Run {
    public static void Main() {
        int[][] jaggedArray = new int[][] {
                                        new int[] {1,3,5,7,9},
                                        new int[] {0,2,4,6},
                                        new int[] {11,22}
        };

        int[][] jaggedArray2 = new int[][] {
                                        new int[] {1,3,5,7,9},
                                        new int[] {0,2,4,6},
                                        new int[] {11,22}
        };
        for (int i = 0; i < jaggedArray.Length; i++) {
            for (int j = 0; j < jaggedArray[i].Length; j++)
                 if (jaggedArray[i][j] != jaggedArray2[i][j])
						Environment.Exit(-1);
        }
    }
}
