using System;
using System.Text;

namespace Multimethod
{
    public class EvaluateExpression
    {
        public static IntegerValue Visit(IntegerValue op1, AddOperator op, IntegerValue op2)
        {
            return new IntegerValue(op1.MyVal + op2.MyVal);
        }

        public static DoubleValue Visit(DoubleValue op1, AddOperator op, IntegerValue op2)
        {
            return new DoubleValue(op1.MyVal + op2.MyVal);
        }

        public static DoubleValue Visit(IntegerValue op1, AddOperator op, DoubleValue op2)
        {
            return new DoubleValue(op1.MyVal + op2.MyVal);
        }

        public static DoubleValue Visit(DoubleValue op1, AddOperator op, DoubleValue op2)
        {
            return new DoubleValue(op1.MyVal + op2.MyVal);
        }

      
    }
}