////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: VisitorSSA.cs                                                        //
// Authors: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                   //
//          Francisco Ortin - francisco.ortin@gmail.com                       //
// Description:                                                               //
//    This class makes the static single assignment algorithm in which every  //
//        variable is assigned exactly once.                                  //
//    It also collects the set of references that are used in the if          //
//        and switch statements to be used later (VisitorTypeInference)       //
//        as theta function parameters                                        //
//    Inheritance: VisitorAdapter.                                            //
//    Implements Visitor pattern [Concrete Visitor].                          //
// -------------------------------------------------------------------------- //
// Create date: 06-04-2007                                                    //
// Modification date: 07-06-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using AST;
using Tools;
using ErrorManagement;

namespace Semantic.SSAAlgorithm {
    /// <summary>
    /// This class makes the static single assignment algorithm in which every
    /// variable is assigned exactly once.
    /// </summary>
    /// <remarks>
    /// Inheritance: VisitorAdapter
    /// Implements Visitor pattern [Concrete Visitor].
    /// </remarks>
    class VisitorSSA : VisitorAdapter {
        #region Fields

        /// <summary>
        /// Mapping between identifiers and its current number in SSA algorithm.
        /// </summary>
        private SSAMap map;

        /// <summary>
        /// Stores the information of fields
        /// </summary>
        private Dictionary<string, FieldDeclaration> fieldsInfo;

        /// <summary>
        /// The referecences used in a if or else statement body that is being analyzed (if any). 
        /// Used for SSA purposes.
        /// </summary>
        IList<SingleIdentifierExpression> referencesUsedInIfElseBody;

        /// <summary>
        /// The referecences used in a case statement body that is being analyzed (if any). 
        /// Used for SSA purposes.
        /// </summary>
        IList<SingleIdentifierExpression> referencesUsedInCaseBody;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of VisitorSSA
        /// </summary>
        public VisitorSSA() {
            this.map = new SSAMap();
            this.fieldsInfo = new Dictionary<string, FieldDeclaration>();
        }

        #endregion

        #region addAssignmentStatements()

        /// <summary>
        /// Adds new assignment statements at the end of the specified block.
        /// </summary>
        /// <param name="vars">Information of the current scope in SSAMap.</param>
        /// <param name="node">Block of statement to add the new declarations.</param>
        private void addAssignmentStatements(Dictionary<string, SSAElement> vars, Block node) {
            FieldAccessExpression op1;
            SingleIdentifierExpression eop1;
            SingleIdentifierExpression op2;
            ThisExpression th = new ThisExpression(node.Location);

            foreach (KeyValuePair<string, SSAElement> pair in vars) {
                if (pair.Value.IndexSSA > 0) {
                    eop1 = new SingleIdentifierExpression(pair.Key.Substring(6, pair.Key.Length - 6), node.Location);
                    eop1.IndexOfSSA = -1;
                    op1 = new FieldAccessExpression(th, eop1, node.Location);
                    op2 = new SingleIdentifierExpression(pair.Key, node.Location);
                    op2.IndexOfSSA = pair.Value.IndexSSA;
                    node.AddStatement(new AssignmentExpression(op1, op2, AssignmentOperator.Assign, node.Location));
                }
            }
        }

        #endregion

        #region addLocalVariable()

        /// <summary>
        /// Adds new local variables according to the information of SSAMap.
        /// </summary>
        /// <param name="vars">Information of the current scope in SSAMap.</param>
        /// <param name="node">Block of statement to add the new declarations.</param>
        private void addLocalVariable(Dictionary<string, SSAElement> vars, Statement node) {
            string type;
            Block statements;
            if (node is Block) {
                statements = (Block)node;
                foreach (KeyValuePair<string, SSAElement> pair in vars) {
                    for (int i = 0; i < pair.Value.IndexSSA; i++) {
                        type = TypeSystem.TypeTable.ObtainNewType(pair.Value.TypeId);
                        statements.AddStatementToTheBeginning(new IdDeclaration(new SingleIdentifierExpression(pair.Key, pair.Value.Location), i + 1, type, pair.Value.Location));
                    }
                }
            }
        }

        #endregion

        // Declarations

        #region Visit(ClassDefinition node, Object obj)

