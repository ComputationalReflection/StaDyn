using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Multimethod
{
    public class Value {}
    public class CharValue: Value
    {
        public char MyVal;
        public CharValue(char val)
        {
            this.MyVal = val;
        }
        public override string ToString()
        {
            return this.MyVal.ToString();
        }
    }

    public class IntegerValue : Value
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

    public class DoubleValue : Value
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

    public class BoolValue : Value
    {
        public bool MyVal;
        public BoolValue(bool val)
        {
            this.MyVal = val;
        }
        public override string ToString()
        {
            return this.MyVal.ToString();
        }
    }

    public class StringValue : Value
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
}