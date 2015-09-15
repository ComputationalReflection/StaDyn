////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: IncorrectArraySizeError.cs                                           //
// Authors: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                   //
// Description:                                                               //
//    Represents a error produced when a specified array size isnt an integer //
// or an expression that contains a integer value.                            //
// -------------------------------------------------------------------------- //
// Create date: 24-07-2007                                                    //
// Modification date: 24-07-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement
{
   /// <summary>
   /// Represents a error produced when a specified array size isnt an integer
   /// or an expression that contains a integer value.
   /// </summary>
   public class IncorrectArraySizeError : ErrorAdapter
   {
      #region Constructor

      /// <summary>
      /// Constructor of IncorrectArraySizeError
      /// </summary>
      /// <param name="fileName">File name.</param>
      /// <param name="line">Line number.</param>
      /// <param name="column">Column number.</param>
      public IncorrectArraySizeError(Location loc) : base(loc)
      {
         StringBuilder aux = new StringBuilder();
         aux.AppendFormat("The array size specified has to be an integer value or an expression that encapsulates an integer value.");
         this.Description = aux.ToString();
      }

      #endregion
   }
}