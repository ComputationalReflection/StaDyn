using System;

class ClassAttribute
{
   var attribute;

   public ClassAttribute(int parameter)
   {
      this.attribute = parameter;
   }

   public var getAttribute()
   {
      return attribute;
   }

   public void setAttribute(var parameter)
   {
      this.attribute = parameter;
   }
}

public class VarsBoxUnbox
{
   public void Test1()
   {
      ClassAttribute obj = new ClassAttribute(2);
      if (obj.getAttribute() != 2)
         Environment.Exit(-1);

      int number = obj.getAttribute();
      if (number != 2)
         Environment.Exit(-1);

      var aux = obj.getAttribute() + 58;
      if (aux != 60)
         Environment.Exit(-1);

      object o = obj.getAttribute();
      if (!((o is Int32) && ((int)o == 2)))
         Environment.Exit(-1);

      double d = obj.getAttribute();
      if (d != 2)
         Environment.Exit(-1);
   }

   public void Test2()
   {
      ClassAttribute obj2 = new ClassAttribute(22);

      if (obj2.getAttribute() != 22)
         Environment.Exit(-1);


      obj2.setAttribute(obj2);
      if (!(obj2.getAttribute() is ClassAttribute))
         Environment.Exit(-1);

      obj2.setAttribute(3.3);
      if (obj2.getAttribute() != 3.3)
         Environment.Exit(-1);
   }

   public void Test3()
   {
      ClassAttribute obj3 = new ClassAttribute(22);

      if (obj3.getAttribute() != 22)
         Environment.Exit(-1);

      obj3.setAttribute(false);

      if (obj3.getAttribute() == true)
         Environment.Exit(-1);
   }

   public void Test4()
   {
      ClassAttribute obj4 = new ClassAttribute(24);
      if (obj4.getAttribute() != 24)
         Environment.Exit(-1);

      int n = obj4.getAttribute();
      if (n != 24)
         Environment.Exit(-1);

      obj4.setAttribute("Hello");
      if (!(obj4.getAttribute().Equals("Hello")))
         Environment.Exit(-1);

      string s = obj4.getAttribute();
      if (!(s.Equals("Hello")))
         Environment.Exit(-1);

      obj4.setAttribute(true);
      if (obj4.getAttribute() == false)
         Environment.Exit(-1);

      bool b = obj4.getAttribute();
      if (obj4.getAttribute() == false)
         Environment.Exit(-1);

      obj4.setAttribute(7.9);
      if (obj4.getAttribute() != 7.9)
         Environment.Exit(-1);

      double n2 = obj4.getAttribute();
      if (n2 != 7.9)
         Environment.Exit(-1);

      obj4.setAttribute(obj4);
      if (!(obj4.getAttribute() is ClassAttribute))
         Environment.Exit(-1);

      object obj5 = obj4.getAttribute();
      if (!(obj5 is ClassAttribute))
         Environment.Exit(-1);

      obj4.setAttribute('P');
      if (obj4.getAttribute() != 'P')
         Environment.Exit(-1);

      char character = obj4.getAttribute();
      if (character != 80)
         Environment.Exit(-1);

      var charac = obj4.getAttribute();
      if (charac != 80)
         Environment.Exit(-1);
   }

   static void Main()
   {
      //VarsBoxUnbox v = new VarsBoxUnbox();
      var v = new VarsBoxUnbox();

      // 2 2 60 2 2
      v.Test1();

      // 22 ClassAttribute 3.3
      v.Test2();

      // 22 false
      v.Test3();

      // 24 Hello true 7.9 ClassAttribute P
      v.Test4();
   }
}

