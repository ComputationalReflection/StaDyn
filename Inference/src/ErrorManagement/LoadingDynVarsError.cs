////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: LoadingDynVarsError.cs                                               //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Represents a error occurred when dynamic variables are loaded.          //
// -------------------------------------------------------------------------- //
// Create date: 04-04-2007                                                    //
// Modification date: 04-04-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement
{
   /// <summary>
   /// Represents the error occurred when dynamic variable are loaded.
   /// </summary>
   public class LoadingDynVarsError : ErrorAdapter
   {
      #region Constructor

      /// <summary>
      /// Constructor of LoadingDynVarsError
      /// </summary>
      /// <param name="info">Error description.</param>
       public LoadingDynVarsError(string info)
      {
          this.Description = info;
      }

      #endregion
   }
}