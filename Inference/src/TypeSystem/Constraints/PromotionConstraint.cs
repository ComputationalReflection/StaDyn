//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: PromotionConstraint.cs                                                    
// Author:  Francisco Ortin - francisco.ortin@gmail.com                       
// Description:                                                               
//    Promotion constraint to be executed each time the associated method
//       has been called. It represents constraints of the form:
//                 op1 <= op2 (op1 is a subtype of op2)
//    Implements Composite pattern [Leaf].                               
//    Implements Command pattern [Concrete Command].                               
//    Implements Memento pattern [Memento].                               
// -------------------------------------------------------------------------- 
// Create date: 05-04-2007                                                    
// Modification date: 25-04-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using ErrorManagement;
using TypeSystem.Operations;

namespace TypeSystem.Constraints {
    /// <summary>
    /// It represents constraints of the form
    ///                 op1 <= op2 (op1 is a subtype of op2)
    /// </summary>
    class PromotionConstraint : ConstraintAdapter {
        #region Fields
        /// <summary>
        /// The type of the second operand
        /// </summary>
        private TypeExpression secondOperand;

        /// <summary>
        /// The operator used when the promotion where required
        /// (it is only used for debuging purposes)
        /// </summary>
        private object op;
        #endregion

        #region Properties
        /// <summary>
        /// The type of the second operand
        /// </summary>
        public TypeExpression SecondOperand {
            get { return this.secondOperand; }
        }

        /// <summary>
        /// The operator used when the promotion where required
        /// (it is only used for debuging purposes)
        /// </summary>
        public object Operator {
            get { return this.op; }
        }
        #endregion


        #region Constructor
        /// <summary>
        /// Constructor for promotion constraints with operator
        /// </summary>
        /// <param name="firstOperand">WriteType of the first operand</param>
        /// <param name="secondOperand">">WriteType of the second operand</param>
        /// <param name="op">The relational operator</param>
        /// <param name="location">The original location where the constraint has been generated</param>
        public PromotionConstraint(TypeExpression firstOperand, TypeExpression secondOperand, object op, Location location)
            : base(firstOperand, location) {
            this.secondOperand = secondOperand;
            this.op = op;
        }
        /// <summary>
        /// Constructor for promotion constraints without operator
        /// </summary>
        /// <param name="firstOperand">WriteType of the first operand</param>
        /// <param name="secondOperand">">WriteType of the second operand</param>
        /// <param name="location">The original location where the constraint has been generated</param>
        public PromotionConstraint(TypeExpression firstOperand, TypeExpression secondOperand, Location location)
            : this(firstOperand, secondOperand, null, location) { }
        #endregion

        #region BuildTypeExpressionString()
        protected override string BuildTypeExpressionString() {
            return String.Format("{0} <= {1}", this.FirstOperand.FullName, this.SecondOperand.FullName);
        }
        #endregion

        #region EqualsAndGetHashCode
        public override bool Equals(object obj) {
            PromotionConstraint constraint = obj as PromotionConstraint;
            if (constraint == null)
                return false;
            return this.FirstOperand.Equals(constraint.FirstOperand) && this.secondOperand.Equals(constraint.secondOperand);
        }
        public override int GetHashCode() {
            return this.FirstOperand.GetHashCode()*this.secondOperand.GetHashCode();
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
                newSecondOperand = this.SecondOperand.CloneTypeVariables(typeVariableMappings, equivalenceClasses, new List<ClassType>());
            if (this.op != null)
                return new PromotionConstraint(newFirstOperand, newSecondOperand, this.Operator, this.Location);
            return new PromotionConstraint(newFirstOperand, newSecondOperand, this.Location);
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
        /// <param name="location">Location of the method call</param>
        /// <returns>If the unification has been satisfied</returns>
        public override TypeExpression Check(MethodType methodAnalyzed, TypeExpression actualImplicitObject, bool showInvocationMessage,
                            SortOfUnification activeSortOfUnification, Location location) {
            TypeExpression result;
            if (this.op!=null)
                //result = this.FirstOperand.Promotion(this.SecondOperand, (Enum)this.Operator, methodAnalyzed, this.Location);
                result = (TypeExpression)this.FirstOperand.AcceptOperation(PromotionOperation.Create(this.SecondOperand, (Enum)this.Operator, methodAnalyzed, this.Location), null);
            else
                result = (TypeExpression)this.FirstOperand.AcceptOperation(PromotionOperation.Create(this.SecondOperand, methodAnalyzed, this.Location), null);
            if (result == null && showInvocationMessage) {
                ErrorManager.Instance.NotifyError(new ConstraintError(location));
                return null;
            }
            // * If no error exists, we return the supertype
            return this.SecondOperand;
        }
        #endregion


    }
}
