using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class IntegerValue
{
    public int MyVal;
    public IntegerValue(int val)
    {
        this.MyVal = val;
    }
    public override string ToString()
    {
        return this.MyVal.ToString();
    }
}

public class DoubleValue
{
    public double MyVal;
    public DoubleValue(double val)
    {
        this.MyVal = val;
    }
    public override string ToString()
    {
        return this.MyVal.ToString();
    }
}

public class StringValue
{
    public string MyVal;
    public StringValue(string val)
    {
        this.MyVal = val;
    }
    public override string ToString()
    {
        return this.MyVal.ToString();
    }
}

public class AddOp
{
    public override string ToString()
    {
        return "+";
    }
}

public class EvaluateExpression
{
    public static IntegerValue Visit(IntegerValue op1, AddOp op, IntegerValue op2) { return new IntegerValue(op1.MyVal + op2.MyVal); }
    public static DoubleValue Visit(IntegerValue op1, AddOp op, DoubleValue op2) { return new DoubleValue(op1.MyVal + op2.MyVal); }
    public static DoubleValue Visit(DoubleValue op1, AddOp op, IntegerValue op2) { return new DoubleValue(op1.MyVal + op2.MyVal); }
    public static DoubleValue Visit(DoubleValue op1, AddOp op, DoubleValue op2) { return new DoubleValue(op1.MyVal + op2.MyVal); }
    public static StringValue Visit(StringValue op1, AddOp op, StringValue op2) { return new StringValue(op1.MyVal + op2.MyVal); }
    public static StringValue Visit(StringValue op1, AddOp op, IntegerValue op2) { return new StringValue(op1.MyVal + op2.MyVal); }
    public static StringValue Visit(StringValue op1, AddOp op, DoubleValue op2) { return new StringValue(op1.MyVal + op2.MyVal); }
    public static StringValue Visit(IntegerValue op1, AddOp op, StringValue op2) { return new StringValue(op1.MyVal + op2.MyVal); }
    public static StringValue Visit(DoubleValue op1, AddOp op, StringValue op2) { return new StringValue(op1.MyVal + op2.MyVal); }
}

public class Program
{
    static var Evaluate(dynamic exp1, dynamic op, dynamic exp2)
    {
        return EvaluateExpression.Visit(exp1, op, exp2);
    }
    static void Main()
    {
        var result;
        var op1;
        if (true)
            op1 = new IntegerValue(1);
        else
            op1 = new DoubleValue(23.34);
        result = Evaluate(op1, new AddOp(), op1);
        Console.WriteLine(op1.ToString() + " + " + op1.ToString() + " = " + result.ToString());
    }
}