        public override Object Visit(ClassDefinition node, Object obj) {
            //this.map.SetScope();

            // first step 
            // we store field declarations in the dictionary fieldsInfo
            this.fieldsInfo.Clear();
            for (int i = 0; i < node.MemberCount; i++) {
                if (node.GetMemberElement(i) is FieldDeclaration)
                    this.fieldsInfo.Add(((FieldDeclaration)node.GetMemberElement(i)).Identifier, (FieldDeclaration)node.GetMemberElement(i));
            }

            // second step
            for (int i = 0; i < node.MemberCount; i++) {
                node.GetMemberElement(i).Accept(this, obj);
            }

            //this.addField(this.map.ResetScope(), node);
            return null;
        }

        #endregion

        #region Visit(IdDeclaration node, Object obj)

        public override Object Visit(IdDeclaration node, Object obj) {
            if (!node.FullName.EndsWith("[]"))
                this.map.AddNewVariable(node.Identifier, node.FullName, node.Location);
            else
                node.IdentifierExp.IndexOfSSA = -1;
            return null;
        }

        #endregion

        #region Visit(Definition node, Object obj)

        public override Object Visit(Definition node, Object obj) {
            if (!node.FullName.EndsWith("[]"))
                this.map.AddNewVariable(node.Identifier, node.FullName, node.Location);
            else
                node.IdentifierExp.IndexOfSSA = -1;
            node.Init.Accept(this, false);
            return null;
        }

        #endregion

        #region Visit(ConstantDefinition node, Object obj)

        public override Object Visit(ConstantDefinition node, Object obj) {
            if (!node.FullName.EndsWith("[]"))
                this.map.AddNewVariable(node.Identifier, node.FullName, node.Location);
            else
                node.IdentifierExp.IndexOfSSA = -1;
            node.Init.Accept(this, false);
            return null;
        }

        #endregion

        #region Visit(MethodDefinition node, Object obj)

        public override Object Visit(MethodDefinition node, Object obj) {
            // Scope to store the local variable created for each field
            this.map.SetScope();
            //Dictionary<string, FieldDeclaration>.KeyCollection keys = this.fieldsInfo.Keys;
            //foreach (string tmpName in keys)
            //{
            //   if (!fieldsInfo[tmpName].TypeInfo.EndsWith("[]"))
            //      this.map.AddNewField("this__" + tmpName, fieldsInfo[tmpName].TypeInfo, node.FileName, fieldsInfo[tmpName].Line, fieldsInfo[tmpName].Column);
            //}

            this.map.SetScope();
            for (int i = 0; i < node.ParametersInfo.Count; i++) {
                if (!node.ParametersInfo[i].ParamType.EndsWith("[]"))
                    this.map.AddNewVariable(node.ParametersInfo[i].Identifier, node.ParametersInfo[i].ParamType, node.Location);
            }

            bool returnFound = (bool)node.Body.Accept(this, obj);
            this.addLocalVariable(this.map.ResetScope(), node.Body);

            // Add local variable for each field in use.
            //this.addLocalVariable(this.map.GetFieldScope(), node.Body);

            // If the last sentence is not a return statement adds new assignment statements for each field in use.
            //if (!returnFound)
            //   this.addAssignmentStatements(this.map.ResetScope(), node.Body);
            //else
            this.map.ResetScope(); // Resets the scope
            return null;
        }

        #endregion

        #region Visit(ConstructorDefinition node, Object obj)

        public override Object Visit(ConstructorDefinition node, Object obj) {
            // Scope to store the auxiliar local variables for each field
            this.map.SetScope();
            //Dictionary<string, FieldDeclaration>.KeyCollection keys = this.fieldsInfo.Keys;
            //foreach (string tmpName in keys)
            //{
            //   if (!fieldsInfo[tmpName].TypeInfo.EndsWith("[]"))
            //      this.map.AddNewField("this__" + tmpName, fieldsInfo[tmpName].TypeInfo, node.FileName, fieldsInfo[tmpName].Line, fieldsInfo[tmpName].Column);
            //}

            this.map.SetScope();
            for (int i = 0; i < node.ParametersInfo.Count; i++) {

                if (!node.ParametersInfo[i].ParamType.EndsWith("[]"))
                    this.map.AddNewVariable(node.ParametersInfo[i].Identifier, node.ParametersInfo[i].ParamType, node.Location);
            }

            if (node.Initialization != null)
               node.Initialization.Accept(this, obj);

            node.Body.Accept(this, obj);
            this.addLocalVariable(this.map.ResetScope(), node.Body);
            // Add local variable for each field in use.
            //this.addLocalVariable(this.map.GetFieldScope(), node.Body);
            this.map.ResetScope(); //this.addAssignmentStatements(this.map.ResetScope(), node.Body);
            return null;
        }

