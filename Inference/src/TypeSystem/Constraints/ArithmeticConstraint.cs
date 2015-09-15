//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: ArithmeticConstraint.cs                                                    
// Author:  Francisco Ortin - francisco.ortin@gmail.com                       
// Description:                                                               
//    Arithmetic or bitwise constraint to be executed each time the associated method
//       has been called. 
//    It represents constraints of the form:
//                 ret := op1 op op2
//      where op is a binary arithmetical operator (+ - / * %) or a bitwise operator (& | ^) or
//                 ret := op op1
//      where op is a unary arithmetical operator (+ - ++ --) or a bitwise operator (~)
//    Implements Composite pattern [Leaf].                               
//    Implements Command pattern [Concrete Command].                               
//    Implements Memento pattern [Memento].                               
// -------------------------------------------------------------------------- 
// Create date: 24-04-2007                                                    
// Modification date: 24-04-2007                                              
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
    /// Where op is an arithmetic operator
    /// </summary>
    class ArithmeticConstraint : ConstraintAdapter {

        #region Fields
        /// <summary>
        /// The type of the second operand
        /// </summary>
        private TypeExpression secondOperand;

        /// <summary>
        /// The binary or unary operator
        /// </summary>
        private Enum op;

        /// <summary>
        /// To select a binary or unary arithmetic operation
        /// </summary>
        private bool isBinary;

        #endregion

        #region Properties
        /// <summary>
        /// The type of the second operand
        /// </summary>
        public TypeExpression SecondOperand {
            get { return this.secondOperand; }
        }

        /// <summary>
        /// The binary operator
        /// </summary>
        public Enum BinaryOperator {
            get { 
                if (!this.IsBinary)
                    throw new InvalidOperationException("Not binary operator is defined for unary expressions");
                return this.op; 
            }
        }

        /// <summary>
        /// The unary operator
        /// </summary>
        public UnaryOperator UnaryOperator {
            get {
                if (this.IsBinary)
                    throw new InvalidOperationException("Not unary operator is defined for binary expressions");
                return (UnaryOperator)this.op;
            }
        }

        /// <summary>
        /// To select a binary or unary arithmetic operation
        /// </summary>
        public bool IsBinary {
            get { return this.isBinary; }
        }
        #endregion


        #region Constructor
        /// <summary>
        /// Constructor for binary arithmetic operations
        /// </summary>
        /// <param name="firstOperand">WriteType of the first operand</param>
        /// <param name="secondOperand">">WriteType of the second operand</param>
        /// <param name="op">The arithmetic operator</param>
        /// <param name="location">The original location where the constraint has been generated</param>
        public ArithmeticConstraint(TypeExpression firstOperand, TypeExpression secondOperand, Enum op, Location location)
            : base(firstOperand, location) {
            this.secondOperand = secondOperand;
            this.op = op;
            this.isBinary = true; // * Binary
        }
        /// <summary>
        /// Private constructor for binary arithmetic operations
        /// </summary>
        /// <param name="firstOperand">WriteType of the first operand</param>
        /// <param name="secondOperand">">WriteType of the second operand</param>
        /// <param name="returnType">The type variable of the return type</param>
        /// <param name="op">The arithmetic operator</param>
        /// <param name="location">The original location where the constraint has been generated</param>
        private ArithmeticConstraint(TypeExpression firstOperand, TypeExpression secondOperand, TypeVariable returnType, Enum op, Location location)
            : base(firstOperand, returnType, location) {
            this.secondOperand = secondOperand;
            this.op = op;
            this.isBinary = true; // * Binary
        }
        /// <summary>
        /// Constructor for unary arithmetic operations
        /// </summary>
        /// <param name="firstOperand">WriteType of the first operand</param>
        /// <param name="op">The arithmetic operator</param>
        /// <param name="location">The original location where the constraint has been generated</param>
        public ArithmeticConstraint(TypeExpression firstOperand, UnaryOperator op, Location location)
            : base(firstOperand, location) {
            this.op = op;
            this.isBinary = false; // * Unary
        }
        /// <summary>
        /// Private constructor for unary arithmetic operations
        /// </summary>
        /// <param name="firstOperand">WriteType of the first operand</param>
        /// <param name="returnType">The type variable of the return type</param>
        /// <param name="op">The arithmetic operator</param>
        /// <param name="location">The original location where the constraint has been generated</param>
        private ArithmeticConstraint(TypeExpression firstOperand, TypeVariable returnType, UnaryOperator op, Location location)
            : base(firstOperand, returnType, location) {
            this.op = op;
            this.isBinary = false; // * Unary
        }
        #endregion

        #region BuildTypeExpressionString()
        protected override string BuildTypeExpressionString() {
            if (this.IsBinary)
                // * Binary
                return String.Format("{0}:={1} {2} {3}", this.ReturnType.FullName, this.FirstOperand.FullName, op.ToString(), this.SecondOperand.FullName);
            // * Unary
            return String.Format("{0}:={1} {2}", this.ReturnType.FullName, op.ToString(), this.FirstOperand.FullName);
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
            if (this.IsBinary) {
                // * Binary
                TypeExpression newSecondOperand = this.SecondOperand.CloneTypeVariables(typeVariableMappings, equivalenceClasses, new List<ClassType>());
                return new ArithmeticConstraint(newFirstOperand, newSecondOperand, newReturnType, this.BinaryOperator, this.Location);
            }
            // * Unary
            return new ArithmeticConstraint(newFirstOperand, newReturnType, this.UnaryOperator, this.Location);
        }
        #endregion

        #region Check()
        /// <summary>
        /// Tries to unify the constraints of a method call
        /// </summary>
        /// <param name="methodAnalyzed">The method that is being analyzed when the operation is performed.</param>
        /// <param name="actualImplicitObject">Only suitable in an assignment constraint. It represents the actual object used to pass the message.</param>
        /// <param name="showInvocationMessage">To show the invocation line and column in case an error exists</param>
        /// <param name="location">The original location where the constraint has been generated</param>
        public override TypeExpression Check(MethodType methodAnalyzed, TypeExpression actualImplicitObject, bool showInvocationMessage,
                        SortOfUnification activeSortOfUnification, Location location) {
            TypeExpression result;
            if (this.IsBinary)
                result= (TypeExpression) this.FirstOperand.AcceptOperation(ArithmeticalOperation.Create(this.SecondOperand, this.BinaryOperator, methodAnalyzed, true, this.Location), null);
            else // * Unary
                result = (TypeExpression)this.FirstOperand.AcceptOperation(ArithmeticalOperation.Create(this.UnaryOperator, methodAnalyzed, true, this.Location), null);
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
