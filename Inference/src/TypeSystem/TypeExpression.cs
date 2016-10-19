////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: TypeExpression.cs                                                    //
// Authors: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                   //
//          Francisco Ortin - francisco.ortin@gmail.com                       //
// Description:                                                               //
//    Abstract class that represents all different types.                     //
//    Implements Composite pattern [Component].                               //
// -------------------------------------------------------------------------- //
// Create date: 15-10-2006                                                    //
// Modification date: 05-06-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

using AST;
using ErrorManagement;
using Tools;
using TypeSystem.Operations;

namespace TypeSystem
{
    /// <summary>
    /// Abstract class that represents all different types.
    /// </summary>
    /// <remarks>
    /// Implements Composite pattern [Component].
    /// </remarks>
    /// //visto
    public abstract class TypeExpression
    {

        #region Fields

        /// <summary>
        /// Represents the type by a debug string
        /// Note: WriteType expression is the longest recursive representation of a type expression. 
        /// Fullname is the sortest one.
        /// </summary>
        internal protected string typeExpression;

        /// <summary>
        /// Represents the full name of the type
        /// Note: WriteType expression is the longest recursive representation of a type expression. 
        /// Fullname is the sortest one.
        /// </summary>
        internal protected string fullName;

        /// <summary>
        /// To implement a type expression cache
        /// </summary>
        protected bool validTypeExpression = false;

        /// <summary>
        /// The cached value ot the HasTypeVariables method
        /// </summary>
        protected bool hasTypeVariablesCache;

        /// <summary>
        /// To cache the result of the HasTypeVariables method
        /// </summary>
        protected bool validHasTypeVariables = false;

        /// <summary>
        /// In order to avoid stack overflow in the construction of typeexpression
        /// string (ToString), we set a maximum level of depth
        /// </summary>
        public const int MAX_DEPTH_LEVEL_TYPE_EXPRESSION = 1;

        /// <summary>
        /// Indicates if the type has been set as dynamic
        /// </summary>
        protected bool isDynamic;
        #endregion

        #region Properties

        /// <summary>
        /// Gets the full name of the type
        /// Note: WriteType expression is the longest recursive representation of a type expression. 
        /// Fullname is the sortest one.
        /// </summary>
        public virtual string FullName
        {
            get { return this.fullName; }
            set { this.fullName = value; }
        }

        /// <summary>
        /// To implement a type expression cache
        /// </summary>
        internal virtual bool ValidTypeExpression
        {
            get { return validTypeExpression; }
            set
            {
                validTypeExpression = value;
                if (!value)
                {
                    // * Updates the full name
                    this.BuildFullName();
                    this.typeExpression = this.fullName;
                }
            }
        }

        /// <summary>
        /// Indicates if the type has been set as dynamic
        /// </summary>
        public virtual bool IsDynamic
        {
            get { return this.isDynamic; }
            set { this.isDynamic = value; }
        }
        #endregion

        // * Constructors

        #region Construtors
        public TypeExpression()
        {
            this.isDynamic = false;
        }
        public TypeExpression(bool isDynamic)
        {
            this.isDynamic = isDynamic;
        }
        #endregion

        // * Debug

        #region ToString()

        /// <summary>
        /// Returns the type expression cached in the typeExpression field.
        /// Note: WriteType expression is the longest recursive representation of a type expression. 
        /// Fullname is the sortest one.
        /// </summary>
        /// <returns>string with the type expression associated.</returns>
        public override string ToString()
        {
            if (!this.ValidTypeExpression)
            {
                this.typeExpression = this.BuildTypeExpressionString(MAX_DEPTH_LEVEL_TYPE_EXPRESSION);
                this.ValidTypeExpression = true;
            }
            return this.typeExpression;
        }

        #endregion

        #region BuildTypeExpressionString()
        /// <summary>
        /// Returns the type expression 
        /// <param name="depthLevel">The maximum depth of recursion to construct type expressions</param>
        /// </summary>
        public virtual string BuildTypeExpressionString(int depthLevel)
        {
            return this.typeExpression;
        }
        #endregion

