////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: CommandLineArgumentsError.cs                                         //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Represents the error occurred when the command line arguments specified //
// are incorrect.                                                             //
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
   /// Represents the error occurred when the command line arguments specified
   /// are incorrect.
   /// </summary>
   public struct CommandLineArgumentsError : IError
   {
      #region Properties

      /// <summary>
      /// Gets the error type
      /// </summary>
      public string ErrorType
      {
         get { return "Command line argument error."; }
      }

      /// <summary>
      /// Gets the error description
      /// </summary>
      public string Description
      {
         get { return "The number of arguments is incorrect. Uso: .exe <dir/s o fich/s>"; }
      }

      #endregion
   }
}