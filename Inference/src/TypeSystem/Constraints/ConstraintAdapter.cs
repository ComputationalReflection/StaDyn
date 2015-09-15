//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: ConstraintAdapter.cs                                                    
// Author:  Francisco Ortin - francisco.ortin@gmail.com                          
// Description:                                                               
//    Constraint adater to refactor the common elements to all the contraints,
//       but the ConstraintList constraints.
//    It is the implementation of the adapter design pattern, using 
//       implementation inheritance.
//    Implements Adapter pattern [Adapter].                               
//    Implements (partially) Memento pattern [Memento].                               
// -------------------------------------------------------------------------- 
// Create date: 25-04-2007                                                    
// Modification date: 25-04-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using ErrorManagement;

namespace TypeSystem.Constraints {
    abstract class ConstraintAdapter: Constraint {
        #region Fields
        /// <summary>
        /// The type of the first operand
        /// </summary>
        private TypeExpression firstOperand;

        /// <summary>
        /// The result of the type inference, as a type variable
        /// </summary>
        private TypeVariable returnType;
        /// <summary>
        /// Localización del item en el texto del programa
        /// </summary>
        private Location location;
        #endregion

        #region Properties
        public Location Location {
            get { return location; }
        }
        /// <summary>
        /// The type of the first operand
        /// </summary>
        public TypeExpression FirstOperand {
            get { return this.firstOperand; }
        }

        /// <summary>
        /// The return type
        /// </summary>
        public TypeVariable ReturnType {
            get { return this.returnType; }
        }

        #endregion



        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="firstOperand">WriteType of the first operand</param>
        /// <param name="loc">The original location where the constraint has been generated</param>
        public ConstraintAdapter(TypeExpression firstOperand, Location loc) {
            this.firstOperand = firstOperand;
            this.returnType = TypeVariable.NewTypeVariable;
            this.location = loc;
        }
        /// <summary>
        /// Private constructor that receives the retun type variable
        /// </summary>
        /// <param name="firstOperand">WriteType of the first operand</param>
        /// <param name="returnType">The type variable of the return type</param>
        /// <param name="loc">The original column where the constraint has been generated</param>
protected ConstraintAdapter(TypeExpression firstOperand, TypeVariable returnType, Location loc) {
            this.firstOperand = firstOperand;
            this.returnType = returnType;
            this.location = loc;
        }
        #endregion

        #region EqualsAndGetHashCode
        public override bool Equals(object obj) {
            ArithmeticConstraint constraint = obj as ArithmeticConstraint;
            if (constraint == null)
                return false;
            return this.returnType.Variable == constraint.returnType.Variable;
        }
        public override int GetHashCode() {
            return this.returnType.Variable;
        }
        #endregion

    }
}
