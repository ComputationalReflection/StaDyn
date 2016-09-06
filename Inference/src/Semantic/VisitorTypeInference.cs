////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: VisitorTypeInference.cs                                              //
// Authors: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                   //
//          Francisco Ortin - francisco.ortin@gmail.com                       //
// Description:                                                               //
//    This class visits the AST to make the type inference of all expressions //
//    In method invocation, if some of the arguments or returned values are   //
//       free variables, the method is analyzed before the current one.       //
//       The unification algorithm requires that a method with free varaibles //
//       must be analyzed previosly to its invocation. We, therefore,         //
//       simulates a kind of abstract interpretation.                         //
// in the source code.                                                        //
//    Includes local variables and access to a static members.                //
//    Inheritance: VisitorAdapter                                             //
//    Implements Visitor pattern [Concrete Visitor].                          //
// -------------------------------------------------------------------------- //
// Create date: 31-01-2007                                                    //
// Modification date: 05-04-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using AST;
using ErrorManagement;
using TypeSystem;
using TypeSystem.Constraints;
using Tools;
using Semantic.SSAAlgorithm;
using DynVarManagement;
using TypeSystem.Operations;

namespace Semantic {
    /// <summary>
    /// This class visits the AST to make the type inference of all expressiones
    /// in the source code.
    /// </summary>
    /// <remarks>
    /// Inheritance: VisitorAdapter.
    /// Implements Visitor pattern [Concrete Visitor].
    /// </remarks>
    class VisitorTypeInference : VisitorAdapter {

        #region Fields

        /// <summary>
        /// To simulate the abstract interpretation analysis, all the method declarations and
        /// definitions that have been analyzed must be included in this List
        /// </summary>
        private IList<MethodDeclaration> methodsVisited = new List<MethodDeclaration>();
        private IList<MethodDeclaration> methodsAllReadyVisited = new List<MethodDeclaration>();

        /// <summary>
        /// This field specifies a stack of type of the actual returned expression. If multiple values
        /// exist, they are included in a UnionType.
        /// </summary>
        private IList<TypeExpression> actualReturnedType = new List<TypeExpression>();

        /// <summary>
        /// The type of the "this" reserved word
        /// </summary>
        private TypeExpression typeOfThis = null;

        /// <summary>
        /// The object used as the implicit parameter in a method call
        /// </summary>
        private TypeExpression actualImplicitObject = null;

        /// <summary>
        /// The kind of unification used. 
        /// Equivalent is the default unification.
        /// Incremental is used for arrays and bodies of the SSA while, for and do structures.
        /// Override is used in field assignments.
        /// </summary>
        private SortOfUnification sortOfUnification = SortOfUnification.Equivalent;
        #endregion

        // * Definitions

        #region Visit(SourceFile node, Object obj)

        public override Object Visit(SourceFile node, Object obj) {
            foreach (string key in node.Namespacekeys) {
                int count = node.GetNamespaceDefinitionCount(key);
                for (int i = 0; i < count; i++) {
                    node.GetNamespaceDeclarationElement(key, i).Accept(this, obj);
                }
            }

            for (int i = 0; i < node.DeclarationCount; i++) {
                node.GetDeclarationElement(i).Accept(this, obj);
            }

            return null;
        }

        #endregion

        #region Visit(Namespace node, Object obj)

        public override Object Visit(Namespace node, Object obj) {
            for (int i = 0; i < node.NamespaceMembersCount; i++) {
                node.GetDeclarationElement(i).Accept(this, obj);
            }
            return null;
        }

        #endregion

        #region Visit(ClassDefinition node, Object obj)

        public override Object Visit(ClassDefinition node, Object obj) {
            this.typeOfThis = (ClassType)node.TypeExpr;
            for (int i = 0; i < node.MemberCount; i++)
                node.GetMemberElement(i).Accept(this, obj);
            return null;
        }

        #endregion

        #region Visit(InterfaceDefinition node, Object obj)

        /// <summary>
        /// Visit method of the Visitor design pattern
        /// </summary>
        /// <param name="node">The node to visit</param>
        /// <param name="obj">A type expression representing the type of the implicit object</param>
        /// <returns>null</returns>
        public override Object Visit(InterfaceDefinition node, Object obj) {
            for (int i = 0; i < node.MemberCount; i++) {
                node.GetMemberElement(i).Accept(this, obj);
            }
            return null;
        }

        #endregion

        #region Visit(Definition node, Object obj)

        /// <summary>
        /// Visit method of the Visitor design pattern
        /// </summary>
        /// <param name="node">The node to visit</param>
        /// <param name="obj">A type expression representing the type of the implicit object</param>
        /// <returns>null</returns>
        public override Object Visit(Definition node, Object obj) {
            // * If the type has type variables, we create a new clone type
            if (node.TypeExpr != null && node.Symbol != null && node.TypeExpr.HasTypeVariables())
                node.TypeExpr = node.Symbol.SymbolType = node.Symbol.SymbolType.CloneType(new Dictionary<TypeVariable, TypeVariable>());
            node.Init.Accept(this, obj);
            if ((node.Init.ExpressionType != null) && (node.TypeExpr != null)) {
                node.TypeExpr.AcceptOperation( new AssignmentOperation (node.Init.ExpressionType, AssignmentOperator.Assign, this.getMethodAnalyzed(),
                    this.sortOfUnification, null, node.Location), null);
            }   

            if (node.TypeExpr != null)
                node.FrozenTypeExpression = node.TypeExpr.Freeze();
            return null;
        }

        #endregion

        /// <summary>
        /// Visit method of the Visitor design pattern
        /// </summary>
        /// <param name="node">The node to visit</param>
        /// <param name="obj">A type expression representing the type of the implicit object</param>
        /// <returns>null</returns>
        #region Visit(ConstantDefinition node, Object obj)
        public override Object Visit(ConstantDefinition node, Object obj) {
            // * If the type has type variables, we create a new clone type
            if (node.TypeExpr.HasTypeVariables())
                node.TypeExpr = node.Symbol.SymbolType = node.Symbol.SymbolType.CloneType(new Dictionary<TypeVariable, TypeVariable>());
            node.Init.Accept(this, obj);
            if ((node.Init.ExpressionType != null) && (node.TypeExpr != null)) {
                node.TypeExpr.AcceptOperation(new AssignmentOperation(node.Init.ExpressionType, AssignmentOperator.Assign, null,
                    this.sortOfUnification, null, node.Location), null);
            }
            return null;
        }
        #endregion

        /// <summary>
        /// Visit method of the Visitor design pattern
        /// </summary>
        /// <param name="node">The node to visit</param>
        /// <param name="obj">A type expression representing the type of the implicit object</param>
        /// <returns>null</returns>
        #region Visit(FieldDefinition node, Object obj)
        public override Object Visit(FieldDefinition node, Object obj) {
            node.Init.Accept(this, obj);
            if ((node.Init.ExpressionType != null) && (node.TypeExpr != null))
                node.TypeExpr.AcceptOperation(new AssignmentOperation(node.Init.ExpressionType, AssignmentOperator.Assign, null,
                    this.sortOfUnification, null, node.Location), null);
            return null;
        }
        #endregion

        /// <summary>
        /// Visit method of the Visitor design pattern
        /// </summary>
        /// <param name="node">The node to visit</param>
        /// <param name="obj">A type expression representing the type of the implicit object</param>
        /// <returns>null</returns>
        #region Visit(ConstantFieldDefinition node, Object obj)
        public override Object Visit(ConstantFieldDefinition node, Object obj) {
            node.Init.Accept(this, obj);
            if ((node.Init.ExpressionType != null) && (node.TypeExpr != null))
                node.TypeExpr.AcceptOperation( new AssignmentOperation(node.Init.ExpressionType, AssignmentOperator.Assign, null,
                    this.sortOfUnification, null, node.Location), null);
            return null;
        }
        #endregion

        #region Visit(IdDeclaration node, Object obj)
        public override Object Visit(IdDeclaration node, Object obj) {
            if (node.TypeExpr == null) // Any semantic error
                return null;
            // * If the type has type variables, we create a new clone type
            if (node.TypeExpr.HasTypeVariables())
                if (node.Symbol == null) // Any semantic error
                    return null;
                node.TypeExpr = node.Symbol.SymbolType = node.Symbol.SymbolType.CloneType(new Dictionary<TypeVariable, TypeVariable>());
            return null;
        }
        #endregion

