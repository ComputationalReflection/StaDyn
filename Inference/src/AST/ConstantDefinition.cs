////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: ConstantDefinition.cs                                                //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a constant definition.                                     //
//    Inheritance: Definition.                                                //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 03-01-2007                                                    //
// Modification date: 01-02-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using Tools;
using TypeSystem;
using ErrorManagement;

namespace AST
{
   /// <summary>
   /// Encapsulates a constant definition.
   /// </summary>
   /// <remarks>
   /// Inheritance: Definition.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class ConstantDefinition : Definition
   {
      #region Constructor

      /// <summary>
      /// Constructor of ConstantDefinition.
      /// </summary>
      /// <param name="id">Name of the definition.</param>
      /// <param name="type">TypeExpression of the definition.</param>
      /// <param name="init">Initialization of the definition.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public ConstantDefinition(SingleIdentifierExpression id, string type, Expression init, Location location)
         : base(id, type, init, location)
      {
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
