////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: ConstantTable.cs                                                     //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com
//         Daniel Zapico Rodríguez  -  daniel.zapico.rodriguez@gmail.com
// Description:                                                               //
//    Implementation of a table of constants.                                 //
// -------------------------------------------------------------------------- //
// Create date: 11-07-2007                                                    //
// Modification date: 11-07-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;


using ErrorManagement;
using AST;

namespace CodeGeneration {
    /// <summary>
    /// Implementation of a table of constants.
    /// </summary>
    class ConstantTable {
        #region Fields

        /// <summary>
        /// Stores the initialization of the constants.
        /// </summary>
        private List<Dictionary<string, Expression>> constants;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of ConstantTable.
        /// </summary>
        public ConstantTable() {
            this.constants = new List<Dictionary<string, Expression>>();
        }

        #endregion

        #region Set()

        /// <summary>
        /// Add a new scope
        /// </summary>
        public void Set() {
            this.constants.Add(new Dictionary<string, Expression>());
        }

        #endregion

        #region Reset()

        /// <summary>
        /// Removes the last scope
        /// </summary>
        public void Reset() {
            this.constants.RemoveAt(this.constants.Count - 1);
        }

        #endregion

        #region Insert()

        /// <summary>
        /// Insert a new constant in the current scope.
        /// </summary>
        /// <param name="id">Constant identifier.</param>
        /// <param name="init">Constant initialization.</param>
        /// <returns>True if the element is inserted, false otherwise.</returns>
        public bool Insert(string id, Expression init) {
            if (this.constants[this.constants.Count - 1].ContainsKey(id))
                return false;
            this.constants[this.constants.Count - 1].Add(id, init);
            return true;
        }

        #endregion

        #region Search()

        /// <summary>
        /// Searches the initialization of constant identifier.
        /// </summary>
        /// <param name="id">Identifier name.</param>
        /// <param name="scope">zero-based scope where the constant is found. -1 if the search does not success</param>
        /// <returns>Returns the initialization expression associated with the constant or null if the constant is not found.</returns>
        /// // devolver un scope
        public Expression Search(string id, out int scope) {
            scope = this.constants.Count - 1 ;

            while (scope >= 0) {
                if (this.constants[scope].ContainsKey(id))
                    return this.constants[scope][id];
                else
                    scope--;
            }
            return null;
        }
        /// <summary>
        /// Searches the initialization of constant identifier. It performs a full search.
        /// </summary>
        /// <param name="id">Identifier name.</param>
        /// <returns>Returns the initialization expression associated to the specified name.</returns>
        /// // devolver un scope
        public Expression Search(string id) {
            int scope;
            return Search(id, out scope);
        }

        #endregion

        
    }
}
