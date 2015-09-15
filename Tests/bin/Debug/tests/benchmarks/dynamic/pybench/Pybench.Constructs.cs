using System;

namespace Pybench.Constructs
{
	public class Pybench
	{		
		public static void Main()
        {				
			Test test = new IfThenElse();
			test.test();	
			test = new NestedForLoops();
			test.test();				
			test = new ForLoops();
			test.test();											
			Console.WriteLine("Pybench completed!!");		
        }	
	}
	
	public abstract class Test 
	{
        public abstract void test();
    }

	public class IfThenElse : Test
    {		
        public override void test()
        {
			var one = 1;
			var two = 2;
			var three = 3;
            var a = one;
			var b = two;
			var	c = three;
			var i = 0;
            while (i < (150000 * 3))
            {				
                if (a == 1) {
                    if (b == 2) {
                        if (c != 3) {
                            c = 3;
                            b = 3;
                        }
                        else {
                            c = 2;
                        }
                    }
                    else {
                        if (b == 3) {
                            b = 2;
                            a = 2;
                        }
                    }
                }
                else {
                    if (a == 2)
                        a = 3;
                    else
                        a = 1;
                }
				
				if (a == 1) {
                    if (b == 2) {
                        if (c != 3) {
                            c = 3;
                            b = 3;
                        }
                        else {
                            c = 2;
                        }
                    }
                    else {
                        if (b == 3) {
                            b = 2;
                            a = 2;
                        }
                    }
                }
                else {
                    if (a == 2)
                        a = 3;
                    else
                        a = 1;
                }
				
				if (a == 1) {
                    if (b == 2) {
                        if (c != 3) {
                            c = 3;
                            b = 3;
                        }
                        else {
                            c = 2;
                        }
                    }
                    else {
                        if (b == 3) {
                            b = 2;
                            a = 2;
                        }
                    }
                }
                else {
                    if (a == 2)
                        a = 3;
                    else
                        a = 1;
                }
				
				if (a == 1) {
                    if (b == 2) {
                        if (c != 3) {
                            c = 3;
                            b = 3;
                        }
                        else {
                            c = 2;
                        }
                    }
                    else {
                        if (b == 3) {
                            b = 2;
                            a = 2;
                        }
                    }
                }
                else {
                    if (a == 2)
                        a = 3;
                    else
                        a = 1;
                }
				
				if (a == 1) {
                    if (b == 2) {
                        if (c != 3) {
                            c = 3;
                            b = 3;
                        }
                        else {
                            c = 2;
                        }
                    }
                    else {
                        if (b == 3) {
                            b = 2;
                            a = 2;
                        }
                    }
                }
                else {
                    if (a == 2)
                        a = 3;
                    else
                        a = 1;
                }
				i++;
            }
        }
    } 

	public class NestedForLoops : Test
	{		
        public override void test() 
		{            
            var l1 = new var[1000];
            var l2 = new var[10];
            var l3 = new var[5];

            for (var i = 0; i < 1000; i++) l1[i] = i;
            for (var i = 0; i < 10; i++) l2[i] = i;
            for (var i = 0; i < 5; i++) l3[i] = i;

            var i = 0;
			while(i < 300)
			{
                for (var j = 0 ; j < l1.Length; j++)
				{
					for (var k = 0 ; k < l2.Length; k++) 
					{
						for (var l = 0 ; l < l3.Length; l++) ;
					}
				}
				i++;
            }
        }
    }
	
	public class ForLoops : Test 
	{		
        public override void test() 
		{
            var l1 = new var[100];
			
			for (var i = 0; i < 100; i++) l1[i] = i;
			
            var temp = 0;

            for (var x = 0; x < 10000; x++)
            {
                temp = 0; for (var i = 0; i < l1.Length; i++) temp++;
				temp = 0; for (var i = 0; i < l1.Length; i++) temp++;
				temp = 0; for (var i = 0; i < l1.Length; i++) temp++;
				temp = 0; for (var i = 0; i < l1.Length; i++) temp++;
				temp = 0; for (var i = 0; i < l1.Length; i++) temp++;
				
				temp = 0; for (var i = 0; i < l1.Length; i++) temp++;
				temp = 0; for (var i = 0; i < l1.Length; i++) temp++;
				temp = 0; for (var i = 0; i < l1.Length; i++) temp++;
				temp = 0; for (var i = 0; i < l1.Length; i++) temp++;
				temp = 0; for (var i = 0; i < l1.Length; i++) temp++;
				
				temp = 0; for (var i = 0; i < l1.Length; i++) temp++;
				temp = 0; for (var i = 0; i < l1.Length; i++) temp++;
				temp = 0; for (var i = 0; i < l1.Length; i++) temp++;
				temp = 0; for (var i = 0; i < l1.Length; i++) temp++;
				temp = 0; for (var i = 0; i < l1.Length; i++) temp++;
				
				temp = 0; for (var i = 0; i < l1.Length; i++) temp++;
				temp = 0; for (var i = 0; i < l1.Length; i++) temp++;
				temp = 0; for (var i = 0; i < l1.Length; i++) temp++;
				temp = 0; for (var i = 0; i < l1.Length; i++) temp++;
				temp = 0; for (var i = 0; i < l1.Length; i++) temp++;
				
				temp = 0; for (var i = 0; i < l1.Length; i++) temp++;
				temp = 0; for (var i = 0; i < l1.Length; i++) temp++;
				temp = 0; for (var i = 0; i < l1.Length; i++) temp++;
				temp = 0; for (var i = 0; i < l1.Length; i++) temp++;
				temp = 0; for (var i = 0; i < l1.Length; i++) temp++;
            }
        }
    }   	
}