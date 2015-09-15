////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: Symbol.cs                                                            //
// Authors: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                   //
//          Francisco Ortin - francisco.ortin@gmail.com                       //
// Description:                                                               //
//    This class represents a symbol associated with a identifier.            //
// -------------------------------------------------------------------------- //
// Create date: 30-03-2007                                                    //
// Modification date: 13-04-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using TypeSystem;

namespace Symbols
{
   /// <summary>
   /// This class represents a symbol associated with its identifier.
   /// </summary>
   public class Symbol
   {
      #region Fields

      /// <summary>
      /// Represents the identifier name associated with the symbol.
      /// </summary>
      private string name;

      /// <summary>
      /// Represents symbol scope.
      /// </summary>
      private int scope;

      /// <summary>
      /// Represents the symbol type.
      /// </summary>
      private TypeExpression type;

      /// <summary>
      /// True if the symbol reference is dynamic, false if the symbol reference is static.
      /// </summary>
      private bool dynamic;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the identifier name associated with the symbol.
      /// </summary>
      public string Name
      {
         get { return this.name; }
      }

      /// <summary>
      /// Gets symbol scope.
      /// </summary>
      public int Scope
      {
         get { return this.scope; }
      }

      /// <summary>
      /// Gets the symbol type.
      /// </summary>
      public TypeExpression SymbolType
      {
         get { return this.type; }
         set { this.type = value; }
      }

      /// <summary>
      /// True if the symbol reference is dynamic, false if the symbol reference is static.
      /// </summary>
      public bool IsDynamic
      {
         get { return this.dynamic; }
         set { this.dynamic = value; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of Symbol.
      /// </summary>
      /// <param name="symbolName">Symbol name.</param>
      /// <param name="symbolScope">Symbol scope.</param>
      /// <param name="symbolType">Symbol type.</param>
      /// <param name="isDinamic">True if the symbol reference is dynamic, false otherwise.</param>
      public Symbol(string symbolName, int symbolScope, TypeExpression symbolType, bool isDinamic)
      {
         this.name = symbolName;
         this.scope = symbolScope;
         this.type = symbolType;
         this.dynamic = isDinamic;
      }

      #endregion

      #region ToString()

      /// <summary>
      /// Returns the symbol information
      /// </summary>
      /// <returns>string with the symbol information.</returns>
      public override string ToString()
      {
         StringBuilder aux = new StringBuilder();
         aux.AppendFormat("Name: {0} Scope: {1} Type: {2} Is dynamic: {3}", this.name, this.scope, this.type, this.dynamic);
         return aux.ToString();
      }

      #endregion
   }
}
