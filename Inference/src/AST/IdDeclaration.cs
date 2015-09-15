////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: IdDeclaration.cs                                                     //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a declaration.                                             //
//    Inheritance: Declaration.                                               //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 04-01-2007                                                    //
// Modification date: 12-06-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using Tools;
using TypeSystem;
using Symbols;
using ErrorManagement;
using AST.Operations;
namespace AST
{
   /// <summary>
   /// Encapsulates a declaration.
   /// </summary>
   /// <remarks>
   /// Inheritance: Declaration.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class IdDeclaration : Declaration
   {
      #region Fields

      /// <summary>
      /// Name of the declaration.
      /// </summary>
      private SingleIdentifierExpression identifier;

       /// <summary>
       /// The symbol of the identifier
       /// </summary>
       private Symbol symbol;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the name associated to the declaration
      /// </summary>
      public string Identifier
      {
         get { return this.identifier.Identifier; }
      }

      /// <summary>
      /// Gets the IL name associated to the declaration identifier.
      /// </summary>
      public string ILName
      {
         get { return this.IdentifierExp.ILName; }
      }

      /// <summary>
      /// Gets the identifier expression
      /// </summary>
      public SingleIdentifierExpression IdentifierExp
      {
         get { return this.identifier; }
      }

      /// <summary>
      /// The symbol of the identifier
      /// </summary>
       public Symbol Symbol {
           get { return symbol; }
           set { symbol = value; }
       }

        #endregion

      #region Constructor

      /// <summary>
      /// Constructor of IdDeclaration
      /// </summary>
      /// <param name="id">Name of the declaration.</param>
      /// <param name="type">TypeExpression of the declaration.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
       public IdDeclaration(SingleIdentifierExpression id, string type, Location location)
           : base(type, location)
      {
         this.identifier = id;
         this.identifier.IndexOfSSA = 0;
      }

      /// <summary>
      /// Constructor of IdDeclaration
      /// </summary>
      /// <param name="id">Name of the declaration.</param>
      /// <param name="type">TypeExpression of the declaration.</param>
      /// <param name="indexSSA">Number associated to the declaration for SSA algorithm</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
       public IdDeclaration(SingleIdentifierExpression id, int indexSSA, string type, Location location)
           : base(type, location)
      {
         this.identifier = id;
         this.identifier.IndexOfSSA = indexSSA;
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
