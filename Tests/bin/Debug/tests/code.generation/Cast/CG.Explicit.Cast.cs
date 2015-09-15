using System;

namespace CG.Cast {
    class CGExplicitCast {
		public static void Main() { 
			// * Not necessary casts
            double d = (double)3;
            object obj = (CGExplicitCast)new CGExplicitCast();

            // * Necessary casts
            int n = (int)d;
            CGExplicitCast test = (CGExplicitCast)obj;
		}
    }
}