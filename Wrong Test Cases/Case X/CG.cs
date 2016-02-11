using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Integer
{
    public int data;
    public Integer(int data)
    {
        this.data = data;
    }
    public override string ToString()
    {
        return this.data.ToString();
    }
}

public class Double
{
    public double data;
    public Double(double data)
    {
        this.data = data;
    }
    public override string ToString()
    {
        return this.data.ToString();
    }
}

public class AddOp { }

public class EvaluateExpression
{
    public static Integer Visit(Integer op1, AddOp op, Integer op2) { return new Integer(op1.data + op2.data); }
    public static Double Visit(Double op1, AddOp op, Double op2) { return new Double(op1.data + op2.data); }   
}

public class Program
{
    static var Evaluate(var exp1, var op, var exp2)
    {
        return EvaluateExpression.Visit(exp1, op, exp2);
    }
    static void Main()
    {
		var result = Evaluate(new Integer(1), new AddOp(), new Integer(2));
        Console.WriteLine("1+2 = {0}", result);       		
    }
}