        #endregion

        // Expressions

        #region Visit(AssignmentExpression node, Object obj)

        public override Object Visit(AssignmentExpression node, Object obj) {
            Object aux = null;
            node.SecondOperand.LeftExpression = false;
            if ((aux = node.SecondOperand.Accept(this, false)) is SingleIdentifierExpression)
                node.SecondOperand = (SingleIdentifierExpression)aux;
            node.FirstOperand.LeftExpression = true;
            if ((aux = node.FirstOperand.Accept(this, false)) is SingleIdentifierExpression)
                node.FirstOperand = (SingleIdentifierExpression)aux;
            // * If the left expression is an SingleID expression, we do not add it to the used
            //   references because the theta function sets its type as a union type
            SingleIdentifierExpression singleId=node.FirstOperand as SingleIdentifierExpression;
            if (singleId!=null) {
                if (this.referencesUsedInIfElseBody!=null)
                    this.referencesUsedInIfElseBody.Remove(singleId);
                if (this.referencesUsedInCaseBody!=null)
                    this.referencesUsedInCaseBody.Remove(singleId);
                }
            return null;
        }

        #endregion

        #region Visit(SingleIdentifierExpression node, Object obj)
        /// <summary>
        /// The obj parameter indicates if it is an explicit member of another object (e.g., obj.member => obj false and member true)
        /// </summary>
        public override Object Visit(SingleIdentifierExpression node, Object obj) {
            // * Is our father a field access
            bool isMember = true;
            if (obj != null)
                isMember = getInheritedAttributes<Boolean>(obj);
            // * If we are in a if statement, we add the reference to the list of used references (node IfElseStatement of the AST)
            if (!isMember) {
                if (this.referencesUsedInIfElseBody != null && !this.referencesUsedInIfElseBody.Contains(node))
                    this.referencesUsedInIfElseBody.Add(node);
                if (this.referencesUsedInCaseBody != null && !this.referencesUsedInCaseBody.Contains(node))
                    this.referencesUsedInCaseBody.Add(node);
            }

            int iValue;
            if (node.LeftExpression) {
                this.map.Increment(node.Identifier);
            }

            if ((iValue = this.map.Search(node.Identifier)) != -1) {
                node.IndexOfSSA = iValue;
            }

            return null;
        }

        #endregion

        #region Visit(FieldAccessExpression node, Object obj)

        public override Object Visit(FieldAccessExpression node, Object obj) {
            Object aux = null;
            if ((aux = node.Expression.Accept(this, false)) is SingleIdentifierExpression)
                node.Expression = (SingleIdentifierExpression)aux;
            node.FieldName.Accept(this, true);
            return null;
        }

        #endregion

        // ---

        #region Visit(ArgumentExpression node, Object obj)

        public override Object Visit(ArgumentExpression node, Object obj) {
            Object aux = null;
            node.Argument.LeftExpression = node.LeftExpression;
            if ((aux = node.Argument.Accept(this, false)) is SingleIdentifierExpression)
                node.Argument = (SingleIdentifierExpression)aux;
            return null;
        }

        #endregion

        #region Visit(ArithmeticExpression node, Object obj)

        public override Object Visit(ArithmeticExpression node, Object obj) {
            Object aux = null;
            node.FirstOperand.LeftExpression = node.LeftExpression;
            node.SecondOperand.LeftExpression = node.LeftExpression;
            if ((aux = node.FirstOperand.Accept(this, false)) is SingleIdentifierExpression)
                node.FirstOperand = (SingleIdentifierExpression)aux;
            if ((aux = node.SecondOperand.Accept(this, false)) is SingleIdentifierExpression)
                node.SecondOperand = (SingleIdentifierExpression)aux;
            return null;
        }

        #endregion

        #region Visit(ArrayAccessExpression node, Object obj)

        public override Object Visit(ArrayAccessExpression node, Object obj) {
            Object aux = null;
            //node.FirstOperand.LeftExpression = node.LeftExpression;
            node.FirstOperand.LeftExpression = false;
            node.SecondOperand.LeftExpression = false;
            if ((aux = node.FirstOperand.Accept(this, false)) is SingleIdentifierExpression)
               node.FirstOperand = (SingleIdentifierExpression)aux;
            if ((aux = node.SecondOperand.Accept(this, false)) is SingleIdentifierExpression)
               node.SecondOperand = (SingleIdentifierExpression)aux;
            node.FirstOperand.LeftExpression = node.LeftExpression;
            return null;
        }

