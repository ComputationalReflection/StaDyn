using System;
namespace Boxing.CastExpression
{    
    public class Program
    {
        public static void Main(string[] args)
        {
            object c;
            //Causes a Runtime Exception (this generated code needs a missing 'box float64' before the 'castclass class object').
            //Fixed at revision 1536:
            //Added a Promotion in VisitorILCodeGeneration::Visit(CastExpression) in a default else part
            c = (object)10.0;
            Console.WriteLine(c);

            //PystoneObject();
            PystoneError();
        }

        public static void PystoneSimple()
        {
            dynamic CharIndex = 'A';
            CharIndex = (char)(((int)CharIndex) + 1);
            Console.WriteLine(CharIndex);
        }

        //This method causes an error in C# and StaDyn because it is conceptually erroneous
        //The char (boxed in an object variable) cannot be directly casted to int (a prior cast to char is needed)
        //PystoneSimple does not cause error because of the type inference: CharIndex is a 'char' variable
        //In this case the solution is a set of nested 'isinst' checks (see commented code)
        public static void PystoneObject()
        {
            object CharIndex = 'A';
            CharIndex = (char)(((int)CharIndex) + 1);
            Console.WriteLine(CharIndex);

            //Working example with (double \/ int \/ char)
            //if(CharIndex is double)
            //    CharIndex = (char)(((int)((double)CharIndex)) + 1);
            //else if(CharIndex is int)
            //    CharIndex = (char)(((int)((int)CharIndex)) + 1);
            //else if(CharIndex is char)
            //    CharIndex = (char)(((int)((char)CharIndex)) + 1);
            //Console.WriteLine(CharIndex);
        }


        public static void PystoneError()
        {
            dynamic CharIndex = 'A';
            if(true)
                ;
            else
                CharIndex = "hola";
            //Now CharIndex is a union type (string \/ char). After nested 'isinst' checks, an erroneus 'box int32' is generated (just before pushing '1' for the sum operation)
            //because of this CharIndex gets a erroneus value. I thought that this error was related to 'rev 1536', but seems to be 'a new issue'
            //TODO: Split this code to a new KnownIssue?
            //TODO2: In the original Pystone (with dynamic) CharIndex is (errorneusly) inferred as an UnionType. The type should be 'char'
            //TODO3: Rev 1596 introduces a runtime exception. This should be caused by TODO3 but I'm not really sure.
            CharIndex = (char)(((int)CharIndex) + 1);
            Console.WriteLine(CharIndex);
        }
    }
}