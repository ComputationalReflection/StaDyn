////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: TypePromotionError.cs                                                //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//   Represents a error produced when the type promotion can not be applied   //
// to specified expressions.                                                  //
// -------------------------------------------------------------------------- //
// Create date: 21-02-2007                                                    //
// Modification date: 23-03-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement
{
   /// <summary>
   /// Represents a error produced when the type promotion can not be applied 
   /// to specified expressions.
   /// different.
   /// </summary>
   public class TypePromotionError : ErrorAdapter
   {

      #region Constructor

      /// <summary>
      /// Constructor of TypePromotionError
      /// </summary>
      /// <param name="id">Idenfifier of type.</param>
      /// <param name="argType">Argument type identifier.</param>
      /// <param name="paramType">Parameter type identifier.</param>
      /// <param name="position">Parameter index.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="line">Line number.</param>
      /// <param name="column">Column number.</param>
       public TypePromotionError(string id, string argType, string paramType, int position, Location loc) : base(loc) 
      {
         StringBuilder aux = new StringBuilder();
         aux.AppendFormat("Argument '{0}': cannot convert from '{1}' to '{2}' in '{3}'.", position, argType, paramType, id );
         this.Description = aux.ToString();
      }

      /// <summary>
      /// Constructor of TypePromotionError
      /// </summary>
      /// <param name="expId">type expression to promotion.</param>
      /// <param name="paramType">Parameter type identifier.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="line">Line number.</param>
      /// <param name="column">Column number.</param>
       public TypePromotionError(string fromType, string toType, Location loc) : base(loc) {
         StringBuilder aux = new StringBuilder();
         aux.AppendFormat("Cannot implicitly convert type '{0}' to '{1}'.", fromType, toType);
         this.Description = aux.ToString();
      }

      /// <summary>
      /// Constructor of TypePromotionError
      /// </summary>
      /// <param name="fromType">type expression from promotion.</param>
      /// <param name="toType">Parameter type to promotion.</param>
      /// <param name="theOperator">The operator that caused the promotion</param>
      /// <param name="fileName">File name.</param>
      /// <param name="line">Line number.</param>
      /// <param name="column">Column number.</param>
       public TypePromotionError(string fromType, string toType, string theOperator, Location loc)
           : base(loc) {
          StringBuilder aux = new StringBuilder();
          aux.AppendFormat("Cannot implicitly convert type '{0}' to '{1}' as required by the operator {2}.", 
              fromType, toType, theOperator);
          this.Description = aux.ToString();
      }
      #endregion


   }
}