        #region Visit(ConstructorDefinition node, object obj)
        /// <summary>
        /// We simply add the ConstructorDefinition node to the list of visited methods
        /// </summary>
        /// <param name="node">The node to visit</param>
        /// <param name="obj">A type expression representing the type of the implicit object</param>
        /// <returns>null</returns>
        public override object Visit(ConstructorDefinition node, object obj) {
            this.methodsVisited.Add(node);
            this.methodsAllReadyVisited.Add(node);
            this.typeOfThis = this.actualImplicitObject = this.getMethodAnalyzed().MemberInfo.Class;
            this.actualReturnedType.Add(null);
            // * The default visiting algorithm is enough
            Object toReturn = base.Visit(node, obj);
            this.actualReturnedType.RemoveAt(this.actualReturnedType.Count - 1);
            this.methodsVisited.RemoveAt(this.methodsVisited.Count - 1);
            return toReturn;
        }

        #endregion

        #region Visit(MethodDefinition node, object obj)
        /// <summary>
        /// We simply add the method definition node to the list of visited methods
        /// </summary>
        /// <param name="node">The node to visit</param>
        /// <param name="obj">A type expression representing the type of the implicit object</param>
        /// <returns>null</returns>
        public override object Visit(MethodDefinition node, object obj) {
            this.methodsVisited.Add(node);
            this.methodsAllReadyVisited.Add(node);
            this.actualReturnedType.Add(null);
            this.typeOfThis = this.actualImplicitObject = this.getMethodAnalyzed().MemberInfo.Class;
            // * The default visiting algorithm is enough
            Object toReturn = base.Visit(node, obj);
            MethodType methodType = (MethodType)node.TypeExpr;            
            // * If the method returns a type variable, it is substituted by a union of actual returned values
            if (methodType.Return.HasTypeVariables())
            {
                if (!methodType.MemberInfo.Modifiers.Contains(Modifier.Abstract))
                {
                    if (this.actualReturnedType[this.actualReturnedType.Count - 1] == null)
                        ErrorManager.Instance.NotifyError(new ReturnExpectedError(node.Location));
                    else
                    {
                        methodType.Return = this.actualReturnedType[this.actualReturnedType.Count - 1];
                        if (methodType.Return is UnionType && ((UnionType)methodType.Return).TypeSet.Contains(NullType.Instance))//If the return type contains the NullType, it is removed.
                        {
                            UnionType ut = (UnionType)methodType.Return;
                            ut.TypeSet.Remove(NullType.Instance);
                            if (ut.TypeSet.Count == 1)
                                methodType.Return = ut.TypeSet[0];
                        }
                    }
                }
            }
            this.actualReturnedType.RemoveAt(this.actualReturnedType.Count - 1);
            this.methodsVisited.RemoveAt(this.methodsVisited.Count - 1);
            // * Assigns the dynamism of the returned type
            /*methodType.Return.IsDynamic = node.IsReturnDynamic;*/
            // * The type has been inferred
            methodType.TypeInferred();            
            return toReturn;
        }
        #endregion

        // Expressions
        /// <summary>
        /// Visit method of the Visitor design pattern
        /// </summary>
        /// <param name="node">The node to visit</param>
        /// <param name="obj">A type expression representing the type of the implicit object</param>
        /// <returns>null</returns>
        #region Visit(ArgumentExpression node, Object obj)
        public override Object Visit(ArgumentExpression node, Object obj) {
            node.Lvalue = false;
            node.Argument.Accept(this, obj);
            node.ExpressionType = node.Argument.ExpressionType;
            return null;
        }
        #endregion

        /// <summary>
        /// Visit method of the Visitor design pattern
        /// </summary>
        /// <param name="node">The node to visit</param>
        /// <param name="obj">A type expression representing the type of the implicit object</param>
        /// <returns>null</returns>
        #region Visit(ArithmeticExpression node, Object obj)
        public override Object Visit(ArithmeticExpression node, Object obj) {
            // Resolves types
            node.FirstOperand.Accept(this, obj);
            node.SecondOperand.Accept(this, obj);
            node.Lvalue = false;

            // Check if the type can realize the operation
            if ((node.FirstOperand.ExpressionType != null) && (node.SecondOperand.ExpressionType != null))
                node.ExpressionType = (TypeExpression)node.FirstOperand.ExpressionType.AcceptOperation(ArithmeticalOperation.Create(node.SecondOperand.ExpressionType, node.Operator, getMethodAnalyzed(), true, node.Location), null);

            return null;
        }
        #endregion

        #region Visit(ArrayAccessExpression node, Object obj)
        /// <summary>
        /// Visit method of the Visitor design pattern
        /// </summary>
        /// <param name="node">The node to visit</param>
        /// <param name="obj">A type expression representing the type of the implicit object</param>
        /// <returns>null</returns>
        public override Object Visit(ArrayAccessExpression node, Object obj) {
            node.Lvalue = true;
            node.FirstOperand.Accept(this, obj);
            node.SecondOperand.Accept(this, obj);
            if ((node.FirstOperand.ExpressionType != null) && (node.SecondOperand.ExpressionType != null))
                node.ExpressionType = (TypeExpression)node.FirstOperand.ExpressionType.AcceptOperation(new SquareBracketOperation(node.SecondOperand.ExpressionType, this.getMethodAnalyzed(), true, node.Location), null);
            return null;
        }
        #endregion

        /// <summary>
        /// Visit method of the Visitor design pattern
        /// </summary>
        /// <param name="node">The node to visit</param>
        /// <param name="obj">A type expression representing the type of the implicit object</param>
        /// <returns>null</returns>
        #region Visit(AssignmentExpression node, Object obj)
        public override Object Visit(AssignmentExpression node, Object obj) {
            // Resolves types
            node.FirstOperand.Accept(this, obj);
            node.SecondOperand.Accept(this, obj);
            node.Lvalue = node.FirstOperand.Lvalue;

            // First operand has to be a lvalue
            if (!(node.FirstOperand.Lvalue)) {
                ErrorManager.Instance.NotifyError(new LValueError(node.Location));
                return null;
            }

            // * Quits if there has been any type inference error
            if (node.FirstOperand.ExpressionType == null || node.SecondOperand.ExpressionType == null)
                return null;

            // * The expression this.(attribute)+ = 3 should generate a constraint
            TypeVariable firstOperandTypeVariable = node.FirstOperand.ExpressionType as TypeVariable;
            FieldAccessExpression firstOperand = node.FirstOperand as FieldAccessExpression;
            if (firstOperand != null && firstOperandTypeVariable != null && firstOperandTypeVariable.Substitution == null) {
                // * An assignment constraint is added, postponing the type inference
                MethodType methodAnalyzed = this.getMethodAnalyzed();
                methodAnalyzed.AddConstraint(new FieldTypeVariableAssignmentConstraint(firstOperandTypeVariable, firstOperand.FieldName.Identifier, node.SecondOperand.ExpressionType, this.sortOfUnification));
                methodAnalyzed.ValidTypeExpression = false;
                return firstOperandTypeVariable;
            }
            
            // * Arrays require incremental unification, eg. var[] v; v[0]=3; v[1]='3'; v:Array(int \/ char)
            // * SSA blocks also require incremental unification
            SortOfUnification unification = (node.FirstOperand is ArrayAccessExpression && node.FirstOperand.ExpressionType.HasTypeVariables()) || this.sortOfUnification == SortOfUnification.Incremental ? SortOfUnification.Incremental : this.sortOfUnification;           
            node.ExpressionType = (TypeExpression)node.FirstOperand.ExpressionType.AcceptOperation(new AssignmentOperation(node.SecondOperand.ExpressionType, node.Operator, getMethodAnalyzed(), unification, null, node.Location), null);
            

            // * WriteType inference of the move statement (if any)
            if (node.MoveStat != null)
                node.MoveStat.Accept(this, obj);

            return null;
        }
        #endregion

        /// <summary>
        /// Visit method of the Visitor design pattern
        /// </summary>
        /// <param name="node">The node to visit</param>
        /// <param name="obj">A type expression representing the type of the implicit object</param>
        /// <returns>null</returns>
        #region Visit(BaseCallExpression node, Object obj)
        public override Object Visit(BaseCallExpression node, Object obj) {
            node.Lvalue = false;
            node.Arguments.Accept(this, obj);
            TypeExpression te = node.BaseType;
            ClassType ct = te as ClassType;
            if (ct != null && ct.BaseClass != null) {
                TypeExpression[] args = compoundExpressionToArray(node.Arguments);
                // * ct.BaseClass.Constructors.WriteType has the type of the attribute (null if there has been some error)
                // * Abstract Interpretation: If this type has type variables and it has not
                //   been visited yet, it is previosly visited
                if (ct.BaseClass.Constructors.Type != null)
                    node.ActualMethodCalled = abstractInterpretation(ct.BaseClass.Constructors.Type, args, node);
                // * We continue with the previous visit
                node.ExpressionType = (TypeExpression)node.BaseType.AcceptOperation(new ParenthesisOperation(this.getActualImplicitObject(), args, this.getMethodAnalyzed(), this.sortOfUnification, node.Location), null);
            }
            else
                ErrorManager.Instance.NotifyError(new UnknownTypeError(node.BaseType.FullName, node.Location));
            return null;
        }

