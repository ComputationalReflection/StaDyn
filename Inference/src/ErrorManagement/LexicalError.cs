////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: LexicalError.cs                                                      //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Represents the error occurred while the source code is lexing.          //
// -------------------------------------------------------------------------- //
// Create date: 14-01-2007                                                    //
// Modification date: 14-01-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement
{
   /// <summary>
   /// Represents the error occurred while the source code is lexing.
   /// </summary>
   public struct LexicalError : IError
   {
      #region Fields

      /// <summary>
      /// Gets the error description.
      /// </summary>
      private string description;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the error type
      /// </summary>
      public string ErrorType
      {
         get { return "Lexical Error"; }
      }

      /// <summary>
      /// Gets the error description
      /// </summary>
      public string Description
      {
         get { return this.description; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of LexicalError.
      /// </summary>
      /// <param name="fileName">File name.</param>
      /// <param name="line">Line number.</param>
      /// <param name="column">Column number.</param>
      /// <param name="description">Error description.</param>
      public LexicalError(string description, Location loc)
      {
         StringBuilder aux = new StringBuilder();
         aux.AppendFormat("An error occurred while the file {0} is parsing. [{1}:{2}]\r\n{3}", loc.FileName, loc.Line, loc.Column, description);
         this.description = aux.ToString();
      }

      #endregion

      #region Equals&GetHashCode
      public override bool Equals(object obj) {
          if (!(obj is LexicalError))
              return false;
          return ((LexicalError)obj).description.Equals(this.description);
      }
      public override int GetHashCode() {
          return this.description.GetHashCode();
      }
      #endregion

   }
}