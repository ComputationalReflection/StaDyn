////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: IError.cs                                                            //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Interface for the different error types.                                //
// -------------------------------------------------------------------------- //
// Create date: 24-10-2006                                                    //
// Modification date: 24-10-2006                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement
{
   /// <summary>
   /// Interfaz for the different error types.
   /// </summary>
   public interface IError
   {
      /// <summary>
      /// Gets the name for the error type.
      /// </summary>
      string ErrorType { get; }

      /// <summary>
      /// Gets the description for the error type.
      /// </summary>
      string Description { get; }
   }
}
