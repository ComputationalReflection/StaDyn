////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
                             //
// -------------------------------------------------------------------------- //
//                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using TypeSystem;

namespace ErrorManagement {
    /// <summary>

    /// </summary>
    public class InternalOperationInterfaceError : ErrorAdapter {
        #region Constructor

        public InternalOperationInterfaceError(String operation, String element, Location location)
            : base(location) {
            if (String.IsNullOrEmpty(element))
                this.body(operation);
            else
                this.body(operation, element);
        }


        public InternalOperationInterfaceError(String operation, Location location)
            : base(location) {
            this.body(operation);
        }

        private void body(String operation, String element) {
            StringBuilder aux = new StringBuilder();
            aux.AppendFormat("Internal Error [{0}]. Element '{1}' cannot use operation [2] interface.", this.location, element, operation);
            this.Description = aux.ToString();
        }
        private void body(String operation) {
            StringBuilder aux = new StringBuilder();
            aux.AppendFormat("Internal Error [{0}].Se ha invocado a la operacion de interface [1] con un argumento nulo", this.location, operation);
            this.Description = aux.ToString();
        }
    
        #endregion
    }
}