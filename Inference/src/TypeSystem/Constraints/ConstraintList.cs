//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: ConstraintList.cs                                                    
// Author:  Francisco Ortin - francisco.ortin@gmail.com                       
// Description:                                                               
//    Composite class capable of representing a (possible empty) 
//        set of constraints.                     
//    Implements Composite pattern [Composite].                               
// -------------------------------------------------------------------------- 
// Create date: 05-04-2007                                                    
// Modification date: 05-04-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using ErrorManagement;

namespace TypeSystem.Constraints
{
    /// <summary>
    /// A set of constraints
    /// </summary>
    public class ConstraintList : Constraint
    {
        #region Fields
        /// <summary>
        /// The set of constraints
        /// </summary>
        private IList<Constraint> constraints = new List<Constraint>();
        #endregion

        #region Properties
        /// <summary>
        /// The set of constraints
        /// </summary>
        public IList<Constraint> Constraints
        {
            get { return this.constraints; }
        }

        /// <summary>
        /// The number of constraints
        /// </summary>
        public int Count
        {
            get { return constraints.Count; }
        }

        /// <summary>
        /// A constraint element
        /// </summary>
        /// <param name="index">The index of the constraint</param>
        /// <returns>The constraint</returns>
        public Constraint this[int index]
        {
            get { return this.constraints[index]; }
        }


        #endregion

        #region BuildTypeExpressionString()
        protected override string BuildTypeExpressionString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.constraints.Count; i++)
            {
                sb.Append(this.constraints[i].ToString());
                if (i < this.constraints.Count - 1)
                    sb.Append(", ");
            }
            return sb.ToString();
        }
        #endregion

        #region Add()
        /// <summary>
        /// To add a new constraint
        /// </summary>
        /// <param name="constraint">The constraint to be added</param>
        /// <returns>If the constraint parameter had been already added</returns>
        public bool Add(Constraint constraint)
        {
            this.constraints.Add(constraint);
            return true;
        }
        #endregion

        #region isEmpty()
        public override bool isEmpty()
        {
            return this.constraints.Count == 0;
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
        public override Constraint CloneTypeVariables(IDictionary<TypeVariable, TypeVariable> typeVariableMappings, IList<EquivalenceClass> equivalenceClasses)
        {
            ConstraintList newConstraint = new ConstraintList();
            foreach (Constraint constraint in this.constraints)
                newConstraint.Add(constraint.CloneTypeVariables(typeVariableMappings, equivalenceClasses));
            newConstraint.ValidTypeExpression = false;
            return newConstraint;
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
        /// <param name="location">The location of the method call</param>
        /// <returns>If the unification has been satisfied</returns>
        public override TypeExpression Check(MethodType methodAnalyzed, TypeExpression actualImplicitObject, bool showInvocationMessage,
                        SortOfUnification activeSortOfUnification, Location location)
        {
            TypeExpression result = null;            
            foreach (Constraint constraint in this.constraints)            
                result = constraint.Check(methodAnalyzed, actualImplicitObject, showInvocationMessage, activeSortOfUnification, location);                            
            return result;
        }
        #endregion

    }
}
