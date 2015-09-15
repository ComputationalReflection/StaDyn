using CodeGeneration;
using TypeSystem;
using AST;

namespace CodeGeneration.Operations {

    internal class CGBitwiseOperation<T> : CGBinaryOperation<T> where T : ILCodeGenerator
    {

        public CGBitwiseOperation(VisitorILCodeGeneration<T> visitor, object obj, int indent, BitwiseExpression node) : base(visitor, obj, indent, node) { }


        protected override UnionType GenerateAllUnionTypes()
        {
            UnionType unions = new UnionType();                        
            unions.AddType(IntType.Instance);            
            return unions;
        }

        protected override TypeExpression MajorType(TypeExpression typeExpression1, TypeExpression typeExpression2)
        {
            return typeExpression1;
        }

        protected override void GenerateOperator(BinaryExpression node, TypeExpression result)
        {
            BitwiseOperator bitwiseOperator = ((BitwiseExpression)node).Operator;
            switch (bitwiseOperator)
            {
                case BitwiseOperator.BitwiseOr:
                    this.codeGenerator.or(this.indent);
                    break;
                case BitwiseOperator.BitwiseAnd:
                    this.codeGenerator.and(this.indent);
                    break;
                case BitwiseOperator.BitwiseXOr:
                    this.codeGenerator.xor(this.indent);
                    break;
                case BitwiseOperator.ShiftLeft:
                    this.codeGenerator.shl(this.indent);
                    break;
                case BitwiseOperator.ShiftRight:
                    this.codeGenerator.shr(this.indent);
                    break;
                default:
                    break;
            }

            if (!IsValueType(node.ExpressionType))
                this.codeGenerator.Box(indent, IntType.Instance);
        }
    }
}