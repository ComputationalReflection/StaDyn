////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: LValueError.cs                                                       //
// Authors: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                   //
//          Francisco Ortin - francisco.ortin@gmail.com                       //
// Description:                                                               //
//   Represents a error produced when the expression is not a lvalue.         //
// -------------------------------------------------------------------------- //
// Create date: 13-02-2007                                                    //
// Modification date: 06-04-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement
{
   /// <summary>
   /// Represents a error produced when the expression is not a lvalue.
   /// </summary>
   public class LValueError : ErrorAdapter
   {
      #region Constructor

      /// <summary>
      /// Constructor of LValueError
      /// </summary>
      /// <param name="fileName">File name.</param>
      /// <param name="line">Line number.</param>
      /// <param name="column">Column number.</param>
       public LValueError(Location loc) : base(loc)
      {
         StringBuilder aux = new StringBuilder();
         aux.AppendFormat("The left-hand side of an assignment must be a variable, a writable property or a indexer (lvalue required).");
         this.Description = aux.ToString();
      }

      #endregion
   }
}