        #endregion

        #region Visit(CastExpression node, Object obj)
        /// <summary>
        /// Visit method of the Visitor design pattern
        /// </summary>
        /// <param name="node">The node to visit</param>
        /// <param name="obj">A type expression representing the type of the implicit object</param>
        /// <returns>null</returns>
        public override Object Visit(CastExpression node, Object obj) {
            node.Lvalue = false;
            node.Expression.Accept(this, obj);
            if (node.Expression.ExpressionType == null)
                return null;
            if (node.CastType == null) {
                ErrorManager.Instance.NotifyError(new UnknownTypeError(node.CastId, node.Location));
                return null;
            }
            node.ExpressionType = (TypeExpression)node.Expression.ExpressionType.AcceptOperation(new CastOperation(node.CastType, this.getMethodAnalyzed(), node.Location), null);
            return null;
        }

        #endregion

        #region Visit(FieldAccessExpression node, Object obj)
        /// <summary>
        /// Visit method of the Visitor design pattern
        /// </summary>
        /// <param name="node">The node to visit</param>
        /// <param name="obj">A type expression representing the type of the implicit object</param>
        /// <returns>null</returns>
        public override Object Visit(FieldAccessExpression node, Object obj) {
            TypeExpression implicitObjectType = this.typeOfThis;

            // * If the type has been solved in the VisitorTypeDefinition (eg, System.Console, MyClass or System.Reflection)
            //   we have finished
            if (node.TypeInferredInVisitorTypeDefinition && node.ExpressionType != null)
                return null;

            // * We pass the actual implicit object as parameters
            node.Expression.Accept(this, null);
            // * We save the implicit object used in the attribute access
            this.actualImplicitObject = node.Expression.ExpressionType;

            // * Quit if there has been some error
            if (node.Expression.ExpressionType == null)
                return null;

            // * We pass the the actual implicit object as parameters
            node.FieldName.Accept(this, this.actualImplicitObject);
            node.Lvalue = node.FieldName.Lvalue;

            node.ExpressionType = node.FieldName.ExpressionType;
            // * Since it could be a field type variable, its substitution might change. We freeze it for CG.
            if (node.ExpressionType != null)
                node.FrozenTypeExpression = node.ExpressionType.Freeze();
            
            return null;
        }
        #endregion

        #region Visit(InvocationExpression node, Object obj)
        /// <summary>
        /// Visit method of the Visitor design pattern
        /// </summary>
        /// <param name="node">The node to visit</param>
        /// <param name="obj">A type expression representing the type of the implicit object</param>
        /// <returns>null</returns>
        public override Object Visit(InvocationExpression node, Object obj) {
            node.Lvalue = false;
            node.Identifier.Accept(this, obj);
            // * If some error, quit.
            if (node.Identifier.ExpressionType == null)
                return this.actualImplicitObject = null;

            // * Saves the actual implicit object (the arguments could be another method invocation)
            TypeExpression actualImplicitObject = this.actualImplicitObject;

            node.Arguments.Accept(this, obj);
            TypeExpression[] args = compoundExpressionToArray(node.Arguments);
            // * If some error, quit.
            if (args == null)
                return this.actualImplicitObject = null;

            // * Restores the actual implicit object
            this.actualImplicitObject = actualImplicitObject;

            // * node.Identifier.ExpressionType has the type of the attribute.
            // * Abstract Interpretation: If this type has type variables and it has not
            //   been visited yet, it is previosly visited
            TypeExpression method = node.ActualMethodCalled = abstractInterpretation(node.Identifier.ExpressionType, args, node);
            // * We continue with the previous visit. 
            // * Method is the actual method to be called (overload resolution).
            if (method == null) {
                ErrorManager.Instance.NotifyError(new UnknownMemberError(node.Location));
                return null;
            }
            // * Obtains the implicit object used to send the message
            node.ExpressionType = (TypeExpression)method.AcceptOperation(new ParenthesisOperation(this.getActualImplicitObject(), args, this.getMethodAnalyzed(), this.sortOfUnification, node.Location), null);
            // * Since it could be a field type variable, its substitution might change. We freeze it for CG.
            if (node.ExpressionType != null)
                node.FrozenTypeExpression = node.ExpressionType.Freeze();

            // * If there's a recursive loop, we register the observer to update the inferred type when it has finished
            MethodType invokedMethod = method as MethodType;
            if (invokedMethod != null && !invokedMethod.IsTypeInferred && node.ExpressionType is TypeVariable)
                invokedMethod.addInferredTypeObserver(this.getMethodAnalyzed(), (TypeVariable)node.ExpressionType);
            return null;
        }
        #endregion

        #region Visit(IsExpression node, Object obj)
        /// <summary>
        /// Visit method of the Visitor design pattern
        /// </summary>
        /// <param name="node">The node to visit</param>
        /// <param name="obj">A type expression representing the type of the implicit object</param>
        /// <returns>null</returns>
        public override Object Visit(IsExpression node, Object obj) {
            node.Lvalue = false;
            node.Expression.Accept(this, obj);
            node.ExpressionType = BoolType.Instance;
            return null;
        }
        #endregion

        #region Visit(LogicalExpression node, Object obj)
        /// <summary>
        /// Visit method of the Visitor design pattern
        /// </summary>
        /// <param name="node">The node to visit</param>
        /// <param name="obj">A type expression representing the type of the implicit object</param>
        /// <returns>null</returns>
        public override Object Visit(LogicalExpression node, Object obj) {
            // Resolves types
            node.FirstOperand.Accept(this, obj);
            node.SecondOperand.Accept(this, obj);
            node.Lvalue = false;

            // Check if the type can realize the operation
            if ((node.FirstOperand.ExpressionType != null) && (node.SecondOperand.ExpressionType != null)) {
                node.FirstOperand.ExpressionType.AcceptOperation(PromotionOperation.Create(BoolType.Instance, node.Operator, this.getMethodAnalyzed(), node.Location), null);
                node.SecondOperand.ExpressionType.AcceptOperation(PromotionOperation.Create(BoolType.Instance, node.Operator, this.getMethodAnalyzed(), node.Location), null);
                node.ExpressionType = BoolType.Instance;
            }
            return null;
        }
        #endregion

        #region Visit(NewArrayExpression node, Object obj)
        /// <summary>
        /// Visit method of the Visitor design pattern
        /// </summary>
        /// <param name="node">The node to visit</param>
        /// <param name="obj">A type expression representing the type of the implicit object</param>
        /// <returns>null</returns>
        public override Object Visit(NewArrayExpression node, Object obj) {
            node.Lvalue = false;
            if (node.Size != null)
                node.Size.Accept(this, obj);
            if (node.Init != null)
                node.Init.Accept(this, obj);

            string typeIdentifier = node.TypeInfo;
            while (typeIdentifier.Contains("[]"))
                typeIdentifier = typeIdentifier.Substring(0, typeIdentifier.Length - 2);
            for (int i = 0; i < node.Rank; i++)
                typeIdentifier = typeIdentifier + "[]";

            if (node.ExpressionType != null)
                node.ExpressionType.AcceptOperation(new SquareBracketOperation(node.Size.ExpressionType, this.getMethodAnalyzed(), true, node.Location), null);
            else
                ErrorManager.Instance.NotifyError(new UnknownTypeError(typeIdentifier, node.Location));

            return null;
        }
        #endregion

