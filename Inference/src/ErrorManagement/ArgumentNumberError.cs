////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: ArgumentNumberError.cs                                               //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//   Represents a error produced when the argument and parameter number is    //
// different.                                                                 //
// -------------------------------------------------------------------------- //
// Create date: 21-02-2007                                                    //
// Modification date: 21-02-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement
{
   /// <summary>
   /// Represents a error produced when the argument and parameter number is
   /// different.
   /// </summary>
   public class ArgumentNumberError : ErrorAdapter
   {
      #region Constructor

      /// <summary>
      /// Constructor of ArgumentNumberError
      /// </summary>
      /// <param name="id">Idenfifier of type.</param>
      /// <param name="args">Argument number.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="line">Line number.</param>
      /// <param name="column">Column number.</param>
       public ArgumentNumberError(string id, int args, Location loc)
           : base(loc)
      {
         StringBuilder aux = new StringBuilder();
         aux.AppendFormat("No overload for method '{0}' takes '{1}' arguments.", id, args);
         this.Description = aux.ToString();
      }

      #endregion
   }
}