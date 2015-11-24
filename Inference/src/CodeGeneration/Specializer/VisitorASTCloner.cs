using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AST;
using ErrorManagement;
using Symbols;
using Tools;
using TypeSystem;
using TypeSystem.Constraints;

namespace CodeGeneration
{
    class VisitorASTCloner : VisitorAdapter
    {
        private MethodType currentMethodType;
        private Dictionary<TypeVariable, TypeVariable> typeVariableMappings;
        private Dictionary<TypeExpression, TypeExpression> typeExpresionVariableMapping;

        #region Visit(MethodDefinition node, Object obj)

        public override Object Visit(MethodDefinition node, Object obj)
        {
            typeVariableMappings = new Dictionary<TypeVariable, TypeVariable>();
            typeExpresionVariableMapping = new Dictionary<TypeExpression, TypeExpression>();            
            MethodType originalMethodType = (MethodType)node.TypeExpr;
            MethodType clonedMethodType = new MethodType(originalMethodType.Return);
            currentMethodType = clonedMethodType;
            TypeExpression[] args = (TypeExpression[])obj;
            TypeExpression[] clonedArgs = new TypeExpression[args.Count()];
            List<Parameter> clonedParametersInfo = new List<Parameter>();
            for (int i = 0; i < node.ParametersInfo.Count; i++)
            {
                Parameter originalParameter = node.ParametersInfo[i];
                TypeExpression originalParamType = originalMethodType.GetParameter(i);
                if (originalParamType is TypeVariable)
                {
                    typeExpresionVariableMapping.Add(originalParamType, args[i]);
                    originalParamType = args[i];
                }
                clonedArgs[i] = originalParamType;
                clonedParametersInfo.Add(new Parameter() { Identifier = originalParameter.Identifier, Column = originalParameter.Column, Line = originalParameter.Line, ParamType = originalParamType.typeExpression });
            }
         
            foreach (var constraint in originalMethodType.Constraints.Constraints)
            {
                if (constraint is CloneConstraint)
                {
                    CloneConstraint cc = constraint as CloneConstraint;
                    if (typeExpresionVariableMapping.ContainsKey(cc.FirstOperand))
                        typeExpresionVariableMapping.Add(cc.ReturnType, typeExpresionVariableMapping[cc.FirstOperand]);                    
                }
            }



            SingleIdentifierExpression clonedSingleIdentifierExpression = new SingleIdentifierExpression(node.IdentifierExp.Identifier, node.IdentifierExp.Location);

            Block clonedBlock = (Block)node.Body.Accept(this, null);
            MethodDefinition clonedMethodDefinition = new MethodDefinition(clonedSingleIdentifierExpression, clonedBlock, node.ReturnTypeInfo, clonedParametersInfo, node.ModifiersInfo, node.Location);
            clonedMethodDefinition.FullName = node.FullName;
            clonedMethodType.MemberInfo = new AccessModifier(originalMethodType.MemberInfo.Modifiers, clonedSingleIdentifierExpression.Identifier, clonedMethodType, false);
            clonedMethodType.MemberInfo.Class = originalMethodType.MemberInfo.Class;
            clonedMethodType.MemberInfo.TypeDefinition = originalMethodType.MemberInfo.TypeDefinition;
            for (int i = 0; i < originalMethodType.ParameterListCount; i++)
            {
                clonedMethodType.AddParameter(clonedArgs[i]);
            }
            clonedMethodType.ASTNode = clonedMethodDefinition;
            clonedMethodDefinition.TypeExpr = clonedMethodType;

            TypeDefinition originalTypeDefinition = clonedMethodType.MemberInfo.TypeDefinition;
            originalTypeDefinition.AddMethod(clonedMethodDefinition);

            UserType originalClass = clonedMethodType.MemberInfo.Class;
            AccessModifier am = originalClass.Members[clonedMethodDefinition.Identifier];
            IntersectionMemberType intersectionMemberType = am.Type as IntersectionMemberType;
            if (intersectionMemberType != null)
            {
                intersectionMemberType.TypeSet.Add(clonedMethodType);
            }
            else
            {
                am = clonedMethodType.MemberInfo;
                IntersectionMemberType intersection = new IntersectionMemberType();
                intersection.TypeSet.Add(originalMethodType);
                intersection.TypeSet.Add(clonedMethodType);
                am.Type = intersection;
                originalClass.Members[clonedMethodDefinition.Identifier] = am;
            }

            return clonedMethodDefinition;
        }