        #region Visit(NewExpression node, Object obj)
        /// <summary>
        /// Visit method of the Visitor design pattern
        /// </summary>
        /// <param name="node">The node to visit</param>
        /// <param name="obj">A type expression representing the type of the implicit object</param>
        /// <returns>null</returns>
        public override Object Visit(NewExpression node, Object obj) {
            node.Lvalue = false;
            if (node.NewType == null) {
                // * If the type is unknown, quits
                ErrorManager.Instance.NotifyError(new UnknownTypeError(node.Location));
                return null;
            }
            node.Arguments.Accept(this, obj);
            ClassType classType = node.NewType.AsClassType();
            if (classType == null) {
                ErrorManager.Instance.NotifyError(new UnknownTypeError(node.TypeInfo, node.Location));
                return null;
            }
            if (classType.Constructors == null && classType is BCLClassType)
                ((BCLClassType)classType).UpdateConstructors(node.Location);
            if (classType.Constructors == null) {
                ErrorManager.Instance.NotifyError(new UnknownMemberError(node.TypeInfo.ToString(), node.Location));
                return null;
            }
            TypeExpression[] args = compoundExpressionToArray(node.Arguments);
            // * ct.Constructors.WriteType has the type of the attribute (null if there has been some error)
            // * Abstract Interpretation: If this type has type variables and it has not
            //   been visited yet, it is previosly visited...
            if (classType.Constructors.Type != null) {
                TypeExpression method = node.ActualMethodCalled = abstractInterpretation(classType.Constructors.Type, args, node);
                // * ... continue with the previous visit. 
                // * Method is the actual method to be called (overload resolution)
                if (method == null)
                    return null;
                // * In a new expression, a new type with fresh variables is created
                ClassType actualImplicitObject = (ClassType)classType.CloneType(new Dictionary<TypeVariable, TypeVariable>());
                actualImplicitObject.ConcreteType = true;
                // * Then we infer the return type
                node.ExpressionType = (TypeExpression)method.AcceptOperation(new ParenthesisOperation(actualImplicitObject, args, this.getMethodAnalyzed(), this.sortOfUnification, node.Location), null);
                if (node.ExpressionType == null)
                    return null;

                ((ClassType)node.ExpressionType).ConcreteType = true;
            }
            return null;
        }
        #endregion

        #region Visit(ReturnStatement node, Object obj)
        public override Object Visit(ReturnStatement node, Object obj) {
            node.Assigns.Accept(this, obj);
            // * Gets the method we are analyzing, and saves it to be used later in the code generation phase
            MethodType methodVisited = (MethodType)this.methodsVisited[this.methodsVisited.Count - 1].TypeExpr;
            node.CurrentMethodType = methodVisited;
            // * No expression in the return
            if (node.ReturnExpression == null)
                if (methodVisited.Return != VoidType.Instance) {
                    ErrorManager.Instance.NotifyError(new ExpressionExpectedError(node.Location));
                    return null;
                }
                    // * A return with no expression
                else return null;
            // * Visits the child node
            node.ReturnExpression.Accept(this, obj);
            // * Finishes if there is some type error
            if (node.ReturnExpression.ExpressionType == null)
                return null;
            // * The returned expression should be a subtype of the formal returned type
            node.ReturnExpression.ExpressionType.AcceptOperation(PromotionOperation.Create(methodVisited.Return, this.getMethodAnalyzed(), node.Location), null);
            this.actualReturnedType[this.actualReturnedType.Count - 1] = UnionType.collect(this.actualReturnedType[this.actualReturnedType.Count - 1], node.ReturnExpression.ExpressionType);
            return null;
        }
        #endregion

        #region Visit(IfElseStatement node, Object obj)
        public override Object Visit(IfElseStatement node, Object obj) {
            // * Saves the visitor's state
            SortOfUnification previousSortOfUnification = this.sortOfUnification;
            this.sortOfUnification = SortOfUnification.Equivalent;

            // * Simplifies the set of references used in the if and else body
            SSAHelper.SimplifyReferences(node.ReferencesUsedInTrueBranch);
            SSAHelper.SimplifyReferences(node.ReferencesUsedInFalseBranch);

            // * WriteType inference of contition
            node.Condition.Accept(this, obj);
            // * The condition type must be boolean
            if (node.Condition.ExpressionType != null)
                node.Condition.ExpressionType.AcceptOperation(PromotionOperation.Create(BoolType.Instance, this.getMethodAnalyzed(), node.Location), null);
            // * WriteType inference of move statements (SSA)
            foreach (MoveStatement statement in node.AfterCondition)
                statement.Accept(this, obj);

            // * WriteType inference of true branch body
            IDictionary<SingleIdentifierExpression, TypeExpression> typeExpressionsBeforeIfElse = new Dictionary<SingleIdentifierExpression, TypeExpression>();
            SSAHelper.CloneTypesOfReferences(node.ReferencesUsedInTrueBranch, this.getMethodAnalyzed(), typeExpressionsBeforeIfElse);
            SSAHelper.CloneTypesOfReferences(node.ReferencesUsedInFalseBranch, this.getMethodAnalyzed(), typeExpressionsBeforeIfElse);
            TypeExpression typeOfThisBeforeIf = null, typeOfThisAfterIf = null, previousTypeOfThis = this.typeOfThis;
            bool methodAnalizedIsStatic = this.getMethodAnalyzed().MemberInfo.hasModifier(Modifier.Static);
            if (!methodAnalizedIsStatic) {
                typeOfThisBeforeIf = SSAHelper.CloneType(this.typeOfThis, this.getMethodAnalyzed());
                typeOfThisAfterIf = SSAHelper.CloneType(this.typeOfThis, this.getMethodAnalyzed());
                this.typeOfThis = typeOfThisBeforeIf;
            }            
            node.TrueBranch.Accept(this, obj);            

            // * WriteType inference of false branch body
            //IDictionary<SingleIdentifierExpression, TypeExpression> typeExpressionsAfterIf = Dictionary<SingleIdentifierExpression, TypeExpression>();
            //SSAHelper.CloneTypesOfReferences(node.ReferencesUsedInTrueBranch, this.getMethodAnalyzed(), typeExpressionsAfterIf);
            IDictionary<SingleIdentifierExpression, TypeExpression> typeExpressionsAfterIf = SSAHelper.GetTypesOfReferences(node.ReferencesUsedInTrueBranch);
            IDictionary<SingleIdentifierExpression, TypeExpression> typeExpressionIntersectionSet = SSAHelper.Intersection(typeExpressionsBeforeIfElse, node.ReferencesUsedInTrueBranch);
            SSAHelper.SetTypesOfReferences(typeExpressionIntersectionSet, node.ReferencesUsedInFalseBranch);
            if (!methodAnalizedIsStatic)
                this.typeOfThis = typeOfThisAfterIf;            
            node.FalseBranch.Accept(this, obj);
            
            // * Apply the union of both branches
            IDictionary<SingleIdentifierExpression, TypeExpression> typeExpressionsAfterElse = SSAHelper.GetTypesOfReferences(node.ReferencesUsedInFalseBranch);
            //IDictionary<SingleIdentifierExpression, TypeExpression> typeExpressionsAfterElse = SSAHelper.CloneTypesOfReferences(node.ReferencesUsedInFalseBranch,this.getMethodAnalyzed());
            SSAHelper.SetUnionTypesOfReferences(typeExpressionsBeforeIfElse, typeExpressionsAfterIf, typeExpressionsAfterElse, this.getMethodAnalyzed(), this.getActualImplicitObject());
            if (!methodAnalizedIsStatic) {
                SSAHelper.AssignAttributes(this.getMethodAnalyzed().MemberInfo.Class, typeOfThisBeforeIf, typeOfThisAfterIf, this.getMethodAnalyzed(), this.sortOfUnification, previousTypeOfThis, node.Location);
                // * Always must be the collectio of both references because constraints could be generated (with these two new references)
                this.typeOfThis = UnionType.collect(typeOfThisBeforeIf, typeOfThisAfterIf);
            }

            // * WriteType inference of theta function assignments (SSA)
            foreach (ThetaStatement theta in node.ThetaStatements)
                theta.Accept(this, obj);

            // * Updates the visitor's state
            this.sortOfUnification = previousSortOfUnification;                        
            return null;
        }
        #endregion

        #region Visit(WhileStatement node, Object obj)
        public override Object Visit(WhileStatement node, Object obj) {
            // * WriteType inference of initial move statements (SSA)
            foreach (MoveStatement statement in node.InitWhile)
                statement.Accept(this, obj);
            // * Condition
            node.Condition.Accept(this, obj);
            // * The condition type must be boolean
            if (node.Condition.ExpressionType != null)
                node.Condition.ExpressionType.AcceptOperation(PromotionOperation.Create(BoolType.Instance, this.getMethodAnalyzed(), node.Condition.Location), null);
            // * WriteType inference of move statements before the condition (SSA)
            foreach (MoveStatement statement in node.AfterCondition)
                statement.Accept(this, obj);

            // * Body: It is analyzed twice (type inference + type checking) using the incremental sort of unification
            int fromConstraint = this.getMethodAnalyzed().Constraints.Count;
            bool previousShowErrorMessages = ErrorManager.Instance.ShowMessages;
            ErrorManager.Instance.ShowMessages = false; // * No error mesagges
            SortOfUnification previousSortOfUnification = this.sortOfUnification;
            this.sortOfUnification = SortOfUnification.Incremental; // * Union types used in unification
            node.Statements.Accept(this, obj); // * WriteType inference
            ErrorManager.Instance.ShowMessages = previousShowErrorMessages; // * Error messages enabled
            //node.Statements.Accept(this, obj); // * WriteType checking
            this.sortOfUnification = previousSortOfUnification;

            // * WriteType inference of theta functions (SSA) must be performed at the end (when all the types have been inferred)
            foreach (ThetaStatement theta in node.BeforeCondition)
                // * WriteType inference
                theta.Accept(this, obj);
            // * Constraints satisfaction            
            ConstraintSatisfaction(this.getMethodAnalyzed(), fromConstraint, node);
            return null;
        }
        #endregion

