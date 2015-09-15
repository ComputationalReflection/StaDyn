
using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement
{
   /// <summary>
   /// Represents a error produced when user type is expected but do not found.
   /// </summary>
   public struct UserTypeExpectedError : IError
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
      /// Constructor of UserTypeExpectedError
      /// </summary>
      /// <param name="fileName">File name.</param>
      /// <param name="line">Line number.</param>
      /// <param name="column">Column number.</param>
      public UserTypeExpectedError(Location loc) {
         StringBuilder aux = new StringBuilder();
         aux.AppendFormat("User type expected.");
         this.description = aux.ToString();
      }

      #endregion
   }
}