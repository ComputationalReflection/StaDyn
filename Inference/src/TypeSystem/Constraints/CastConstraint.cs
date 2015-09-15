//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: CastConstraint.cs                                                    
// Author:  Francisco Ortin - francisco.ortin@gmail.com                       
// Description:                                                               
//    Cast constraint to be executed each time the associated method
//       has been called. It represents constraints of the form:
//                 (op1)op2 (op2 might be converted int a op1)
//    Implements Composite pattern [Leaf].                               
//    Implements Command pattern [Concrete Command].                               
//    Implements Memento pattern [Memento].                               
// -------------------------------------------------------------------------- 
// Create date: 13-07-2007                                                    
// Modification date: 13-07-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using ErrorManagement;
using TypeSystem.Operations;

namespace TypeSystem.Constraints {
    /// <summary>
    /// It represents constraints of the form
    ///    (op1)op2 (op2 might be converted int a op1)
    /// </summary>
    class CastConstraint : ConstraintAdapter {
        #region Fields
        /// <summary>
        /// The cast type
        /// </summary>
        private TypeExpression castType;

        #endregion

        #region Properties
        /// <summary>
        /// The cast type
        /// </summary>
        public TypeExpression CastType {
            get { return this.castType; }
        }

        #endregion


        #region Constructor
        /// <summary>
        /// Constructor for cast constraints with operator
        /// </summary>
        /// <param name="firstOperand">The type to be cast</param>
        /// <param name="castType">The type of the cast</param>
        /// <param name="location">The original location where the constraint has been generated</param>
        public CastConstraint(TypeExpression firstOperand, TypeExpression castType, Location loc)
            : base(firstOperand, loc) {
            this.castType = castType;
        }
        #endregion

        #region BuildTypeExpressionString()
        protected override string BuildTypeExpressionString() {
            return String.Format("({0}){1}", this.CastType.FullName, this.FirstOperand.FullName);
        }
        #endregion

        #region EqualsAndGetHashCode
        public override bool Equals(object obj) {
            CastConstraint constraint = obj as CastConstraint;
            if (constraint == null)
                return false;
            return this.FirstOperand.Equals(constraint.FirstOperand) && this.CastType.Equals(constraint.CastType);
        }
        public override int GetHashCode() {
            return this.FirstOperand.GetHashCode()*this.CastType.GetHashCode();
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
            TypeExpression newFirstOperand = this.FirstOperand.CloneTypeVariables(typeVariableMappings, equivalenceClasses, new List<ClassType>()),
                newCastType = this.CastType.CloneTypeVariables(typeVariableMappings, equivalenceClasses, new List<ClassType>());
            return new CastConstraint(newFirstOperand, newCastType, this.Location);
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
        /// <param name="location">The location where method is called</param>
        public override TypeExpression Check(MethodType methodAnalyzed, TypeExpression actualImplicitObject, bool showInvocationMessage,
                            SortOfUnification activeSortOfUnification, Location location) {
            TypeExpression result = (TypeExpression)this.FirstOperand.AcceptOperation(new CastOperation (this.CastType, methodAnalyzed,this.Location), null);
            if (result == null && showInvocationMessage) {
                ErrorManager.Instance.NotifyError(new ConstraintError(location));
                return null;
            }
            // * If no error exists, we return the casttype
            return this.CastType;
        }
        #endregion


    }
}
