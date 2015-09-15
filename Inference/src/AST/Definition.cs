////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: Definition.cs                                                        //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a definition.                                              //
//    Inheritance: IdDeclaration.                                             //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 07-12-2006                                                    //
// Modification date: 01-02-2006                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using Tools;
using TypeSystem;
using ErrorManagement;
using AST.Operations;

namespace AST
{
   /// <summary>
   /// Encapsulates a definition.
   /// </summary>
   /// <remarks>
   /// Inheritance: IdDeclaration.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class Definition : IdDeclaration
   {
      #region Fields

      /// <summary>
      /// Represents the initialization of the definition. 
      /// </summary>
      private Expression initialization;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the initialization of the definition
      /// </summary>
      public Expression Init
      {
         get { return this.initialization; }
      }

      /// <summary>
      /// WriteType variable may change its type's substitution (e.g., field type variables)
      /// This attribute saves the type in an specific time (frozen).
      /// If this type's substitution changes, the frozen type does not.
      /// </summary>
      public TypeExpression FrozenTypeExpression
      {
         get { return this.frozenTypeExpression; }
         set { this.frozenTypeExpression = value; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of Definition.
      /// </summary>
      /// <param name="id">Name of the definition.</param>
      /// <param name="type">TypeExpression of the definition.</param>
      /// <param name="init">Initialization of the definition.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public Definition(SingleIdentifierExpression id, string type, Expression init, Location location)
         : base(id, type, location)
      {
         this.initialization = init;
      }

      /// <summary>
      /// Constructor of Definition
      /// </summary>
      /// <param name="id">Name of the declaration.</param>
      /// <param name="type">TypeExpression of the declaration.</param>
      /// <param name="init">Initialization of the definition.</param>
      /// <param name="indexSSA">Number associated to the declaration for SSA algorithm</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public Definition(SingleIdentifierExpression id, string type, Expression init, int indexSSA, Location location)
         : base(id, indexSSA, type, location)
      {
         this.initialization = init;
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