        #endregion

        #region Visit(BaseCallExpression node, Object obj)

        public override Object Visit(BaseCallExpression node, Object obj) {
            node.Arguments.LeftExpression = false;
            node.Arguments.Accept(this, false);
            return null;
        }

        #endregion

        #region Visit(BinaryExpression node, Object obj)

        public override Object Visit(BinaryExpression node, Object obj) {
            Object aux = null;
            node.FirstOperand.LeftExpression = node.LeftExpression;
            node.SecondOperand.LeftExpression = node.LeftExpression;
            if ((aux = node.FirstOperand.Accept(this, false)) is SingleIdentifierExpression)
                node.FirstOperand = (SingleIdentifierExpression)aux;
            if ((aux = node.SecondOperand.Accept(this, false)) is SingleIdentifierExpression)
                node.SecondOperand = (SingleIdentifierExpression)aux;
            return null;
        }

        #endregion

        #region Visit(CastExpression node, Object obj)

        public override Object Visit(CastExpression node, Object obj) {
            Object aux = null;
            node.Expression.LeftExpression = node.LeftExpression;
            if ((aux = node.Expression.Accept(this, false)) is SingleIdentifierExpression)
                node.Expression = (SingleIdentifierExpression)aux;
            return null;
        }

        #endregion

        #region Visit(CompoundExpression node, Object obj)

        public override Object Visit(CompoundExpression node, Object obj) {
            Object aux = null;
            for (int i = 0; i < node.ExpressionCount; i++) {
                node.GetExpressionElement(i).LeftExpression = node.LeftExpression;
                if ((aux = node.GetExpressionElement(i).Accept(this, false)) is SingleIdentifierExpression)
                    node.SetExpressionElement(i, (SingleIdentifierExpression)aux);
            }
            return null;
        }

        #endregion

        #region Visit(InvocationExpression node, Object obj)

        public override Object Visit(InvocationExpression node, Object obj) {
            Object aux = null;
            node.Identifier.LeftExpression = node.LeftExpression;
            node.Arguments.LeftExpression = false;
            if ((aux = node.Identifier.Accept(this, true)) is SingleIdentifierExpression)
                node.Identifier = (SingleIdentifierExpression)aux;
            node.Arguments.Accept(this, false);
            return null;
        }

        #endregion

        #region Visit(IsExpression node, Object obj)

        public override Object Visit(IsExpression node, Object obj) {
            Object aux = null;
            node.Expression.LeftExpression = node.LeftExpression;
            if ((aux = node.Expression.Accept(this, false)) is SingleIdentifierExpression)
                node.Expression = (SingleIdentifierExpression)aux;
            return null;
        }

        #endregion

        #region Visit(LogicalExpression node, Object obj)

        public override Object Visit(LogicalExpression node, Object obj) {
            Object aux = null;
            node.FirstOperand.LeftExpression = node.LeftExpression;
            node.SecondOperand.LeftExpression = node.LeftExpression;
            if ((aux = node.FirstOperand.Accept(this, false)) is SingleIdentifierExpression)
                node.FirstOperand = (SingleIdentifierExpression)aux;
            if ((aux = node.SecondOperand.Accept(this, false)) is SingleIdentifierExpression)
                node.SecondOperand = (SingleIdentifierExpression)aux;
            return null;
        }

        #endregion

        #region Visit(NewArrayExpression node, Object obj)

        public override Object Visit(NewArrayExpression node, Object obj) {
            Object aux = null;
            if (node.Size != null) {
                node.Size.LeftExpression = node.LeftExpression;
                if ((aux = node.Size.Accept(this, false)) is SingleIdentifierExpression)
                    node.Size = (SingleIdentifierExpression)aux;
            }
            if (node.Init != null) {
                node.Init.LeftExpression = node.LeftExpression;
                node.Init.Accept(this, false);
            }
            return null;
        }

        #endregion

        #region Visit(NewExpression node, Object obj)

        public override Object Visit(NewExpression node, Object obj) {
            node.Arguments.LeftExpression = false;
            node.Arguments.Accept(this, false);
            return null;
        }

