using System;


public class Ppal {
    static int TicksToMillis(DateTime t1, DateTime t2) {
        TimeSpan difference = t2.Subtract(t1);
        return difference.get_Milliseconds() + difference.Seconds * 1000 + difference.Minutes * 60000;
    }
    static void Show(DateTime dt) {
        Console.WriteLine(dt.ToString());
    }
    static void Main() {
        DateTime time1 = DateTime.Now;
        Show(time1);
        for (int i = 0; i < 100000; i++) ;
        DateTime time2 = DateTime.Now;
        Show(time2);
        Console.WriteLine(time2.Subtract(time1));
        Console.WriteLine(TicksToMillis(time2, time1));
    }
}


