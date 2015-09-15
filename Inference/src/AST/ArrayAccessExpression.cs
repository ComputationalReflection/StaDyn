////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: ArrayAccessExpression.cs                                             //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates the array expression to access the concrete position.      //
//    Inheritance: BinaryExpression.                                          //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 07-12-2006                                                    //
// Modification date: 14-12-2006                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using Tools;
using ErrorManagement;

namespace AST
{
   #region ArrayOperator

   /// <summary>
   /// Array operators
   /// </summary>
   public enum ArrayOperator
   {
      /// <summary>
      /// a[b]
      /// </summary>
      Indexer,
   }

   #endregion

   /// <summary>
   /// Encapsulates the array expression to access the concrete position.
   /// </summary>
   /// <remarks>
   /// Inheritance: BinaryExpression.
   /// Implements Composite pattern [Component].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class ArrayAccessExpression : BinaryExpression
   {
      #region Fields

      /// <summary>
      /// Operator of the array expression
      /// </summary>
      private ArrayOperator op;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the operator of the array expression
      /// </summary>
      public ArrayOperator Operator
      {
         get { return op; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of ArrayAccessExpression
      /// </summary>
      /// <param name="id">Name for the array expression.</param>
      /// <param name="index">Index of the array expression.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public ArrayAccessExpression(Expression id, Expression index, Location location)
         : base(id, index, location)
      {
         this.op = ArrayOperator.Indexer;
      }

      #endregion

      #region Accept()

      /// <summary>
      /// Accept method of a concrete visitor.
      /// </summary>
      /// <param name="v">Concrete visitor</param>
      /// <param name="o">Optional information to use in the visit.</param>
      /// <returns>Optional information to return</returns>
      public override Object Accept(Visitor v, Object o)
      {
         return v.Visit(this, o);
      }

      #endregion
   }
}
