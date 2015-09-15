///////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: VisitorCodeGeneration2.cs                                            //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
//         Daniel Zapico Rodríguez - daniel.zapico.rodriguez@gmail.com        //
// Description:                                                               //
//    This class walks the AST to obtain the field and localinit directives.  //
//    Inheritance: VisitorAdapter                                             //
//    Implements Visitor pattern [Concrete Visitor].                          //
// -------------------------------------------------------------------------- //
// Create date: 11-06-2007                                                    //
// Modification date: 11-06-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;

using AST;
using TypeSystem;

namespace CodeGeneration {
    /// <summary>
    /// This class walks the AST to obtain the field and     localinit directives.
    /// </summary>
    /// <remarks>
    /// Inheritance: VisitorAdapter.
    /// Implements Visitor pattern [Concrete Visitor].
    /// </remarks>
    class VisitorCodeGeneration2 : VisitorCodeGenerationBase {
        #region Fields

        /// <summary>
        /// Stores the field declarations. Index 0: non static fields. Index1: static fields.
        /// </summary>
        private List<FieldDefinition>[] fields;

        /// <summary>
        /// Stores the local variables declarations.
        /// </summary>
        private List<IdDeclaration> decls;

        /// <summary>
        /// Identifier used to create auxiliar array variables.
        /// </summary>
        private const string auxiliarVar = "v_array_temp_";
        /// <summary>
        /// 
        /// To create auxiliar variables of value types in IL
        /// </summary>
        private const string auxiliarValue = "V_"; //this is the syntax of VS

        /// <summary>
        /// Number to append as suffix to create the identifier of auxiliar variables.
        /// </summary>
        private static int currentAuxiliarSuffix = 0;