        #endregion

        #region Visit(QualifiedIdentifierExpression node, Object obj)

        public override Object Visit(QualifiedIdentifierExpression node, Object obj) {
            node.IdName.LeftExpression = node.LeftExpression;
            node.IdExpression.LeftExpression = node.LeftExpression;
            node.IdName.Accept(this, false);
            node.IdExpression.Accept(this, true);
            return null;
        }

        #endregion

        #region Visit(RelationalExpression node, Object obj)

        public override Object Visit(RelationalExpression node, Object obj) {
            Object aux = null;
            node.FirstOperand.LeftExpression = node.LeftExpression;
            node.SecondOperand.LeftExpression = node.LeftExpression;
            if ((aux = node.FirstOperand.Accept(this, false)) is SingleIdentifierExpression)
                node.FirstOperand = (SingleIdentifierExpression)aux;
            if ((aux = node.SecondOperand.Accept(this, false)) is SingleIdentifierExpression)
                node.SecondOperand = (SingleIdentifierExpression)aux;
            return null;
        }

        #endregion

        #region Visit(TernaryExpression node, Object obj)

        public override Object Visit(TernaryExpression node, Object obj) {
            Object aux = null;
            node.FirstOperand.LeftExpression = node.LeftExpression;
            node.SecondOperand.LeftExpression = node.LeftExpression;
            node.ThirdOperand.LeftExpression = node.LeftExpression;
            if ((aux = node.FirstOperand.Accept(this, false)) is SingleIdentifierExpression)
                node.FirstOperand = (SingleIdentifierExpression)aux;
            if ((aux = node.SecondOperand.Accept(this, false)) is SingleIdentifierExpression)
                node.SecondOperand = (SingleIdentifierExpression)aux;
            if ((aux = node.ThirdOperand.Accept(this, false)) is SingleIdentifierExpression)
                node.ThirdOperand = (SingleIdentifierExpression)aux;
            return null;
        }

        #endregion

        #region Visit(UnaryExpression node, Object obj)

       public override Object Visit(UnaryExpression node, Object obj)
       {
          Object aux = null;
          node.Operand.LeftExpression = node.LeftExpression;

          if ((aux = node.Operand.Accept(this, false)) is SingleIdentifierExpression)
             node.Operand = (SingleIdentifierExpression)aux;

          return null;
       }

        #endregion

        // Statements

        #region Visit(Block node, Object obj)

        /// <returns>Returns true if the last sentence is a return statement. Otherwise, false.</returns>
        public override Object Visit(Block node, Object obj) {
            bool returnFound = false;

            for (int i = 0; i < node.StatementCount; i++) {
                returnFound = false;
                Object aux = node.GetStatementElement(i).Accept(this, obj);
                if (aux is bool)
                    returnFound = (bool)aux;
            }
            return returnFound;
        }

        #endregion

        #region Visit(ReturnStatement node, Object obj)

        public override Object Visit(ReturnStatement node, Object obj) {
            Object aux = null;
            if (node.ReturnExpression != null)
                if ((aux = node.ReturnExpression.Accept(this, false)) is SingleIdentifierExpression)
                    node.ReturnExpression = (SingleIdentifierExpression)aux;
            return true;
        }

        #endregion

        #region Visit(IfElseStatement node, Object obj)

        public override Object Visit(IfElseStatement node, Object obj) {
            Object aux = null;
            if ((aux = node.Condition.Accept(this, false)) is SingleIdentifierExpression)
                node.Condition = (SingleIdentifierExpression)aux;
            SSAMap map1 = this.map.Clone();
            this.map.SetScope();

            // * Updates the current if statatement body being analyzed
            IList<SingleIdentifierExpression> previousReferencesUsedInIfElseBody = this.referencesUsedInIfElseBody;
            this.referencesUsedInIfElseBody = node.ReferencesUsedInTrueBranch;
            node.TrueBranch.Accept(this, obj);

            this.addLocalVariable(this.map.ResetScope(), node.TrueBranch);
            SSAMap map2 = this.map.Clone();
            this.map.SetScope();
            this.referencesUsedInIfElseBody = node.ReferencesUsedInFalseBranch;
            node.FalseBranch.Accept(this, obj);
            // * Updates the previous if statatement being analyzed
            this.referencesUsedInIfElseBody = previousReferencesUsedInIfElseBody;

            this.addLocalVariable(this.map.ResetScope(), node.FalseBranch);
            // map3 = this.map

            List<MoveStatement> mvSt = map1.GetMoveStatementsForIf(map2, this.map, node.Location.FileName, node.Condition.Location.Line);
            if (mvSt.Count != 0)
                node.AfterCondition = mvSt;

            SSAInfo info = new SSAInfo(map2, this.map, null, null);
            node.TrueBranch.Accept(new VisitorSSA2(), info);

            info = new SSAInfo(this.map, this.map, map2, map1);
            node.FalseBranch.Accept(new VisitorSSA2(), info);

            List<ThetaStatement> thSt;
            if (node.HaveElseBlock())
                thSt = map2.GetThetaStatements(ref this.map, map1, node.Location.FileName, node.FalseBranch.Location.Line);
            else
                thSt = map1.GetThetaStatements(ref this.map, node.Location.FileName, node.FalseBranch.Location.Line);

            if (thSt.Count != 0)
                node.ThetaStatements = thSt;

            return null;
        }

