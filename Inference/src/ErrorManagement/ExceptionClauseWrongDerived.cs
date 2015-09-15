////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project Stadyn                                                             //
// -------------------------------------------------------------------------- //
// File: ExceptionClauseWrongDerived.cs                                                  //
// Author: Daniel Zapixo Rodríguezz//
// Description:                                                               //
//   Represents a error produced a class not derived from System.Exception is used in a Catch Clasuse.        //
// -------------------------------------------------------------------------- //
// Create date: 5-01-2010                                                    //
// Modification date: 5-01-2010                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement
{
   /// <summary>
   /// Represents a error produced when the defined type already exists.
   /// </summary>
   public class ExceptionClauseWrongDerived : ErrorAdapter
   {
      #region Constructor

      /// <summary>
      /// Constructor of DefinedTypeError
      /// </summary>
      /// <param name="id">Idenfifier of incorrect type.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="line">Line number.</param>
      /// <param name="column">Column number.</param>
       public ExceptionClauseWrongDerived(string id, Location loc)
           : base(loc)
      {
         StringBuilder aux = new StringBuilder();
         aux.AppendFormat("The exception {0} must be derive from System.Exception.", id);
         this.Description = aux.ToString();
      }

      #endregion
   }
}