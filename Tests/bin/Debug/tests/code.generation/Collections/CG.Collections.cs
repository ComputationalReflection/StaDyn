using System.Collections;
using System;

namespace NSCollectionsTest
{
   class CollectionsTest
   {
      public void MethodArrayList()
      {
         System.Collections.ArrayList al = new ArrayList();

         al.Add("Hello");

         if (!(al[0].Equals("Hello")))
            Environment.Exit(-1);

         al.Add("Hello");
         al[1] = "World";

         if (!(al[1].Equals("World")))
            Environment.Exit(-1);
      }

      public void MethodHashTable()
      {
         System.Collections.Hashtable ht;
         ht = new Hashtable(84);

         ht.Add(21, "Value");

         if (!(ht[21].Equals("Value")))
            Environment.Exit(-1);

         ht[21] = "NewValue";

         if (!(ht[21].Equals("NewValue")))
            Environment.Exit(-1);
      }

      public void MethodHashTable2()
      {
         System.Collections.Hashtable ht;
         ht = new Hashtable(84);

         ht.Add(21, 21);

         if (!(ht[21].ToString().Equals("21")))
            Environment.Exit(-1);

         ht[21] = 2121;

         if (!(ht[21].ToString().Equals("2121")))
            Environment.Exit(-1);
      }

      public void MethodStack()
      {
         System.Collections.Stack s = new Stack(20);

         s.Push("Element");

         if (!(s.Peek().Equals("Element")))
            Environment.Exit(-1);

         Object[] objs = s.ToArray();

         if (!(objs[0].Equals("Element")))
            Environment.Exit(-1);
      }

      public void MethodSortedList()
      {
         System.Collections.SortedList sl = new SortedList();
         sl.Add("key", "SortedObject");
         int index = sl.IndexOfKey("key");

         if (!(sl["key"].Equals("SortedObject")))
            Environment.Exit(-1);

         if (!(sl.GetByIndex(index).Equals("SortedObject")))
            Environment.Exit(-1);

         sl["key"] = "NewSortedObject";
         if (!(sl["key"].Equals("NewSortedObject")))
            Environment.Exit(-1);

         if (!(sl.GetByIndex(index).Equals("NewSortedObject")))
            Environment.Exit(-1);
      }

      public void MethodArray()
      {
         object[] objArr = new object[4];

         objArr[0] = 68;
         objArr[1] = 'c';
         objArr[2] = "Array";
         objArr[3] = 75.4;

         if (!(objArr[0].ToString().Equals("68")))
            Environment.Exit(-1);

         if (!(objArr[1].ToString().Equals("c")))
            Environment.Exit(-1);

         if (!(objArr[2].ToString().Equals("Array")))
            Environment.Exit(-1);
         
         if (!(objArr[3].ToString().Equals("75,4")))
            Environment.Exit(-1);
      }

      static void Main()
      {
         CollectionsTest ct = new CollectionsTest();
         ct.MethodArrayList();
         ct.MethodHashTable();
         ct.MethodHashTable2();
         ct.MethodStack();
         ct.MethodSortedList();
         ct.MethodArray();
		 Console.WriteLine("Success!!");	
      }
   }
}
