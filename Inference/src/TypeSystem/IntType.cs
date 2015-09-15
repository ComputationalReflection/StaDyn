////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: IntType.cs                                                           //
// Authors: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                   //
//          Francisco Ortin - francisco.ortin@gmail.com                       //
// Description:                                                               //
//    Represent a integer type.                                               //
//    Inheritance: TypeExpression.                                            //
//    Implements Composite pattern [Leaf].                                    //
//    Implements Singleton pattern.                                           //
// -------------------------------------------------------------------------- //
// Create date: 15-10-2006                                                    //
// Modification date: 05-06-2007                                              //
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
    /// Represent a integer type.
    /// </summary>
    /// <remarks>
    /// Inheritance: TypeExpression.
    /// Implements Composite pattern [Leaf].
    /// Implements Singleton pattern.
    /// </remarks>
    public class IntType : TypeExpression {
        #region Fields

        /// <summary>
        /// instance of class IntType. (unique)
        /// </summary>
        private static IntType instance;

        /// <summary>
        /// To delegate all the object oriented behaviour that the built-in type does not offer
        /// </summary>
        private BCLClassType BCLType = new BCLClassType("System.Int32", Type.GetType("System.Int32"));

        #endregion

        #region Properties

        /// <summary>
        /// Gets the unique instance of IntType
        /// </summary>
        public static IntType Instance {
            get {
                if (instance == null)
                    instance = new IntType();
                return instance;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Private constructor of IntType.
        /// </summary>
        private IntType() {
            this.typeExpression = "int";
            this.fullName = "int";
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
            IntType it = te as IntType;
            if (it != null)
                return true;
            if (te is TypeVariable && unification!=SortOfUnification.Incremental)
                // * No incremental unification is commutative
                return te.Unify(this, unification, previouslyUnified);
            if (te is FieldType && unification == SortOfUnification.Equivalent)
                return ((FieldType)te).FieldTypeExpression.Unify(this, unification, previouslyUnified);
            return false;
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
           return "int32";
        }

        #endregion

        #region IsValueType()

        /// <summary>
        /// True if type expression is a ValueType. Otherwise, false.
        /// </summary>
        /// <returns>Returns true if the type expression is a ValueType. Otherwise, false.</returns>
        public override bool IsValueType()
        {
           return true;
        }

        #endregion


    }
}