        #region ConstraintSatisfaction()
        /// <summary>
        /// Constraints satisfaction. All the type cheching postponed when using the LHS of theta functions must be checked and inferred
        /// Constraints generated by fresh variables are maintained because the check method creates a new one
        /// </summary>
        /// <param name="method">The method whose constraints must be satisfied</param>
        /// <param name="fromConstraint">The number of the first constraint to be satisfied</param>
        /// <param name="node">The AST node</param>
        private void ConstraintSatisfaction(MethodType method, int fromConstraint, AstNode node) {
            if (method == null)
                return;
            int currentNumberOfConstraints = method.Constraints.Count;
            for (int i = fromConstraint; i < currentNumberOfConstraints; i++) {
                Constraint constraint = method.Constraints[fromConstraint];
                // * The constraint is removed (if it has been applied to a fresh variable, a new constraint has been generated)
                method.RemoveConstraint(fromConstraint);
                // * Checks if there is some error
                constraint.Check(method, this.getActualImplicitObject(), false, this.sortOfUnification, node.Location);
            }           
        }
        #endregion

        #region Visit(ForStatement node, Object obj)
        public override Object Visit(ForStatement node, Object obj) {
            // * The initialization block
            for (int i = 0; i < node.InitializerCount; i++)
                node.GetInitializerElement(i).Accept(this, obj);
            // * WriteType inference of initial move statements (SSA)
            foreach (MoveStatement moveStatement in node.AfterInit)
                moveStatement.Accept(this, obj);

            // * Condition, Body and Iterator: They are analyzed twice (type inference + type checking) using the incremental sort of unification
            int fromConstraint = this.getMethodAnalyzed().Constraints.Count;
            bool previousShowErrorMessages = ErrorManager.Instance.ShowMessages;
            ErrorManager.Instance.ShowMessages = false; // * No error mesagges
            SortOfUnification previousSortOfUnification = this.sortOfUnification;
            this.sortOfUnification = SortOfUnification.Incremental; // * Union types used in unification
            // * WriteType inference
            node.Condition.Accept(this, obj);
            if (node.Condition.ExpressionType != null)
                // * The condition type must be boolean
                node.Condition.ExpressionType.AcceptOperation(PromotionOperation.Create(BoolType.Instance, this.getMethodAnalyzed(), node.Condition.Location), null);
            node.Statements.Accept(this, obj);
            for (int i = 0; i < node.IteratorCount; i++)
                node.GetIteratorElement(i).Accept(this, obj);
            // * WriteType checking
            ErrorManager.Instance.ShowMessages = previousShowErrorMessages; // * Error messages enabled
            node.Condition.Accept(this, obj);
            node.Statements.Accept(this, obj);
            for (int i = 0; i < node.IteratorCount; i++)
                node.GetIteratorElement(i).Accept(this, obj);
            this.sortOfUnification = previousSortOfUnification;

            // * The inference of move statements of the condition (SSA)
            foreach (MoveStatement moveStatement in node.AfterCondition)
                moveStatement.Accept(this, obj);
            // * WriteType inference of theta functions (SSA) must be performed at the end (when all the types have been inferred)
            foreach (ThetaStatement thetaStatement in node.BeforeCondition)
                thetaStatement.Accept(this, obj);
            // * Constraints satisfaction
            ConstraintSatisfaction(this.getMethodAnalyzed(), fromConstraint, node);            
            return null;
        }
        #endregion

        #region Visit(DoStatement node, Object obj)
        public override Object Visit(DoStatement node, Object obj) {
            // * WriteType inference of initial move statements (SSA)
            foreach (MoveStatement moveStatement in node.InitDo)
                moveStatement.Accept(this, obj);

            // * Body and condition: They are analyzed twice (type inference + type checking) using the incremental sort of unification
            int fromConstraint = this.getMethodAnalyzed().Constraints.Count;
            bool previousShowErrorMessages = ErrorManager.Instance.ShowMessages;
            ErrorManager.Instance.ShowMessages = false; // * No error mesagges
            SortOfUnification previousSortOfUnification = this.sortOfUnification;
            this.sortOfUnification = SortOfUnification.Incremental; // * Union types used in unification
            // * WriteType inference
            node.Statements.Accept(this, obj);
            node.Condition.Accept(this, obj);
            if (node.Condition.ExpressionType != null)
                // * The condition type must be boolean
                node.Condition.ExpressionType.AcceptOperation(PromotionOperation.Create(BoolType.Instance, this.getMethodAnalyzed(), node.Condition.Location), null);
            // * WriteType checking
            ErrorManager.Instance.ShowMessages = previousShowErrorMessages; // * Error messages enabled
            node.Statements.Accept(this, obj);
            node.Condition.Accept(this, obj);
            this.sortOfUnification = previousSortOfUnification;

            // * The condition type must be boolean
            if (node.Condition.ExpressionType != null)
                node.Condition.ExpressionType.AcceptOperation(PromotionOperation.Create(BoolType.Instance, this.getMethodAnalyzed(), node.Condition.Location), null);
            // * WriteType inference of theta functions (SSA) must be performed at the end (when all the types have been inferred)
            foreach (ThetaStatement thetaStatement in node.BeforeBody)
                thetaStatement.Accept(this, obj);
            // * Constraint satisfaction
            this.ConstraintSatisfaction(this.getMethodAnalyzed(), fromConstraint, node);
            return null;
        }
        #endregion

