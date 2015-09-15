using System;
using System.Collections.Generic;
using System.Text;

namespace DynVarManagement
{
    /// <summary>
    /// The parent class for all exceptions related
    /// to DynVars
    /// </summary>
    public class DynVarException : Exception {
        public DynVarException(string msg)
            : base( msg )
        {
        }
    }

    /// <summary>
    /// When the associated file is not found
    /// </summary>
    public class DynVarInfoNotFound : DynVarException {
        public DynVarInfoNotFound(string msg)
            : base( msg )
        {
        }
    }

    /// <summary>
    /// When the XML syntax of the associated file is not valid.
    /// </summary>
    public class DynVarInfoSyntaxException : DynVarException {
        public DynVarInfoSyntaxException(string msg) : base( msg )
        {
        }
    }

    /// <summary>
    /// When a dynVar, a block(DynVars object), class or namespace
    /// is not found in the associated info.
    /// </summary>
    public class DynVarNotFound : DynVarException {
        public DynVarNotFound(string msg)
            : base( msg )
        {
        }
    }
}