        public int CurrentAuxiliarSuffix {
            get { return currentAuxiliarSuffix; }
            set { currentAuxiliarSuffix = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of VisitorCodeGeneration2
        /// </summary>
        public VisitorCodeGeneration2() {
            this.fields = new List<FieldDefinition>[2];
            this.fields[0] = new List<FieldDefinition>();
            this.fields[1] = new List<FieldDefinition>();
            this.decls = new List<IdDeclaration>();
            TemporalVariablesTable.Instance.Clear();
        }

        #endregion

        // Declarations

        #region Visit(ClassDefinition node, Object obj)

        public override Object Visit(ClassDefinition node, Object obj) {
            for (int i = 0; i < node.MemberCount; i++) {
                node.GetMemberElement(i).Accept(this, obj);
            }
            return this.fields;
        }

        #endregion

        //#region Visit(MethodDefinition node, Object obj)

        public override Object Visit(InvocationExpression node, Object obj) {
            node.Identifier.Accept(this, null);
            if (node.FrozenTypeExpression.IsValueType()) {
                SingleIdentifierExpression sid = new SingleIdentifierExpression(auxiliarValue + this.CurrentAuxiliarSuffix++, node.Location);
                IdDeclaration id = new IdDeclaration(sid, node.ILTypeExpression.ILType(), node.Location);
                id.TypeExpr = node.ExpressionType ?? node.ILTypeExpression;
                if (TemporalVariablesTable.Instance.SearchId(id.FullName) == null) { // it's a new inserction
                    TemporalVariablesTable.Instance.Insert(id.FullName, id.ILName);
                    decls.Add(id);
                }
            }
            node.Arguments.Accept(this, obj);
            return null;
        }
        #region Visit(CastExpression node, Object obj)

        public override Object Visit(CastExpression node, Object obj) {
            node.Expression.Accept(this, obj);

            if (node.Expression.ExpressionType.IsValueType()) {
                SingleIdentifierExpression sId = new SingleIdentifierExpression(auxiliarValue + this.CurrentAuxiliarSuffix++, node.Location);
                IdDeclaration id = new IdDeclaration(sId, node.Expression.ExpressionType.ILType(), node.Location);
                id.TypeExpr = node.Expression.ExpressionType;
                // temporal variables of the same type are forbidden
                if (TemporalVariablesTable.Instance.SearchId(id.FullName) == null) { // it's a new inserction
                    TemporalVariablesTable.Instance.Insert(id.FullName, id.ILName);
                    decls.Add(id);
                }
            }
            return null;
        }
        #endregion
        public override Object Visit(FieldAccessExpression node, Object obj) {
            if (node.Expression.ExpressionType.IsValueType()) {
                SingleIdentifierExpression sId = new SingleIdentifierExpression(auxiliarValue + this.CurrentAuxiliarSuffix++, node.Location);
                IdDeclaration id = new IdDeclaration(sId, node.Expression.ExpressionType.ILType(), node.Location);
                id.TypeExpr = node.Expression.ExpressionType;
                // temporal variables of the same type are forbidden
                if (TemporalVariablesTable.Instance.SearchId(id.FullName) == null) { // it's a new inserction
                    TemporalVariablesTable.Instance.Insert(id.FullName, id.ILName);
                    decls.Add(id);
                }
            }
            node.FieldName.Accept(this, obj);
            return null;
        }


        public override Object Visit(MethodDefinition node, Object obj) {
            CurrentAuxiliarSuffix = 0;
            node.Body.Accept(this, obj);
            return this.decls;
        }


        #region Visit(ConstructorDefinition node, Object obj)

        public override Object Visit(ConstructorDefinition node, Object obj) {
            this.CurrentAuxiliarSuffix = 0;
            node.Body.Accept(this, obj);
            return this.decls;
        }

        #endregion

        // Fields

        #region Visit(FieldDefinition node, Object obj)

        public override Object Visit(FieldDefinition node, Object obj) {
            node.Init.Accept(this, obj);
            if ((((FieldType)node.TypeExpr).MemberInfo.ModifierMask & Modifier.Static) != 0)
                this.fields[1].Add(node);
            else
                this.fields[0].Add(node);
            return null;
        }



        //public override Object Visit(ConstantDefinition node, Object obj) {
        //    Object obj = node.Init.Accept(this, obj);
        //    if (cnode is node.
        //    if ((((FieldType)node.TypeExpr).MemberInfo.ModifierMask & Modifier.Static) != 0)
        //        this.fields[1].Add(node);
        //}

        #endregion

        // Local Variables

        #region Visit(IdDeclaration node, Object obj)

        public override Object Visit(IdDeclaration node, Object obj) {
            this.decls.Add(node);
            return null;
        }

        #endregion

        #region Visit(Definition node, Object obj)

        public override Object Visit(Definition node, Object obj) {
            this.decls.Add(node);
            node.Init.Accept(this, obj);
            return null;
        }

        #endregion

        // Expressions

        #region Visit(NewArrayExpression node, Object obj)

        public override Object Visit(NewArrayExpression node, Object obj) {
            string tmpName = auxiliarVar + CurrentAuxiliarSuffix++;
            SingleIdentifierExpression sId = new SingleIdentifierExpression(tmpName, node.Location);
            IdDeclaration id = new IdDeclaration(sId, -1, node.TypeInfo, node.Location);
            id.TypeExpr = node.ILTypeExpression;

            if (TemporalVariablesTable.Instance.SearchId(id.FullName) == null) { // it's a new inserction
                TemporalVariablesTable.Instance.Insert(id.FullName, id.ILName);
            }
            node.Identifier = id.Identifier;
            //decls.Add(id);
            this.decls.Add(id);
            if (node.Size != null)
                node.Size.Accept(this, obj);
            if (node.Init != null)
                node.Init.Accept(this, obj);
            return this.decls;
        }

        #endregion

        // Literals

        #region Visit(BoolLiteralExpression node, Object obj)

        public override Object Visit(BoolLiteralExpression node, Object obj) {
            return Convert.ToString(node.BoolValue);
        }

        #endregion

        #region Visit(CharLiteralExpression node, Object obj)

        public override Object Visit(CharLiteralExpression node, Object obj) {
            return Convert.ToString(Convert.ToUInt16(node.CharValue));
        }

        #endregion

        #region Visit(DoubleLiteralExpression node, Object obj)

        public override Object Visit(DoubleLiteralExpression node, Object obj) {
            return node.ILValue;
        }

        #endregion

        #region Visit(IntLiteralExpression node, Object obj)

        public override Object Visit(IntLiteralExpression node, Object obj) {
            return node.ILValue;
        }

        #endregion

        #region Visit(StringLiteralExpression node, Object obj)

        public override Object Visit(StringLiteralExpression node, Object obj) {
            return node.StringValue;
        }

        #endregion

        public override void AddExceptionCode() {
            //this.codeGenerator.WriteCodeOfExceptions();
            System.Diagnostics.Debug.Assert(false, "no debería llegar aqú");
        }
        public override void Close() {
            System.Diagnostics.Debug.Assert(false, "no debería llegar aqú");
        }
    }
}
    