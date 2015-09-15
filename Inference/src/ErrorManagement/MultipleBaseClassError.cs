////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: MultipleBaseClassError.cs                                            //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Represents the error occurred when a class has multiple base classes.   //
// -------------------------------------------------------------------------- //
// Create date: 26-01-2007                                                    //
// Modification date: 14-02-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement
{
   /// <summary>
   /// Represents the error occurred when a class has multiple base classes.
   /// </summary>
   public class MultipleBaseClassError : ErrorAdapter
   {
      #region Constructor

      /// <summary>
      /// Constructor of MultipleBaseClassError
      /// </summary>
      /// <param name="fileName">File name.</param>
      /// <param name="line">Line number.</param>
      /// <param name="column">Column number.</param>
      /// <param name="id">Class name with multiple inheritance.</param>
       public MultipleBaseClassError(string id, Location loc)
           : base(loc)
      {
         StringBuilder aux = new StringBuilder();
         aux.AppendFormat("{0} can not have multiple base classes.", id);
         this.Description = aux.ToString();
      }

      #endregion
   }
}