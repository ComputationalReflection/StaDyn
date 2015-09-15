//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: FieldTypeAssignmentConstraint.cs                                                    
// Author:  Francisco Ortin - francisco.ortin@gmail.com                       
// Description:                                                               
//    Assignment constraint to be executed each time the associated method
//       has been called. It represents constraints of the form:
//                 alpha := beta
//       being alpha a field type
//    Implements Composite pattern [Leaf].                               
// -------------------------------------------------------------------------- 
// Create date: 05-04-2007                                                    
// Modification date: 05-04-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using TypeSystem;
using AST;
using Tools;
using ErrorManagement;
using TypeSystem.Operations;

namespace TypeSystem.Constraints {
    /// <summary>
    /// It represents constraints of the form: alpha := beta, being alpha a field type
    /// </summary>
    class FieldTypeAssignmentConstraint : Constraint {
        #region Fields
        /// <summary>
        /// The type expression to be modifed (has type variables)
        /// </summary>
        private FieldType alpha;

        /// <summary>
        /// The type expression to be added
        /// </summary>
        private TypeExpression beta;

        /// <summary>
        /// The kind of unification used in the assignment
        /// </summary>
        private SortOfUnification sortOfUnification;
        #endregion

        #region Properties
        /// <summary>
        /// The variable type to be modifed
        /// </summary>
        public FieldType LeftType {
            get { return alpha; }
        }

        /// <summary>
        /// The type expression to be added
        /// </summary>
        public TypeExpression RightType {
            get { return beta; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alpha">The type expression to be modified</param>
        /// <param name="beta">The type to be added</param>
        /// <param name="sortOfUnification">The kind of unification used in the assignment</param>
        public FieldTypeAssignmentConstraint(FieldType alpha, TypeExpression beta, SortOfUnification sortOfUnification) {
            this.alpha = alpha;
            this.beta = beta;
            if (sortOfUnification == SortOfUnification.Equivalent)
                // * Equivalence is translated into override when fields are assigned
                this.sortOfUnification = SortOfUnification.Override;
            else
                this.sortOfUnification = sortOfUnification;
        }
        #endregion

        #region EqualsAndGetHashCode
        public override bool Equals(object obj) {
            FieldTypeAssignmentConstraint ac = obj as FieldTypeAssignmentConstraint;
            if (ac == null)
                return false;
            return this.alpha.Equals(ac.alpha) && this.beta.Equals(ac.beta);
        }
        public override int GetHashCode() {
            return this.alpha.fullName.GetHashCode() * this.beta.FullName.GetHashCode();
        }
        #endregion

        #region BuildTypeExpressionString()
        protected override string BuildTypeExpressionString() {
            string fieldTypeString = this.alpha.FieldTypeExpression.ToString();
            if (this.sortOfUnification==SortOfUnification.Override)
                return String.Format("{0}:={1}", fieldTypeString, this.beta);
            return String.Format("{0}:=\\/[{0},{1}]", fieldTypeString, this.beta);
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
            FieldType newAlpha = (FieldType)this.alpha.CloneTypeVariables(typeVariableMappings, equivalenceClasses, new List<ClassType>());
            TypeExpression newBeta = this.beta.CloneTypeVariables(typeVariableMappings, equivalenceClasses, new List<ClassType>());
            FieldTypeAssignmentConstraint newConstraint = new FieldTypeAssignmentConstraint(newAlpha, newBeta, this.sortOfUnification);
            return newConstraint;
        }
        #endregion

        #region Check()
        /// <summary>
        /// Checks the constraints of a attribute assigment in a method call
        /// </summary>
        /// <param name="methodAnalyzed">The method that is being analyzed when the operation is performed.</param>
        /// <param name="actualImplicitObject">Only suitable in an assignment constraint. It represents the actual object used to pass the message.</param>
        /// <param name="showInvocationMessage">To show the invocation line and column in case an error exists</param>
        /// <param name="fileName">The name of its source file</param>
        /// <param name="activeSortOfUnification">The active sort of unification used (Equivalent is the default
        /// one and Incremental is used in the SSA bodies of the while, for and do statements)</param>
        /// <param name="location">Location of the method call.</param>
        /// <returns>The return type expression</returns>
        public override TypeExpression Check(MethodType methodAnalyzed, TypeExpression actualImplicitObject, bool showInvocationMessage,
            SortOfUnification activeSortOfUnification, Location location) {

            // * If the unification in the method call is incremental, this is the one to be used
            SortOfUnification unification = activeSortOfUnification==SortOfUnification.Incremental ? SortOfUnification.Incremental: this.sortOfUnification;
            // * Infers the type
            this.alpha.AcceptOperation(new AssignmentOperation(this.beta, AssignmentOperator.Assign, methodAnalyzed, unification, actualImplicitObject, location), null);
            return this.alpha;

        }
        #endregion


    }

}
