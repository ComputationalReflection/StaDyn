////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: VisitorAdapter.cs                                                    //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Abstract class to define different visits over the abstract syntax tree //
// It makes a deep walker.                                                    //
//    Inheritance: Visitor                                                    //
//    Implements Visitor pattern [Visitor].                                   //
// -------------------------------------------------------------------------- //
// Create date: 11-12-2006                                                    //
// Modification date: 30-07-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Text;

using AST;

namespace Tools {
    /// <summary>
    /// Abstract class to define different visits over the abstract syntax tree.
    /// It makes a deep walker.
    /// </summary>
    /// <remarks>
    /// Inheritance: Visitor.
    /// Implements Visitor pattern [Visitor].
    /// </remarks>
    abstract public class VisitorAdapter : Visitor {
        #region Fields

        /// <summary>
        /// Name of the current file.
        /// </summary>
        protected string currentFile;

        #endregion

        #region Visit(SourceFile node, Object obj)

        public override Object Visit(SourceFile node, Object obj) {
            this.currentFile = node.Location.FileName;

            foreach (string key in node.Namespacekeys) {
                int count = node.GetNamespaceDefinitionCount(key);
                for (int i = 0; i < count; i++)
                    node.GetNamespaceDeclarationElement(key, i).Accept(this, obj);
            }

            for (int i = 0; i < node.DeclarationCount; i++)
                node.GetDeclarationElement(i).Accept(this, obj);

            return null;
        }

        #endregion

        #region Visit(Namespace node, Object obj)

        public override Object Visit(Namespace node, Object obj) {
            node.Identifier.Accept(this, obj);

            for (int i = 0; i < node.NamespaceMembersCount; i++)
                node.GetDeclarationElement(i).Accept(this, obj);

            return null;
        }

        #endregion

        #region Visit(DeclarationSet node, Object obj)

        public override Object Visit(DeclarationSet node, Object obj) {
            for (int i = 0; i < node.Count; i++)
                node.GetDeclarationElement(i).Accept(this, obj);

            return null;
        }

        #endregion

        #region Visit(FieldDeclarationSet node, Object obj)

        public override Object Visit(FieldDeclarationSet node, Object obj) {
            return null;
        }

        #endregion

        #region Visit(IdDeclaration node, Object obj)

        public override Object Visit(IdDeclaration node, Object obj) {
            return null;
        }

        #endregion

        #region Visit(Definition node, Object obj)

        public override Object Visit(Definition node, Object obj) {
            return node.Init.Accept(this, obj);
        }

        #endregion

        #region Visit(ConstantDefinition node, Object obj)

        public override Object Visit(ConstantDefinition node, Object obj) {
            return node.Init.Accept(this, obj);
        }

        #endregion

        #region Visit(PropertyDefinition node, Object obj)

        public override Object Visit(PropertyDefinition node, Object obj) {
            if (node.GetBlock != null)
                node.GetBlock.Accept(this, obj);

            if (node.SetBlock != null)
                node.SetBlock.Accept(this, obj);

            return null;
        }

        #endregion

        #region Visit(ClassDefinition node, Object obj)

        public override Object Visit(ClassDefinition node, Object obj) {
            for (int i = 0; i < node.MemberCount; i++)
                node.GetMemberElement(i).Accept(this, obj);

            return null;
        }

        #endregion

        #region Visit(InterfaceDefinition node, Object obj)

        public override Object Visit(InterfaceDefinition node, Object obj) {
            for (int i = 0; i < node.MemberCount; i++)
                node.GetMemberElement(i).Accept(this, obj);

            return null;
        }

        #endregion

        #region Visit(ConstructorDefinition node, Object obj)

        public override Object Visit(ConstructorDefinition node, Object obj) {
            if (node.Initialization != null)
                node.Initialization.Accept(this, obj);

            return node.Body.Accept(this, obj);
        }

        #endregion

        #region Visit(FieldDeclaration node, Object obj)

        public override Object Visit(FieldDeclaration node, Object obj) {

            return null;
        }

        #endregion

        #region Visit(FieldDefinition node, Object obj)

        public override Object Visit(FieldDefinition node, Object obj) {

            return node.Init.Accept(this, obj);
        }

        #endregion

        #region Visit(ConstantFieldDefinition node, Object obj)

        public override Object Visit(ConstantFieldDefinition node, Object obj) {

            return node.Init.Accept(this, obj);
        }

