////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: ParserError.cs                                                       //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Represents the error occurred while the source code is parsing.         //
// -------------------------------------------------------------------------- //
// Create date: 15-12-2006                                                    //
// Modification date: 18-12-2006                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement
{
   /// <summary>
   /// Represents the error occurred while the source code is parsing.
   /// </summary>
   public class ParserError : ErrorAdapter
   {
      #region Constructor

      /// <summary>
      /// Constructor of ParserError.
      /// </summary>
      /// <param name="fileName">File name.</param>
      /// <param name="line">Line number.</param>
      /// <param name="column">Column number.</param>
      /// <param name="description">Error description.</param>
       public ParserError(Location loc, string description)
           : base(loc)
      {
         StringBuilder aux = new StringBuilder();
         aux.AppendFormat("An error occurred while the file {0} is parsing.\r\n{1}", loc.FileName, description);
         this.Description = aux.ToString();
      }

      #endregion
   }
}