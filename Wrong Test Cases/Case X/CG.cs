using System;
using System.Collections.Generic;
using System.ComponentModel;
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


public class EvaluateExpression
{
    public static Integer Visit(Integer op1) { return new Integer(op1.data); }
    public static Double Visit(Double op1) { return new Double(op1.data); }
    
}

public class Program
{
    static object Evaluate(object exp1)
    {
		if(exp1 is Integer)
			return EvaluateExpression.Visit((Integer)exp1);
		return EvaluateExpression.Visit((Double)exp1);
    }

    static void Main()
    {
        var result;
        var op1;
        if (true)
            op1 = new Integer(1);
        else
            op1 = new Double(1);
        result = Evaluate(op1);
        Console.WriteLine("Result = {0}", result);

    }
}
