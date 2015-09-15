////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: NullType.cs                                                          //
// Authors: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                   //
//          Francisco Ortin - francisco.ortin@gmail.com                       //
// Description:                                                               //
//    Represent a null type.                                                  //
//    Inheritance: TypeExpression.                                            //
//    Implements Composite pattern [Leaf].                                    //
//    Implements Singleton pattern.                                           //
// -------------------------------------------------------------------------- //
// Create date: 05-04-2007                                                    //
// Modification date: 05-04-2007                                              //
////////////////////////////////////////////////////////////////////////////////
//VISTO
using System;
using System.Collections.Generic;
using System.Text;

using AST;
using ErrorManagement;
using Tools;
using TypeSystem.Operations;

namespace TypeSystem {
    /// <summary>
    /// Represent a null type.
    /// </summary>
    /// <remarks>
    /// Inheritance: TypeExpression.
    /// Implements Composite pattern [Leaf].
    /// Implements Singleton pattern.
    /// </remarks>
    public class NullType : TypeExpression {
        
        #region Fields

        /// <summary>
        /// Instance of class NullType. (unique)
        /// </summary>
        private static readonly NullType instance = new NullType();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the unique instance of NullType
        /// </summary>
        public static NullType Instance {
            get { return instance; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Private constructor of NullType.
        /// </summary>
        private NullType() {
            this.typeExpression = "null";
            this.fullName = "null";
        }

        /// <summary>
        /// Private and static constructor of NullType.
        /// </summary>
        static NullType() {
        }

        #endregion

        // WriteType Inference

        #region Dispatcher
        public override object AcceptOperation(TypeSystemOperation op, object arg) { return op.Exec(this, arg); }
        #endregion


        // WriteType Unification

        #region Unify
        /// <summary>
        /// This method unifies two type expressions (this and te)
        /// </summary>
        /// <param name="unification">Indicates if the kind of unification (equivalent, incremental or override).</param>
        /// <param name="te">The expression to be unfied with this</param>
        /// <param name="previouslyUnified">To detect infinite loops. The previously unified pairs of type expressions.</param>
        /// <returns>If the unification was successful</returns>
        public override bool Unify(TypeExpression te, SortOfUnification unification, IList<Pair<TypeExpression, TypeExpression>> previouslyUnified) {

            throw new NotImplementedException("NullType.Unify() Not implemented");
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

        public bool Equals(NullType other)
        {
            return true;
        }

        public override bool Equals(object obj)
        {            
            if (obj.GetType() != typeof (NullType)) 
                return false;
            return Equals((NullType) obj);
        }

        public override int GetHashCode()
        {
            return 0;
        }        
    }
}
