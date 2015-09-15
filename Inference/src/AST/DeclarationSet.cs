////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: DeclarationSet.cs                                                    //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a statement with several declarations.                     //
//    Inheritance: Declaration.                                               //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 03-01-2007                                                    //
// Modification date: 04-01-2007                                              //
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
   /// Encapsulates a statement with several declarations.
   /// </summary>
   /// <remarks>
   /// Inheritance: Declaration.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class DeclarationSet : Declaration
   {
      #region Fields

      /// <summary>
      /// Set of declarations.
      /// </summary>
      private List<Statement> declarations;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the number of declarations
      /// </summary>
      public int Count
      {
         get { return this.declarations.Count; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of DeclarationSet
      /// </summary>
      /// <param name="type">WriteType of the declaration.</param>
      /// <param name="decls">Set of declarations with the same type.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public DeclarationSet(string type, List<Statement> decls, Location location)
         : base(type,location)
      {
         this.declarations = decls;
      }

      #endregion

      #region GetDeclarationElement()

      /// <summary>
      /// Gets the element stored in the specified index.
      /// </summary>
      /// <param name="index">Index.</param>
      /// <returns>Element stored in the specified index.</returns>
      public Statement GetDeclarationElement(int index)
      {
         return this.declarations[index];
      }

      #endregion

      #region ContainsId()

      /// <summary>
      /// Searches the identifier and returns true if it exists.
      /// </summary>
      /// <param name="id">identifier to search</param>
      /// <returns>True if identifier exists. Otherwise, false.</returns>
      public bool ContainsId(string id)
      {
         for (int i = 0; i < this.declarations.Count; i++)
         {
            if (this.declarations[i] is IdDeclaration)
               if (((IdDeclaration)this.declarations[i]).Identifier.Equals(id))
                  return true;
         }
         return false;
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
