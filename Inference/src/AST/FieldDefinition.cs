////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: FieldDefinition.cs                                                   //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a definition of a concrete field.                          //
//    Inheritance: FieldDeclaration.                                          //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 28-01-2007                                                    //
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
   /// Encapsulates a definition of a concrete field.
   /// </summary>
   /// <remarks>
   /// Inheritance: FieldDeclaration.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class FieldDefinition : FieldDeclaration
   {
      #region Fields

      /// <summary>
      /// Represents the initialization of the field. 
      /// </summary>
      private Expression initialization;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the initialization of the field
      /// </summary>
      public Expression Init
      {
         get { return this.initialization; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of FieldDefinition.
      /// </summary>
      /// <param name="id">Name of the definition.</param>
      /// <param name="init">Field Initialization.</param>
      /// <param name="type">Name of the field type.</param>
      /// <param name="modifiers">Modifiers information.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public FieldDefinition(SingleIdentifierExpression id, Expression init, string type, List<Modifier> modifiers, Location location)
          : base(id, type, modifiers, location)
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
   }
}
