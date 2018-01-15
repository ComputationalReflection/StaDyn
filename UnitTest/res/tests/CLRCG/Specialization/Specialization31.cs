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

public class Bool
{
    public bool data;
    public Bool(bool data)
    {
        this.data = data;
    }
    public override string ToString()
    {
        return this.data.ToString();
    }
}

public class AddOp { }
public class EqualToOp { }

public class EvaluateExpression
{
    public static Integer Visit(Integer op1, AddOp op, Integer op2) { return new Integer(op1.data + op2.data); }
    public static Double Visit(Double op1, AddOp op, Integer op2) { return new Double(op1.data + op2.data); }
    public static Double Visit(Integer op1, AddOp op, Double op2) { return new Double(op1.data + op2.data); }
    public static Double Visit(Double op1, AddOp op, Double op2) { return new Double(op1.data + op2.data); }
    
    // EqualTo 
    public static Bool Visit(Integer op1, EqualToOp op, Integer op2) { return new Bool(op1.data == op2.data); }
    public static Bool Visit(Double op1, EqualToOp op, Integer op2) { return new Bool((int)(op1.data) == op2.data); }
    public static Bool Visit(Integer op1, EqualToOp op, Double op2) { return new Bool(op1.data == ((int)op2.data)); }
    public static Bool Visit(Double op1, EqualToOp op, Double op2) { return new Bool(op1.data == op2.data); }
    public static Bool Visit(Bool op1, EqualToOp op, Bool op2) { return new Bool(op1.data == op2.data); }
}

public class Program
{
    static var Evaluate(var exp1, var op, var exp2)
    {
        return EvaluateExpression.Visit(exp1, op, exp2);
    }
    static void Main()
    {
        var result;
        result = Evaluate(new Integer(1), new AddOp(), new Integer(2));
        Console.WriteLine("1+2 = {0}", result);
        result = Evaluate(new Integer(1), new AddOp(), new Double(2.1));
        Console.WriteLine("1+2.1 = {0}", result);
        result = Evaluate(new Double(1.1), new AddOp(), new Integer(2));
        Console.WriteLine("1.1+2 = {0}", result);
        result = Evaluate(new Double(1.1), new AddOp(), new Double(2.1));
        Console.WriteLine("1.1+2.1 = {0}", result);
        
        result = Evaluate(new Integer(1), new EqualToOp(), new Integer(2));
        Console.WriteLine("1==2 = {0}", result);
        result = Evaluate(new Integer(1), new EqualToOp(), new Double(2.1));
        Console.WriteLine("1==2.1 = {0}", result);
        result = Evaluate(new Double(1.1), new EqualToOp(), new Integer(2));
        Console.WriteLine("1.1==2 = {0}", result);
        result = Evaluate(new Double(1.1), new EqualToOp(), new Double(2.1));
        Console.WriteLine("1==2.1 = {0}", result);
        result = Evaluate(new Bool(true), new EqualToOp(), new Bool(false));
        Console.WriteLine("true==false ={0}", result);
    }
}