        #endregion

        //Expressions

        #region Visit(ArgumentExpression node, Object obj)

        public override Object Visit(ArgumentExpression node, Object obj)
        {
            return new ArgumentExpression((Expression)node.Argument.Accept(this, obj), node.Location);
        }

        #endregion

        #region Visit(ArithmeticExpression node, Object obj)

        public override Object Visit(ArithmeticExpression node, Object obj)
        {
            Expression clonedFirstOperand = (Expression)node.FirstOperand.Accept(this, obj);
            Expression clonedSecondOperand = (Expression)node.SecondOperand.Accept(this, obj);
            ArithmeticExpression clonedArithmeticExpression = new ArithmeticExpression(clonedFirstOperand, clonedSecondOperand, node.Operator, node.Location);
            return clonedArithmeticExpression;
        }

        #endregion

        #region Visit(ArrayAccessExpression node, Object obj)

        public override Object Visit(ArrayAccessExpression node, Object obj)
        {
            Expression clonedFirstOperand = (Expression)node.FirstOperand.Accept(this, obj);
            Expression clonedSecondOperand = (Expression)node.SecondOperand.Accept(this, obj);
            ArrayAccessExpression clonedArrayAccessExpression = new ArrayAccessExpression(clonedFirstOperand, clonedSecondOperand, node.Location);
            return clonedArrayAccessExpression;
        }

        #endregion

        #region Visit(AssignmentExpression node, Object obj)

        public override Object Visit(AssignmentExpression node, Object obj)
        {
            Expression clonedFirstOperand = (Expression)node.FirstOperand.Accept(this, obj);
            Expression clonedSecondOperand = (Expression)node.SecondOperand.Accept(this, obj);
            AssignmentExpression clonedAssignmentExpression = new AssignmentExpression(clonedFirstOperand, clonedSecondOperand, node.Operator, node.Location);
            if (node.MoveStat != null)
                clonedAssignmentExpression.MoveStat = (MoveStatement)node.MoveStat.Accept(this, obj);
            return clonedAssignmentExpression;
        }

        #endregion

        #region Visit(BitwiseExpression node, Object obj)

        public override Object Visit(BitwiseExpression node, Object obj)
        {
            Expression clonedFirstOperand = (Expression)node.FirstOperand.Accept(this, obj);
            Expression clonedSecondOperand = (Expression)node.SecondOperand.Accept(this, obj);
            BitwiseExpression clonedBitwiseExpression = new BitwiseExpression(clonedFirstOperand, clonedSecondOperand, node.Operator, node.Location);
            return clonedBitwiseExpression;
        }

        #endregion

        #region Visit(BoolLiteralExpression node, Object obj)

        public override Object Visit(BoolLiteralExpression node, Object obj)
        {
            return new BoolLiteralExpression(node.BoolValue, node.Location);
        }

        #endregion

        #region Visit(CharLiteralExpression node, Object obj)

        public override Object Visit(CharLiteralExpression node, Object obj)
        {
            return new CharLiteralExpression(node.CharValue, node.Location);
        }

        #endregion

        #region Visit(DoubleLiteralExpression node, Object obj)

        public override Object Visit(DoubleLiteralExpression node, Object obj)
        {
            return new DoubleLiteralExpression(node.DoubleValue, node.Location);
        }

        #endregion

        #region Visit(FieldAccessExpression node, Object obj)

        public override Object Visit(FieldAccessExpression node, Object obj)
        {
            FieldAccessExpression clonedFieldAccessExpression = new FieldAccessExpression((Expression)node.Expression.Accept(this, obj), (SingleIdentifierExpression)node.FieldName.Accept(this, obj), new Location("", 1000, 1000));
            if (node.ExpressionType != null)
                clonedFieldAccessExpression.ExpressionType = node.ExpressionType.CloneType(this.typeVariableMappings,this.typeExpresionVariableMapping);
            return clonedFieldAccessExpression;
        }

        #endregion

        #region Visit(IntLiteralExpression node, Object obj)

        public override Object Visit(IntLiteralExpression node, Object obj)
        {
            return new IntLiteralExpression(node.IntValue, node.Location);
        }

        #endregion

        #region Visit(LogicalExpression node, Object obj)

