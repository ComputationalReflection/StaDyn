//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: DotConstraint.cs                                                    
// Author:  Francisco Ortin - francisco.ortin@gmail.com                       
// Description:                                                               
//    Member access constraint to be executed each time the associated method
//       has been called. 
//    It represents constraints of the form:
//                 ret := op1 . memberName
//    Implements Composite pattern [Leaf].                               
//    Implements Command pattern [Concrete Command].                               
//    Implements Memento pattern [Memento].                               
// -------------------------------------------------------------------------- 
// Create date: 26-04-2007                                                    
// Modification date: 26-04-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using AST;
using ErrorManagement;
using Tools;
using TypeSystem.Operations;

namespace TypeSystem.Constraints {
    /// <summary>
    /// It represents constraints of the form:
    ///                 ret := op1 . memberName
    /// </summary>
    class DotConstraint : ConstraintAdapter {

        #region Fields
        /// <summary>
        /// The name of the attribute
        /// </summary>
        private string memberName;
        #endregion

        #region Properties
        /// <summary>
        /// The name of the attribute
        /// </summary>
        public string MemberName {
            get { return this.memberName; }
        }
        #endregion


        #region Constructor
        /// <summary>
        /// Constructor for binary attribute access operations
        /// </summary>
        /// <param name="firstOperand">WriteType of the first operand</param>
        /// <param name="memberName">">WriteType name of the attribute</param>
        /// <param name="location"> location of the method call</param>
        public DotConstraint(TypeExpression firstOperand, string memberName, Location location)
            : base(firstOperand, location) {
            this.memberName = memberName;
        }
        /// <summary>
        /// Private constructor for binary relational operations
        /// </summary>
        /// <param name="firstOperand">WriteType of the first operand</param>
        /// <param name="memberName">">WriteType name of the attribute</param>
        /// <param name="location"> location of the method call</param>
        private DotConstraint(TypeExpression firstOperand, string memberName, TypeVariable returnType, Location location)
            : base(firstOperand, returnType, location) {
            this.memberName = memberName;
        }
        #endregion

        #region BuildTypeExpressionString()
        protected override string BuildTypeExpressionString() {
            return String.Format("{0}:={1}.{2}", this.ReturnType.FullName, this.FirstOperand.FullName, this.MemberName);
        }
        #endregion

        #region CloneTypeVariables()
        /// <summary>
        /// Method that clones each type variable of a constraint.
        /// Equivalence classes are not cloned (but included in the equivalenceClasses parameter.
        /// </summary>
        /// <param name="typeVariableMappings">Each new type varaiable represent a copy of another existing one.
        /// This parameter is a mapping between them, wher tmpName=old and value=new</param>
        /// <param name="equivalenceClasses">Each equivalence class of all the type variables.</param>
        /// <returns>The new type expression (itself by default)</returns>
        public override Constraint CloneTypeVariables(IDictionary<TypeVariable, TypeVariable> typeVariableMappings, IList<EquivalenceClass> equivalenceClasses) {
            TypeExpression newFirstOperand = this.FirstOperand.CloneTypeVariables(typeVariableMappings, equivalenceClasses, new List<ClassType>());
            TypeVariable newReturnType = (TypeVariable)this.ReturnType.CloneTypeVariables(typeVariableMappings, equivalenceClasses, new List<ClassType>());
            return new DotConstraint(newFirstOperand, this.memberName, newReturnType, this.Location);
        }
        #endregion

        #region Check()
        /// <summary>
        /// Tries to unify the constraints of a method call
        /// </summary>
        /// <param name="methodAnalyzed">The method that is being analyzed when the operation is performed.</param>
        /// <param name="actualImplicitObject">Only suitable in an assignment constraint. It represents the actual object used to pass the message.</param>
        /// <param name="showInvocationMessage">To show the invocation line and column in case an error exists</param>
        /// <param name="activeSortOfUnification">The active sort of unification used (Equivalent is the default
        /// one and Incremental is used in the SSA bodies of the while, for and do statements)</param>
        /// <param name="location"> location of the method call</param>
        /// <returns>If the unification has been satisfied</returns>
        public override TypeExpression Check(MethodType methodAnalyzed, TypeExpression actualImplicitObject, bool showInvocationMessage,
                        SortOfUnification activeSortOfUnification, Location location) {
            TypeExpression result = (TypeExpression) this.FirstOperand.AcceptOperation(DotOperation.Create(this.memberName, methodAnalyzed, new List<TypeExpression>(), this.Location), null);
            if (result == null && showInvocationMessage) {
                ErrorManager.Instance.NotifyError(new ConstraintError(location));
                return null;
            }
            // * If no error exists, we unify the return type variable with the actual result
            this.ReturnType.Unify(result, SortOfUnification.Equivalent, new List<Pair<TypeExpression, TypeExpression>>());
            this.ReturnType.ValidTypeExpression = this.ValidTypeExpression = false;
            return this.ReturnType;
        }
        #endregion


    }
}
