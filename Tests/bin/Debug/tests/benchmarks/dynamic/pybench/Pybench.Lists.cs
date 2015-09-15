using System;
using System.Collections;

namespace Pybench.Lists
{
	public class Pybench
	{		
		public static void Main()
        {				
			Test test = new SimpleListManipulation();
			test.test();
			test = new ListSlicing();
			test.test();			
			Console.WriteLine("Pybench.Lists completed!!");		
        }	
	}
	
	public abstract class Test 
	{
        public abstract void test();
    }
	
	public class SimpleListManipulation : Test 
	{
        public override void test() 
		{
            var l = new var[10000];
            var x;
            var addCont = 0;
			
            var one = 1;
			var two = 2;
            var three = 3;
            var four = 4;
            var five = 5;            
         
            for (var i = 0; i < 130000; i++)
            {
                l[addCont++ % 10000] = two;
                l[addCont++ % 10000] = three;
                l[addCont++ % 10000] = four;
                l[addCont++ % 10000] = two;
                l[addCont++ % 10000] = three;
                l[addCont++ % 10000] = four;

                l[0] = three;
                l[1] = four;
                l[2] = five;
                l[3] = three;
                l[4] = four;
                l[5] = five;

                x = l[0];
                x = l[1];
                x = l[2];
                x = l[3];
                x = l[4];
                x = l[5];

                l[addCont++ % 10000] = two;
                l[addCont++ % 10000] = three;
                l[addCont++ % 10000] = four;
                l[addCont++ % 10000] = two;
                l[addCont++ % 10000] = three;
                l[addCont++ % 10000] = four;

                l[0] = three;
                l[1] = four;
                l[2] = five;
                l[3] = three;
                l[4] = four;
                l[5] = five;

                x = l[0];
                x = l[1];
                x = l[2];
                x = l[3];
                x = l[4];
                x = l[5];
				
				l[addCont++ % 10000] = two;
                l[addCont++ % 10000] = three;
                l[addCont++ % 10000] = four;
                l[addCont++ % 10000] = two;
                l[addCont++ % 10000] = three;
                l[addCont++ % 10000] = four;

                l[0] = three;
                l[1] = four;
                l[2] = five;
                l[3] = three;
                l[4] = four;
                l[5] = five;

                x = l[0];
                x = l[1];
                x = l[2];
                x = l[3];
                x = l[4];
                x = l[5];
				
				l[addCont++ % 10000] = two;
                l[addCont++ % 10000] = three;
                l[addCont++ % 10000] = four;
                l[addCont++ % 10000] = two;
                l[addCont++ % 10000] = three;
                l[addCont++ % 10000] = four;

                l[0] = three;
                l[1] = four;
                l[2] = five;
                l[3] = three;
                l[4] = four;
                l[5] = five;

                x = l[0];
                x = l[1];
                x = l[2];
                x = l[3];
                x = l[4];
                x = l[5];
				
				l[addCont++ % 10000] = two;
                l[addCont++ % 10000] = three;
                l[addCont++ % 10000] = four;
                l[addCont++ % 10000] = two;
                l[addCont++ % 10000] = three;
                l[addCont++ % 10000] = four;

                l[0] = three;
                l[1] = four;
                l[2] = five;
                l[3] = three;
                l[4] = four;
                l[5] = five;

                x = l[0];
                x = l[1];
                x = l[2];
                x = l[3];
                x = l[4];
                x = l[5];

                if (addCont > 10000) {
                    addCont = 0;
                }
            }
		}
	}
	
	public class ListSlicing : Test 
	{
        public override void test() 
		{
            var n = new ArrayList(100);
            var r = new ArrayList(25);

            for (var i = 0; i < 100; i++)
                n.Add(i);
            for (var i = 0; i < 25; i++)
                r.Add(i);

			var l;
			var m;			
            for(var i = 0; i < 800 ; i++)
            {
                l = new ArrayList(n.GetRange(0, n.Count));				
                for (var j = 0; j < r.Count; j++)
                {				
                    m = l.GetRange(50, l.Count - 50);
                    m = l.GetRange(l.Count - 25, 25);
                    m = l.GetRange(50, 5);
                    l.AddRange(n.GetRange(3, n.Count - 3));
                    m = l.GetRange(0, l.Count - 1);
                    m = l.GetRange(1, l.Count - 1);
                    l.RemoveAt(l.Count - 1);
                    l.AddRange(n.GetRange(0, n.Count));		
                }				
            }
        }
    }
}