        public override Object Visit(LogicalExpression node, Object obj)
        {
            Expression clonedFirstOperand = (Expression)node.FirstOperand.Accept(this, obj);
            Expression clonedSecondOperand = (Expression)node.SecondOperand.Accept(this, obj);
            LogicalExpression clonedLogicalExpression = new LogicalExpression(clonedFirstOperand, clonedSecondOperand, node.Operator, node.Location);
            return clonedLogicalExpression;
        }

        #endregion

        #region Visit(NewArrayExpression node, Object obj)

        public override Object Visit(NewArrayExpression node, Object obj)
        {
            NewArrayExpression clonedNewArrayExpression = new NewArrayExpression(node.TypeInfo, node.Location);

            if (node.Size != null)
                clonedNewArrayExpression.Size = (Expression)node.Size.Accept(this, obj);

            if (node.Init != null)
                clonedNewArrayExpression.Init = (CompoundExpression)node.Init.Accept(this, obj);
            clonedNewArrayExpression.ExpressionType = node.ExpressionType.CloneType(this.typeVariableMappings, this.typeExpresionVariableMapping);
            return clonedNewArrayExpression;
        }

        #endregion

        #region Visit(RelationalExpression node, Object obj)

        public override Object Visit(RelationalExpression node, Object obj)
        {
            RelationalExpression clonedRelationalExpression = new RelationalExpression((Expression)node.FirstOperand.Accept(this, obj), (Expression)node.SecondOperand.Accept(this, obj), node.Operator, node.Location);
            clonedRelationalExpression.ExpressionType = node.ExpressionType.CloneType(this.typeVariableMappings, this.typeExpresionVariableMapping);
            return clonedRelationalExpression;
        }

        #endregion

        #region Visit(SingleIdentifierExpression node, Object obj)

        public override Object Visit(SingleIdentifierExpression node, Object obj)
        {
            SingleIdentifierExpression clonedSingleIdentifierExpression = new SingleIdentifierExpression(node.Identifier, node.Location);
            if (node.ExpressionType != null)
                clonedSingleIdentifierExpression.ExpressionType = node.ExpressionType.CloneType(this.typeVariableMappings, this.typeExpresionVariableMapping);
            if (node.IdSymbol != null)
            {
                Symbol originalSymbol = node.IdSymbol;
                Symbol symbol = new Symbol(originalSymbol.Name, originalSymbol.Scope, originalSymbol.SymbolType.CloneType(this.typeVariableMappings, this.typeExpresionVariableMapping), originalSymbol.IsDynamic);
                clonedSingleIdentifierExpression.IdSymbol = symbol;
            }
            clonedSingleIdentifierExpression.IndexOfSSA = node.IndexOfSSA;
            return clonedSingleIdentifierExpression;
        }

        #endregion

        #region Visit(StringLiteralExpression node, Object obj)

        public override Object Visit(StringLiteralExpression node, Object obj)
        {
            return new StringLiteralExpression(node.StringValue, node.Location);
        }

        #endregion

        #region Visit(TernaryExpression node, Object obj)

        public override Object Visit(TernaryExpression node, Object obj)
        {
            Expression clonedFirstOperand = (Expression)node.FirstOperand.Accept(this, obj);
            Expression clonedSecondOperand = (Expression)node.SecondOperand.Accept(this, obj);
            Expression clonedThirdOperand = (Expression)node.ThirdOperand.Accept(this, obj);
            TernaryExpression clonedTernaryExpression = new TernaryExpression(clonedFirstOperand, clonedSecondOperand, clonedThirdOperand, node.Operator, node.Location);
            return clonedTernaryExpression;
        }

        #endregion

        #region Visit(ThisExpression node, Object obj)

        public override Object Visit(ThisExpression node, Object obj)
        {
            return new ThisExpression(node.Location);
        }

        #endregion

        #region Visit(UnaryExpression node, Object obj)

        public override Object Visit(UnaryExpression node, Object obj)
        {
            Expression clonedOperand = (Expression)node.Operand.Accept(this, obj);
            UnaryExpression clonedUnaryExpression = new UnaryExpression(clonedOperand, node.Operator, node.Location);
            return clonedUnaryExpression;
        }

        #endregion

        #region Visit(CastExpression node, Object obj)

