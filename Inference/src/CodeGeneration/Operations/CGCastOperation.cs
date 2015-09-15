using System;
using System.Collections.Generic;
using CodeGeneration;
using TypeSystem.Operations;
using TypeSystem;
using ErrorManagement;

namespace CodeGeneration.Operations {

    /// <summary>
       ///  It typechecks the runtime arguments, embeded in a method call, with the parametes of this method.
       ///  </summary>       
    internal class CGCastOperation <T>:TypeSystemOperation where T: ILCodeGenerator {

        /// <summary>
        /// streamto to write to
        /// </summary>
        private T codeGenerator;
        /// <summary>
        /// indentation to use
        /// </summary>
        private int indent;

 
        public CGCastOperation(T codeGenerator, int indent) {
            this.codeGenerator = codeGenerator;
            this.indent = indent;
          }

        public override object Exec(ClassType ct, object arg) {
            this.codeGenerator.castclass(this.indent, ct);
            return null;
        }
        public override object Exec(IntType it, object arg) {
            this.codeGenerator.convToInt(this.indent);            
            return null;
        }
        public override object Exec(CharType ct, object arg)
        {
            this.codeGenerator.convToChar(this.indent);
            return null;
        }

        public override object Exec(DoubleType dt, object arg) {
            this.codeGenerator.convToDouble(this.indent);
            return null;
        }

        public override object Exec(TypeVariable tv, object arg)
        {
            if (arg == null)
                return this.ReportError(tv);
            if (tv.Substitution == null)
                return this.Exec(GenerateAllUnionTypes(), arg);
            return this.Exec(tv.Substitution, arg);
        }

        public override object Exec(TypeExpression te, object arg)
        {
            if (arg == null)
                return this.ReportError(te);
            if (te is UnionType)
                this.Exec(te as UnionType, arg);
            else if (te is TypeVariable)
                this.Exec(te as TypeVariable, arg);
            else if (te is PropertyType)
                this.Exec(((PropertyType)te).PropertyTypeExpression, arg);
            else if (te is FieldType)
                this.Exec(((FieldType)te).FieldTypeExpression, arg);
            else if (te is StringType)
                this.codeGenerator.castclass(indent, StringType.Instance);
            else if (IsValueType(te))
            {
                this.codeGenerator.UnboxAny(indent, te);
                ((TypeExpression) arg).AcceptOperation(this, null);
            }
           
            return null;
        }

        private static UnionType GenerateAllUnionTypes()
        {
            UnionType unions = new UnionType();
            unions.AddType(CharType.Instance);
            unions.AddType(IntType.Instance);
            unions.AddType(DoubleType.Instance);
            return unions;
        }

        public override object Exec(UnionType tv, object arg)
        {
            if (arg == null)
                return this.ReportError(tv);
            List<TypeExpression> typeSet = new List<TypeExpression>();
            for (int i = 0; i < tv.TypeSet.Count; i++)
                typeSet.AddRange(GetTypes(tv.TypeSet[i]));
            typeSet = new List<TypeExpression>(new HashSet<TypeExpression>(typeSet));

            String finalLabel = this.codeGenerator.NewLabel;
            String nextLabel = "";
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
                ((TypeExpression)arg).AcceptOperation(this, arg);                
                if (i != typeSet.Count - 1)
                    this.codeGenerator.br(indent, finalLabel);
            }
            this.codeGenerator.WriteLabel(indent, finalLabel);
            return null;
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

        public override object ReportError(TypeExpression tE)
        {
            ErrorManager.Instance.NotifyError(new CodeGenerationError("No se ha definido la operación solicitada"));
            return null;
        }

        #region Static methods, Candidates to implement as Operations

        public static bool IsValueType(TypeExpression exp)
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
        
        public static bool ChechBox(TypeExpression te)
        {
            if ((te is TypeVariable) && ((TypeVariable)te).Substitution == null)
                return true;
            if ((te is TypeVariable) && ((TypeVariable)te).Substitution is UnionType)
                return true;
            if (te is PropertyType)
                return ChechBox(((PropertyType)te).PropertyTypeExpression);
            if (te is FieldType)
                return ChechBox(((FieldType)te).FieldTypeExpression);            
            return false;
        }

       public static bool IsInternallyAnObject(TypeExpression typeExpression)
        {
            if (typeExpression is UnionType)
                return true;
            else if (typeExpression is TypeVariable)
            {
                if (((TypeVariable)typeExpression).Substitution == null)
                    return true;
                else
                    return !IsValueType(((TypeVariable)typeExpression).Substitution);
            }
            else if (typeExpression is PropertyType)
                return !IsValueType(((PropertyType)typeExpression).PropertyTypeExpression);
            else if (typeExpression is FieldType)
                return !IsValueType(((FieldType)typeExpression).FieldTypeExpression);
            else if (typeExpression is StringType)
                return true;
            return false;
        }
        #endregion
    }
}