        #endregion

        #region Visit(MethodDeclaration node, Object obj)

        public override Object Visit(MethodDeclaration node, Object obj) {

            return null;
        }

        #endregion

        #region Visit(MethodDefinition node, Object obj)

        public override Object Visit(MethodDefinition node, Object obj) {

            return node.Body.Accept(this, obj);
        }

        #endregion

        // Expressions
        #region Visit(ArgumentExpression node, Object obj)

        public override Object Visit(ArgumentExpression node, Object obj) {

            return node.Argument.Accept(this, obj);
        }

        #endregion

        #region Visit(ArithmeticExpression node, Object obj)

        public override Object Visit(ArithmeticExpression node, Object obj) {
            node.FirstOperand.Accept(this, obj);
            node.SecondOperand.Accept(this, obj);

            return null;
        }

        #endregion

        #region Visit(ArrayAccessExpression node, Object obj)

        public override Object Visit(ArrayAccessExpression node, Object obj) {
            node.FirstOperand.Accept(this, obj);
            node.SecondOperand.Accept(this, obj);

            return null;
        }

        #endregion

        #region Visit(AssignmentExpression node, Object obj)

        public override Object Visit(AssignmentExpression node, Object obj) {
            node.FirstOperand.Accept(this, obj);
            node.SecondOperand.Accept(this, obj);

            if (node.MoveStat != null)
                node.MoveStat.Accept(this, obj);

            return null;
        }

        #endregion

        #region Visit(BaseCallExpression node, Object obj)

        public override Object Visit(BaseCallExpression node, Object obj) {

            return node.Arguments.Accept(this, obj);
        }

        #endregion

        #region Visit(BaseExpression node, Object obj)

        public override Object Visit(BaseExpression node, Object obj) {

            return null;
        }

        #endregion

        #region Visit(BinaryExpression node, Object obj)

        public override Object Visit(BinaryExpression node, Object obj) {
            node.FirstOperand.Accept(this, obj);
            node.SecondOperand.Accept(this, obj);

            return null;
        }

        #endregion

        #region Visit(BitwiseExpression node, Object obj)

        public override Object Visit(BitwiseExpression node, Object obj) {
            node.FirstOperand.Accept(this, obj);
            node.SecondOperand.Accept(this, obj);

            return null;
        }

        #endregion

        #region Visit(BoolLiteralExpression node, Object obj)

        public override Object Visit(BoolLiteralExpression node, Object obj) {

            return null;
        }

        #endregion

        #region Visit(CastExpression node, Object obj)

        public override Object Visit(CastExpression node, Object obj) {

            return node.Expression.Accept(this, obj);
        }

        #endregion

        #region Visit(CharLiteralExpression node, Object obj)

        public override Object Visit(CharLiteralExpression node, Object obj) {

            return null;
        }

        #endregion

        #region Visit(CompoundExpression node, Object obj)

        public override Object Visit(CompoundExpression node, Object obj) {
            for (int i = 0; i < node.ExpressionCount; i++)
                node.GetExpressionElement(i).Accept(this, obj);

            return null;
        }

        #endregion

        #region Visit(DoubleLiteralExpression node, Object obj)

        public override Object Visit(DoubleLiteralExpression node, Object obj) {

            return null;
        }

        #endregion

        #region Visit(FieldAccessExpression node, Object obj)

        public override Object Visit(FieldAccessExpression node, Object obj) {
            node.Expression.Accept(this, obj);
            node.FieldName.Accept(this, obj);

            return null;
        }

        #endregion

        #region Visit(IntLiteralExpression node, Object obj)

        public override Object Visit(IntLiteralExpression node, Object obj) {

            return null;
        }

        #endregion

        #region Visit(InvocationExpression node, Object obj)

        public override Object Visit(InvocationExpression node, Object obj) {
            node.Identifier.Accept(this, obj);
            node.Arguments.Accept(this, obj);

            return null;
        }

        #endregion

        #region Visit(IsExpression node, Object obj)

        public override Object Visit(IsExpression node, Object obj) {

            return node.Expression.Accept(this, obj);
        }

        #endregion

        #region Visit(LogicalExpression node, Object obj)

        public override Object Visit(LogicalExpression node, Object obj) {
            node.FirstOperand.Accept(this, obj);
            node.SecondOperand.Accept(this, obj);

            return null;
        }

        #endregion

        #region Visit(NewArrayExpression node, Object obj)

