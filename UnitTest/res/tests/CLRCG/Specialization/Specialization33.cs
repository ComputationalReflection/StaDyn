using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class IntegerValue
{
    public int Data;
    public IntegerValue(int Data)
    {
        this.Data = Data;
    }
    public override string ToString()
    {
        return this.Data.ToString();
    }
}

public class DoubleValue
{
    public double Data;
    public DoubleValue(double Data)
    {
        this.Data = Data;
    }
    public override string ToString()
    {
        return this.Data.ToString();
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
    public static IntegerValue Visit(IntegerValue op1, AddOp op, IntegerValue op2) { return new IntegerValue(op1.Data + op2.Data); }
    public static DoubleValue Visit(IntegerValue op1, AddOp op, DoubleValue op2) { return new DoubleValue(op1.Data + op2.Data); }
    public static DoubleValue Visit(DoubleValue op1, AddOp op, IntegerValue op2) { return new DoubleValue(op1.Data + op2.Data); }
    public static DoubleValue Visit(DoubleValue op1, AddOp op, DoubleValue op2) { return new DoubleValue(op1.Data + op2.Data); }
}

public class Program
{
    static var Evaluate(dynamic exp1, dynamic op, dynamic exp2)
    {
        return EvaluateExpression.Visit(exp1, op, exp2);
    }
    static void Main()
    {
        int expressionsLength = 2;
        int operatorsLength = 1;

        var expressions = new var[expressionsLength];
        expressions[0] = new IntegerValue(3);
        expressions[1] = new DoubleValue(4.3);

        var operators = new var[operatorsLength];
        operators[0] = new AddOp();

        int i = 0;
        for (; i < expressionsLength; i = i + 1)
        {
            int j = 0;
            for (; j < operatorsLength; j = j + 1)
            {
                int k = 0;
                for (; k < expressionsLength; k = k + 1)
                {
                    var op1 = expressions[i];
                    var op = operators[j];
                    var op2 = expressions[k];
                    var result = Evaluate(op1, op, op2);
                    Console.WriteLine(op1.ToString() + " " + op.ToString() + " " + op2.ToString() + " = " + result.ToString());
                }
            }
        }
    }
}