        public override Object Visit(CastExpression node, Object obj)
        {
            CastExpression clonedCastExpression = new CastExpression(node.CastId, (Expression)node.Expression.Accept(this, obj), node.Location);
            clonedCastExpression.CastType = node.CastType.CloneType(typeVariableMappings, this.typeExpresionVariableMapping);
            return clonedCastExpression;
        }

        #endregion

        #region Visit(IsExpression node, Object obj)

        public override Object Visit(IsExpression node, Object obj)
        {
            IsExpression clonedIsExpression = new IsExpression((Expression)node.Expression.Accept(this, obj), node.TypeId, node.Location);
            clonedIsExpression.TypeExpr = node.TypeExpr.CloneType(typeVariableMappings, this.typeExpresionVariableMapping);
            return clonedIsExpression;
        }

        #endregion

        #region Visit(CompoundExpression node, Object obj)

        public override Object Visit(CompoundExpression node, Object obj)
        {
            CompoundExpression clonedCompoundExpression = new CompoundExpression(node.Location);
            for (int i = 0; i < node.ExpressionCount; i++)
                clonedCompoundExpression.AddExpression((Expression)node.GetExpressionElement(i).Accept(this, obj));
            return clonedCompoundExpression;
        }

        #endregion

        #region Visit(NullExpression node, Object obj)

        public override Object Visit(NullExpression node, Object obj)
        {
            NullExpression clonedNullExpression = new NullExpression(node.Location);
            clonedNullExpression.ExpressionType = node.ExpressionType.CloneType(this.typeVariableMappings,this.typeExpresionVariableMapping);
            return clonedNullExpression;
        }

        #endregion

        #region Visit(InvocationExpression node, Object obj)

        public override Object Visit(InvocationExpression node, Object obj)
        {
            InvocationExpression clonedInvocationExpression = new InvocationExpression((Expression)node.Identifier.Accept(this, obj), (CompoundExpression)node.Arguments.Accept(this, obj), node.Location);
            clonedInvocationExpression.ExpressionType = node.ExpressionType.CloneType(this.typeVariableMappings, this.typeExpresionVariableMapping);
            return clonedInvocationExpression;
        }

        #endregion

        #region Visit(NewExpression node, Object obj)

        public override Object Visit(NewExpression node, Object obj)
        {
            NewExpression clonedNewExpression = new NewExpression(node.TypeInfo, (CompoundExpression)node.Arguments.Accept(this, obj), node.Location);
            clonedNewExpression.NewType = node.NewType;
            return clonedNewExpression;
        }

        #endregion

        #region Visit(QualifiedIdentifierExpression node, Object obj)

        public override Object Visit(QualifiedIdentifierExpression node, Object obj)
        {
            return null;
        }

        #region Visit(BaseExpression node, Object obj)

        public override Object Visit(BaseExpression node, Object obj)
        {
            BaseExpression clonedBaseExpression = new BaseExpression(node.Location);
            clonedBaseExpression.ExpressionType = node.ExpressionType.CloneType(this.typeVariableMappings, this.typeExpresionVariableMapping);
            return clonedBaseExpression;
        }

        #endregion

        #endregion

        // Statements
        #region Visit(Definition node, Object obj)

        public override Object Visit(Definition node, Object obj)
        {
            SingleIdentifierExpression clonedIdentifierExp = new SingleIdentifierExpression(node.IdentifierExp.Identifier, node.IdentifierExp.Location);
            Expression init = (Expression)node.Init.Accept(this, obj);
            Definition clonedDefinition = new Definition(clonedIdentifierExp, node.TypeExpr.typeExpression, init, node.Location);
            clonedIdentifierExp.IndexOfSSA = node.IdentifierExp.IndexOfSSA;
            clonedDefinition.TypeExpr = node.TypeExpr.CloneType(this.typeVariableMappings, this.typeExpresionVariableMapping);
            Symbol originalSymbol = node.Symbol;
            Symbol symbol = new Symbol(originalSymbol.Name, originalSymbol.Scope, originalSymbol.SymbolType.CloneType(this.typeVariableMappings, this.typeExpresionVariableMapping), originalSymbol.IsDynamic);
            clonedDefinition.Symbol = symbol;
            return clonedDefinition;
        }

        #endregion

        #region Visit(DeclarationSet node, Object obj)