        public override Object Visit(NewArrayExpression node, Object obj) {
            if (node.Size != null)
                node.Size.Accept(this, obj);

            if (node.Init != null)
                node.Init.Accept(this, obj);

            return null;
        }

        #endregion

        #region Visit(NewExpression node, Object obj)

        public override Object Visit(NewExpression node, Object obj) {

            return node.Arguments.Accept(this, obj);
        }

        #endregion

        #region Visit(NullExpression node, Object obj)

        public override Object Visit(NullExpression node, Object obj) {

            return null;
        }

        #endregion

        #region Visit(QualifiedIdentifierExpression node, Object obj)

        public override Object Visit(QualifiedIdentifierExpression node, Object obj) {
            node.IdName.Accept(this, obj);
            node.IdExpression.Accept(this, obj);

            return null;
        }

        #endregion

        #region Visit(RelationalExpression node, Object obj)

        public override Object Visit(RelationalExpression node, Object obj) {
            node.FirstOperand.Accept(this, obj);
            node.SecondOperand.Accept(this, obj);

            return null;
        }

        #endregion

        #region Visit(SingleIdentifierExpression node, Object obj)

        public override Object Visit(SingleIdentifierExpression node, Object obj) {

            return null;
        }

        #endregion

        #region Visit(StringLiteralExpression node, Object obj)

        public override Object Visit(StringLiteralExpression node, Object obj) {

            return null;
        }

        #endregion

        #region Visit(TernaryExpression node, Object obj)

        public override Object Visit(TernaryExpression node, Object obj) {
            node.FirstOperand.Accept(this, obj);
            node.SecondOperand.Accept(this, obj);
            node.ThirdOperand.Accept(this, obj);

            return null;
        }

        #endregion

        #region Visit(ThisExpression node, Object obj)

        public override Object Visit(ThisExpression node, Object obj) {

            return null;
        }

        #endregion

        #region Visit(UnaryExpression node, Object obj)

        public override Object Visit(UnaryExpression node, Object obj) {
            node.Operand.Accept(this, obj);

            return null;
        }

        #endregion

        // Statements

        #region Visit(AssertStatement node, Object obj)

        public override Object Visit(AssertStatement node, Object obj) {
            node.Condition.Accept(this, obj);
            node.Expression.Accept(this, obj);

            return null;
        }

        #endregion

        #region Visit(BreakStatement node, Object obj)

        public override Object Visit(BreakStatement node, Object obj) {

            return null;
        }

        #endregion

        #region Visit(CatchStatement node, Object obj)

        public override Object Visit(CatchStatement node, Object obj) {
            node.Exception.Accept(this, obj);
            node.Statements.Accept(this, obj);

            return null;
        }

        #endregion

        #region Visit(Block node, Object obj)

        public override Object Visit(Block node, Object obj) {
            for (int i = 0; i < node.StatementCount; i++)
                node.GetStatementElement(i).Accept(this, obj);

            return null;
        }

        #endregion

        #region Visit(ContinueStatement node, Object obj)

        public override Object Visit(ContinueStatement node, Object obj) {

            return null;
        }

        #endregion

        #region Visit(DoStatement node, Object obj)

        public override Object Visit(DoStatement node, Object obj) {
            for (int i = 0; i < node.InitDo.Count; i++)
                node.InitDo[i].Accept(this, obj);

            for (int i = 0; i < node.BeforeBody.Count; i++)
                node.BeforeBody[i].Accept(this, obj);

            node.Statements.Accept(this, obj);
            node.Condition.Accept(this, obj);

            return null;
        }

        #endregion

        #region Visit(ForeachStatement node, Object obj)

        public override Object Visit(ForeachStatement node, Object obj) {
            node.ForEachDeclaration.Accept(this, obj);
            node.ForeachExp.Accept(this, obj);
            node.ForeachBlock.Accept(this, obj);

            return null;
        }

        #endregion

        #region Visit(ForStatement node, Object obj)

        public override Object Visit(ForStatement node, Object obj) {
            for (int i = 0; i < node.InitializerCount; i++)
                node.GetInitializerElement(i).Accept(this, obj);

            for (int i = 0; i < node.AfterInit.Count; i++)
                node.AfterInit[i].Accept(this, obj);

            for (int i = 0; i < node.BeforeCondition.Count; i++)
                node.BeforeCondition[i].Accept(this, obj);

            node.Condition.Accept(this, obj);
            for (int i = 0; i < node.AfterCondition.Count; i++)
                node.AfterCondition[i].Accept(this, obj);

            node.Statements.Accept(this, obj);

            for (int i = 0; i < node.IteratorCount; i++)
                node.GetIteratorElement(i).Accept(this, obj);

            return null;
        }

