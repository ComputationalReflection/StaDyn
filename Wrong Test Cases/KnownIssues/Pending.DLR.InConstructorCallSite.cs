using System;
namespace StaticAttributes.Initialization
{
    //ATTENTION!!!! This program causes an error only when it is compiled with /server option

    public class A
    {
        public dynamic GetMessage()
        {
            return "Hello World!";
        }
    }

    public class Program
    {
        dynamic Attr;
        public Program(dynamic param)
        {
                        //this CallSite is problematic because of the "naming" for the CallSite Container
            this.Attr = param.GetMessage();

            //Refactoring avoids the problem
            //this.Initialize(param);
        }

        //public void Initialize(dynamic param)
        //{
        //    this.Attr = param.GetMessage();
        //}

        public static void Main(string[] args)
        {
            Program p = new Program(new A());
        }
    }
}