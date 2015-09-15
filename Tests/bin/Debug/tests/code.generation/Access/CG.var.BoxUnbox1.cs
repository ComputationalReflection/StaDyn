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
      Console.WriteLine(obj.getAttribute());

      int number = obj.getAttribute();
      Console.WriteLine(number);

      var aux = obj.getAttribute() + 58;
      Console.WriteLine(aux);

      object o = obj.getAttribute();
      Console.WriteLine(o);

      double d = obj.getAttribute();
      Console.WriteLine(d);
   }

   public void Test2()
   {
      ClassAttribute obj2 = new ClassAttribute(22);
      Console.WriteLine(obj2.getAttribute());

      obj2.setAttribute(obj2);
      Console.WriteLine(obj2.getAttribute());

      obj2.setAttribute(3.3);
      Console.WriteLine(obj2.getAttribute());
   }

   public void Test3()
   {
      ClassAttribute obj3 = new ClassAttribute(22);
      Console.WriteLine(obj3.getAttribute());

      obj3.setAttribute(false);
      Console.WriteLine(obj3.getAttribute());
   }

   public void Test4()
   {
      ClassAttribute obj4 = new ClassAttribute(24);
      Console.WriteLine(obj4.getAttribute());

      int n = obj4.getAttribute();
      Console.WriteLine(n);

      obj4.setAttribute("Hello");
      Console.WriteLine(obj4.getAttribute());

      string s = obj4.getAttribute();
      Console.WriteLine(s);

      obj4.setAttribute(true);
      Console.WriteLine(obj4.getAttribute());

      bool b = obj4.getAttribute();
      Console.WriteLine(b);

      obj4.setAttribute(7.9);
      Console.WriteLine(obj4.getAttribute());

      double n2 = obj4.getAttribute();
      Console.WriteLine(n2);

      obj4.setAttribute(obj4);
      Console.WriteLine(obj4.getAttribute());

      object obj5 = obj4.getAttribute();
      Console.WriteLine(obj5);

      obj4.setAttribute('P');
      Console.WriteLine(obj4.getAttribute());

      char character = obj4.getAttribute();
      Console.WriteLine(character);
   }

   static void Main()
   {
      VarsBoxUnbox v = new VarsBoxUnbox();
      //var v = new VarsBoxUnbox();

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

// pruebas con char
// - atributos/metodos/locales/parametros
// - pruebas con value types distintos de char int double
// double n = obj.GetAttribute()
// object o = obj.GetAttribute()
// algo que necesite un unbox y una conversion -> unbox int32, conv.r8
// Todos son TypeVariable:
// ValueType --> ValueType
// ValueType --> Object
// Object --> ValueType
// Object --> Object
// Despues ver que pasa cuando es FreshType


