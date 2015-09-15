////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: OperationNotAllowedError.cs                                          //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//   Represents a error produced when tries to make an operation not allowed  //
// for the specified type.                                                    //
// -------------------------------------------------------------------------- //
// Create date: 12-02-2007                                                    //
// Modification date: 23-03-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using TypeSystem;

namespace ErrorManagement
{
   /// <summary>
   /// Represents a error produced when tries to make an operation not allowed
   /// for the specified type.
   /// </summary>
   public class OperationNotAllowedError : ErrorAdapter
   {
      #region Constructor
      /// <summary>
      /// Constructor of OperationNotAllowedError
      /// </summary>
      /// <param name="op1">WriteType idenfifier of first operand.</param>
      /// <param name="op2">WriteType identifier of second operand.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="line">Line number.</param>
      /// <param name="column">Column number.</param>
       public OperationNotAllowedError(string operation, string op1, string op2, Location loc)
           : base(loc)
      {
         StringBuilder aux = new StringBuilder();
         aux.AppendFormat("The operation '{0}' cannot be applied to operands of type '{1}' and '{2}'.", operation, op1, op2);
         this.Description = aux.ToString();
      }

      /// <summary>
      /// Constructor of OperationNotAllowedError
      /// </summary>
      /// <param name="operation">The operation to be applied.</param>
      /// <param name="op2">WriteType identifier of operand.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="line">Line number.</param>
      /// <param name="column">Column number.</param>
       public OperationNotAllowedError(string operation, string te, Location loc)
           : base(loc) {
          StringBuilder aux = new StringBuilder();
          aux.AppendFormat("The operation '{0}' cannot be applied to operand of type '{1}'.", operation, te);
          this.Description = aux.ToString();
      }

      #endregion
   }
}