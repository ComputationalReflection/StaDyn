////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: DefinedTypeError.cs                                                  //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//   Represents a error produced when the defined type already exists.        //
// -------------------------------------------------------------------------- //
// Create date: 24-01-2007                                                    //
// Modification date: 24-01-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement
{
   /// <summary>
   /// Represents a error produced when the defined type already exists.
   /// </summary>
   public class DefinedTypeError : ErrorAdapter
   {
      #region Constructor

      /// <summary>
      /// Constructor of DefinedTypeError
      /// </summary>
      /// <param name="id">Idenfifier of incorrect type.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="line">Line number.</param>
      /// <param name="column">Column number.</param>
       public DefinedTypeError(string id, Location loc)
           : base(loc)
      {
         StringBuilder aux = new StringBuilder();
         aux.AppendFormat("The type {0} already exists.", id);
         this.Description = aux.ToString();
      }

      #endregion
   }
}