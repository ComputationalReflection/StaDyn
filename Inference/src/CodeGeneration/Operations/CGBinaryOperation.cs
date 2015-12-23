using System;
using System.Collections.Generic;
using CodeGeneration;
using TypeSystem.Operations;
using TypeSystem;
using ErrorManagement;
using AST;

namespace CodeGeneration.Operations {    

        abstract class CGBinaryOperation<T> : TypeSystemOperation where T : ILCodeGenerator {

        protected BinaryExpression node;

        protected VisitorILCodeGeneration<T> visitor;
     
        protected T codeGenerator;

        protected int indent;
   
        protected object obj;

        protected string label_result;
        protected string label_op2;
 
        protected CGBinaryOperation(VisitorILCodeGeneration<T> visitor, object obj, int indent, BinaryExpression node) {
            this.codeGenerator = visitor.codeGenerator;
            this.indent = indent;
            this.visitor = visitor;
            this.obj = obj;
            this.node = node;            
            this.node.FirstOperand.Accept(this.visitor, this.obj);                
        }

        protected abstract void GenerateOperator(BinaryExpression node, TypeExpression result);
        protected abstract UnionType GenerateAllUnionTypes();
        protected abstract TypeExpression MajorType(TypeExpression typeExpression1, TypeExpression typeExpression2);
        
        public override object ReportError(TypeExpression te)
        {
            ErrorManager.Instance.NotifyError(new CodeGenerationError("No se ha definido la operación solicitada"));
            return null;
        }

        public override object Exec(TypeExpression typeExpression, object arg)
        {
            if (typeExpression is UnionType)
                this.Exec(typeExpression as UnionType, arg);
            else if (typeExpression is TypeVariable)
                this.Exec(typeExpression as TypeVariable, arg);
            else if (typeExpression is PropertyType)
                this.Exec(((PropertyType)typeExpression).PropertyTypeExpression, arg);
            else if (typeExpression is FieldType)
            {
                FieldType fieldType = ((FieldType) typeExpression);
                if (fieldType.FieldTypeExpression is TypeVariable && ((TypeVariable)fieldType.FieldTypeExpression).Substitution != null && ((TypeVariable)fieldType.FieldTypeExpression).Substitution.IsValueType())
                {
                    UnionType union = new UnionType();
                    union.AddType(((TypeVariable)((FieldType)typeExpression).FieldTypeExpression).Substitution);
                    this.Exec(union, arg);
                }                
                else
                    this.Exec(((FieldType) typeExpression).FieldTypeExpression, arg);
            }
            else if (IsValueType(typeExpression) || TypeExpression.Is<StringType>(typeExpression))
                GenerateRightOperand(typeExpression, this.node.SecondOperand.ILTypeExpression);
            else if (typeExpression is NullType)
            {
                this.codeGenerator.ldnull(this.indent);
                GenerateRightOperand(typeExpression, this.node.SecondOperand.ILTypeExpression);
            }
            else if (typeExpression is UserType)
            {
                GenerateRightOperand(typeExpression, this.node.SecondOperand.ILTypeExpression);
            }
            return null;
        }

        public override object Exec(UnionType teLeft, object arg)
        {
            String finalLabel = this.codeGenerator.NewLabel;
            String nextLabel = "";

            List<TypeExpression> typeSet = new List<TypeExpression>();
            for (int i = 0; i < teLeft.TypeSet.Count; i++)
                typeSet.AddRange(GetTypes(teLeft.TypeSet[i]));
            typeSet = new List<TypeExpression>(new HashSet<TypeExpression>(typeSet));            
            if (typeSet.Count == 0 && !(this.node.SecondOperand.ExpressionType is NullType))
            {
                typeSet = new List<TypeExpression>(new HashSet<TypeExpression>(GenerateAllUnionTypes().TypeSet));
            }
            if (typeSet.Count > 0)
            {
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
                    GenerateRightOperand(typeSet[i], this.node.SecondOperand.ExpressionType);
                    if (i != typeSet.Count - 1)
                        this.codeGenerator.br(indent, finalLabel);
                }
                this.codeGenerator.WriteLabel(indent, finalLabel);
            }
            else //All types of the UnionType are TypeVariables without substitution, and default union is empty
            {
                GenerateRightOperand(TypeVariable.NewTypeVariable, this.node.SecondOperand.ExpressionType);
            }
            return null;
        }

        public override object Exec(TypeVariable teLeft, object arg)
        {
            //if (this.node.FirstOperand.ExpressionType is NullType || this.node.SecondOperand.ExpressionType is NullType)
            if (teLeft.Substitution == null)
                return this.Exec(GenerateAllUnionTypes(), arg);
            else
                return this.Exec(teLeft.Substitution, arg);
        }

