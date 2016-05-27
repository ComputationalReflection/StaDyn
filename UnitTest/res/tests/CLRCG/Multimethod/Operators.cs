using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Multimethod
{
    public class Operator { }
    public class ArithmeticOperator : Operator {}
    public class ComparisonOperator : Operator {}
    public class LogicalOperator : Operator {}

    public class AddOperator : ArithmeticOperator
    {
        public override string ToString()
        {
            return "+";
        }
    }

    public class SubOperator : ArithmeticOperator
    {
        public override string ToString()
        {
            return "-";
        }
    }

    public class EqualToOperator : ComparisonOperator
    {
        public override string ToString()
        {
            return "==";
        }
    }

    public class AndOperator : LogicalOperator
    {
        public override string ToString()
        {
            return "&&";
        }
    }

    public class OrOperator : LogicalOperator
    {
        public override string ToString()
        {
            return "||";
        }
    }
}