////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: InterfaceDefinition.cs                                               //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a definition of a concrete interface.                      //
//    Inheritance: IdDeclaration.                                             //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 14-01-2007                                                    //
// Modification date: 01-02-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using Tools;
using TypeSystem;

namespace AST
{
   /// <summary>
   /// Encapsulates a definition of a concrete interface.
   /// </summary>
   /// <remarks>
   /// Inheritance: IdDeclaration.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class InterfaceDefinition : TypeDefinition
   {
      #region Constructor

      /// <summary>
      /// Constructor of ClassDefinition.
      /// </summary>
      /// <param name="id">Name of the class.</param>
      /// <param name="mods">List of modifier identifiers.</param>
      /// <param name="bases">List of base class identifiers.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public InterfaceDefinition(SingleIdentifierExpression id, List<Modifier> mods, List<string> bases, List<Declaration> decls, ErrorManagement.Location location)
         : base(id, mods, bases, decls, location)
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