        private void GenerateRightOperand(TypeExpression teLeft, TypeExpression teRight)
        {
            if (teRight is UnionType)
                this.GenerateRightOperand(teLeft, teRight as UnionType);
            else if (teRight is TypeVariable)
                this.GenerateRightOperand(teLeft, teRight as TypeVariable);
            else if (teRight is PropertyType)
                GenerateRightOperand(teLeft, ((PropertyType)teRight).PropertyTypeExpression);
            else if (teRight is FieldType)
            {
                FieldType fieldType = ((FieldType)teRight);
                if (fieldType.FieldTypeExpression is TypeVariable && ((TypeVariable)fieldType.FieldTypeExpression).Substitution != null && ((TypeVariable)fieldType.FieldTypeExpression).Substitution.IsValueType())                
                {
                    UnionType union = new UnionType();
                    union.AddType(((TypeVariable)((FieldType)teRight).FieldTypeExpression).Substitution);
                    GenerateRightOperand(teLeft, union);
                }
                else
                    GenerateRightOperand(teLeft, ((FieldType) teRight).FieldTypeExpression);
            }
            else if (IsValueType(teRight) || TypeExpression.Is<StringType>(teRight))
            {
                if (IsInternallyAnObject(node.FirstOperand))
                {
                    if (!(teLeft is StringType) && IsValueType(teRight))
                        this.codeGenerator.UnboxAny(indent, teLeft);
                }
                else
                {
                    if (!CheckUnBox(teLeft, teRight))
                        CheckBox(teLeft, teRight);
                }
                LoadSecondOperand();
                if (IsInternallyAnObject(node.SecondOperand))
                {
                    if (!(teRight is StringType) && IsValueType(teLeft))
                        this.codeGenerator.UnboxAny(indent, teRight);
                }
                else
                {
                    if (!CheckUnBox(teRight, teLeft))
                       CheckBox(teRight, teLeft);
                }
                GenerateOperator(node, MajorType(teLeft, teRight));                
            }
            else if(teRight is NullType)
            {
                this.codeGenerator.ldnull(this.indent);
                GenerateOperator(node, MajorType(teLeft, teRight));                
            }                
        }

        protected bool IsInternallyAnObject(Expression expression)
        {
            TypeExpression typeExpression = expression.ExpressionType;
            if (typeExpression is UnionType)
                return true;
            else if (typeExpression is NullType)
                return true;
            else if (typeExpression is TypeVariable)
            {
                if (((TypeVariable)typeExpression).Substitution == null)
                    return true;
                if (IsValueType(((TypeVariable)typeExpression).Substitution))
                    return false;
                if (IsValueType(expression.ILTypeExpression))
                    return false;
                return true;
            }
            else if (typeExpression is PropertyType)
                return !IsValueType(((PropertyType)typeExpression).PropertyTypeExpression);
            else if (typeExpression is FieldType && ((FieldType)typeExpression).FieldTypeExpression is TypeVariable)
                return true;
            else if (typeExpression is FieldType)
                return !IsValueType(((FieldType)typeExpression).FieldTypeExpression);
            else if (typeExpression is StringType)
                return true;
            return false;
        }

