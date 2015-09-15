//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: ConstraintList.cs                                                    
// Author:  Francisco Ortin - francisco.ortin@gmail.com                       
// Description:                                                               
//    Abstract class that represents any constraint to be part of a Method WriteType.
//       In our type system, methods could have constraints associated to be
//       satisfied every time it is invoked. This class represent the 
//       homogeneous treatment of the Composite desing pattern.
//    Implements Composite pattern [Component].                               
//    Implements Command pattern [Command].                               
// -------------------------------------------------------------------------- 
// Create date: 05-04-2007                                                    
// Modification date: 25-04-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using ErrorManagement;

namespace TypeSystem.Constraints {
    abstract public class Constraint {

        #region Fields
        /// <summary>
        /// Represents the type by a debug string
        /// </summary>
        private string typeExpression;

        /// <summary>
        /// To implement a type expression cache
        /// </summary>
        private bool validTypeExpression = false;
        #endregion

        #region Properties
        /// <summary>
        /// To implement a type expression cache
        /// </summary>
        public bool ValidTypeExpression {
            set { 
                if (!value)
                    this.typeExpression = this.BuildTypeExpressionString();
            }
        }
        #endregion


        /// <summary>
        /// To know if any constraint has been set
        /// </summary>
        /// <returns></returns>
        public virtual bool isEmpty() { return false; }

        public override string ToString() {
            if (this.validTypeExpression)
                return this.typeExpression;
            return this.typeExpression = this.BuildTypeExpressionString();
        }

        protected abstract string BuildTypeExpressionString();

        /// <summary>
        /// Method that clones each type variable of a constraint.
        /// Equivalence classes are not cloned (but included in the equivalenceClasses parameter.
        /// </summary>
        /// <param name="typeVariableMappings">Each new type varaiable represent a copy of another existing one.
        /// This parameter is a mapping between them, wher tmpName=old and value=new</param>
        /// <param name="equivalenceClasses">Each equivalence class of all the type variables.</param>
        /// <returns>The new type expression (itself by default)</returns>
        public abstract Constraint CloneTypeVariables(IDictionary<TypeVariable, TypeVariable> typeVariableMappings, IList<EquivalenceClass> equivalenceClasses);

        /// <summary>
        /// Checks the satisfaction of a constraint in a method call
        /// </summary>
        /// <param name="methodAnalyzed">The method that is being analyzed when the operation is performed.</param>
        /// <param name="actualImplicitObject">Only suitable in an assignment constraint. It represents the actual object used to pass the message.</param>
        /// <param name="showInvocationMessage">To show the invocation line and column in case an error exists</param>
        /// <param name="activeSortOfUnification">The active sort of unification used (Equivalent is the default
        /// one and Incremental is used in the SSA bodies of the while, for and do statements)</param>
        /// <param name="location">The location where the method is called</param>
        /// <returns>The return type expression</returns>
        public abstract TypeExpression Check(MethodType methodAnalyzed, TypeExpression actualImplicitObject, bool showInvocationMessage, 
            SortOfUnification activeSortOfUnification, Location location);
    }
}