        #endregion

        #region Visit(IfElseStatement node, Object obj)

        public override Object Visit(IfElseStatement node, Object obj) {
            node.Condition.Accept(this, obj);

            for (int i = 0; i < node.AfterCondition.Count; i++)
                node.AfterCondition[i].Accept(this, obj);

            node.TrueBranch.Accept(this, obj);
            node.FalseBranch.Accept(this, obj);

            for (int i = 0; i < node.ThetaStatements.Count; i++)
                node.ThetaStatements[i].Accept(this, obj);

            return null;
        }

        #endregion

        #region Visit(ReturnStatement node, Object obj)

        public override Object Visit(ReturnStatement node, Object obj) {
            node.Assigns.Accept(this, obj);

            if (node.ReturnExpression != null)
                return node.ReturnExpression.Accept(this, obj);

            return null;
        }

        #endregion

        #region Visit(SwitchLabel node, Object obj)

        public override Object Visit(SwitchLabel node, Object obj) {
            if (node.SwitchSectionType == SectionType.Case)
                node.Condition.Accept(this, obj);

            return null;
        }

        #endregion

        #region Visit(SwitchSection node, Object obj)

        public override Object Visit(SwitchSection node, Object obj) {
            for (int i = 0; i < node.LabelSection.Count; i++)
                node.LabelSection[i].Accept(this, obj);

            for (int i = 0; i < node.SwitchBlock.StatementCount; i++)
                node.SwitchBlock.GetStatementElement(i).Accept(this, obj);

            return null;
        }

        #endregion

        #region Visit(SwitchStatement node, Object obj)

        public override Object Visit(SwitchStatement node, Object obj) {
            node.Condition.Accept(this, obj);

            for (int i = 0; i < node.AfterCondition.Count; i++)
                node.AfterCondition[i].Accept(this, obj);

            for (int i = 0; i < node.SwitchBlockCount; i++)
                node.GetSwitchSectionElement(i).Accept(this, obj);

            for (int i = 0; i < node.ThetaStatements.Count; i++)
                node.ThetaStatements[i].Accept(this, obj);

            return null;
        }

        #endregion

        #region Visit(ThrowStatement node, Object obj)

        public override Object Visit(ThrowStatement node, Object obj) {
            if (node.ThrowExpression == null)
                return null;
            return node.ThrowExpression.Accept(this, obj);
        }

        #endregion

        #region Visit(ExceptionManagementStatement node, Object obj)

        public override Object Visit(ExceptionManagementStatement node, Object obj) {
            node.TryBlock.Accept(this, obj);

            for (int i = 0; i < node.CatchCount; i++)
                node.GetCatchElement(i).Accept(this, obj);
            
            if (node.FinallyBlock != null)
                node.FinallyBlock.Accept(this, obj);

            return null;
        }

        #endregion

        #region Visit(WhileStatement node, Object obj)

        public override Object Visit(WhileStatement node, Object obj) {
            for (int i = 0; i < node.InitWhile.Count; i++)
                node.InitWhile[i].Accept(this, obj);

            for (int i = 0; i < node.BeforeCondition.Count; i++)
                node.BeforeCondition[i].Accept(this, obj);

            node.Condition.Accept(this, obj);

            for (int i = 0; i < node.AfterCondition.Count; i++)
                node.AfterCondition[i].Accept(this, obj);

            node.Statements.Accept(this, obj);

            return null;
        }

        #endregion

        #region Visit(MoveStatement node, Object obj)

        public override Object Visit(MoveStatement node, Object obj) {
            node.LeftExp.Accept(this, obj);
            node.RightExp.Accept(this, obj);

            if (node.MoveStat != null)
                node.MoveStat.Accept(this, obj);

            return null;
        }

        #endregion

        #region Visit(ThetaStatement node, Object obj)

        public override Object Visit(ThetaStatement node, Object obj) {
            node.ThetaId.Accept(this, obj);

            for (int i = 0; i < node.ThetaList.Count; i++)
                node.ThetaList[i].Accept(this, obj);

            return null;
        }

        #endregion

    }
}
