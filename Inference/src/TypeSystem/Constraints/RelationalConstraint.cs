//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: RelationalConstraint.cs                                                    
// Author:  Francisco Ortin - francisco.ortin@gmail.com                       
// Description:                                                               
//    Relational constraint to be executed each time the associated method
//       has been called. 
//    It represents constraints of the form:
//                 ret := op1 op op2
//       where op is an relational operator (== != >= <= > <)
//    Implements Composite pattern [Leaf].                               
//    Implements Command pattern [Concrete Command].                               
//    Implements Memento pattern [Memento].                               
// -------------------------------------------------------------------------- 
// Create date: 25-04-2007                                                    
// Modification date: 25-04-2007                                              
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
    ///                 ret := op1 op op2
    /// where op is an relational operator (== != >= <= > <)
    /// </summary>
    class RelationalConstraint : ConstraintAdapter {

        #region Fields
        /// <summary>
        /// The type of the second operand
        /// </summary>
        private TypeExpression secondOperand;

        /// <summary>
        /// The binary relational operator
        /// </summary>
        private RelationalOperator op;
        #endregion

        #region Properties
        /// <summary>
        /// The type of the second operand
        /// </summary>
        public TypeExpression SecondOperand {
            get { return this.secondOperand; }
        }

        /// <summary>
        /// The binary relational operator
        /// </summary>
        public RelationalOperator Operator {
            get { return this.op; }
        }
        #endregion


        #region Constructor
        /// <summary>
        /// Constructor for binary relational operations
        /// </summary>
        /// <param name="firstOperand">WriteType of the first operand</param>
        /// <param name="secondOperand">">WriteType of the second operand</param>
        /// <param name="op">The relational operator</param>
        /// <param name="location">THe location where the constraint has been generated</param>
        public RelationalConstraint(TypeExpression firstOperand, TypeExpression secondOperand, RelationalOperator op, Location location)
            : base(firstOperand, location) {
            this.secondOperand = secondOperand;
            this.op = op;
        }
        /// <summary>
        /// Private constructor for binary relational operations
        /// </summary>
        /// <param name="firstOperand">WriteType of the first operand</param>
        /// <param name="secondOperand">">WriteType of the second operand</param>
        /// <param name="returnType">The type variable of the return type</param>
        /// <param name="op">The relational operator</param>
        /// <param name="location">THe location where the constraint has been generated</param>
        private RelationalConstraint(TypeExpression firstOperand, TypeExpression secondOperand, TypeVariable returnType, RelationalOperator op, Location location)
            : base(firstOperand, returnType, location) {
            this.secondOperand = secondOperand;
            this.op = op;
        }
        #endregion

        #region BuildTypeExpressionString()
        protected override string BuildTypeExpressionString() {
            return String.Format("{0}:={1} {2} {3}", this.ReturnType.FullName, this.FirstOperand.FullName, op.ToString(), this.SecondOperand.FullName);
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
            TypeVariable newReturnType = (TypeVariable)this.ReturnType.CloneTypeVariables(typeVariableMappings, equivalenceClasses, new List<ClassType>());
            return new RelationalConstraint(newFirstOperand, newSecondOperand, newReturnType, this.Operator, this.Location);
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
        /// one and Incremental is used in the SSA bodies of the while, for and do statements)<    /// <param name="location">Location of the item</param>
        public override TypeExpression Check(MethodType methodAnalyzed, TypeExpression actualImplicitObject, bool showInvocationMessage,
                            SortOfUnification activeSortOfUnification, Location location) {
            TypeExpression result = (TypeExpression) this.FirstOperand.AcceptOperation(new RelationalOperation(this.SecondOperand, this.Operator, methodAnalyzed, true, this.Location), null);
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
