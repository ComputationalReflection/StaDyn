////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: ConstantFieldDefinition.cs                                           //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a constant field definition.                               //
//    Inheritance: FieldDefinition.                                           //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 07-02-2007                                                    //
// Modification date: 07-02-2007                                              //
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
   /// Encapsulates a constant field definition.
   /// </summary>
   /// <remarks>
   /// Inheritance: FieldDefinition.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class ConstantFieldDefinition : FieldDefinition
   {
      #region Constructor

      /// <summary>
      /// Constructor of ConstantFieldDefinition.
      /// </summary>
      /// <param name="id">Name of the definition.</param>
      /// <param name="type">TypeExpression of the definition.</param>
      /// <param name="init">Initialization of the definition.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public ConstantFieldDefinition(SingleIdentifierExpression id, string type, Expression init, List<Modifier> mods, Location location)
         : base(id, init, type, mods, location)
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