        #endregion

        #region Visit(WhileStatement node, Object obj)

        public override Object Visit(WhileStatement node, Object obj) {
            Object aux = null;
            SSAMap map1 = this.map.Clone();
            if ((aux = node.Condition.Accept(this, false)) is SingleIdentifierExpression)
                node.Condition = (SingleIdentifierExpression)aux;
            SSAMap map2 = this.map.Clone();
            this.map.SetScope();
            node.Statements.Accept(this, obj);
            this.addLocalVariable(this.map.ResetScope(), node.Statements);
            // map3 = this.map

            List<MoveStatement> mvSt = map1.GetMoveStatements(this.map, node.Location.FileName, node.Location.Line);
            if (mvSt.Count != 0)
                node.InitWhile = mvSt;

            List<ThetaStatement> thSt = map1.GetThetaStatements(map2, ref this.map, node.Location.FileName, node.Condition.Location.Line);
            if (thSt.Count != 0)
                node.BeforeCondition = thSt;

            mvSt = map1.GetMoveStatements(map2, this.map, node.Location.FileName, node.Condition.Location.Line);
            if (mvSt.Count != 0)
                node.AfterCondition = mvSt;

            SSAInfo info = new SSAInfo(null, null, map1, this.map);
            node.Condition.Accept(new VisitorSSA2(), info);

            info = new SSAInfo(this.map, null, map1, this.map);
            node.Statements.Accept(new VisitorSSA2(), info);

            return null;
        }

        #endregion

        #region Visit(ForStatement node, Object obj)

        public override Object Visit(ForStatement node, Object obj) {
            Object aux = null;
            this.map.SetScope();
            for (int i = 0; i < node.InitializerCount; i++) {
                node.GetInitializerElement(i).Accept(this, false);
            }
            SSAMap map1 = this.map.Clone();

            if ((aux = node.Condition.Accept(this, false)) is SingleIdentifierExpression)
                node.Condition = (SingleIdentifierExpression)aux;
            SSAMap map2 = this.map.Clone();

            node.Statements.Accept(this, obj);
            SSAMap map3 = this.map.Clone();

            for (int i = 0; i < node.IteratorCount; i++) {
                node.GetIteratorElement(i).Accept(this, false);
            }
            // map4 = this.map

            List<MoveStatement> mvSt = map1.GetMoveStatements(this.map, node.Location.FileName, node.Location.Line);
            if (mvSt.Count != 0)
                node.AfterInit = mvSt;

            List<ThetaStatement> thSt = map1.GetThetaStatements(map2, ref this.map, node.Location.FileName, node.Location.Line);
            if (thSt.Count != 0)
                node.BeforeCondition = thSt;

            mvSt = map1.GetMoveStatements(map2, this.map, node.Location.FileName, node.Location.Line);
            if (mvSt.Count != 0)
                node.AfterCondition = mvSt;

            SSAInfo info = new SSAInfo(null, null, map1, this.map);
            node.Condition.Accept(new VisitorSSA2(), info);

            info = new SSAInfo(this.map, null, map1, this.map);
            node.Statements.Accept(new VisitorSSA2(), info);

            info = new SSAInfo(this.map, null, map1, this.map);
            for (int i = 0; i < node.IteratorCount; i++) {
                node.GetIteratorElement(i).Accept(new VisitorSSA2(), info);
            }

            this.addLocalVariable(this.map.ResetScope(), node.AuxInitializer);
            node.UpdateInitializer();

            return null;
        }

