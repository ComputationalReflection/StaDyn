////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: ArgumentOutOfRangeError.cs                                           //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Represents the error occurred when the argument value is out of range.  //
// -------------------------------------------------------------------------- //
// Create date: 23-01-2007                                                    //
// Modification date: 23-01-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement
{
   /// <summary>
   /// Represents the error occurred when the argument value is out of range.
   /// </summary>
   public class ArgumentOutOfRangeError : ErrorAdapter
   {
      #region Constructor

      /// <summary>
      /// Constructor of ArgumentOutOfRangeError
      /// </summary>
      /// <param name="index">Argument value out of range.</param>
      /// <param name="path">Place where the error occurs.</param>
       public ArgumentOutOfRangeError(int index, string path)
      {
          this.Description = "The argument " + index + " is out of range in " + path;
      }

      #endregion
   }
}