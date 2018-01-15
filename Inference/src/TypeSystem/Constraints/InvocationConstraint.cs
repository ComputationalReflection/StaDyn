using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ErrorManagement;

namespace TypeSystem.Constraints
{
    /// <summary>
    /// A InvocationContraint it is used to check a method invocation taking into account method overloading.
    /// It represents constraints of the form:
    ///         ret := op1(paramList_1 OR paramList_2 OR ... paramList_n)
    /// where op1 is a method, paramList_X is the list of parameters of each method overload.
    /// </summary>
    /// <remarks>
    /// Each constraint list represents one method overload.
    /// </remarks>
    public class InvocationConstraint : Constraint
    {        
        #region Fields

        /// <summary>
        /// The list of constraintLists
        /// </summary>
        public IList<ConstraintList> ConstraintLists { get; private set; } 

        /// <summary>
        /// Full name of the invoked method
        /// </summary>
        public String MethodName { get; private set; }

        /// <summary>
        /// The number of constraint lists
        /// </summary>
        public int Count
        {
            get { return ConstraintLists.Count; }
        }

        #endregion


        #region Add()
        /// <summary>
        /// To add a new constraint list
        /// </summary>
        /// <param name="constraint">The constraint list to be added</param>
        /// <returns>If the constraint parameter had been already added</returns>
        public bool Add(ConstraintList constraint)
        {
            this.ConstraintLists.Add(constraint);
            return true;
        }
        #endregion

        public InvocationConstraint(String methodName)
        {
            this.MethodName = methodName;
            this.ConstraintLists = new List<ConstraintList>();
        }

        
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
        public override TypeExpression Check(MethodType methodAnalyzed, TypeExpression actualImplicitObject, bool showInvocationMessage, SortOfUnification activeSortOfUnification, Location location)
        {             
            foreach (ConstraintList constraint in this.ConstraintLists)
            {
                TypeExpression result = constraint.Check(methodAnalyzed, actualImplicitObject, false, activeSortOfUnification, location);
                if (result != null)
                    return result;
            }
            if (showInvocationMessage)
                ErrorManager.Instance.NotifyError(new ConstraintError(location));
            return null;
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
            InvocationConstraint newConstraint = new InvocationConstraint(this.MethodName);
            foreach (var constraintList in ConstraintLists)
                newConstraint.Add((ConstraintList)constraintList.CloneTypeVariables(typeVariableMappings, equivalenceClasses));
            newConstraint.ValidTypeExpression = false;
            return newConstraint;
        }
        #endregion

        #region BuildTypeExpressionString()
        protected override string BuildTypeExpressionString()
        {            
            StringBuilder sb = new StringBuilder();
            sb.Append(MethodName + "(");
            for (int i = 0; i < ConstraintLists.Count; i++)
            { 
                sb.Append("[");
                for (int j = 0; j < ConstraintLists[i].Constraints.Count; j++)
                {
                    sb.Append(ConstraintLists[i].Constraints[j].ToString());
                    if (j < ConstraintLists[i].Constraints.Count - 1)
                        sb.Append(", ");
                }
                sb.Append("]");
                if (i < ConstraintLists.Count - 1)
                    sb.Append(" OR ");
            }
            sb.Append(")");
            return sb.ToString();
        }
        #endregion
    }
}