        public override Object Visit(DeclarationSet node, Object obj)
        {
            List<Statement> declarations = new List<Statement>();
            for (int i = 0; i < node.Count; i++)
                declarations.Add((Statement)node.GetDeclarationElement(i).Accept(this, obj));
            return new DeclarationSet(node.FullName, declarations, node.Location); ;
        }

        #endregion

        #region Visit(AssertStatement node, Object obj)
        public override Object Visit(IdDeclaration node, Object obj)
        {
            SingleIdentifierExpression clonedIdentifierExp = new SingleIdentifierExpression(node.IdentifierExp.Identifier, node.IdentifierExp.Location);
            clonedIdentifierExp.IndexOfSSA = node.IdentifierExp.IndexOfSSA;
            IdDeclaration clonedIdDeclaration = new IdDeclaration(clonedIdentifierExp, clonedIdentifierExp.IndexOfSSA, node.TypeExpr.typeExpression, node.Location);
            clonedIdDeclaration.TypeExpr = node.TypeExpr.CloneType(this.typeVariableMappings, this.typeExpresionVariableMapping);
            Symbol originalSymbol = node.Symbol;
            Symbol symbol = new Symbol(originalSymbol.Name, originalSymbol.Scope, originalSymbol.SymbolType.CloneType(this.typeVariableMappings, this.typeExpresionVariableMapping), originalSymbol.IsDynamic);
            clonedIdDeclaration.Symbol = symbol;
            return clonedIdDeclaration;
        }
        #endregion

        #region Visit(AssertStatement node, Object obj)

        public override Object Visit(AssertStatement node, Object obj)
        {
            return new AssertStatement((Expression)node.Condition.Accept(this, obj), (Expression)node.Expression.Accept(this, obj), node.Location);
        }

        #endregion

        #region Visit(BreakStatement node, Object obj)

        public override Object Visit(BreakStatement node, Object obj)
        {
            return new BreakStatement(node.Location);
        }

        #endregion

        #region Visit(CatchStatement node, Object obj)

        public override Object Visit(CatchStatement node, Object obj)
        {
            return new CatchStatement((IdDeclaration)node.Exception.Accept(this, obj), (Block)node.Statements.Accept(this, obj), node.Location);
        }

        #endregion

        #region Visit(Block node, Object obj)

        public override Object Visit(Block node, Object obj)
        {
            Block clonedBlock = new Block(node.Location);
            for (int i = 0; i < node.StatementCount; i++)
                clonedBlock.AddStatement((Statement)node.GetStatementElement(i).Accept(this, obj));
            return clonedBlock;
        }

        #endregion

        #region Visit(ContinueStatement node, Object obj)

        public override Object Visit(ContinueStatement node, Object obj)
        {
            return new ContinueStatement(node.Location);
        }

        #endregion

        #region Visit(DoStatement node, Object obj)

        public override Object Visit(DoStatement node, Object obj)
        {
            DoStatement clonedDoStatement = new DoStatement((Statement)node.Statements.Accept(this, obj), (Expression)node.Condition.Accept(this, obj), node.Location);
            for (int i = 0; i < node.InitDo.Count; i++)
                clonedDoStatement.InitDo.Add((MoveStatement)node.InitDo[i].Accept(this, obj));
            for (int i = 0; i < node.BeforeBody.Count; i++)
                clonedDoStatement.BeforeBody.Add((ThetaStatement)node.BeforeBody[i].Accept(this, obj));
            return clonedDoStatement;
        }

        #endregion

        #region Visit(ForeachStatement node, Object obj)

        public override Object Visit(ForeachStatement node, Object obj)
        {
            IdDeclaration clonedSingleIdentifierExpression = (IdDeclaration)node.ForEachDeclaration.Accept(this, obj);
            return new ForeachStatement(clonedSingleIdentifierExpression.TypeExpr.typeExpression, clonedSingleIdentifierExpression.IdentifierExp, (Expression)node.ForeachExp.Accept(this, obj), (Statement)node.ForeachBlock.Accept(this, obj), node.Location);
        }

        #endregion

        #region Visit(ForStatement node, Object obj)

