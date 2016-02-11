using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Integer
{
    public int value;
    public Integer(int value)
    {
        this.value = value;
    }
    public override string ToString()
    {
        return this.value.ToString();
    }
}

public class Double
{
    public double value;
    public Double(double value)
    {
        this.value = value;
    }
    public override string ToString()
    {
        return this.value.ToString();
    }
}

public class AddOp { }

public class EvaluateExpression
{
    public static Integer Visit(Integer op1, AddOp op, Integer op2) { return new Integer(op1.value + op2.value); }
   
}

public class Program
{
    static dynamic Evaluate(dynamic exp1, dynamic op, dynamic exp2)
    {
        return EvaluateExpression.Visit(exp1, op, exp2);
    }
    static void Main()
    {
        Console.WriteLine("1+2 = {0}", Evaluate(new Integer(1), new AddOp(), new Integer(2)));       
    }
}