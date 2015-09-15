//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: ExecutionError.cs                                                 
// Author: Francisco Ortin - francisco.ortin@gmail.com
// Description:                                                               
//    Represents the error occurred when compiled application has been
//         executed.
// -------------------------------------------------------------------------- 
// Create date: 22-08-2007                                                    
// Modification date: 22-08-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement {
    /// <summary>
    /// Represents the error occurred when compiled application has been executed.
    /// </summary>
    public class ExecutionError : IError {

        /// <summary>
        /// The textual description of the error
        /// </summary>
        private string description;

        /// <summary>
        /// Gets the name for the error type.
        /// </summary>
        public string ErrorType { get { return "Execution Error"; } }

        /// <summary>
        /// Gets the description for the error type.
        /// </summary>
        public string Description { get { return description; } }

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="executableFileName">Name of the input file.</param>
        public ExecutionError(string executableFileName) {
            this.description = "Some error detected while executing the \"" + executableFileName + "\" file.";
        }
        #endregion
    }
}