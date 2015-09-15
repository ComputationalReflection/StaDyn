////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: InterfaceType.cs                                                     //
// Authors: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                   //
//          Francisco Ortin - francisco.ortin@gmail.com                       //
// Description:                                                               //
//    Represents a interface type.                                            //
//    Inheritance: UserType.                                                  //
//    Implements Composite pattern [Composite].                               //
//    Implements Template Method pattern.                                     //
// -------------------------------------------------------------------------- //
// Create date: 22-10-2006                                                    //
// Modification date: 09-03-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using ErrorManagement;
using AST;
using Tools;
using TypeSystem.Operations;

namespace TypeSystem {
    /// <summary>
    /// Represents a interface type.
    /// </summary>
    /// <remarks>
    /// Inheritance: UserType.
    /// Implements Composite pattern [Composite].
    /// </remarks>
    public class InterfaceType : UserType {
        #region Constructor

        /// <summary>
        /// Constructor of InterfaceType
        /// </summary>
        /// <param name="identifier">Class identifier.</param>
        /// <param name="fullName">Class full identifier.</param>
        /// <param name="modifiers">Modifiers of the class type</param>
        public InterfaceType(string identifier, string fullName, List<Modifier> modifiers)
            : base(fullName) {
            this.name = identifier;
            this.Modifiers = modifiers;
        }

        /// <summary>
        /// Constructor of InterfaceType
        /// </summary>
        /// <param name="name">Class identifier.</param>
        public InterfaceType(string name)
            : base(name) {
            this.name = name;
            this.Modifiers = new List<Modifier>();
        }
        #endregion
        
        #region AddBaseClass()
        /// <summary>
        /// Adds a new inherited type
        /// </summary>
        /// <param name="inheritedClass">Information about inherited type.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="line">Line number.</param>
        /// <param name="column">Column number.</param>
        public override void AddBaseClass(ClassType inheritedClass, Location location) {
            System.Diagnostics.Debug.Assert(false, "A base class cannot be added to an interface.");
        }
        #endregion
        
        #region BuiltTypeExpression()

        /// <summary>
        /// Creates the type expression string.
        /// </summary>
        public override string BuildTypeExpressionString(int depthLevel) {
            if (this.ValidTypeExpression) return this.typeExpression;
            if (depthLevel <= 0) return this.FullName;

            StringBuilder tE = new StringBuilder();
            // tE: Interface(id, modifiers, interfaces, members)
            tE.AppendFormat("Interface({0},", this.fullName);
            // modifiers
            if (this.modifierList.Count != 0) {
                for (int i = 0; i < this.modifierList.Count - 1; i++) {
                    tE.AppendFormat(" {0} x", this.modifierList[i]);
                }
                tE.AppendFormat(" {0}", this.modifierList[this.modifierList.Count - 1]);
            }
            tE.Append(", ");

            // interfaces
            if (this.interfaceList.Count != 0) {
                for (int i = 0; i < this.interfaceList.Count - 1; i++) {
                    tE.AppendFormat(" {0} x", this.interfaceList[i].FullName);
                }
                tE.AppendFormat(" {0}", this.interfaceList[this.interfaceList.Count - 1].FullName);
            }
            tE.Append(", ");

            // members
            if (this.Members.Count != 0) {
                Dictionary<string, AccessModifier>.KeyCollection keys = this.Members.Keys;
                int i = 0;
                foreach (string key in keys) {
                    tE.Append(this.Members[key].Type.BuildTypeExpressionString(depthLevel - 1));
                    if (i < keys.Count - 1)
                        tE.Append(" x");
                    i++;
                }
            }
            tE.Append(")");
            this.ValidTypeExpression = true;
            return this.typeExpression = tE.ToString();
        }

        #endregion

        // WriteType Inference
        #region Dispatcher
        public override object AcceptOperation(TypeSystemOperation op, object arg) { return op.Exec(this, arg); }
        #endregion


        // WriteType Unification

        #region ILType()

        /// <summary>
        /// Gets the string type to use in IL code.
        /// </summary>
        /// <returns>Returns the string type to use in IL code.</returns>
        public override string ILType() {
            StringBuilder aux = new StringBuilder();
            aux.AppendFormat("class {0}", this.fullName);
            return aux.ToString();
        }

        #endregion


        #region Unify
        /// <summary>
        /// This method unifies two type expressions (this and te)
        /// </summary>
        /// <param name="te">The expression to be unfied with this</param>
        /// <param name="unification">Indicates if the kind of unification (equivalent, incremental or override).</param>
        /// <param name="previouslyUnified">To detect infinite loops. The previously unified pairs of type expressions.</param>
        /// <returns>If the unification was successful</returns>
        public override bool Unify(TypeExpression te, SortOfUnification unification, IList<Pair<TypeExpression, TypeExpression>> previouslyUnified) {
            InterfaceType it = TypeExpression.As<InterfaceType>(te);
            if (it != null) {
                bool success = (bool)this.AcceptOperation(new EquivalentOperation(it), null);
                // * Clears the type expression cache
                this.ValidTypeExpression = false;
                te.ValidTypeExpression = false;
                return success;
            }
            if (te is TypeVariable && unification!=SortOfUnification.Incremental)
                // * No incremental unification is commutative
                return te.Unify(this, unification, previouslyUnified);
            return false;
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
