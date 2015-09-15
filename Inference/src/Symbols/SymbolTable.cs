////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: SymbolTable.cs                                                       //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Implementation of a symbol table.                                       //
// -------------------------------------------------------------------------- //
// Create date: 30-03-2007                                                    //
// Modification date: 30-03-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using DynVarManagement;
using ErrorManagement;
using TypeSystem;

namespace Symbols
{
   /// <summary>
   /// Implementation of a symbol table.
   /// </summary>
   class SymbolTable
   {
      #region Fields

      /// <summary>
      /// Provides a mapping between a identifier name and its symbol.
      /// </summary>
      private List<Dictionary<string, Symbol>> table;

      #endregion

      #region Constructors

      /// <summary>
      /// Constructor of SymbolTable.
      /// </summary>
      public SymbolTable()
      {
         this.table = new List<Dictionary<string, Symbol>>();
      }

      #endregion

      #region Set()

      /// <summary>
      /// Add a new scope
      /// </summary>
      public void Set()
      {
         //scope++;
         this.table.Add(new Dictionary<string, Symbol>());
      }

      #endregion

      #region Reset()

      /// <summary>
      /// Removes the last scope
      /// </summary>
      public void Reset()
      {
         //scope--;
         this.table.RemoveAt(this.table.Count - 1);
      }

      #endregion

      #region Insert()

      /// <summary>
      /// Insert a new symbol in the current scope.
      /// </summary>
      /// <param name="id">Symbol identifier.</param>
      /// <param name="type">Symbol type.</param>
      /// <param name="isDynamic">True if the symbol is dynamic, false otherwise.</param>
      /// <returns>The symbol inserted, null otherwise.</returns>
      public Symbol Insert(string id, TypeExpression type, bool isDynamic)
      {
         if (this.table[this.table.Count - 1].ContainsKey(id))
            return null;
         if (type is TypeVariable && type.IsDynamic && id.EndsWith("0"))
             updateSSATypeVariables(id);
         Symbol s = new Symbol(id, this.table.Count - 1, type, isDynamic);
         this.table[this.table.Count - 1].Add(id, s);
         //Console.WriteLine(s.ToString());                  
        return s;
      }

       private void updateSSATypeVariables(string id)
       {
           foreach (String variableId in this.table[this.table.Count - 1].Keys)
               if (variableId.StartsWith(id.Substring(0, id.Length - 1)))
               {
                   TypeExpression te = this.table[this.table.Count - 1][variableId].SymbolType;
                   DynVarOptions.Instance.AssignDynamism(te, true);
                   this.table[this.table.Count - 1][variableId].IsDynamic = true;                   
               }
       }

       #endregion

      #region Search()

      /// <summary>
      /// Searches in the symbol table for a symbol matching the specified name.
      /// </summary>
      /// <param name="id">Identifier name.</param>
      /// <returns>Returns the symbol matching specified name.</returns>
      public Symbol Search(string id)
      {
         int scope = this.table.Count - 1;

         while (scope >= 0)
         {
            if (this.table[scope].ContainsKey(id))
               return this.table[scope][id];
            else
               scope--;
         }
         return null;
      }

      #endregion
   }
}
