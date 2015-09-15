////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: FileNotFoundError.cs                                                 //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Represents the error occurred when the file specified does not exist.   //
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
   /// Represents the error occurred when the file specified does not exist.
   /// </summary>
   public class FileNotFoundError : ErrorAdapter
   {
      #region Constructor

      /// <summary>
      /// Constructor of FileNotFoundError
      /// </summary>
      /// <param name="file">Name of the file specified.</param>
       public FileNotFoundError(string file)
      {
          this.Description = file + " not found.";
      }

      #endregion
   }
}