        #region BuildFullName()
        /// <summary>
        /// Creates/Updates the full name of the type expression
        /// </summary>
        public virtual void BuildFullName() { }
        #endregion

        #region Dispatcher
        public virtual object AcceptOperation(TypeSystemOperation op, object arg) { return op.Exec(this, arg); }
        #endregion

        #region AsClassType()
        /// <summary>
        /// Represent a type as a class. It is mainly used to obtain the BCL representation of types
        /// (string=String, int=Int32, []=Array...)
        /// </summary>
        /// <returns>The class type is there is a map, null otherwise</returns>
        public virtual ClassType AsClassType()
        {
            return null;
        }

        #endregion

        // WriteType Unification

        #region Unify()
        /// <summary>
        /// Tries to unify the type expression of this and the parameter
        /// </summary>
        /// <param name="te">The type expression to be unified together with this</param>
        /// <param name="unification">Indicates if the kind of unification (equivalent, incremental or override).</param>
        /// <param name="previouslyUnified">To detect infinite loops. The previously unified pairs of type expressions.</param>
        /// <returns>If both type expressionas could be unfied</returns>
        public abstract bool Unify(TypeExpression te, SortOfUnification unification, IList<Pair<TypeExpression, TypeExpression>> previouslyUnified);
        #endregion

        #region HasTypeVariables()
        /// <summary>
        /// To know if the type expression has some type variables and requieres unification
        /// The default implementation is return false
        /// </summary>
        /// <returns>If it has any type variable</returns>
        public virtual bool HasTypeVariables()
        {
            return false;
        }
        #endregion

        #region IsFreshVariable()
        /// <summary>
        /// To know if it is a type variable with no substitution
        /// </summary>
        /// <returns>True in case it is a fresh variable with no substitution</returns>
        public virtual bool IsFreshVariable()
        {
            return false;
        }
        #endregion

        #region HasFreshVariable()
        /// <summary>
        /// To know if it is a type variable with no substitution
        /// </summary>
        /// <returns>True in case it is a fresh variable with no substitution. This method has a different behaviour than IsFreshVariable with UnionTypes, the rest of types have the same.</returns>
        public virtual bool HasFreshVariable()
        {
            return IsFreshVariable();
        }
        #endregion

        #region HasIntersectionVariable()
        /// <summary>
        /// To know if it is a type variable with no substitution
        /// </summary>
        /// <returns>True in case it is a fresh variable with no substitution. This method has a different behaviour than IsFreshVariable with UnionTypes, the rest of types have the same.</returns>
        public virtual bool HasIntersectionVariable()
        {
            return false;
        }
        #endregion


        #region CloneType()
        /// <summary>
        /// This method creates a new type, creating new type variables for
        /// type expression. It these type variables where bounded to types or other
        /// type variables, they are maintained.
        /// </summary>
        /// <param name="typeVariableMappings">Each new type varaiable represent a copy of another existing one.
        /// This parameter is a mapping between them, wher tmpName=old and value=new.</param>
        /// <returns>The new cloned class type</returns>
        public virtual TypeExpression CloneType(IDictionary<TypeVariable, TypeVariable> typeVariableMappings)
        {
            // * By default, no clone is performed (built-in types)
            return this;
        }

