//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: CloneConstraint.cs                                                    
// Author:  Francisco Ortin - francisco.ortin@gmail.com                       
// Description:                                                               
//    A postponed clone operation for SSA algoritm.
//    It represents constraints of the form:
//                 ret := Clone(op1)
//    Implements Composite pattern [Leaf].                               
//    Implements Command pattern [Concrete Command].                               
//    Implements Memento pattern [Memento].                               
// -------------------------------------------------------------------------- 
// Create date: 11-06-2007                                                    
// Modification date: 1-06-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using AST;
using ErrorManagement;
using Tools;
using Semantic.SSAAlgorithm;

namespace TypeSystem.Constraints {
    /// <summary>
    //    It represents constraints of the form:
    //                 ret := Clone(op1)
    /// </summary>
    class CloneConstraint : ConstraintAdapter {

        #region Fields
        #endregion

        #region Properties
        #endregion


        #region Constructor
        /// <summary>
        /// Constructor for binary attribute access operations
        /// </summary>
        /// <param name="firstOperand">WriteType to be cloned</param>
        public CloneConstraint(TypeExpression firstOperand)
            : base(firstOperand, new Location()) {
        }
        /// <summary>
        /// Private constructor for binary relational operations
        /// </summary>
        /// <param name="firstOperand">WriteType to be cloned</param>
        /// <param name="returnType">The type variable of the return type</param>
        private CloneConstraint(TypeExpression firstOperand, TypeVariable returnType)
            : base(firstOperand, returnType, new Location()) {
        }
        #endregion

        #region BuildTypeExpressionString()
        protected override string BuildTypeExpressionString() {
            return String.Format("{0}:=Clone({1})", this.ReturnType.FullName, this.FirstOperand.FullName);
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
            return new CloneConstraint(newFirstOperand, newReturnType);
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
        /// <param name="location">The original location where the constraint has been generated</param>
        /// <returns>If the unification has been satisfied</returns>
        public override TypeExpression Check(MethodType methodAnalyzed, TypeExpression actualImplicitObject, bool showInvocationMessage,
                        SortOfUnification activeSortOfUnification, Location location) {
            TypeExpression result = SSAHelper.CloneType(this.FirstOperand,methodAnalyzed);
            this.ReturnType.Unify(result, SortOfUnification.Equivalent, new List<Pair<TypeExpression, TypeExpression>>());
            this.ReturnType.ValidTypeExpression = this.ValidTypeExpression = false;
            return this.ReturnType;
        }
        #endregion


    }
}
