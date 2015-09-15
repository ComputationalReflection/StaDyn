using System;

class ClassAttribute
{
   var attribute;

   public ClassAttribute(var parameter)
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
      var obj = new ClassAttribute(2);
      if (obj.getAttribute() != 2)
         Environment.Exit(-1);

      var number = obj.getAttribute();
      if (number != 2)
         Environment.Exit(-1);

      var aux = obj.getAttribute() + 58;
      if (aux != 60)
         Environment.Exit(-1);

      var o = obj.getAttribute();
      if (o != 2)
         Environment.Exit(-1);

      var d = obj.getAttribute();
      if (d != 2.0)
         Environment.Exit(-1);

      double d2 = obj.getAttribute();
      if (d2 != 2.0)
         Environment.Exit(-1);
   }

   public void Test2()
   {
      var obj2 = new ClassAttribute(22);

      if (obj2.getAttribute() != 22)
         Environment.Exit(-1);

      obj2.setAttribute(new ClassAttribute(true));
      if (!(obj2.getAttribute() is ClassAttribute))
         Environment.Exit(-1);

      obj2.setAttribute(3.3);
      if (obj2.getAttribute() != 3.3)
         Environment.Exit(-1);
   }

   public void Test3()
   {
      var obj3 = new ClassAttribute(22);

      if (obj3.getAttribute() != 22)
         Environment.Exit(-1);

      obj3.setAttribute(false);

      if (obj3.getAttribute() == true)
         Environment.Exit(-1);
   }

   public void Test4()
   {
      var obj4 = new ClassAttribute(24);
      if (obj4.getAttribute() != 24)
         Environment.Exit(-1);

      var n = obj4.getAttribute();
      if (n != 24)
         Environment.Exit(-1);

      obj4.setAttribute("Hello");
      if (!(obj4.getAttribute().Equals("Hello")))
         Environment.Exit(-1);

      var s = obj4.getAttribute();
      if (!(s.Equals("Hello")))
         Environment.Exit(-1);

      obj4.setAttribute(true);
      if (obj4.getAttribute() == false)
         Environment.Exit(-1);

      var b = obj4.getAttribute();
      if (obj4.getAttribute() == false)
         Environment.Exit(-1); 

      obj4.setAttribute(7.9);
      if (obj4.getAttribute() != 7.9)
         Environment.Exit(-1); 

      var n2 = obj4.getAttribute();
      if (n2 != 7.9)
         Environment.Exit(-1); 

      obj4.setAttribute(new ClassAttribute(true));
      if (!(obj4.getAttribute() is ClassAttribute))
         Environment.Exit(-1); 

      var obj5 = obj4.getAttribute();
      if (!(obj5 is ClassAttribute))
         Environment.Exit(-1);

      obj4.setAttribute('P');
      if (obj4.getAttribute() != 'P')
         Environment.Exit(-1);

      var character = obj4.getAttribute();
      if (character != 80)
         Environment.Exit(-1);
   }

   static void Main()
   {
      var v = new VarsBoxUnbox();
      v.Test1();
      v.Test2();
      v.Test3();
      v.Test4();
   }
}