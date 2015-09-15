using System;

namespace CSGrande
{
	public class NumericSortTest
    {
        public var array_rows;
        public var TestArray; 
        public void buildTestData()
        {
            TestArray = new int[array_rows];
			var rndnum = new Random(1729); 
			var i = 0;
            while(i < array_rows)
			{
                TestArray[i] = rndnum.Next();				
				i = i + 1;
			}			
			
        }

        public void Do()
        {
            NumHeapSort();
        }
		
        private void NumSift(var min, var max)    
        {						
			while ((min + min) <= max)
            {   				
				var inc = false;
                if ((min + min) < max && TestArray[min + min] < TestArray[min + min + 1])
                    inc = true;
                var k = inc ? min + min + 1 : min + min;   							
                if (TestArray[min] < TestArray[k])
                {   
					var temp = TestArray[k];									
                    TestArray[k] = TestArray[min];
                    TestArray[min] = temp;                 					
                    min = k;                    
                }
                else
                    min = max + 1;
            }					
        }

        private void NumHeapSort()
        {
            var temp; 
								
            var top = array_rows - 1;			
			var i = top / 2;
            while( i > 0)
			{								
                NumSift(i, top);
				i = i - 1;
			}	
			i = top;
            while( i > 0)
            {
                NumSift(0, i);
                temp = TestArray[0];
                TestArray[0] = TestArray[i];
                TestArray[i] = temp;
				i = i - 1;
            }			
        }

        public void freeTestData()
        {
            TestArray = new int[0];
            System.GC.Collect(); 
        }
    }

    public class JGFHeapSortBench : NumericSortTest
    {
        private var size;
        private var datasizes;
		
		public JGFHeapSortBench()
		{
			datasizes = new int[3];
			datasizes[0] = 1000000;
			datasizes[1] = 5000000;
			datasizes[2] = 25000000;			
		}

        public void JGFsetsize(var size)
        {
            this.size = size;
        }

        public void JGFinitialise()
        {
            array_rows = datasizes[size];
            buildTestData();
        }

        public void JGFkernel()
        {
            Do();
        }

        public void JGFvalidate()
        {
            var error = false;
			var i = 1;
            while (i < array_rows)
            {
                error = (TestArray[i] < TestArray[i - 1]);				
                if (error)
                {
                    Console.WriteLine("Validation failed");
                    Console.WriteLine("Item " + i + " = " + TestArray[i]);
                    Console.WriteLine("Item " + (i - 1) + " = " + TestArray[i - 1]);
					Console.ReadLine();
                    break;
                }
				i = i + 1;
            }
        }

        public void JGFtidyup()
        {
            freeTestData();
        }

        public void JGFrun() {
            JGFrun1(0);
        }

        public void JGFrun1(var size)
        {
            JGFsetsize(size);
            JGFinitialise();
            JGFkernel();
            JGFvalidate();
            JGFtidyup();
        }

        public static void Main()
        {			
			var hb = new JGFHeapSortBench();
            hb.JGFrun();		
			Console.WriteLine("NumericSortTest completed!!");						
        }
    }
}
