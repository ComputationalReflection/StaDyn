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

        public static StringValue Visit(StringValue op1, AddOperator op, StringValue op2)
        {
            return new StringValue(op1.MyVal + op2.MyVal);
        }

        public static StringValue Visit(StringValue op1, AddOperator op, IntegerValue op2)
        {
            return new StringValue(op1.MyVal + op2.MyVal);
        }

        public static StringValue Visit(StringValue op1, AddOperator op, DoubleValue op2)
        {
            return new StringValue(op1.MyVal + op2.MyVal);
        }

        public static StringValue Visit(StringValue op1, AddOperator op, BoolValue op2)
        {
            return new StringValue(op1.MyVal + op2.MyVal);
        }

        public static StringValue Visit(IntegerValue op1, AddOperator op, StringValue op2)
        {
            return new StringValue(op1.MyVal + op2.MyVal);
        }

        public static StringValue Visit(DoubleValue op1, AddOperator op, StringValue op2)
        {
            return new StringValue(op1.MyVal + op2.MyVal);
        }

        public static StringValue Visit(BoolValue op1, AddOperator op, StringValue op2)
        {
            return new StringValue(op1.MyVal + op2.MyVal);
        }

        /********************* EqualTo ****************************/

        public static BoolValue Visit(IntegerValue op1, EqualToOperator op, IntegerValue op2)
        {
            return new BoolValue(op1.MyVal == op2.MyVal);
        }

        public static BoolValue Visit(DoubleValue op1, EqualToOperator op, IntegerValue op2)
        {
            return new BoolValue((int)(op1.MyVal) == op2.MyVal);
        }

        public static BoolValue Visit(IntegerValue op1, EqualToOperator op, DoubleValue op2)
        {
            return new BoolValue(op1.MyVal == ((int)op2.MyVal));
        }

        public static BoolValue Visit(DoubleValue op1, EqualToOperator op, DoubleValue op2)
        {
            return new BoolValue(op1.MyVal == op2.MyVal);
        }

        public static BoolValue Visit(BoolValue op1, EqualToOperator op, BoolValue op2)
        {
            return new BoolValue(op1.MyVal == op2.MyVal);
        }

        public static BoolValue Visit(StringValue op1, EqualToOperator op, StringValue op2)
        {
            return new BoolValue(op1.MyVal.Equals(op2.MyVal));
        }

        /********************* And ****************************/

        public static BoolValue Visit(BoolValue op1, AndOperator op, BoolValue op2)
        {
            return new BoolValue(op1.MyVal && op2.MyVal);
        }

        /********************* The rest of combinations ****************************/

        public static Value Visit(Value op1, Operator op, Value op2)
        {
            return null;
        }
    }
}