////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: DispatcherError.cs                                                  //
// Author: Daniel Zapico Rodríguez                    //
// Description:                                                              //
//   Represents a error produced in a wrong invocation of the dispatchers    //
// -------------------------------------------------------------------------- //
// Create date: 24-01-2009                                                 //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement {
    /// <summary>
    /// Represents a error produced in a wrong invocation of the dispatchers
    /// </summary>
    public class DispatcherError : IError {

        #region Fields
        string description;
        #endregion
        #region Constructor


        public DispatcherError(string description) {
                 this.description = description;
        }

        #endregion

        #region Miembros de IError

        string IError.ErrorType {
            get { return "Dispatcher Error"; }
        }

        string IError.Description {
            get {  return this.description;}
        }

        #endregion
    }
}