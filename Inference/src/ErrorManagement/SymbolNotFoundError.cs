////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: SymbolNotFoundError.cs                                               //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//   Represents a error produced when the specified symbol does not exist.    //
// -------------------------------------------------------------------------- //
// Create date: 02-04-2007                                                    //
// Modification date: 02-04-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement
{
   /// <summary>
   /// Represents a error produced when the specified symbol does not exist.
   /// </summary>
   public class SymbolNotFoundError : ErrorAdapter
   {

      #region Constructor

      /// <summary>
      /// Constructor of SymbolNotFoundError
      /// </summary>
      /// <param name="id">Idenfifier of symbol.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="line">Line number.</param>
      /// <param name="column">Column number.</param>
       public SymbolNotFoundError(string id, Location loc) : base(loc) {
         StringBuilder aux = new StringBuilder();
         aux.AppendFormat("The symbol name '{0}' could not be found.", id);
         this.Description = aux.ToString();
      }

      #endregion

   }
}