using System;
using System.Collections;
using System.Collections.Generic;

namespace Pystone
{
    public class Idents_Ok
    {
        public object Ident1;

        public Idents_Ok()
        {
            this.Ident1 = 1;
        }
    }

    public class Idents_ClassAtt
    {
        public static object Ident2 = 2;
    }

    public class Idents_InstanceAtt
    {
        public object Ident3 = 3;
    }
    
    public class Program 
    {
        public static void Main(string[] args) 
        {
            //Explicit constructor is working
            Idents_Ok i = new Idents_Ok();
            Console.WriteLine(i.Ident1);

            //Runtime Error. box op missing in implicit class constructor
            Console.WriteLine(Idents_ClassAtt.Ident2);

            //Runtime Error. box op missing in implicit instance constructor
            Idents_InstanceAtt i_obj = new Idents_InstanceAtt();
            Console.WriteLine(i_obj.Ident3);
        }
    }
}