        protected virtual void GenerateRightOperand(TypeExpression teLeft, UnionType teRight)
        {
            List<TypeExpression> typeSet = new List<TypeExpression>();
            for (int i = 0; i < teRight.TypeSet.Count; i++)
                typeSet.AddRange(GetTypes(teRight.TypeSet[i]));
            typeSet = new List<TypeExpression>(new HashSet<TypeExpression>(typeSet));
            String finalLabel = this.codeGenerator.NewLabel;
            String nextLabel = "";
            if (typeSet.Count == 0)
                typeSet.AddRange(GenerateAllUnionTypes().TypeSet);
            for (int i = 0; i < typeSet.Count; i++)
            {
                if (!String.IsNullOrEmpty(nextLabel))
                    this.codeGenerator.WriteLabel(indent, nextLabel);
                if (i != typeSet.Count - 1)
                {
                    LoadSecondOperand();                    
                    nextLabel = this.codeGenerator.NewLabel;
                    this.codeGenerator.isinst(indent, typeSet[i]);
                    this.codeGenerator.brfalse(indent, nextLabel);
                }
                if ((node.FirstOperand.ExpressionType is UnionType || node.FirstOperand.ExpressionType is TypeVariable) && IsValueType(typeSet[i]) && IsInternallyAnObject(node.FirstOperand))
                    this.codeGenerator.UnboxAny(indent, teLeft);
                else if (this.node.FirstOperand.ExpressionType is FieldType && ((FieldType)this.node.FirstOperand.ExpressionType).MemberInfo.Modifiers.Contains(Modifier.Static) && ((FieldType)this.node.FirstOperand.ExpressionType).FieldTypeExpression is TypeVariable)                
                    this.codeGenerator.UnboxAny(indent, teLeft);
                else if (this.node.FirstOperand.ExpressionType is FieldType && ((FieldType)this.node.FirstOperand.ExpressionType).FieldTypeExpression is TypeVariable && node.SecondOperand.ExpressionType.IsValueType())
                    this.codeGenerator.UnboxAny(indent, teLeft);                
                else if (this.node.FirstOperand.ExpressionType is FieldType && ((FieldType)this.node.FirstOperand.ExpressionType).FieldTypeExpression is TypeVariable  &&  ((TypeVariable)((FieldType)this.node.FirstOperand.ExpressionType).FieldTypeExpression).Substitution == null)
                    this.codeGenerator.UnboxAny(indent, teLeft);                
                else if (IsValueType(node.FirstOperand.ExpressionType) && typeSet[i] is StringType)
                    this.codeGenerator.Box(indent, teLeft);
                LoadSecondOperand();                 
                if (!(typeSet[i] is StringType) && !(teLeft is StringType))
                    this.codeGenerator.UnboxAny(this.indent, typeSet[i]);
                GenerateOperator(node, MajorType(teLeft, typeSet[i]));
                if (i != typeSet.Count - 1)
                    this.codeGenerator.br(indent, finalLabel);
            }
            this.codeGenerator.WriteLabel(indent, finalLabel);
            if (!String.IsNullOrEmpty(label_result))
                this.codeGenerator.ldloc(this.indent, label_result);
        }


        protected virtual void LoadSecondOperand()
        {
            this.node.SecondOperand.Accept(this.visitor, this.obj);            
        }


        protected List<TypeExpression> GetTypes(TypeExpression typeExpression, List<TypeExpression> evaluated = null)
        {
            List<TypeExpression> typeSet = new List<TypeExpression>();
            if(evaluated == null)
                evaluated = new List<TypeExpression>();
            if (evaluated.Contains(typeExpression))
                return typeSet;
            else
                evaluated.Add(typeExpression);
            if (IsValueType(typeExpression) || typeExpression is StringType)
            {
                if(typeExpression is TypeVariable)
                    typeSet.Add(((TypeVariable) typeExpression).Substitution);
                else
                    typeSet.Add(typeExpression);
            }
            else if (typeExpression is TypeVariable)
            {
                if (((TypeVariable) typeExpression).Substitution != null)
                    typeSet.AddRange(GetTypes(((TypeVariable) typeExpression).Substitution, evaluated));
            }
            else if (typeExpression is UnionType)
            {
                UnionType union = typeExpression as UnionType;
                foreach (var expression in union.TypeSet)
                    typeSet.AddRange(GetTypes(expression, evaluated));
            }
            else if (typeExpression is FieldType)
            {
                FieldType fieldType = typeExpression as FieldType;
                typeSet.AddRange(GetTypes(fieldType.FieldTypeExpression, evaluated));
            }
            else if (typeExpression is PropertyType)
            {
                PropertyType propertyType = typeExpression as PropertyType;
                typeSet.AddRange(GetTypes(propertyType.PropertyTypeExpression, evaluated));
            }
            return typeSet;
        }

        private void GenerateRightOperand(TypeExpression teLeft, TypeVariable teRight)
        {
            if (teRight.Substitution == null)
                this.GenerateRightOperand(teLeft, GenerateAllUnionTypes());
            else
                this.GenerateRightOperand(teLeft, teRight.Substitution);
        }

        protected bool CheckUnBox(TypeExpression op1, TypeExpression op2)
        {
            if (op1 is TypeVariable || op1 is UnionType || op1.HasTypeVariables())
            {
                if (IsValueType(op2) || op2 is TypeVariable || op2 is UnionType)
                {
                    this.codeGenerator.UnboxAny(indent, op1);
                    return true;
                }
            }
            return false;
        }

        protected bool CheckBox(TypeExpression op1, TypeExpression op2)
        {
            if (IsValueType(op1))
            {
                if (op2 is StringType)
                {
                    this.codeGenerator.Box(indent, op1);
                    return true;
                }
            }
            return false;
        }

        protected bool IsValueType(TypeExpression exp)
        {
            if (exp is BoolType)
                return true;
            if (exp is CharType)
                return true;
            if (exp is IntType)
                return true;
            if (exp is DoubleType)
                return true;
            if (exp is TypeVariable)
                return IsValueType(((TypeVariable) exp).Substitution);
            return false;
        }
    }
}