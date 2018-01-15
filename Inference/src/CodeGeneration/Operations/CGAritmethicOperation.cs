using System;
using CodeGeneration;
using CodeGeneration.ExceptionManagement;
using CodeGeneration.Operations;
using TypeSystem;
using AST;

namespace CodeGeneration.Operations
{

    internal class CGArithmeticOperation<T> : CGBinaryOperation<T> where T : ILCodeGenerator
    {

        
        public CGArithmeticOperation(VisitorILCodeGeneration<T> visitor, object obj, int indent, ArithmeticExpression node) : base(visitor, obj, indent, node)
        {
            LoadSecondOperand();
        }

        protected override sealed void LoadSecondOperand()
        {            
            if (String.IsNullOrEmpty(label_op2))
            {
                this.node.SecondOperand.Accept(this.visitor, this.obj);
                label_op2 = "tempvar_" + this.codeGenerator.NewLabel;
                if (IsInternallyAnObject(node.SecondOperand))
                    this.codeGenerator.WriteAuxiliarLocalVariable(this.indent, label_op2,"object");
                else
                    this.codeGenerator.WriteAuxiliarLocalVariable(this.indent, label_op2, this.node.SecondOperand.ILTypeExpression.ILType());
                this.codeGenerator.stloc(this.indent, label_op2);
            }
            else
                this.codeGenerator.ldloc(this.indent, label_op2);            
        }

        protected override void GenerateRightOperand(TypeExpression teLeft, UnionType teRight)
        {
            if (node is ArithmeticExpression)
            {
                label_result = "tempvar_" + this.codeGenerator.NewLabel;
                this.codeGenerator.WriteAuxiliarLocalVariable(this.indent, label_result, node.ExpressionType.ILType());
            }
            base.GenerateRightOperand(teLeft,teRight);
        }

        protected override UnionType GenerateAllUnionTypes()
        {
            UnionType unions = new UnionType();                        
            unions.AddType(IntType.Instance);
            unions.AddType(DoubleType.Instance);
            unions.AddType(CharType.Instance);
            if(((ArithmeticExpression)node).Operator == ArithmeticOperator.Plus)
                unions.AddType(StringType.Instance);
            return unions;
        }

        protected override TypeExpression MajorType(TypeExpression typeExpression1, TypeExpression typeExpression2)
        {
            return (TypeExpression)typeExpression1.AcceptOperation(new MajorTypeForArithMeticOperation(typeExpression2, (ArithmeticExpression)node), null);
        }

        protected override void GenerateOperator(BinaryExpression node, TypeExpression result)
        {                           
            ArithmeticOperator arithmeticOperator = ((ArithmeticExpression) node).Operator;
            bool isConcat = false;
            switch (arithmeticOperator)
            {
                case ArithmeticOperator.Minus:
                    this.codeGenerator.sub(this.indent);
                    break;
                case ArithmeticOperator.Plus:
                    if (TypeExpression.Is<StringType>(result))
                    {
                        isConcat = true;
                        this.codeGenerator.concat(this.indent);
                    }
                    else
                        this.codeGenerator.add(this.indent);
                    break;
                case ArithmeticOperator.Mult:
                    this.codeGenerator.mul(this.indent);
                    break;
                case ArithmeticOperator.Div:
                    this.codeGenerator.div(this.indent);
                    break;
                case ArithmeticOperator.Mod:
                    this.codeGenerator.rem(this.indent);
                    break;
                default: // Error 
                    this.codeGenerator.Comment("ERROR");
                    break;
            }            
            if (!isConcat && !IsValueType(node.ExpressionType) && !(node.ExpressionType is StringType))
                this.codeGenerator.Box(indent, result);           
            if (!string.IsNullOrEmpty(label_result))
            {                
                this.codeGenerator.stloc(this.indent, label_result);
            }
            if (result == null)
            {
                codeGenerator.WriteThrowNonSuitableObjectException(indent, StringType.Instance.FullName, arithmeticOperator.ToString());
            }
        }
    }
}