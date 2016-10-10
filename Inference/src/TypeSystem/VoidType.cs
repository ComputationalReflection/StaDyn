////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: VoidType.cs                                                          //
// Authors: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                   //
//          Francisco Ortin - francisco.ortin@gmail.com                       //
// Description:                                                               //
//    Represents a void type.                                                 //
//    Inheritance: TypeExpression.                                            //
//    Implements Composite pattern [Leaf].                                    //
//    Implements Singleton pattern.                                           //
// -------------------------------------------------------------------------- //
// Create date: 22-10-2006                                                    //
// Modification date: 20-02-2007                                              //
////////////////////////////////////////////////////////////////////////////////
//visto
using System;
using System.Collections.Generic;
using System.Text;
using Tools;
using TypeSystem.Operations;

namespace TypeSystem
{
   /// <summary>
   /// Represents a void type.
   /// </summary>
   /// <remarks>
   /// Inheritance: TypeExpression.
   /// Implements Composite pattern [Leaf].
   /// Implements Singleton pattern.
   /// </remarks>
   public class VoidType : TypeExpression
   {
      #region Fields

      /// <summary>
      /// Instance of VoidType. (unique)
      /// </summary>
      private static readonly VoidType instance = new VoidType();

      #endregion

      #region Properties

      /// <summary>
      /// Gets the unique instance of VoidType
      /// </summary>
      public static VoidType Instance
      {
         get { return instance; }
      }

      #endregion

      #region Constructors

      /// <summary>
      /// Private constructor of VoidType.
      /// </summary>
      private VoidType()
      {
         this.typeExpression = "void";
         this.fullName = "void";
      }

      /// <summary>
      /// Private and static constructor of VoidType.
      /// </summary>
      static VoidType()
      {
      }

      #endregion

       // WriteType Promotion

      #region Dispatcher
      public override object AcceptOperation(TypeSystemOperation op, object arg) { return op.Exec(this, arg); }
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
          VoidType vt = te as VoidType;
          if (vt != null)
              return true;
          if (te is TypeVariable && unification!=SortOfUnification.Incremental)
              // * No incremental unification is commutative
              return te.Unify(this, unification, previouslyUnified);
          return false;
      }
      #endregion


   }
}
