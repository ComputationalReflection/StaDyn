////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: FieldDeclaration.cs                                                  //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a declaration of a concrete field.                         //
//    Inheritance: IdDeclaration.                                             //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 28-01-2007                                                    //
// Modification date: 11-04-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using Symbols;
using Tools;
using TypeSystem;
using ErrorManagement;

namespace AST
{
   /// <summary>
   /// Encapsulates a declaration of a concrete field.
   /// </summary>
   /// <remarks>
   /// Inheritance: IdDeclaration.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class FieldDeclaration : IdDeclaration
   {
      #region Fields

      /// <summary>
      /// Modifiers information used to obtain its type
      /// </summary>
      private List<Modifier> modifiersInfo;

      /// <summary>
      /// Represents the field type string;
      /// </summary>
      private string fieldType;

      /// <summary>
      /// Represents the symbol of the declaration.
      /// </summary>
      private Symbol symbol;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the modifiers information used to obtain its type
      /// </summary>
      public List<Modifier> ModifiersInfo
      {
         get { return this.modifiersInfo; }
      }

      /// <summary>
      /// Gets the field type string
      /// </summary>
      public string TypeInfo
      {
         get { return this.fieldType; }
      }

      /// <summary>
      /// Gets or sets the field symbol
      /// </summary>
      public Symbol FieldSymbol
      {
         get { return this.symbol; }
         set { this.symbol = value; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of FieldDeclaration.
      /// </summary>
      /// <param name="id">Name of the definition.</param>
      /// <param name="type">Name of its type.</param>
      /// <param name="modifiers">Modifiers information.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public FieldDeclaration(SingleIdentifierExpression id, string type, List<Modifier> modifiers, Location location)
          : base(id, -1, null, location)
      {
         this.modifiersInfo = modifiers;
         this.fieldType = type;
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
