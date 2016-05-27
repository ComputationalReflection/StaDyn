using System;

public class Integer {
    public int data;
    public Integer(int data){this.data = data;}
    public override string ToString() {
        return this.data.ToString();
    }
}

public class Double {
    public double data;
    public Double(double data){this.data = data;}
    public override string ToString(){
        return this.data.ToString();
    }
}

public class AddOp { }

public class EvaluateExpression {
    public static Integer Visit(Integer op1, AddOp op, Integer op2) { return new Integer(op1.data + op2.data); }
    public static Double Visit(Double op1, AddOp op, Double op2) { return new Double(op1.data + op2.data); }
}

public class Program {
    static object Evaluate(object exp1, object op, object exp2) {...}
    static void Main() {        
        var op1;
        if (true)
            op1 = new Integer(1);
        else
            op1 = new Double(1);        
        var result = EvaluateDispatcher(op1, new AddOp(), op1);
        Console.WriteLine("1+2 = {0}", result);
    }
		
    static object EvaluateDispatcher(object exp1, object op, object exp2){
        if(exp1 is Integer && op is AddOp && exp2 is Integer)
            return Evaluate((Integer)exp1, (AddOp)op, (Integer)exp2);
        if (exp1 is Double && op is AddOp && exp2 is Double)
            return Evaluate((Double)exp1, (AddOp)op, (Double)exp2);
        return Evaluate(exp1, op, exp2);
    }
	static var Evaluate(Integer exp1, AddOp op, Integer exp2) {
        return EvaluateExpression.Visit(exp1, op, exp2);
    }
    static var Evaluate(Double exp1, AddOp op, Double exp2){
        return EvaluateExpression.Visit(exp1, op, exp2);
    }
}
