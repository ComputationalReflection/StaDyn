//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: ClassTypeProxy.cs                                                        
// Author: Francisco Ortin - francisco.ortin@gmail.com                       
// Description:                                                               
//    Represents proxy of a class type. It is needed when a class has a 
//        field that is recursively of the same class. It implements a
//        lazy clone type variables scheme.
//    Implements the unfold operatations of theoretical type systes.
//    Inheritance: TypeExpression.                                                  
//    Implements Composite pattern [Composite].                               
//    Implements Proxy pattern [Proxy].                               
// -------------------------------------------------------------------------- 
// Create date: 02-05-2007
// Modification date: 02-05-2007
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using AST;
using ErrorManagement;
using Tools;
using TypeSystem.Operations;

namespace TypeSystem {
    /// <summary>
    /// Represents a proxy of a class type. It implements the unfold operatations of theoretical type systes.
    /// </summary>
    /// <remarks>
    /// Inheritance: ClassType.
    /// </remarks>
    /// vISTO
    public class ClassTypeProxy : TypeExpression {
        #region Fields

        /// <summary>
        /// The real subject of the proxy (see the Proxy design pattern)
        /// </summary>
        private ClassType realType = null;

        /// <summary>
        /// A reference to the original class that wasn't possible to be created because of recursion
        /// </summary>
        private ClassType originalClass;

        /// <summary>
        /// The mappings that were passed in the recursive clone operation
        /// </summary>
        private IDictionary<TypeVariable, TypeVariable> typeVariableMappings;

        /// <summary>
        /// The equivalence classes that were passed in the recursive clone operation
        /// </summary>
        private IList<EquivalenceClass> equivalenceClasses;
        #endregion

        #region Properties
        /// <summary>
        /// The real subject of the proxy (see the Proxy design pattern)
        /// </summary>
        public ClassType RealType {
            get {
                this.unfold();
                return this.realType;
            }
        }
        //// New read-only Property created to expose original class to the dispatcher
        //public ClassType OriginalClass {
        //    get {return this.originalClass}
        //}
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor of the ClassType Proxy
        /// </summary>
        /// <param name="originalClass">A reference to the original class that wasn't possible to be created because of recursion</param>
        /// <param name="typeVariableMappings">The mappings that were passed in the recursive clone operation</param>
        /// <param name="equivalenceClasses">The equivalence classes that were passed in the recursive clone operation</param>
        public ClassTypeProxy(ClassType originalClass, IDictionary<TypeVariable, TypeVariable> typeVariableMappings, IList<EquivalenceClass> equivalenceClasses) {
            this.originalClass = originalClass;
            this.typeVariableMappings = typeVariableMappings;
            this.equivalenceClasses = equivalenceClasses;
            this.fullName = originalClass.FullName;
        }
        #endregion

        #region unfold()
        /// <summary>
        /// Implements the unfold operation of theoretical type systems.
        /// It the real subject has not been created, it is expanded in one level of recursion.
        /// </summary>
        private void unfold() {
            if (this.realType == null) {
                this.realType = (ClassType)this.originalClass.CloneTypeVariables(this.typeVariableMappings, this.equivalenceClasses, new List<ClassType>());
                // * For each equivalence class we create a new one, 
                //   substituting the old type variables for the new ones
                // * The substitution is not altered
                // * Since equivalence classes and type variables have a bidirectional association,
                //   the new equivalence classes will make type variables update their new equivalence classes
                foreach (EquivalenceClass equivalenceClass in equivalenceClasses)
                    equivalenceClass.UpdateEquivalenceClass(typeVariableMappings);
                // * The new class type is returned
                this.realType.ValidTypeExpression = false;
            }
        }

        #endregion

        #region BuildTypeExpressionString()

        /// <summary>
        /// Creates the type expression string.
        /// </summary>
        public override string BuildTypeExpressionString(int depthLevel) {
            if (this.realType.ValidTypeExpression)
                return this.realType.typeExpression;
            return this.realType.FullName;
        }

        #endregion

        // WriteType inference
        #region Dispatcher
        public override object AcceptOperation(TypeSystemOperation op, object arg) { return op.Exec(this, arg); }
        #endregion


        // WriteType Unification

