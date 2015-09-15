////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: DeclarationFoundError.cs                                             //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//   Represents a error produced when the declaration already exists.         //
// -------------------------------------------------------------------------- //
// Create date: 14-02-2007                                                    //
// Modification date: 14-02-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement
{
   /// <summary>
   /// Represents a error produced when the declaration already exists.
   /// </summary>
   public class DeclarationFoundError : ErrorAdapter
   {
      #region Constructor

      /// <summary>
      /// Constructor of DeclarationFoundError
      /// </summary>
      /// <param name="id">Idenfifier of incorrect type.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="line">Line number.</param>
      /// <param name="column">Column number.</param>
       public DeclarationFoundError(string id, Location loc)
           : base(loc)
      {
         StringBuilder aux = new StringBuilder();
         aux.AppendFormat("The declaration {0} already exists.", id);
         this.Description = aux.ToString();
      }

      #endregion
   }
}