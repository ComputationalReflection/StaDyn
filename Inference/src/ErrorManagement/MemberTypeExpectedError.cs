////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: MemberTypeExpectedError.cs                                           //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Represents a error produced when member type is expected but do not     //
// found.                                                                     //
// -------------------------------------------------------------------------- //
// Create date: 30-05-2007                                                    //
// Modification date: 30-05-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement
{
   /// <summary>
   /// Represents a error produced when member type is expected but do not found.
   /// </summary>
   public struct MemberTypeExpectedError : IError
   {
      #region Fields

      /// <summary>
      /// Error description
      /// </summary>
      private string description;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the name for the error type.
      /// </summary>
      public string ErrorType
      {
         get { return "Semantic error"; }
      }

      /// <summary>
      /// Gets the description for the error type.
      /// </summary>
      public string Description
      {
         get { return this.description; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of MemberTypeExpectedError
      /// </summary>
      /// <param name="fileName">File name.</param>
      /// <param name="line">Line number.</param>
      /// <param name="column">Column number.</param>
      public MemberTypeExpectedError(Location location)
      {
         StringBuilder aux = new StringBuilder();
         aux.AppendFormat("Member type expected.");
         this.description = aux.ToString();
      }

      #endregion
   }
}