////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: StringType.cs                                                        //
// Authors: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                   //
//          Francisco Ortin - francisco.ortin@gmail.com                       //
// Description:                                                               //
//    Represent a string type.                                                //
//    Inheritance: TypeExpression.                                            //
//    Implements Composite pattern [Leaf].                                    //
//    Implements Singleton pattern.                                           //
// -------------------------------------------------------------------------- //
// Create date: 22-10-2006                                                    //
// Modification date: 22-03-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using AST;
using ErrorManagement;
using Tools;
using TypeSystem.Operations;

namespace TypeSystem {
    /// <summary>
    /// Represent a string type.
    /// </summary>
    /// <remarks>
    /// Inheritance: TypeExpression.
    /// Implements Composite pattern [Leaf].
    /// Implements Singleton pattern.
    /// </remarks>
    public class StringType : TypeExpression {
        
        #region Fields

        /// <summary>
        /// instance of class StringType. (unique)
        /// </summary>
        private static readonly StringType instance = new StringType();

        /// <summary>
        /// To delegate all the object oriented behaviour that the built-in type does not offer
        /// </summary>
        private BCLClassType BCLType = new BCLClassType("System.String", Type.GetType("System.String"));

        #endregion

        #region Properties

        /// <summary>
        /// Gets the unique instance of StringType
        /// </summary>
        public static StringType Instance {
            get { return instance; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Private constructor of StringType.
        /// </summary>
        private StringType() {
            this.typeExpression = "string";
            this.fullName = "string";
        }

        /// <summary>
        /// Private and static constructor of StringType.
        /// </summary>
        static StringType() {
        }

        #endregion

        // WriteType Inference
        #region Dispatcher
        public override object AcceptOperation(TypeSystemOperation op, object arg) { return op.Exec(this, arg); }
        #endregion


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
            StringType st = te as StringType;
            if (st != null)
                return true;
            if (te is TypeVariable && unification != SortOfUnification.Incremental)
                // * No incremental unification is commutative
                return te.Unify(this, unification, previouslyUnified);
            return false;
        }
        #endregion
    }
}
