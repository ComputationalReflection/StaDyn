//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: ParenthesisConstraint.cs                                                    
// Author:  Francisco Ortin - francisco.ortin@gmail.com                       
// Description:                                                               
//    Method invocation constraint to be executed each time the associated method
//       has been called. 
//    It represents constraints of the form:
//                 ret := op1(implicitObj, param*)
//       where op1 is a method, implicitObj is the actual object used to pass the
//       message and param* is a sequence of parameters
//    Implements Composite pattern [Leaf].                               
//    Implements Command pattern [Concrete Command].                               
//    Implements Memento pattern [Memento].                               
// -------------------------------------------------------------------------- 
// Create date: 26-04-2007                                                    
// Modification date: 26-04-2007                                              
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
    ///         ret := op1(implicitObj, param*)
    /// where op1 is a method, implicitObj is the actual object used to pass the
    /// message and param* is a sequence of parameters
    /// </summary>
    class ParenthesisConstraint : ConstraintAdapter {

        #region Fields
        /// <summary>
        /// The actual implicit object used to pass the message
        /// </summary>
        private TypeExpression actualImplicitObject;

        /// <summary>
        /// The set of actual parameters
        /// </summary>
        private TypeExpression[] parameters;

        /// <summary>
        /// The kind of unification used in the method call
        /// </summary>
        private SortOfUnification sortOfUnification;
        #endregion

        #region Properties
        /// <summary>
        /// The actual implicit object used to pass the message
        /// </summary>
        public TypeExpression ActualImplicitObject {
            get { return this.actualImplicitObject; }
        }

        /// <summary>
        /// The set of actual parameters
        /// </summary>
        public TypeExpression[] Parameters {
            get { return this.parameters; }
        }
        #endregion


        #region Constructor
        /// <summary>
        /// Constructor for method call constraints
        /// </summary>
        /// <param name="firstOperand">WriteType of the first operand</param>
        /// <param name="actualImplicitObject">The actual implicit object used to pass the message</param>
        /// <param name="parameters">The set of actual parameters</param>
        /// <param name="sortOfUnification">The kind of unification used in the method call</param>
        /// <param name="location">The original location where the constraint has been generated</param>
        public ParenthesisConstraint(TypeExpression firstOperand, TypeExpression actualImplicitObject, TypeExpression[] parameters, SortOfUnification  sortOfUnification, Location location)
            : base(firstOperand, location) {
            this.actualImplicitObject = actualImplicitObject;
            this.parameters = parameters;
            this.sortOfUnification = sortOfUnification;
        }
        /// <summary>
        /// Private constructor for method call constraints
        /// </summary>
        /// <param name="firstOperand">WriteType of the first operand</param>
        /// <param name="actualImplicitObject">The actual implicit object used to pass the message</param>
        /// <param name="parameters">The set of actual parameters</param>
        /// <param name="returnType">The type variable of the return type</param>
        /// <param name="sortOfUnification">The kind of unification used in the method call</param>
        /// <param name="location">The original location where the constraint has been generated</param>
        private ParenthesisConstraint(TypeExpression firstOperand, TypeExpression actualImplicitObject, TypeExpression[] parameters, 
                            TypeVariable returnType, SortOfUnification sortOfUnification, Location location)
            : base(firstOperand, returnType, location) {
            this.actualImplicitObject = actualImplicitObject;
            this.parameters = parameters;
            this.sortOfUnification = sortOfUnification;
        }
        #endregion

        #region BuildTypeExpressionString()
        protected override string BuildTypeExpressionString() {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}:={1}({2}", this.ReturnType.FullName, this.FirstOperand.FullName, this.ActualImplicitObject.FullName);
            foreach (TypeExpression parameter in this.Parameters)
                sb.AppendFormat(",{0}", parameter.FullName);
            sb.Append(")");
            return sb.ToString();
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
            TypeExpression newActualImplicitObject = null;
            if (this.ActualImplicitObject != null)
                newActualImplicitObject = this.ActualImplicitObject.CloneTypeVariables(typeVariableMappings, equivalenceClasses, new List<ClassType>());
            TypeVariable newReturnType = (TypeVariable)this.ReturnType.CloneTypeVariables(typeVariableMappings, equivalenceClasses, new List<ClassType>());
            TypeExpression[] newParameters = new TypeExpression[this.parameters.Length];
            for (int i = 0; i < this.parameters.Length; i++)
                newParameters[i] = this.parameters[i].CloneTypeVariables(typeVariableMappings, equivalenceClasses, new List<ClassType>());
            return new ParenthesisConstraint(newFirstOperand, newActualImplicitObject, newParameters, newReturnType, this.sortOfUnification, this.Location);
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
        /// <param name="location">The location where the location is called</param>
        /// <returns>If the unification has been satisfied</returns>
        public override TypeExpression Check(MethodType methodAnalyzed, TypeExpression actualImplicitObject, bool showInvocationMessage,
            SortOfUnification activeSortOfUnification, Location location) {
            // * If the actual implicit object is a field, we take its field's type
            FieldType implicitObjectAsField = TypeExpression.As<FieldType>(this.actualImplicitObject);
            ClassType implicitObjectAsClass;
            TypeExpression implicitObject=this.ActualImplicitObject;
            if (implicitObjectAsField != null) {
                implicitObject = implicitObjectAsField.FieldTypeExpression;
                implicitObjectAsClass = TypeExpression.As<ClassType>(implicitObjectAsField.FieldTypeExpression);
            }
            else
                implicitObjectAsClass = TypeExpression.As<ClassType>(this.actualImplicitObject);
            // * If the actual implicit object is concrete, so it is the saved implicit object
            if (ClassType.IsConcreteType(actualImplicitObject) != null && implicitObjectAsClass != null)
                implicitObjectAsClass.ConcreteType = true;

            // * If the unification in the method call is incremental, this is the one to be used
            SortOfUnification unification = activeSortOfUnification == SortOfUnification.Incremental ? SortOfUnification.Incremental : this.sortOfUnification;
            // * Checks the parenthesis operation
            TypeExpression result = (TypeExpression)this.FirstOperand.AcceptOperation(new ParenthesisOperation(implicitObject, this.Parameters, methodAnalyzed, unification, this.Location), null);
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
