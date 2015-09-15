//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: NameSpaceType.cs                                                     
// Author: Francisco Ortin - francisco.ortin@gmail.com                       
// Description:                                                               
//    Represents a type obtained when explicitely using namespaces IDs.
//        Eg: "System.Diagnostics".Debug
//    Inheritance: TypeExpression.                                            
//    Implements Composite pattern [Leaf].                               
// -------------------------------------------------------------------------- 
// Create date: 06-04-2007                                                    
// Modification date: 06-04-2007                                              
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using Tools;
using TypeSystem.Operations;

namespace TypeSystem {
    /// <summary>
    /// WriteType of a namespace 
    /// </summary>
    public class NameSpaceType : TypeExpression {

        #region Fields
        /// <summary>
        /// The qualified name of the namespace
        /// </summary>
        private string name;
        #endregion


        #region Properties
        /// <summary>
        /// The qualified name of the namespace
        /// </summary>
        public string Name {
            get { return name; }
            set { name = value; }
        }
        #endregion

        #region Constructors
        public NameSpaceType(string name) {
            this.name = name;
            this.fullName = name;
        }

        public NameSpaceType(string name,string fullname)
        {
            this.name = name;
            this.fullName = fullname;
        }
        #endregion

        #region Dispatcher
        public override object AcceptOperation(TypeSystemOperation op, object arg) { return op.Exec(this, arg); }
        #endregion

        #region Unify()
        /// <summary>
        /// This method unifies two type expressions (this and te)
        /// </summary>
        /// <param name="te">The expression to be unfied with this</param>
        /// <param name="unification">Indicates if the kind of unification (equivalent, incremental or override).</param>
        /// <param name="previouslyUnified">To detect infinite loops. The previously unified pairs of type expressions.</param>
        /// <returns>If the unification was successful</returns>
        public override bool Unify(TypeExpression te, SortOfUnification unification, IList<Pair<TypeExpression, TypeExpression>> previouslyUnified) {
            return false;
        }

        /// <summary>
        /// Modifies this namespace to create it as a child namespace
        /// </summary>
        /// <param name="subNamespace">The name of the child namespace</param>
        /// <returns>The modified namespace type</returns>
        public NameSpaceType concat(string subNamespace) {
            this.name = String.Format("{0}.{1}", this.name, subNamespace);
            this.fullName = this.name;
            return this;
        }

        #region ToString()
        public override string ToString() {
            return this.name;
        }
        
        #endregion
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