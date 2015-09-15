using System.Collections;
using System;

namespace NSCollectionsTest
{
   class Auxiliar
   {
      public ArrayList al = new ArrayList();
      public Hashtable ht = new Hashtable();
      public static Hashtable ht2 = new Hashtable();
      public Stack s = new Stack();
      public static SortedList sl = new SortedList();
      public object[] objArr = new object[4];
   }

   class CollectionsTest
   {
      public void MethodArrayList()
      {
         Auxiliar a = new Auxiliar();
         a.al.Add("Hello");
         if (!(a.al[0].Equals("Hello")))
            Environment.Exit(-1);

         a.al.Add("Hello");
         a.al[1] = "World";
         if (!(a.al[1].Equals("World")))
            Environment.Exit(-1);
      }

      public void MethodHashTable()
      {
         Auxiliar a = new Auxiliar();
         a.ht.Add(21, "Value");
         if (!(a.ht[21].Equals("Value")))
            Environment.Exit(-1);

         a.ht[21] = "NewValue";
         if (!(a.ht[21].Equals("NewValue")))
            Environment.Exit(-1);
      }

      public void MethodHashTable2()
      {
         Auxiliar.ht2.Add(21, 21);
         if (!(Auxiliar.ht2[21].ToString().Equals("21")))
            Environment.Exit(-1);

         Auxiliar.ht2[21] = 2121;
         if (!(Auxiliar.ht2[21].ToString().Equals("2121")))
            Environment.Exit(-1);
      }

      public void MethodStack()
      {
         Auxiliar a = new Auxiliar();
         a.s.Push("Element");
         if (!(a.s.Peek().Equals("Element")))
            Environment.Exit(-1);

         Object[] objs = a.s.ToArray();
         if (!(objs[0].Equals("Element")))
            Environment.Exit(-1);
      }

      public void MethodSortedList()
      {
         Auxiliar.sl.Add("key", "SortedObject");
         int index = Auxiliar.sl.IndexOfKey("key");
         if (!(Auxiliar.sl["key"].Equals("SortedObject")))
            Environment.Exit(-1);

         if (!(Auxiliar.sl.GetByIndex(index).Equals("SortedObject")))
            Environment.Exit(-1);

         Auxiliar.sl["key"] = "NewSortedObject";
         if (!(Auxiliar.sl["key"].Equals("NewSortedObject")))
            Environment.Exit(-1);
         
         if (!(Auxiliar.sl.GetByIndex(index).Equals("NewSortedObject")))
            Environment.Exit(-1);
      }

      public void MethodArray()
      {
         Auxiliar a = new Auxiliar();

         a.objArr[0] = 68;
         a.objArr[1] = 'c';
         a.objArr[2] = "Array";
         a.objArr[3] = 75.4;

         if (!(a.objArr[0].ToString().Equals("68")))
            Environment.Exit(-1);

         if (!(a.objArr[1].ToString().Equals("c")))
            Environment.Exit(-1);
         
         if (!(a.objArr[2].Equals("Array")))
            Environment.Exit(-1);
         
         if (!(a.objArr[3].ToString().Equals("75,4")))
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
      }
   }
}
