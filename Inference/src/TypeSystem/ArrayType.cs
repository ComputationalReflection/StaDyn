////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: ArrayType.cs                                                         //
// Authors: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                   //
//          Francisco Ortin - francisco.ortin@gmail.com                       //
// Description:                                                               //
//    Represents an array type.                                               //
//    Inheritance: TypeExpression.                                            //
//    Implements Composite pattern [Composite].                               //
// -------------------------------------------------------------------------- //
// Create date: 27-01-2007                                                    //
// Modification date: 05-06-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using AST;
using ErrorManagement;
using Tools;
using TypeSystem.Operations;

namespace TypeSystem {
    /// <summary>
    /// Representa an array type.
    /// </summary>
    /// <remarks>
    /// Inheritance: TypeExpression.
    /// Implements Composite pattern [Composite].
    /// </remarks>
    public class ArrayType : TypeExpression {
        #region Fields

        /// <summary>
        /// Represents the array type.
        /// </summary>
        private TypeExpression arrayType;

        /// <summary>
        /// To delegate all the object oriented behaviour that the built-in type does not offer
        /// </summary>
        private BCLClassType BCLType;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the array type.
        /// </summary>
        public TypeExpression ArrayTypeExpression {
            get { return this.arrayType; }
        }
      

        /// <summary>
        /// Indicates if the type has been set as dynamic
        /// </summary>
        public override bool IsDynamic {
            set {
                // * If an array is dynamic, so it is its element
                this.isDynamic = value;
                if (this.arrayType != null)
                    this.arrayType.IsDynamic = value;
            }
        }
      
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of ArrayType
        /// </summary>
        public ArrayType(TypeExpression type) {
            this.arrayType = type;
            this.fullName = type.FullName + "[]";
            string introspectiveName = type.GetType().FullName + "[]";
            this.BCLType = new BCLClassType(this.fullName, Type.GetType(introspectiveName));

        }

        #endregion
        
        #region Dispatcher
        public override object AcceptOperation(TypeSystemOperation op, object arg) { return op.Exec(this, arg); }
        #endregion

        #region BuildTypeExpressionString()
        public override string BuildTypeExpressionString(int depthLevel) {
            if (this.ValidTypeExpression) return this.typeExpression;
            if (depthLevel <= 0) return this.FullName;

            this.typeExpression = this.typeExpression = String.Format("Array({0})", this.arrayType.BuildTypeExpressionString(depthLevel - 1));
            this.ValidTypeExpression = true;
            return typeExpression;
        }
        #endregion

        #region BuildFullName()
        /// <summary>
        /// Creates/Updates the full name of the type expression
        /// </summary>
        public override void BuildFullName() {
            this.fullName = this.arrayType.fullName + "[]";
        }
        #endregion

        // WriteType inference

        
        #region AsClassType()
        /// <summary>
        /// Represent a type as a class. It is mainly used to obtain the BCL representation of types
        /// (string=String, int=Int32, []=Array...)
        /// </summary>
        /// <returns>The class type is there is a map, null otherwise</returns>
        public override ClassType AsClassType() {
            return this.BCLType;
        }
        #endregion

        // WriteType promotion

        #region Equals&GetHashCode()
        public override bool Equals(object obj) {
            ArrayType parameter = obj as ArrayType;
            if (parameter == null) 
                return false;
            if (parameter == this)
                return true;
            return this.arrayType.Equals(parameter.arrayType);
        }
        public override int GetHashCode() {
            return this.FullName.GetHashCode();
        }
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
            // * Infinite recursion detection
            Pair<TypeExpression, TypeExpression> pair = new Pair<TypeExpression, TypeExpression>(this, te);
            if (previouslyUnified.Contains(pair))
                return true;
            previouslyUnified.Add(pair);

            bool success = false;
            ArrayType at = te as ArrayType;
            if (at != null)
                success = this.arrayType.Unify(at.arrayType, unification, previouslyUnified);
            else if (te is TypeVariable) {
                TypeVariable typeVariable = (TypeVariable)te;
                if (unification != SortOfUnification.Incremental)
                    // * Incremental is commutative
                    success = typeVariable.Unify(this, unification, previouslyUnified);
                else { // * Array(var) should unify to Var=Array(int)
                    if (typeVariable.Substitution != null)
                        success = this.Unify(typeVariable.Substitution, unification, previouslyUnified);
                    else success = false;
                }
            }
            else if (te is UnionType)
                success = te.Unify(this, unification, previouslyUnified);
            // * Clears the type expression cache
            this.ValidTypeExpression = false;
            te.ValidTypeExpression = false;
            return success;
        }
        #endregion

        #region HasTypeVariables()
        /// <summary>
        /// To know if the type expression has some type variables and requieres unification
        /// The default implementation is return false
        /// </summary>
        /// <returns>If it has any type variable</returns>
        public override bool HasTypeVariables() {
            if (this.validHasTypeVariables)
                return this.hasTypeVariablesCache;
            bool toReturn = this.arrayType.HasTypeVariables();
            this.validHasTypeVariables = true;
            return this.hasTypeVariablesCache = toReturn;
        }
        #endregion

