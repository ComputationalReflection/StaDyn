//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: ErrorAdapter.cs                                                  
// Author: Francisco Ortin -  francisco.ortin@gmail.com                    
// Description:                                                               
//   Represents an adapter of existing features in every error.        
//   Implements Adapter pattern [Adapter].                               
// -------------------------------------------------------------------------- 
// Create date: 30-05-2007                                                    
// Modification date: 30-05-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement {
    /// <summary>
    /// Represents an adapter of existing features in every error.
    /// </summary>
    public class ErrorAdapter : IError {
        #region Fields
         /// <summary>
        ///Location: Encapsulates in one object the line, column and filename
        // the ocurrence of an intem in the text program
		/// </summary>
        protected Location location;     
        /// <summary>
        /// Gets description of the error.
        /// </summary>
        string description;
        #endregion
        #region Properties
     

        public Location Location {
            get { return location; }
        }
       
        /// <summary>
        /// Gets the name for the error type.
        /// </summary>
        public string ErrorType {
            get { return "Semantic error"; }
        }

        /// <summary>
        /// Gets the description for the error type.
        /// </summary>
        public string Description {
            get { return this.description; }
            set { this.description = value; }
        }

        #endregion

        #region Constructor
        public ErrorAdapter(Location location) {
            this.location = location;
        }
        public ErrorAdapter() {
            this.location = new Location();
        }        
        #endregion
        #region Equals&GetHashCode
        public override bool Equals(object obj) {
            ErrorAdapter error = obj as ErrorAdapter;
            if (error==null)
                return false;
            return error.description.Equals(this.description) &&location.Equals(error.location);
        }
        public override int GetHashCode() {
            return this.description.GetHashCode() *location.GetHashCode();
        }
        #endregion

    }
}