//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: TypeCastError.cs                              
// Author: Francisco Ortin -  francisco.ortin@gmail.com
// Description:                                                               
//   Represents a error produced when a type cast can not be applied   
// to the specified expressions.                                                  
// --------------------------------------------------------------------------
// Create date: 13-07-2007                                                    
// Modification date: 13-07-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement {
    /// <summary>
    /// Represents a error produced when a type cast can not be applied 
    /// to specified expressions.
    /// </summary>
    public class TypeCastError : ErrorAdapter {

        #region Constructor

        /// <summary>
        /// Constructor of TypeCastError
        /// </summary>
        /// <param name="fromType">WriteType expression to be cast.</param>
        /// <param name="toType">Cast WriteType.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="line">Line number.</param>
        /// <param name="column">Column number.</param>
        public TypeCastError(string fromType, string toType, Location loc) : base(loc) {
            StringBuilder aux = new StringBuilder();
            aux.AppendFormat("Cannot cast type '{0}' to '{1}'.", fromType, toType);
            this.Description = aux.ToString();
        }
        #endregion

    }

}