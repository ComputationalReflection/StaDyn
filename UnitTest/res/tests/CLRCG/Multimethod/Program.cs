using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multimethod
{
    public class Program
    {
        public static var Evaluate(dynamic exp1, dynamic op, dynamic exp2)
        {
            return EvaluateExpression.Visit(exp1, op, exp2);
        }

        static void Main(string[] args)
        {            
            var result;
            IntegerValue integerValue = new IntegerValue(3);
            DoubleValue doubleValue = new DoubleValue(23.34);
            BoolValue boolValue = new BoolValue(true);
            StringValue stringValue = new StringValue("StaDyn");
            result = Evaluate(integerValue, new AddOperator(), stringValue);
            Console.WriteLine("3+StaDyn = {0}", result);
            result = Evaluate(stringValue, new AndOperator(), doubleValue);
            Console.WriteLine("StaDyn && 23.34 = {0}", result);
            //result = Evaluate(boolValue, doubleValue, stringValue); //Compilation error            
            dynamic union;
            if (args.Length > 0)
                union = integerValue;
            else
                union = doubleValue;
            result = Evaluate(union, new AddOperator(), union);
            Console.WriteLine(union.ToString() + " + " + union.ToString() + " = " + result.ToString());
            Console.WriteLine("Successful!!");
        }
    }

}