        #region Visit(SwitchStatement node, Object obj)
        public override Object Visit(SwitchStatement node, Object obj) {
            // * Saves the previous sort of unification
            SortOfUnification previousSortOfUnification = this.sortOfUnification;
            this.sortOfUnification = SortOfUnification.Equivalent;
            bool previousShowErrorMessages = ErrorManager.Instance.ShowMessages;
            ErrorManager.Instance.ShowMessages = true;

            // * Simplifies the set of references used in the if and else body
            foreach (IList<SingleIdentifierExpression> references in node.ReferencesUsedCases.Values)
                SSAHelper.SimplifyReferences(references);

            // * The condition
            node.Condition.Accept(this, obj);
            // * The condition type must be int (string is not taken into account)
            if (node.Condition.ExpressionType != null)
                node.Condition.ExpressionType.AcceptOperation(PromotionOperation.Create(IntType.Instance, this.getMethodAnalyzed(), node.Condition.Location), null);
            // * WriteType inference of initial move statements (SSA)
            foreach (MoveStatement moveStatement in node.AfterCondition)
                moveStatement.Accept(this, obj);

            // * Each case is visited

            // * We get all the reference types used in the cases
            IDictionary<Block, IDictionary<SingleIdentifierExpression, TypeExpression>> typeExpressionsBeforeSwitch = new Dictionary<Block, IDictionary<SingleIdentifierExpression, TypeExpression>>(),
                                                                  typeExpressionsAfterCases = new Dictionary<Block, IDictionary<SingleIdentifierExpression, TypeExpression>>();
            IList<TypeExpression> typesOfThisAfterCases = new List<TypeExpression>();
            for (int i = 0; i < node.SwitchBlockCount; i++) {
                IDictionary<SingleIdentifierExpression, TypeExpression> typesBeforeSwitch = SSAHelper.GetTypesOfReferences(node.ReferencesUsedCases[node.GetSwitchSectionElement(i).SwitchBlock]);
                typeExpressionsBeforeSwitch[node.GetSwitchSectionElement(i).SwitchBlock] = typesBeforeSwitch;
            }
            TypeExpression typeOfThisBeforeSwitch = null;
            bool methodAnalizedIsStatic = this.getMethodAnalyzed().MemberInfo.hasModifier(Modifier.Static);
            if (!methodAnalizedIsStatic)
                typeOfThisBeforeSwitch = this.typeOfThis;
            // * Case bodies
            for (int i = 0; i < node.SwitchBlockCount; i++) {
                // * Clone reference types in the case body
                IDictionary<SingleIdentifierExpression, TypeExpression> typeExpressionsBeforeCase = new Dictionary<SingleIdentifierExpression, TypeExpression>();
                SSAHelper.CloneTypesOfReferences(typeExpressionsBeforeSwitch[node.GetSwitchSectionElement(i).SwitchBlock], this.getMethodAnalyzed(), typeExpressionsBeforeCase);
                SSAHelper.SetTypesOfReferences(typeExpressionsBeforeCase, node.ReferencesUsedCases[node.GetSwitchSectionElement(i).SwitchBlock]);
                if (!methodAnalizedIsStatic)
                    this.typeOfThis = SSAHelper.CloneType(typeOfThisBeforeSwitch, this.getMethodAnalyzed());

                node.GetSwitchSectionElement(i).Accept(this, obj);

                // * Gets the modified references
                IDictionary<SingleIdentifierExpression, TypeExpression> typesAfterCase = SSAHelper.GetTypesOfReferences(node.ReferencesUsedCases[node.GetSwitchSectionElement(i).SwitchBlock]);
                typeExpressionsAfterCases[node.GetSwitchSectionElement(i).SwitchBlock] = typesAfterCase;
                if (!methodAnalizedIsStatic)
                    typesOfThisAfterCases.Add(this.typeOfThis);
            }
            // * Gets the references that have been used in all the cases (only if default exists)
            IDictionary<SingleIdentifierExpression, TypeExpression> typeExpressionIntersectionSet = null;
            if (node.GetSwitchSectionElement(node.SwitchBlockCount - 1).LabelSection[0].SwitchSectionType == SectionType.Default)
                for (int i = 0; i < node.SwitchBlockCount; i++)
                    if (typeExpressionIntersectionSet == null)
                        typeExpressionIntersectionSet = SSAHelper.GetTypesOfReferences(node.ReferencesUsedCases[node.GetSwitchSectionElement(i).SwitchBlock]);
                    else
                        typeExpressionIntersectionSet = SSAHelper.Intersection(typeExpressionIntersectionSet, node.ReferencesUsedCases[node.GetSwitchSectionElement(i).SwitchBlock]);
            // * Sets the union types of references
            SSAHelper.SetUnionTypesOfReferences(typeExpressionsBeforeSwitch, typeExpressionsAfterCases, typeExpressionIntersectionSet, this.getMethodAnalyzed(), this.getActualImplicitObject());
            // * Sets the union types of this reference
            if (!methodAnalizedIsStatic) {
                this.typeOfThis = typeOfThisBeforeSwitch;
                foreach (TypeExpression typeOfThis in typesOfThisAfterCases)
                    this.typeOfThis = UnionType.collect(this.typeOfThis, typeOfThis);
                SSAHelper.AssignAttributes(this.getMethodAnalyzed().MemberInfo.Class, typesOfThisAfterCases, this.getMethodAnalyzed(), this.sortOfUnification, typeOfThisBeforeSwitch, node.Location);
            }

            // * Theta assignments
            foreach (ThetaStatement thetaStatement in node.ThetaStatements)
                thetaStatement.Accept(this, obj);

            // * Updates the visitor's state
            this.sortOfUnification = previousSortOfUnification;
            ErrorManager.Instance.ShowMessages = previousShowErrorMessages;
            return null;
        }
        #endregion

        #region Visit(SwitchSection node, Object obj)
        public override Object Visit(SwitchSection node, Object obj) {
            // * Every expression in each case must be int (string is not taken into account)
            foreach (SwitchLabel switchLabel in node.LabelSection) {
                switchLabel.Accept(this, obj);
                // * The condition type must be int (string is not taken into account)
                if (switchLabel.Condition != null && switchLabel.Condition.ExpressionType != null)
                    switchLabel.Condition.ExpressionType.AcceptOperation(PromotionOperation.Create(IntType.Instance, this.getMethodAnalyzed(), switchLabel.Condition.Location), null);
            }
            // * Block statements
            for (int i = 0; i < node.SwitchBlock.StatementCount; i++)
                node.SwitchBlock.GetStatementElement(i).Accept(this, obj);
            return null;
        }
        #endregion


        #region Visit(RelationalExpression node, Object obj)
        /// <summary>
        /// Visit method of the Visitor design pattern
        /// </summary>
        /// <param name="node">The node to visit</param>
        /// <param name="obj">A type expression representing the type of the implicit object</param>
        /// <returns>null</returns>
        public override Object Visit(RelationalExpression node, Object obj) {
            // Resolves types
            node.FirstOperand.Accept(this, obj);
            node.SecondOperand.Accept(this, obj);
            node.Lvalue = false;

            // Check if the type can realize the operation
            if ((node.FirstOperand.ExpressionType != null) && (node.SecondOperand.ExpressionType != null))
            {
                node.FirstOperand.ExpressionType.AcceptOperation(new RelationalOperation(node.SecondOperand.ExpressionType, node.Operator, this.getMethodAnalyzed(), true, node.Location), null);
                node.ExpressionType = BoolType.Instance;
            }
            return null;
        }

        #endregion

        #region Visit(TernaryExpression node, Object obj)
        /// <summary>
        /// Visit method of the Visitor design pattern
        /// </summary>
        /// <param name="node">The node to visit</param>
        /// <param name="obj">A type expression representing the type of the implicit object</param>
        /// <returns>null</returns>
        public override Object Visit(TernaryExpression node, Object obj) {
            node.Lvalue = false;

            node.FirstOperand.Accept(this, obj);
            if (node.FirstOperand.ExpressionType == null)
                // * Previous errors
                return null;

            // * First operand must be bool
            node.FirstOperand.ExpressionType.AcceptOperation(PromotionOperation.Create(BoolType.Instance, TernaryOperator.Question, this.getMethodAnalyzed(), node.Location), null);

            node.SecondOperand.Accept(this, obj);
            node.ThirdOperand.Accept(this, obj);

            if (node.SecondOperand.ExpressionType == null || node.ThirdOperand.ExpressionType == null)
                // * Previous errors
                return null;

            // * Second and third operand must promote one to the other
            if ((int)node.SecondOperand.ExpressionType.AcceptOperation(new PromotionLevelOperation(node.ThirdOperand.ExpressionType), null) != -1) {
                // * The second promotes to the third one
                node.ExpressionType = node.ThirdOperand.ExpressionType;
                return null;
            }
            // * If the second does not promoto to the third, then the opposite is necessary
            if ((int)node.ThirdOperand.ExpressionType.AcceptOperation(new PromotionLevelOperation(node.SecondOperand.ExpressionType), null) != -1) {
                // * The third promotes to the second one
                node.ExpressionType = node.SecondOperand.ExpressionType;
                return null;
            }
            ErrorManager.Instance.NotifyError(new TernaryError(node.SecondOperand.ExpressionType.FullName, node.ThirdOperand.ExpressionType.FullName, node.Location));
            return null;
        }
        #endregion

        #region Visit(UnaryExpression node, Object obj)
        /// <summary>
        /// Visit method of the Visitor design pattern
        /// </summary>
        /// <param name="node">The node to visit</param>
        /// <param name="obj">A type expression representing the type of the implicit object</param>
        /// <returns>null</returns>
        public override Object Visit(UnaryExpression node, Object obj) {
            node.Lvalue = false;
            node.Operand.Accept(this, obj);
            if (node.Operand.ExpressionType == null)
                return null;
            // * Operator ~ requires the operand to be int 
            if (node.Operator == UnaryOperator.BitwiseNot) {
                node.Operand.ExpressionType.AcceptOperation(PromotionOperation.Create(IntType.Instance, node.Operator, this.getMethodAnalyzed(), node.Location), null);
                node.ExpressionType = IntType.Instance;
                return null;
            }
            // * Operator ! requires the operand to be bool
            if (node.Operator == UnaryOperator.Not) {
                node.Operand.ExpressionType.AcceptOperation(PromotionOperation.Create(BoolType.Instance, node.Operator, this.getMethodAnalyzed(), node.Location), null);
                node.ExpressionType = BoolType.Instance;
                return null;
            }
            // * Operators ++ -- + - require the operand to be arithmetic
            //node.ExpressionType = node.Operand.ExpressionType.Arithmetic(node.Operator, this.getMethodAnalyzed(), true, node.Location);
            node.ExpressionType = (TypeExpression)node.Operand.ExpressionType.AcceptOperation(ArithmeticalOperation.Create(node.Operator, this.getMethodAnalyzed(), true, node.Location), null);
            return null;
        }
        #endregion

