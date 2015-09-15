////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: NewArrayExpression.cs                                                //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a new array expression.                                    //
//    Inheritance: Expression.                                                //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 17-01-2007                                                    //
// Modification date: 26-03-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using AST.Operations;
using ErrorManagement;
using Tools;

namespace AST
{
   /// <summary>
   /// Encapsulates a new array expression.
   /// </summary>
   /// <remarks>
   /// Inheritance: Expression.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class NewArrayExpression : Expression
   {
      #region Fields

      /// <summary>
      /// Stores the name about the new type
      /// </summary>
      private string typeInfo;

      /// <summary>
      /// Stores the array initialization.
      /// </summary>
      private CompoundExpression init;

      /// <summary>
      /// Stores the rank of the array definition.
      /// </summary>
      private int rank = 1;

      /// <summary>
      /// Stores the size specified for the array.
      /// </summary>
      private Expression size;

      /// <summary>
      /// Auxiliar variable identifier used to store the new array in code generation.
      /// </summary>
      private string identifier;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the name of the new type
      /// </summary>
      public string TypeInfo
      {
         get { return this.typeInfo; }
      }

      /// <summary>
      /// Gets or sets the array initialization
      /// </summary>
      public CompoundExpression Init
      {
         get { return this.init; }
         set 
         { 
            this.init = value;
            if (this.size == null)
               this.size = new IntLiteralExpression(this.init.ExpressionCount, this.Location);
         }
      }

      /// <summary>
      /// Gets or sets the array rank
      /// </summary>
      public int Rank
      {
         get { return this.rank; }
         set { this.rank = value; }
      }

      /// <summary>
      /// Gets or sets the array size
      /// </summary>
      public Expression Size
      {
         get { return this.size; }
         set { this.size = value; }
      }

      /// <summary>
      /// Gets or sets the auxiliar variable identifier used to store the new array in code generation.
      /// </summary>
      public string Identifier
      {
         get { return this.identifier; }
         set { this.identifier = value; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of NewArrayExpression
      /// </summary>
      /// <param name="arrayType">Array type.</param>
      /// <param name="arrayRank">Rank of the new array expression.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public NewArrayExpression(string arrayType, Location location)
          : base(location) 
      {
         this.typeInfo = arrayType;
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
	  
	  #region Dispatcher AstOperation
      /// <summary>
      /// Dispatches expressions to the operation passed as argument.
      /// It provokes the execution of op.AcceptOperation(AstNode) with the parameter
      /// resolved polymorfically
      /// </summary>
      /// <param name="op">AstOperation to dispatch</param>
      /// <returns></returns>
      public override object AcceptOperation(AstOperation op, object arg) {
          return op.Exec(this, arg);
      }

      #endregion
   }
}
