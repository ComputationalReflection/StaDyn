// -------------------------------------------------------------------------- 
// File: SortOfUnification.cs
// Author:  Francisco Ortin - francisco.ortin@gmail.com                       
// Description:                                                               
//    Differen kinds of unification algorithms. 
// -------------------------------------------------------------------------- 
// Create date: 03-05-2007                                                    
// Modification date: 03-05-2007                                              
//////////////////////////////////////////////////////////////////////////////

namespace TypeSystem {
    public enum SortOfUnification {
        /// <summary>
        /// Two type expressions must be equivalent
        /// </summary>
        Equivalent,

        /// <summary>
        /// When one type variable has a substitution, it is incremented with
        /// a union type of itself and type type to be unified with
        /// </summary>
        Incremental,

        /// <summary>
        /// When one type variable has a substitution, this is overridden by
        /// the type to be unified with.
        /// </summary>
        Override
    }
}
