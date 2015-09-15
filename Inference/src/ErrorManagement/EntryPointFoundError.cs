////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: EntryPointFoundError.cs                                              //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//   Represents a error produced when source code has more the one entry      //
// point defined.                                                             //
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
   /// Represents a error produced when source code has more the one entry
   /// point defined.
   /// </summary>
   public class EntryPointFoundError : ErrorAdapter
   {
      #region Constructor

      /// <summary>
      /// Constructor of EntryPointFoundError
      /// </summary>
      /// <param name="fileName">File name.</param>
      /// <param name="line">Line number.</param>
      /// <param name="column">Column number.</param>
       public EntryPointFoundError(Location loc)
           : base(loc)
      {
         StringBuilder aux = new StringBuilder();
         aux.AppendFormat("The application has more than one entry point defined.");
         this.Description = aux.ToString();
      }

      #endregion
   }
}