using System;

namespace CSGrande
{
    public class NumericSortTest
    {
        public int array_rows;
        public int[] TestArray; 
        public void buildTestData()
        {
            TestArray = new int[array_rows];
			Random rndnum = new Random(1729); 
            for (int i = 0; i < array_rows; i = i + 1)
                TestArray[i] = rndnum.Next();
        }

        public void Do()
        {
            NumHeapSort();
        }

        private void NumSift(int min, int max)    
        {
            int k;     
            int temp;  
            while ((min + min) <= max)
            {
                k = min + min;
                if (k < max)
                    if (TestArray[k] < TestArray[k + 1])
                        k = k + 1;
                if (TestArray[min] < TestArray[k])
                {
                    temp = TestArray[k];
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
            int temp;  
            int top = array_rows - 1;
            for (int i = top / 2; i > 0; i = i - 1)
                NumSift(i, top);
            for (int i = top; i > 0; i = i - 1)
            {
                NumSift(0, i);
                temp = TestArray[0];
                TestArray[0] = TestArray[i];
                TestArray[i] = temp;
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
        private int size;
        private int[] datasizes;
		
		public JGFHeapSortBench()
		{
			datasizes = new int[3];
			datasizes[0] = 1000000;
			datasizes[1] = 5000000;
			datasizes[2] = 25000000;			
		}

        public void JGFsetsize(int size)
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
            bool error;
            error = false;
            for (int i = 1; i < array_rows; i++)
            {
                error = (TestArray[i] < TestArray[i - 1]);
                if (error)
                {
                    Console.WriteLine("Validation failed");
                    Console.WriteLine("Item " + i + " = " + TestArray[i]);
                    Console.WriteLine("Item " + (i - 1) + " = " + TestArray[i - 1]);
                    break;
                }
            }
        }

        public void JGFtidyup()
        {
            freeTestData();
        }

        public void JGFrun() {
            JGFrun1(0);
        }

        public void JGFrun1(int size)
        {
            JGFsetsize(size);
            JGFinitialise();
            JGFkernel();
            JGFvalidate();
            JGFtidyup();
        }

        public static void Main()
        {
			JGFHeapSortBench hb = new JGFHeapSortBench();
            hb.JGFrun();
			Console.WriteLine("NumericSortTest completed!!");
        }
    }
}
