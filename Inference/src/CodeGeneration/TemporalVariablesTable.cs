////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project Stadyn                                                             //
// -------------------------------------------------------------------------- //
// File: TemporalVarsTable.cs                                                 //
// Author: Daniel Zapico Rodriguez  -  daniel.zapico.rodriguez@gmail.com       //
// Description:                                                               //
//    Implementation of a table of temporal variables.                        //
// -------------------------------------------------------------------------- //
// Create date: 24-09-2009                                                    //
// Modification date:                                                         //
////////////////////////////////////////////////////////////////////////////////
// COMENTAR CON PACO LA VIABILIDAD DE HACER LAS CONSULTAS E INSERCIONES CON EXCCEPCIONES
using System;
using System.Collections.Generic;
using System.Text;

using ErrorManagement;
using AST;

namespace CodeGeneration {
    /// <summary>
    /// Implementation of a table of variables.
    /// thist tables search for an id according to its string type representation
    /// </summary>
    /// 
    class TemporalVariablesTable {

        #region Fields
        /// <summary>
        /// Maps the corsespondency between a the type of a variable and the name of itself.
        /// </summary>
        private Dictionary<string, string> typesToId;
        
        /// <summary>
        /// instance of class TemporalVariablesTable. (unique)
        /// </summary>
        private static TemporalVariablesTable instance;

        

        #endregion

        #region Properties

        
        /// <summary>
        /// Gets the unique instance of TemporalVariablesTable
        /// </summary>
        public static TemporalVariablesTable Instance {
            get {
                if (instance == null)
                    instance = new TemporalVariablesTable();
                return instance;
            }
        }
        
        #endregion

        #region Constructor
        // </summary>
        public TemporalVariablesTable() {
            
        this.typesToId = new Dictionary<string, string> ();
        }
        #endregion

        #region Insert()

        /// <summary>
        /// Insert a new temporal var. If the variable exists it. the new pair is not inserted
        /// </summary>
        /// <param name="type">String representing the type to map.</param>
        /// <param name="id">identifier of the variable.</param>
        /// if the type exists it raises and exception
        public void Insert(string type, string id) {
            if (this.SearchId(type) == null) 
                this.typesToId.Add(type, id);
        }


        #endregion

        #region Search()

        /// <summary>
        /// Searches the temporal variable.whose type is represented in type
        /// </summary>
        /// <param name="type">type of of the auxiliar variable.</param>
        /// <returns>Returns the associated id whose type is "type", or null if not found.</returns>
        public string SearchId(string type) {
            if (this.typesToId.ContainsKey(type))
                return this.typesToId[type];
            return null;
        }

        
        #endregion

#region Remove
        /// <summary>
        /// Deletes all the values in the table
        /// </summary>
        public void Clear() {
            this.typesToId.Clear();
        }

#endregion

    }
}