        #region Unify
        /// <summary>
        /// This method unifies two type expressions (this and te)
        /// </summary>
        /// <param name="te">The expression to be unfied with this</param>
        /// <param name="unification">Indicates if the kind of unification (equivalent, incremental or override).</param>
        /// <param name="previouslyUnified">To detect infinite loops. The previously unified pairs of type expressions.</param>
        /// <returns>If the unification was successful</returns>
        public override bool Unify(TypeExpression te, SortOfUnification unification, IList<Pair<TypeExpression, TypeExpression>> previouslyUnified) {
            return this.RealType.Unify(te, unification, previouslyUnified);
        }
        #endregion


        #region HasTypeVariables()
        /// <summary>
        /// To know if the type expression has some type variables and requieres unification
        /// The default implementation is return false
        /// </summary>
        /// <returns>If it has any type variable</returns>
        public override bool HasTypeVariables() {
            return this.RealType.HasTypeVariables();
        }

        public bool HasTypeVariables(IList<String> evaluated)
        {
            return this.RealType.HasTypeVariables(evaluated);            
        }
        #endregion

        #region CloneType()
        /// <summary>
        /// This method creates a new class type, creating new type variables for
        /// each field. Methods are not cloned.
        /// It these type variables where bounded to types or other
        /// type variables, they are maintained.
        /// </summary>
        /// <param name="typeVariableMappings">Each new type varaiable represent a copy of another existing one.
        /// This parameter is a mapping between them, wher tmpName=old and value=new.</param>
        /// <returns>The new cloned class type</returns>
        public override TypeExpression CloneType(IDictionary<TypeVariable, TypeVariable> typeVariableMappings) {
            return this.RealType.CloneType(typeVariableMappings);
        }
        #endregion


        #region CloneTypeVariables()
        /// <summary>
        /// Method that clones each type variable of a type expression.
        /// Equivalence classes are not cloned (but included in the equivalenceClasses parameter.
        /// The default implementation is do nothing (for built-in types).
        /// </summary>
        /// <param name="typeVariableMappings">Each new type varaiable represent a copy of another existing one.
        /// This parameter is a mapping between them, wher tmpName=old and value=new</param>
        /// <param name="equivalenceClasses">Each equivalence class of all the type variables.</param>
        /// <param name="clonedClasses">This parameter collects the set of all cloned classes. It is used to detect infinite recursion.</param>
        /// <returns>The new type expression (itself by default)</returns>
        public override TypeExpression CloneTypeVariables(IDictionary<TypeVariable, TypeVariable> typeVariableMappings, IList<EquivalenceClass> equivalenceClasses, IList<ClassType> clonedClasses) {
            return this.RealType.CloneTypeVariables(typeVariableMappings, equivalenceClasses, clonedClasses);
        }
        #endregion

        // SSA

        #region Clone()
        /// <summary>
        /// Clones a type to be used in SSA. It must taken into account that:
        /// - In case it has no type variables, no clone is performed
        /// - WriteType variables, equivalence classes and substitutions are cloned
        /// </summary>
        /// <param name="clonedTypeVariables">WriteType variables that have been cloned.</param>
        /// <param name="equivalenceClasses">Equivalence classes of the type cloned variables. These
        /// equivalence classes need to be updated with the new cloned type variables.</param>
        /// <param name="methodAnalyzed">The method that is being analyzed when the operation is performed.</param>
        /// <returns>The cloned type</returns>
        internal override TypeExpression Clone(IDictionary<int, TypeVariable> clonedTypeVariables, IList<EquivalenceClass> equivalenceClasses, MethodType methodAnalyzed) {
            if (!this.HasTypeVariables())
                return this;
            ClassTypeProxy newClassProxy = (ClassTypeProxy)this.MemberwiseClone();
            if (newClassProxy.realType != null)
                newClassProxy.realType = (ClassType)newClassProxy.realType.Clone(clonedTypeVariables, equivalenceClasses, methodAnalyzed);
            return newClassProxy;
        }
        #endregion

        // Loop detection

        #region Equals&GetHashCode()
        /// <summary>
        /// To check loops in recursive types, proxy must be equal that the classes they represent
        /// </summary>
        public override bool Equals(object obj) {
            return this.RealType.Equals(obj);
        }
        public override int GetHashCode() {
            return this.FullName.GetHashCode();
        }
        #endregion

        #region IsValueType()

        /// <summary>
        /// True if type expression is a ValueType. Otherwise, false.
        /// </summary>
        /// <returns>Returns true if the type expression is a ValueType. Otherwise, false.</returns>
        public override bool IsValueType()
        {
           return this.realType.IsValueType();
        }

        #endregion

    }
}