        #region Visit(BitwiseExpression node, Object, obj)
        /// <summary>
        /// Visit method for the bitwise expressions
        /// </summary>
        /// <param name="node">The node to visit</param>
        /// <param name="obj">A type expression representing the type of the implicit object</param>
        /// <returns>null</returns>
        public override object Visit(BitwiseExpression node, object obj) {
            node.Lvalue = false;
            node.FirstOperand.Accept(this, obj);
            node.SecondOperand.Accept(this, obj);
            if (node.FirstOperand.ExpressionType == null || node.SecondOperand.ExpressionType == null)
                return null;
            // * Operators | & ^ infer types the same as the arithmetical ones
            if (node.Operator == BitwiseOperator.BitwiseAnd || node.Operator == BitwiseOperator.BitwiseOr || node.Operator == BitwiseOperator.BitwiseXOr) {
                node.ExpressionType = (TypeExpression)node.FirstOperand.ExpressionType.AcceptOperation(ArithmeticalOperation.Create(node.SecondOperand.ExpressionType, node.Operator, this.getMethodAnalyzed(), true, node.Location), null);
                return null;
            }
            // * Operators << and >> require both operands to be int
            node.FirstOperand.ExpressionType.AcceptOperation(PromotionOperation.Create(IntType.Instance, node.Operator, this.getMethodAnalyzed(), node.Location), null);
            node.SecondOperand.ExpressionType.AcceptOperation(PromotionOperation.Create(IntType.Instance, node.Operator, this.getMethodAnalyzed(), node.Location), null);
            node.ExpressionType = IntType.Instance;
            return null;
        }
        #endregion

        #region Visit(ThetaStatement node, object obj)
        public override object Visit(ThetaStatement node, object obj) {
            // * WriteType infernece of the LHS in the assignment
            node.ThetaId.Accept(this, obj);

            // * WriteType inference of the parameters
            TypeExpression unionType = null;
            foreach (SingleIdentifierExpression parameter in node.ThetaList) {
                parameter.Accept(this, obj);
                unionType = UnionType.collect(unionType, parameter.ExpressionType);
                DynVarOptions.Instance.AssignDynamism(unionType, parameter.ExpressionType.IsDynamic);
            }

            // * The assignment
            node.ThetaId.ExpressionType.AcceptOperation(new AssignmentOperation(unionType, AssignmentOperator.Assign, this.getMethodAnalyzed(), SortOfUnification.Override, null, node.Location), null);
            return null;
        }
        #endregion

        // Literals

        #region Visit(BaseExpression node, Object obj)
        /// <summary>
        /// Visit method of the Visitor design pattern
        /// </summary>
        /// <param name="node">The node to visit</param>
        /// <param name="obj">A type expression representing the type of the implicit object</param>
        /// <returns>null</returns>
        public override Object Visit(BaseExpression node, Object obj) {
            node.Lvalue = false;
            // * Already inferred in VisitorTypeDefinition
            return null;
        }
        #endregion

        #region Visit(BoolLiteralExpression node, Object obj)
        /// <summary>
        /// Visit method of the Visitor design pattern
        /// </summary>
        /// <param name="node">The node to visit</param>
        /// <param name="obj">A type expression representing the type of the implicit object</param>
        /// <returns>null</returns>
        public override Object Visit(BoolLiteralExpression node, Object obj) {
            node.ExpressionType = BoolType.Instance;
            node.Lvalue = false;
            return null;
        }
        #endregion

        #region Visit(CharLiteralExpression node, Object obj)
        /// <summary>
        /// Visit method of the Visitor design pattern
        /// </summary>
        /// <param name="node">The node to visit</param>
        /// <param name="obj">A type expression representing the type of the implicit object</param>
        /// <returns>null</returns>
        public override Object Visit(CharLiteralExpression node, Object obj) {
            node.ExpressionType = CharType.Instance;
            node.Lvalue = false;
            return null;
        }
        #endregion

        #region Visit(DoubleLiteralExpression node, Object obj)
        /// <summary>
        /// Visit method of the Visitor design pattern
        /// </summary>
        /// <param name="node">The node to visit</param>
        /// <param name="obj">A type expression representing the type of the implicit object</param>
        /// <returns>null</returns>
        public override Object Visit(DoubleLiteralExpression node, Object obj) {
            node.ExpressionType = DoubleType.Instance;
            node.Lvalue = false;
            return null;
        }
        #endregion

        #region Visit(IntLiteralExpression node, Object obj)
        /// <summary>
        /// Visit method of the Visitor design pattern
        /// </summary>
        /// <param name="node">The node to visit</param>
        /// <param name="obj">A type expression representing the type of the implicit object</param>
        /// <returns>null</returns>
        public override Object Visit(IntLiteralExpression node, Object obj) {
            node.ExpressionType = IntType.Instance;
            node.Lvalue = false;
            return null;
        }
        #endregion

        #region Visit(NullExpression node, Object obj)
        /// <summary>
        /// Visit method of the Visitor design pattern
        /// </summary>
        /// <param name="node">The node to visit</param>
        /// <param name="obj">A type expression representing the type of the implicit object</param>
        /// <returns>null</returns>
        public override Object Visit(NullExpression node, Object obj) {
            node.Lvalue = false;
            node.ExpressionType = NullType.Instance;
            return null;
        }
        #endregion

        #region Visit(SingleIdentifierExpression node, Object obj)
        /// <summary>
        /// Visit method of the Visitor design pattern
        /// </summary>
        /// <param name="node">The node to visit</param>
        /// <param name="obj">A type expression representing the type of the implicit object</param>
        /// <returns>null</returns>
        public override Object Visit(SingleIdentifierExpression node, Object obj) {
            TypeExpression typeOfThis = this.typeOfThis;
            // * Inherited attribute: The type of the implicit object
            TypeExpression actualImplicitObject = (TypeExpression)obj;

            node.Lvalue = isLValue(typeOfThis);

            // * Is it a type (in a static attribute access)?
            if (node.ExpressionType != null && node.IdMode == IdentifierMode.UserType)
                // * Everythings OK
                return null;

            // * Is it a namespace (in a qualified name use)?
            if (node.ExpressionType != null && node.IdMode == IdentifierMode.NameSpace)
                // * Everythings OK
                return null;

            // * Has been used a field access operation (obj.attribute)?
            //   actualImplicitObject is the actual object used to access its attribute
            if (actualImplicitObject != null && actualImplicitObject != typeOfThis) {
                node.ExpressionType = (TypeExpression)actualImplicitObject.AcceptOperation(DotOperation.Create(node.Identifier, this.getMethodAnalyzed(), new List<TypeExpression>(), node.Location), null);
                // * Since it could be a field type variable, its substitution might change. We freeze it for CG.
                if (node.ExpressionType != null)
                    node.FrozenTypeExpression = node.ExpressionType.Freeze();
                // * Is dynamic?
                if (node.IdSymbol != null)
                    DynVarOptions.Instance.AssignDynamism(node.ExpressionType, node.IdSymbol.IsDynamic);
                return null;
            }
            // * A local variable
            else if (actualImplicitObject == null && node.IdSymbol != null && !(node.IdSymbol.SymbolType is FieldType || node.IdSymbol.SymbolType is MethodType || node.IdSymbol.SymbolType is PropertyType)) {
                // * Assigns the symbol typen
                node.ExpressionType = node.IdSymbol.SymbolType;
                if (node.ExpressionType != null) {
                    node.FrozenTypeExpression = node.ExpressionType.Freeze();
                    FieldType aux = node.FrozenTypeExpression as FieldType;
                    if (aux != null)
                        node.FrozenTypeExpression = aux.FieldTypeExpression;
                }
                // * If the symbol is dynamic, so it is the type
                DynVarOptions.Instance.AssignDynamism(node.ExpressionType, node.IdSymbol.IsDynamic);
                if (node.ExpressionType.HasTypeVariables())
                    node.ExpressionType.ValidTypeExpression = false;
                if (node.IdSymbol.Scope == 1) // * Class scope
                    // * The reserved word this is the actual implicit object
                    this.actualImplicitObject = typeOfThis;
            }
            else {
                // * The reserved word this is the actual implicit object
                this.actualImplicitObject = typeOfThis;
                if (node.ExpressionType == null) {// Searches as a attribute 
                    node.ExpressionType = (TypeExpression)typeOfThis.AcceptOperation(DotOperation.Create(node.Identifier, this.getMethodAnalyzed(), new List<TypeExpression>(), node.Location), null);
                    // * Since it could be a field type variable, its substitution might change. We freeze it for CG.
                    if (node.ExpressionType != null)
                        node.FrozenTypeExpression = node.ExpressionType.Freeze();
                }
            }
            if (node.ExpressionType == null)
                if (node.IdSymbol != null && node.IdSymbol.SymbolType != null)
                    ErrorManager.Instance.NotifyError(new UnknownMemberError(node.IdSymbol.SymbolType.FullName, node.Identifier, node.Location));
                else
                    ErrorManager.Instance.NotifyError(new UnknownIDError(node.Identifier, node.Location));
            return null;
        }
        #endregion

