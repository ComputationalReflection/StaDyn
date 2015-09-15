////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: PropertyType.cs                                                      //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Represents a property type.                                             //
//    Inheritance: MemberType.                                                //
//    Implements Composite pattern [Composite].                               //
// -------------------------------------------------------------------------- //
// Create date: 16-03-2007                                                    //
// Modification date: 29-03-2007                                              //
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
    /// Represents a property type.
    /// </summary>
    /// <remarks>
    /// Inheritance: MemberType.
    /// Implements Composite pattern [Composite].
    /// </remarks>
    public class PropertyType : TypeExpression, IMemberType {
        #region Fields

        /// <summary>
        /// Represents the property type expression.
        /// </summary>
        private TypeExpression propertyType;

        /// <summary>
        /// Represents if the property can be written.
        /// </summary>
        private bool setAccess;

        /// <summary>
        /// Represents if the property can be read.
        /// </summary>
        private bool getAccess;

        /// <summary>
        /// Links to attribute information (modifiers and its class type)
        /// </summary>
        private AccessModifier memberInfo;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the property type.
        /// </summary>
        public TypeExpression PropertyTypeExpression {
            get { return this.propertyType; }
        }

        /// <summary>
        /// Gets or sets the attribute information of method type
        /// </summary>
        public AccessModifier MemberInfo {
            get { return this.memberInfo; }
            set {
                if (this.memberInfo == null) {
                    this.memberInfo = value;
                }
                else
                    ErrorManager.Instance.NotifyError(new ClassTypeInfoError(this.memberInfo.MemberIdentifier, this.memberInfo.Class.FullName, value.Class.FullName));
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of PropertyType
        /// </summary>
        /// <param name="type">Property type.</param>
        /// <param name="canRead">True if the property can be read, false otherwise.</param>
        /// <param name="canWrite">True if the property can be written, false otherwise.</param>
        public PropertyType(TypeExpression type, bool canRead, bool canWrite) {
            this.propertyType = type;
            this.getAccess = canRead;
            this.setAccess = canWrite;
            this.BuildFullName();
        }

        #endregion

        #region BuildTypeExpressionString

        /// <summary>
        /// Creates the type expression string.
        /// </summary>
        public override string BuildTypeExpressionString(int depthLevel) {
            if (this.ValidTypeExpression) return this.typeExpression;
            if (depthLevel <= 0) return this.FullName;

            //this.fullName = this.MemberInfo.Class.FullName + "." + this.MemberInfo.MemberIdentifier;
            if (this.propertyType == null)
                return "null";
            this.fullName = this.propertyType.FullName;

            StringBuilder tE = new StringBuilder();
            // tE: Property(IdClass, IdProperty, mods, type)
            tE.AppendFormat("Property({0}, {1},", this.MemberInfo.Class.BuildTypeExpressionString(depthLevel-1), this.MemberInfo.MemberIdentifier);
            // modifiers
            if (this.MemberInfo.Modifiers.Count != 0) {
                for (int i = 0; i < this.MemberInfo.Modifiers.Count - 1; i++) {
                    tE.AppendFormat(" {0} x", this.MemberInfo.Modifiers[i]);
                }
                tE.AppendFormat(" {0}", this.MemberInfo.Modifiers[this.MemberInfo.Modifiers.Count - 1]);
            }
            tE.Append(", ");

            // type
            tE.Append(this.propertyType.BuildTypeExpressionString(depthLevel-1));
            tE.Append(")");
            this.ValidTypeExpression = true;
            return this.typeExpression=tE.ToString();
        }
        #endregion

        #region BuildFullName()
        /// <summary>
        /// Creates/Updates the full name of the type expression
        /// </summary>
        public override void BuildFullName() {
            if (this.MemberInfo != null)
                this.fullName = String.Format("{0}.{1}:{2}", this.MemberInfo.Class.FullName,
                    this.MemberInfo.MemberIdentifier, this.propertyType.FullName);
            else
                this.fullName = this.propertyType.FullName;
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
            // * Clears the type expression cache
            this.ValidTypeExpression = false;
            te.ValidTypeExpression = false;
            // TODO (not implemented yet; it is not possible to define var properties)
            return false;
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
            bool toReturn = this.propertyType.HasTypeVariables();
            this.validHasTypeVariables = true;
            return this.hasTypeVariablesCache = toReturn;

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
            PropertyType newPropertyType = (PropertyType)this.MemberwiseClone();
            if (newPropertyType.propertyType != null)
                newPropertyType.propertyType = newPropertyType.propertyType.Clone(clonedTypeVariables,equivalenceClasses, methodAnalyzed);
            return newPropertyType;
        }
        #endregion

        // Code Generation

        #region ILType()

        /// <summary>
        /// Gets the string type to use in IL code.
        /// </summary>
        /// <returns>Returns the string type to use in IL code.</returns>
        public override string ILType()
        {
           return this.propertyType.ILType();
        }

        #endregion

        #region IsValueType()

        /// <summary>
        /// True if type expression is a ValueType. Otherwise, false.
        /// </summary>
        /// <returns>Returns true if the type expression is a ValueType. Otherwise, false.</returns>
        public override bool IsValueType()
        {
           return this.propertyType.IsValueType();
        }

        #endregion


    }
}
