////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: EntryPointNotFoundError.cs                                           //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//   Represents a error produced when source code has not any entry point.    //
// -------------------------------------------------------------------------- //
// Create date: 31-05-2007                                                    //
// Modification date: 31-05-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement
{
   /// <summary>
   /// Represents a error produced when source code has not any entry point.
   /// </summary>
   public class EntryPointNotFoundError : ErrorAdapter
   {
      #region Constructor

      /// <summary>
      /// Constructor of EntryPointNotFoundError
      /// </summary>
      public EntryPointNotFoundError()
      {
         StringBuilder aux = new StringBuilder();
         aux.AppendFormat("The application does not contain a static 'Main' method suitable for an entry point");
         this.Description = aux.ToString();
      }

      #endregion
   }
}