        #region Visit(StringLiteralExpression node, Object obj)
        /// <summary>
        /// Visit method of the Visitor design pattern
        /// </summary>
        /// <param name="node">The node to visit</param>
        /// <param name="obj">A type expression representing the type of the implicit object</param>
        /// <returns>null</returns>
        public override Object Visit(StringLiteralExpression node, Object obj) {
            node.ExpressionType = StringType.Instance;
            node.Lvalue = false;
            return null;
        }
        #endregion

        #region Visit(ThisExpression node, Object obj)
        /// <summary>
        /// Visit method of the Visitor design pattern
        /// </summary>
        /// <param name="node">The node to visit</param>
        /// <param name="obj">A type expression representing the type of the implicit object</param>
        /// <returns>null</returns>
        public override Object Visit(ThisExpression node, Object obj) {
            node.Lvalue = false;
            MethodType method = this.getMethodAnalyzed();
            if (method == null)
                return null;
            this.actualImplicitObject = this.getMethodAnalyzed().MemberInfo.Class;
            if (method.MemberInfo.hasModifier(Modifier.Static))
                ErrorManager.Instance.NotifyError(new ThisWordFromStaticMethodError(method.MemberInfo.MemberIdentifier, node.Location));
            // * WriteType already inferred in VisitorTypeDefinition
            node.ExpressionType = this.typeOfThis;
            return null;
        }
        #endregion

        // Helper Methods

        #region compoundExpressionToArray()
        /// <summary>
        /// Gets the type expression of the arguments.
        /// </summary>
        /// <param name="args">Arguments information.</param>
        /// <returns>Returns the argument type expressions </returns>
        public TypeExpression[] compoundExpressionToArray(CompoundExpression args) {
            TypeExpression[] aux = new TypeExpression[args.ExpressionCount];
            TypeExpression te;

            for (int i = 0; i < args.ExpressionCount; i++) {
                if ((te = args.GetExpressionElement(i).ExpressionType) != null)
                    aux[i] = te;
                else
                    return null;
            }
            return aux;
        }
        #endregion


        #region isLValue()
        /// <summary>
        /// To know if a type can be in the left hand side of an assignment
        /// </summary>
        /// <param name="typeExpression">The type of the left hand side</param>
        /// <returns>Whether it is a lvalue or not</returns>
        private static bool isLValue(TypeExpression typeExpression) {
            // * In case it is a property, it must be CanWrite
            PropertyType property = typeExpression as PropertyType;
            if (property != null)
                // * It is a lvalue if the property is writable
                if (property.MemberInfo.hasModifier(Modifier.CanWrite))
                    return true;
                else
                    return false;
            return true;
        }
        #endregion


        #region abstactInterpretation()
        /// <summary>
        /// If this type has type variables and it has not
        /// been visited yet, it is previosly visited
        /// </summary>
        /// <param name="memberTypeExpression">The method type expression to be visited</param>
        /// <param name="arguments">The actual arguments of method call (once visited)</param>
        /// <returns>The method type once overload is resolved</returns>
        private TypeExpression abstractInterpretation(TypeExpression memberTypeExpression, TypeExpression[] arguments, AstNode node) {
            if (memberTypeExpression is FieldType || memberTypeExpression is PropertyType || memberTypeExpression is NameSpaceType)
                // * No abstract interpretation in fields, properties or namespaces 
                return null;

            // * Is it is a class?
            ClassType ct = memberTypeExpression as ClassType;
            if (ct != null)
                // * If it is a class, we must take its constructors
                memberTypeExpression = ct.Constructors.Type;

            if (!(memberTypeExpression is MethodType)) {
                // * WriteType variable
                if (memberTypeExpression is TypeVariable)
                    return memberTypeExpression;

                // * Intersection type?
                IntersectionType it = memberTypeExpression as IntersectionType;
                if (it != null)
                    // * Overload resolution
                    memberTypeExpression = it.overloadResolution(arguments, node.Location);
                if (memberTypeExpression == null)
                    // * An error with the paramenters has ocurred (no abstract interpretation)
                    return null;

                // * Union type?
                UnionType unionType = memberTypeExpression as UnionType;
                if (unionType != null) {
                    // * Recursive abstract interpretation
                    TypeExpression returnUnionType = null;
                    foreach (TypeExpression type in unionType.TypeSet)
                        returnUnionType = UnionType.collect(returnUnionType, this.abstractInterpretation(type, arguments, node));
                    return returnUnionType;
                }
            }

            MethodType mte = memberTypeExpression as MethodType;

            // * The corresponding ASTNode is taken
            MethodDefinition md = mte.ASTNode as MethodDefinition;
            if (md == null)
                // * No abstract interpretation in method declarations (abstract or interfaces)
                return mte;
            
            if (this.methodsAllReadyVisited.Contains(md))
                // * Already visited, let's exit
                return mte;

            // * If the method has no type variables, no abstract interpretation is needed
            if (!mte.HasTypeVariables())
                return mte;


            // * Resets the error messages state and the unification state
            bool previousShowErrorMessagesState = ErrorManager.Instance.ShowMessages;
            ErrorManager.Instance.ShowMessages = true;
            SortOfUnification previousSortOfUnificationState = this.sortOfUnification;
            this.sortOfUnification = SortOfUnification.Equivalent;
            TypeExpression actualImplicitObject = this.actualImplicitObject;
            TypeExpression typeOfThis = this.typeOfThis;
            this.typeOfThis = mte.MemberInfo.Class;

            // * Let's visit the appropiate MethodDefinition AST Node

            md.Accept(this, null);

            // * Sets the Error manager to its previous state
            ErrorManager.Instance.ShowMessages = previousShowErrorMessagesState;
            this.sortOfUnification = previousSortOfUnificationState;
            this.actualImplicitObject = actualImplicitObject;
            this.typeOfThis = typeOfThis;

            // * Returns the method
            return mte;
        }
        #endregion


        #region getActualImplicitObject()
        /// <summary>
        /// Gets the current implicit object used to pass a message.
        /// If it is a type variable, it gets its substitution.
        /// </summary>
        /// <returns>The actual implicit object to pass a message; null if it is n/a</returns>
        private TypeExpression getActualImplicitObject() {
            // * Class WriteType
            if (this.actualImplicitObject is ClassType)
                return this.actualImplicitObject;
            // * Class WriteType Proxy
            ClassTypeProxy proxy = this.actualImplicitObject as ClassTypeProxy;
            if (proxy != null)
                return proxy.RealType;
            // * WriteType variable
            TypeVariable typeVariable = this.actualImplicitObject as TypeVariable;
            if (typeVariable != null) {
                if (typeVariable.Substitution != null) {
                    this.actualImplicitObject = typeVariable.Substitution;
                    return getActualImplicitObject();
                }
                return typeVariable;
            }
            // * Field WriteType
            FieldType field = this.actualImplicitObject as FieldType;
            if (field != null) {
                this.actualImplicitObject = field.FieldTypeExpression;
                return getActualImplicitObject();
            }
            return this.actualImplicitObject;
        }
        #endregion

        #region getMethodAnalyzed()
        /// <summary>
        /// To know which is the method that is being analyzed.
        /// This is used for the generation of constraints
        /// </summary>
        /// <returns>The method being analyzed; null if n/a</returns>
        private MethodType getMethodAnalyzed() {
            return methodsVisited.Count == 0 ? null : (MethodType)this.methodsVisited[methodsVisited.Count - 1].TypeExpr;
        }
        #endregion


        /// <summary>
        /// Tells if the parameter has any statement.
        /// Used in the if statement.
        /// </summary>
        /// <param name="statement">The statement</param>
        /// <returns>If the statement is empty</returns>
        private static bool isNullBlock(Statement statement) {
            if (statement == null)
                return true;
            Block block = statement as Block;
            if (block == null)
                return false;
            return block.StatementCount == 0;
        }

    }
}
