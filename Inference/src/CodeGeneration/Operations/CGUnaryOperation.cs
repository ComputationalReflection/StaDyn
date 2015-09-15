using System;
using CodeGeneration;
using TypeSystem.Operations;
using TypeSystem;
using AST;
using System.Collections.Generic;

namespace CodeGeneration.Operations {

    internal class CGUnaryOperation<T> : TypeSystemOperation where T : ILCodeGenerator 
    {
        
        protected UnaryExpression node;

        protected VisitorILCodeGeneration<T> visitor;
       
        protected T codeGenerator;
       
        protected int indent;
       
        protected object obj;

        public CGUnaryOperation(VisitorILCodeGeneration<T> visitor, object obj, int indent, UnaryExpression node)
        {
            this.codeGenerator = visitor.codeGenerator;
            this.visitor = visitor;
            this.obj = obj;
            this.indent = indent;
            this.node = node;            
        }
       
        public override object Exec(TypeExpression typeExpression, object arg)
        {            
            if (typeExpression is UnionType)
                this.Exec(typeExpression as UnionType, arg);
            if (typeExpression is TypeVariable)
                this.Exec(typeExpression as TypeVariable, arg);
            if (typeExpression is PropertyType)
                this.Exec(((PropertyType)typeExpression).PropertyTypeExpression, arg);
            else if (typeExpression is FieldType)
                this.Exec(((FieldType)typeExpression).FieldTypeExpression, arg);
            else if (IsValueType(typeExpression))
            {
                node.Operand.Accept(visitor, obj);
                GenerateOperator();
            }
            return null;
        }

        public override object Exec(TypeVariable teLeft, object arg)
        {
            if (teLeft.Substitution == null)
                return this.Exec(GenerateAllUnionTypes(), arg);
            else
                return this.Exec(teLeft.Substitution, arg);
        }

        public override object Exec(UnionType teLeft, object arg)
        {
            node.Operand.Accept(visitor, obj);
            if (node.Operator == UnaryOperator.Plus || node.Operator == UnaryOperator.Minus)
            {

                String finalLabel = this.codeGenerator.NewLabel;
                String nextLabel = "";

                List<TypeExpression> typeSet = new List<TypeExpression>();
                for (int i = 0; i < teLeft.TypeSet.Count; i++)
                    typeSet.AddRange(GetTypes(teLeft.TypeSet[i]));
                typeSet = new List<TypeExpression>(new HashSet<TypeExpression>(typeSet));

                for (int i = 0; i < typeSet.Count; i++)
                {
                    if (!String.IsNullOrEmpty(nextLabel))
                        this.codeGenerator.WriteLabel(indent, nextLabel);
                    if (i != typeSet.Count - 1)
                    {
                        nextLabel = this.codeGenerator.NewLabel;
                        this.codeGenerator.dup(indent);
                        this.codeGenerator.isinst(indent, typeSet[i]);
                        this.codeGenerator.brfalse(indent, nextLabel);
                    }
                    this.codeGenerator.UnboxAny(indent, typeSet[i]);
                    GenerateOperator();
                    this.codeGenerator.Box(indent, typeSet[i]);
                    if (i != typeSet.Count - 1)
                        this.codeGenerator.br(indent, finalLabel);
                }
                this.codeGenerator.WriteLabel(indent, finalLabel);
            }
            else
                GenerateOperator();
            return null;
        }

        private UnionType GenerateAllUnionTypes()
        {
            UnionType unions = new UnionType();
            if (node.Operator == UnaryOperator.Plus || node.Operator == UnaryOperator.Minus || node.Operator == UnaryOperator.BitwiseNot)
            {
                unions.AddType(CharType.Instance);
                unions.AddType(IntType.Instance);
                unions.AddType(DoubleType.Instance);
            }
            else if (node.Operator == UnaryOperator.Not)
                unions.AddType(BoolType.Instance);
            return unions;
        }

        private void GenerateOperator()
        {
            switch (node.Operator)
            {
                case UnaryOperator.Not:                    
                    this.codeGenerator.ldc(this.indent, false);
                    this.codeGenerator.ceq(this.indent);
                    break;
                case UnaryOperator.BitwiseNot:                    
                    this.codeGenerator.not(this.indent);
                    break;
                case UnaryOperator.Minus:                    
                    this.codeGenerator.neg(this.indent);
                    break;
                case UnaryOperator.Plus:                    
                    break;
                default:
                    break;
            }            
        }

        private List<TypeExpression> GetTypes(TypeExpression typeExpression)
        {
            List<TypeExpression> typeSet = new List<TypeExpression>();
            if (IsValueType(typeExpression) || typeExpression is StringType)
                typeSet.Add(typeExpression);
            else if (typeExpression is TypeVariable)
            {
                if (((TypeVariable)typeExpression).Substitution != null)
                    typeSet.AddRange(GetTypes(((TypeVariable)typeExpression).Substitution));
            }
            else if (typeExpression is UnionType)
            {
                UnionType union = typeExpression as UnionType;
                foreach (var expression in union.TypeSet)
                    typeSet.AddRange(GetTypes(expression));
            }
            return typeSet;
        }

        private bool IsValueType(TypeExpression exp)
        {
            if (exp is BoolType)
                return true;
            if (exp is CharType)
                return true;
            if (exp is IntType)
                return true;
            if (exp is DoubleType)
                return true;
            return false;
        }
    }
}