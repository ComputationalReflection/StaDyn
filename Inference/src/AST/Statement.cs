////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: Statement.cs                                                         //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Abstract class represents a programming language statement.             //
//    Inheritance: AstNode.                                                   //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Element].                                   //
// -------------------------------------------------------------------------- //
// Create date: 04-12-2006                                                    //
// Modification date: 09-01-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using AST.Operations;
using ErrorManagement;

namespace AST
{
   /// <summary>
   /// Abstract class represents a programming language statement.
   /// </summary>
   /// <remarks>
   /// Inheritance: AstNode.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Element].
   /// </remarks>
   public abstract class Statement : AstNode
   {
      #region Constructor

      /// <summary>
      /// Protected constructor of Statement.
      /// </summary>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      protected Statement(Location location) : base(location) { }

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
