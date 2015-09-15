//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: PropertyWriteError.cs                                                      
// Author: Francisco Ortin francisco.ortin@gmail.com                          
// Description:                                                               
//   Represents a error produced when an property without write permisses
//       is assigned a value.
// -------------------------------------------------------------------------- 
// Create date: 24-05-2007                                                    
// Modification date: 24-05-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement {
    /// <summary>
    /// Represents a error produced when an property without write permisses
    ///       is assigned a value.
    /// </summary>
    public class PropertyWriteError : ErrorAdapter {

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="property">The name of the property</param>
        /// <param name="fileName">File name.</param>
        /// <param name="line">Line number.</param>
        /// <param name="column">Column number.</param>
        public PropertyWriteError(string property, Location loc) : base(loc) {
            StringBuilder aux = new StringBuilder();
            aux.AppendFormat("The property '{0}' has not write permises. ", loc);
            this.Description = aux.ToString();
        }
        #endregion

    }
}    