        #endregion

        #region Visit(DoStatement node, Object obj)

        public override Object Visit(DoStatement node, Object obj) {
            Object aux = null;
            SSAMap map1 = this.map.Clone();
            this.map.SetScope();
            node.Statements.Accept(this, obj);
            this.addLocalVariable(this.map.ResetScope(), node.Statements);
            SSAMap map2 = this.map.Clone();
            if ((aux = node.Condition.Accept(this, false)) is SingleIdentifierExpression)
                node.Condition = (SingleIdentifierExpression)aux;
            // map3 = this.map

            List<MoveStatement> mvSt = map1.GetMoveStatements(this.map, node.Location.FileName, node.Location.Line);
            if (mvSt.Count != 0)
                node.InitDo = mvSt;

            List<ThetaStatement> thSt = map1.GetThetaStatements(ref this.map, node.Location.FileName, node.Statements.Location.Line);
            if (thSt.Count != 0)
                node.BeforeBody = thSt;

            SSAInfo info = new SSAInfo(this.map, null, map1, this.map);
            node.Statements.Accept(new VisitorSSA2(), info);

            info = new SSAInfo(this.map, null, map1, this.map);
            node.Condition.Accept(new VisitorSSA2(), info);

            return null;
        }

        #endregion

        #region Visit(SwitchStatement node, Object obj)

        public override Object Visit(SwitchStatement node, Object obj) {
            Object aux = null;
            List<SSAMap> mapsSwitch = new List<SSAMap>();
            int indexDefaultMap = -1;

            if ((aux = node.Condition.Accept(this, false)) is SingleIdentifierExpression)
                node.Condition = (SingleIdentifierExpression)aux;
            SSAMap map0 = this.map.Clone();

            IList<SingleIdentifierExpression> previousCaseReferences = this.referencesUsedInCaseBody;
            for (int i = 0; i < node.SwitchBlockCount; i++) {
                // * Updates the current if statatement body being analyzed
                IList<SingleIdentifierExpression> caseReferences=new List<SingleIdentifierExpression>();
                node.ReferencesUsedCases[node.GetSwitchSectionElement(i).SwitchBlock] = caseReferences;
                IList<SingleIdentifierExpression> previousReferencesUsedInCaseBody = this.referencesUsedInCaseBody;
                this.referencesUsedInCaseBody = caseReferences;

                this.map.SetScope();
                node.GetSwitchSectionElement(i).Accept(this, false);
                if (node.GetSwitchSectionElement(i).IsDefaultCase())
                    indexDefaultMap = i;
                this.addLocalVariable(this.map.ResetScope(), node.GetSwitchSectionElement(i).SwitchBlock);
                mapsSwitch.Add(this.map.Clone());
            }
            this.referencesUsedInCaseBody = previousCaseReferences;

            // lastMap => this.map

            List<MoveStatement> mvSt;
            if (indexDefaultMap == -1)
                mvSt = map0.GetMoveStatements(this.map, node.Location.FileName, node.Location.Line);
            else
                mvSt = map0.GetMoveStatementsForSwitch(mapsSwitch, node.Location.FileName, node.Location.Line);
            if (mvSt.Count != 0)
                node.AfterCondition = mvSt;


            for (int i = 0; i < node.SwitchBlockCount; i++) {
                SSAInfo info;
                if (i > 0)
                    info = new SSAInfo(mapsSwitch[i], this.map, mapsSwitch[i - 1], map0);
                else
                    info = new SSAInfo(mapsSwitch[i], this.map, null, null);
                node.GetSwitchSectionElement(i).Accept(new VisitorSSA2(), info);
            }

            List<ThetaStatement> thSt;
            if (indexDefaultMap != -1)
                thSt = map0.GetThetaStatements(mapsSwitch, ref this.map, true, node.Location.FileName, node.GetSwitchSectionElement(node.SwitchBlockCount - 1).SwitchBlock.Location.Line);
            else
                thSt = map0.GetThetaStatements(mapsSwitch, ref this.map, false, node.Location.FileName, node.GetSwitchSectionElement(node.SwitchBlockCount - 1).SwitchBlock.Location.Line);

            if (thSt.Count != 0)
                node.ThetaStatements = thSt;

            return null;
        }
        #endregion

    }
}
