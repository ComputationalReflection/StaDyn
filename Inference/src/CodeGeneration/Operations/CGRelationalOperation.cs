using CodeGeneration;
using TypeSystem;
using AST;

namespace CodeGeneration.Operations {

    internal class CGRelationalOperation<T> : CGBinaryOperation<T> where T : ILCodeGenerator
    {

        public CGRelationalOperation(VisitorILCodeGeneration<T> visitor, object obj, int indent, RelationalExpression node) : base(visitor, obj, indent, node) { }


        protected override UnionType GenerateAllUnionTypes()
        {
            
            UnionType unions = new UnionType();
            if (!(node.FirstOperand.ExpressionType is NullType) && !(node.SecondOperand.ExpressionType is NullType))
            {                
                unions.AddType(IntType.Instance);
                unions.AddType(DoubleType.Instance);
                unions.AddType(CharType.Instance);
            }
            return unions;
        }

        protected override TypeExpression MajorType(TypeExpression typeExpression1, TypeExpression typeExpression2)
        {
            return null;
        }

        protected override void GenerateOperator(BinaryExpression node, TypeExpression result)
        {
            RelationalOperator relationalOperator = ((RelationalExpression)node).Operator;            
            switch (relationalOperator)
            {
                case RelationalOperator.NotEqual:
                    this.codeGenerator.ceq(this.indent);
                    this.codeGenerator.ldc(this.indent, false);
                    this.codeGenerator.ceq(this.indent);
                    break;
                case RelationalOperator.Equal:
                    this.codeGenerator.ceq(this.indent);
                    break;
                case RelationalOperator.LessThan:
                    this.codeGenerator.clt(this.indent);
                    break;
                case RelationalOperator.GreaterThan:
                    this.codeGenerator.cgt(this.indent);
                    break;
                case RelationalOperator.LessThanOrEqual:
                    this.codeGenerator.cgt(this.indent);
                    this.codeGenerator.ldc(this.indent, false);
                    this.codeGenerator.ceq(this.indent);
                    break;
                case RelationalOperator.GreaterThanOrEqual:
                    this.codeGenerator.clt(this.indent);
                    this.codeGenerator.ldc(this.indent, false);
                    this.codeGenerator.ceq(this.indent);
                    break;                
                default: // Error 
                    this.codeGenerator.Comment("ERROR");
                    break;
            }

           // if (!IsValueType(node.ExpressionType) && !(node.ExpressionType is StringType))
             //   this.codeGenerator.Box(indent, BoolType.Instance);
        }
    }
}