        public virtual TypeExpression CloneType(IDictionary<TypeVariable, TypeVariable> typeVariableMappings, IDictionary<TypeExpression, TypeExpression> typeExpresionVariableMapping)
        {
            return CloneType(typeVariableMappings);            
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
        public virtual TypeExpression CloneTypeVariables(IDictionary<TypeVariable, TypeVariable> typeVariableMappings, IList<EquivalenceClass> equivalenceClasses, IList<ClassType> clonedClasses)
        {
            return this;
        }
        #endregion

        #region UpdateEquivalenceClass()
        /// <summary>
        /// Replaces the equivalence class of type variables substituting the old type variables for the new ones.
        /// </summary>
        /// <param name="typeVariableMappings">Each new type varaiable represent a copy of another existing one.
        /// This parameter is a mapping between them, wher tmpName=old and value=new.</param>
        /// <param name="previouslyUpdated">To detect infinite loops. Previously updated type expressions.</param>
        public virtual void UpdateEquivalenceClass(IDictionary<TypeVariable, TypeVariable> typeVariableMappings, IList<TypeExpression> previouslyUpdated)
        {
            // * Does nothing (built-in types are not recursive)
        }
        #endregion

        #region ReplaceTypeVariables()
        /// <summary>
        /// Replaces type variables substituting the old type variables for the new ones.
        /// </summary>
        /// <param name="typeVariableMappings">Each new type varaiable represent a copy of another existing one.
        /// This parameter is a mapping between them, wher tmpName=old and value=new.</param>
        public virtual void ReplaceTypeVariables(IDictionary<TypeVariable, TypeVariable> typeVariableMappings)
        {
            // * Nothing to to (built-in types)
        }
        #endregion

        #region Freeze()
        /// <summary>
        /// WriteType variable may change its type's substitution (e.g., field type variables)
        /// This method returns the type in an specific time (frozen).
        /// If this type's substitution changes, the frozen type does not.
        /// <returns>The frozen type</returns>
        /// </summary>
        public virtual TypeExpression Freeze()
        {
            return this;
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
        internal virtual TypeExpression Clone(IDictionary<int, TypeVariable> clonedTypeVariables, IList<EquivalenceClass> equivalenceClasses, MethodType methodAnalyzed)
        {
            if (this.HasTypeVariables())
                throw new InvalidOperationException("The type should implement a Clone method.");
            // * Default implementation (types with no type variables)
            return this;
        }
        #endregion

        // Loop Detection

        #region Remove()
        /// <summary>
        /// When loops are detected, it is necesary to suppress a new extra variable returned in 
        /// the return type of recursive functions
        /// </summary>
        /// <param name="toRemove">The type variable to remove</param>
        /// <returns>If it has been actually removed</returns>
        public virtual bool Remove(TypeVariable toRemove)
        {
            return false;
        }
        #endregion

        // Code Generation

        #region ILType()
        /// <summary>
        /// Gets the type name to use in IL code.
        /// </summary>
        /// <returns>Returns the type name to use in IL code.</returns>
        public virtual string ILType()
        {
            return this.fullName;
        }

        #endregion

        #region IsValueType()
        /// <summary>
        /// True if type expression is a ValueType. Otherwise, false.
        /// </summary>
        /// <returns>Returns true if the type expression is a ValueType. Otherwise, false.</returns>
        public abstract bool IsValueType();
        #endregion

        // Helper Methods

        #region As<T>()
        /// <summary>
        /// Returns a concrete type expression from a general one. It takes into accout that it can
        /// be a type variable and, if so, it does the same with its substitution.
        /// </summary>
        /// <typeparam name="T">A concrete type expression</typeparam>
        /// <param name="type">The general type expression</param>
        /// <returns>The cast type</returns>
        public static T As<T>(TypeExpression type) where T : TypeExpression
        {
            TypeVariable typeVariable = type as TypeVariable;
            if (typeVariable != null)
                type = typeVariable.Substitution;
            T castType = type as T;
            return castType;
        }
        #endregion

        #region Is<T>()
        /// <summary>
        /// Tells if a type expression is a type or a type variable
        /// unified to a type
        /// </summary>
        /// <typeparam name="T">A concrete type expression</typeparam>
        /// <param name="type">The general type expression</param>
        /// <returns>If the type is the expected one</returns>
        public static bool Is<T>(TypeExpression type) where T : TypeExpression
        {
            return TypeExpression.As<T>(type) != null;
        }
        #endregion

        virtual public BCLClassType getBCLType()
        {
            System.Diagnostics.Debug.Assert(true, "getBCLType called in type expression inconsistence in the program");
            return null;
        }


        #region Simplify()
        /// <summary>
        /// Gets the simplified version of the typeexpression.
        /// </summary>
        /// <returns>Returns the simplified type.</returns>
        public virtual TypeExpression Simplify(bool includeTypeVariables = true)
        {
            return this;
        }

        #endregion

    }
}