        public override Object Visit(ForStatement node, Object obj)
        {
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

        public override Object Visit(IfElseStatement node, Object obj)
        {
            IfElseStatement clonedIfElseStatement = new IfElseStatement((Expression)node.Condition.Accept(this, obj), (Statement)node.TrueBranch.Accept(this, obj), (Statement)node.FalseBranch.Accept(this, obj), node.Location);
            foreach (var afterCondition in node.AfterCondition)
                clonedIfElseStatement.AfterCondition.Add((MoveStatement)afterCondition.Accept(this, obj));
            foreach (var thetaStatement in node.ThetaStatements)
                clonedIfElseStatement.ThetaStatements.Add((ThetaStatement)thetaStatement.Accept(this, obj));
            return clonedIfElseStatement;
        }

        #endregion

        #region Visit(ReturnStatement node, Object obj)

        public override Object Visit(ReturnStatement node, Object obj)
        {
            Expression clonedReturnExpression = (Expression)node.ReturnExpression.Accept(this, obj);
            ReturnStatement clonedReturnStatement = new ReturnStatement(clonedReturnExpression, node.ReturnExpression.Location);
            clonedReturnStatement.CurrentMethodType = currentMethodType;
            return clonedReturnStatement;
        }

        #endregion

        #region Visit(SwitchLabel node, Object obj)

        public override Object Visit(SwitchLabel node, Object obj)
        {
            if (node.SwitchSectionType == SectionType.Case)
                node.Condition.Accept(this, obj);

            return null;
        }

        #endregion

        #region Visit(SwitchSection node, Object obj)

        public override Object Visit(SwitchSection node, Object obj)
        {
            for (int i = 0; i < node.LabelSection.Count; i++)
                node.LabelSection[i].Accept(this, obj);

            for (int i = 0; i < node.SwitchBlock.StatementCount; i++)
                node.SwitchBlock.GetStatementElement(i).Accept(this, obj);

            return null;
        }

        #endregion

        #region Visit(SwitchStatement node, Object obj)

        public override Object Visit(SwitchStatement node, Object obj)
        {
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

        public override Object Visit(ThrowStatement node, Object obj)
        {
            if (node.ThrowExpression == null)
                return null;
            return node.ThrowExpression.Accept(this, obj);
        }

        #endregion

        #region Visit(ExceptionManagementStatement node, Object obj)

        public override Object Visit(ExceptionManagementStatement node, Object obj)
        {
            node.TryBlock.Accept(this, obj);

            for (int i = 0; i < node.CatchCount; i++)
                node.GetCatchElement(i).Accept(this, obj);

            if (node.FinallyBlock != null)
                node.FinallyBlock.Accept(this, obj);

            return null;
        }

        #endregion

        #region Visit(WhileStatement node, Object obj)

        public override Object Visit(WhileStatement node, Object obj)
        {
            WhileStatement clonedWhileStatement = new WhileStatement((Expression)node.Condition.Accept(this, obj), (Statement)node.Statements.Accept(this, obj), node.Location);
            for (int i = 0; i < node.InitWhile.Count; i++)
                clonedWhileStatement.InitWhile.Add((MoveStatement)node.InitWhile[i].Accept(this, obj));
            for (int i = 0; i < node.BeforeCondition.Count; i++)
                clonedWhileStatement.BeforeCondition.Add((ThetaStatement)node.BeforeCondition[i].Accept(this, obj));
            for (int i = 0; i < node.AfterCondition.Count; i++)
                clonedWhileStatement.AfterCondition.Add((MoveStatement)node.AfterCondition[i].Accept(this, obj));
            return clonedWhileStatement;
        }

        #endregion

        #region Visit(MoveStatement node, Object obj)

        public override Object Visit(MoveStatement node, Object obj)
        {
            MoveStatement clonedMoveStatement = new MoveStatement((SingleIdentifierExpression)node.LeftExp.Accept(this, obj), (SingleIdentifierExpression)node.RightExp.Accept(this, obj), node.Location.FileName, node.Location.Line);
            if (node.MoveStat != null)
                clonedMoveStatement.MoveStat = (MoveStatement)node.MoveStat.Accept(this, obj);
            return clonedMoveStatement;
        }

        #endregion

        #region Visit(ThetaStatement node, Object obj)

        public override Object Visit(ThetaStatement node, Object obj)
        {
            List<SingleIdentifierExpression> list = new List<SingleIdentifierExpression>();
            for (int i = 0; i < node.ThetaList.Count; i++)
                list.Add((SingleIdentifierExpression)node.ThetaList[i].Accept(this, obj));
            return new ThetaStatement((SingleIdentifierExpression)node.ThetaId.Accept(this, obj), list, node.Location);
        }

        #endregion

    }
}