        #region CloneType()
        /// <summary>
        /// This method creates a new class type, creating new type variables for
        /// each field. It these type variables where bounded to types or other
        /// type variables, they are maintained.
        /// </summary>
        /// <param name="typeVariableMappings">Each new type varaiable represent a copy of another existing one.
        /// This parameter is a mapping between them, wher tmpName=old and value=new.</param>
        /// <returns>The new cloned class type</returns>
        public override TypeExpression CloneType(IDictionary<TypeVariable, TypeVariable> typeVariableMappings) {
            if (!this.HasTypeVariables())
                return this;
            // * We clone the type of the elements
            IList<EquivalenceClass> equivalenceClasses = new List<EquivalenceClass>();
            ArrayType newArrayType = (ArrayType)this.CloneTypeVariables(typeVariableMappings, equivalenceClasses, new List<ClassType>());
            // * For each equivalence class we create a new one, 
            //   substituting the old type variables for the new ones
            // * The substitution is not altered
            // * Since equivalence classes and type variables have a bidirectional association,
            //   the new equivalence classes will make type variables update their new equivalence classes
            foreach (EquivalenceClass equivalenceClass in equivalenceClasses)
                equivalenceClass.UpdateEquivalenceClass(typeVariableMappings);
            newArrayType.ValidTypeExpression = false;
            // * The new class type is returned
            return newArrayType;
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
            if (!this.HasTypeVariables())
                return this;
            ArrayType newType = (ArrayType)this.MemberwiseClone();
            newType.arrayType = newType.arrayType.CloneTypeVariables(typeVariableMappings, equivalenceClasses, clonedClasses);
            newType.ValidTypeExpression = false;
            return newType;
        }
        #endregion


        #region UpdateEquivalenceClass()
        /// <summary>
        /// Replaces the equivalence class of type variables substituting the old type variables for the new ones.
        /// </summary>
        /// <param name="typeVariableMappings">Each new type varaiable represent a copy of another existing one.
        /// This parameter is a mapping between them, wher tmpName=old and value=new.</param>
        /// <param name="previouslyUpdated">To detect infinite loops. Previously updated type expressions.</param>
        public override void UpdateEquivalenceClass(IDictionary<TypeVariable, TypeVariable> typeVariableMappings, IList<TypeExpression> previouslyUpdated) {
            // * Checks infinite loops
            if (previouslyUpdated.Contains(this))
                return;
            previouslyUpdated.Add(this);

            // * Updates the equivalence class
            if (!this.HasTypeVariables())
                return;
            this.arrayType.UpdateEquivalenceClass(typeVariableMappings, previouslyUpdated);
            this.ValidTypeExpression = false;
        }
        #endregion

        #region ReplaceTypeVariables()
        /// <summary>
        /// Replaces type variables substituting the old type variables for the new ones.
        /// </summary>
        /// <param name="typeVariableMappings">Each new type varaiable represent a copy of another existing one.
        /// This parameter is a mapping between them, wher tmpName=old and value=new.</param>
        public override void ReplaceTypeVariables(IDictionary<TypeVariable, TypeVariable> typeVariableMappings) {
            TypeVariable arrayTypeVariable = this.arrayType as TypeVariable;
            if (arrayTypeVariable == null) {
                if (this.arrayType.HasTypeVariables())
                    this.arrayType.ReplaceTypeVariables(typeVariableMappings);
                return;
            }
            if (typeVariableMappings.ContainsKey(arrayTypeVariable))
                this.arrayType = typeVariableMappings[arrayTypeVariable];
            this.ValidTypeExpression = false;
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
            ArrayType newType = (ArrayType)this.MemberwiseClone();
            newType.arrayType = newType.arrayType.Clone(clonedTypeVariables, equivalenceClasses, methodAnalyzed);
            newType.ValidTypeExpression = false;
            return newType;
        }
        #endregion

        // Code Generation

        #region ILType()

        /// <summary>
        /// Gets the string type to use in IL code.
        /// </summary>
        /// <returns>Returns the string type to use in IL code.</returns>
        public override string ILType() {
            if (this.ArrayTypeExpression is TypeVariable && ((TypeVariable)this.ArrayTypeExpression).Substitution != null && ((TypeVariable)this.ArrayTypeExpression).Substitution.IsValueType())            
                return "object[]";            
            else            
                return this.arrayType.ILType() + "[]";                         
        }

        #endregion

        #region IsValueType()

        /// <summary>
        /// True if type expression is a ValueType. Otherwise, false.
        /// </summary>
        /// <returns>Returns true if the type expression is a ValueType. Otherwise, false.</returns>
        public override bool IsValueType()
        {
           return false;
        }

        #endregion

    }
}
