//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: AssemblerError.cs                                                 
// Author: Francisco Ortin - francisco.ortin@gmail.com
// Description:                                                               
//    Represents the error occurred when the assembler is not capable of
//         assembling an IL file.
// -------------------------------------------------------------------------- 
// Create date: 22-08-2007                                                    
// Modification date: 22-08-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement {
    /// <summary>
    /// Represents the error occurred when the assembler is not capable of
    /// assembling an IL file.
    /// </summary>
    public class AssemblerError : IError {

        /// <summary>
        /// The textual description of the error
        /// </summary>
        private string description;

        /// <summary>
        /// Gets the name for the error type.
        /// </summary>
        public string ErrorType { get { return "Assemble Error"; } }

        /// <summary>
        /// Gets the description for the error type.
        /// </summary>
        public string Description { get { return description; } }

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ilFileName">Name of the input file.</param>
        public AssemblerError(string ilFileName) {
            this.description = "Some error detected while assembling the \"" + ilFileName + "\" file.";
        }
        #endregion
    }
}