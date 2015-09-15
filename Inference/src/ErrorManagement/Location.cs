////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project StaDyn                                                             //
// -------------------------------------------------------------------------- //
// File: Location.cs                                                          //
// Author: Daniel Zapico Rodríguez -  danzapico@gmail.com                     //
//         Francisco Ortín Soler - ortin@uniovi.es                            //
// Description:                                                               //
//    It Stores de location of a text element. I.e.class definition, an arith-//
//    metical operation,etc.                                                  //
//    Specificly, it is used for locating anspecific token of source code.    // 
//    An inmutable pattern is used because the instances of this class are    //
//    passive in nature. The instance don not ever need to change its state.  //
// -------------------------------------------------------------------------- //
// Create date: 05-03-2009                                                    //
// Modification date: 05-03-2009                                              //
////////////////////////////////////////////////////////////////////////////////
namespace ErrorManagement {
    /// <summary>
    /// This class encapsulates a location in a specific file.
    /// Implements an Inmutable pattern. So it can be used in any context, that is 
    /// his internal fields never change.
    /// </summary>
    public class Location {
        
        #region Fields
        /// <summary>
        /// The name of the file.
        /// </summary>
        private string fileName;
        /// <summary>
        /// Line where the item is situated
        /// </summary>
        private int line;
        /// <summary>
        /// Column where the item is situated
        /// </summary>
        private int column;

        /// <summary>
        /// Indicates whether the object state has been created with a coherent state. 
        /// In case it is false, its FileName, Line and Column must be properly written.
        /// </summary>
        bool valid = false;
        #endregion

        #region Properties

        /// <summary>
        /// Gets de name of the file
        /// </summary>
        public string FileName {
            get { return fileName; }
        }
        /// <summary>
        /// Gets line where the item is located
        /// </summary>
        public int Line {
            get { return line; }
        }
        /// <summary>
        /// Gets column where the item is located
        /// </summary>
        public int Column {
            get { return column; }
        }

        /// <summary>
        /// Indicates whether the object state has been created with a coherent state. 
        /// In case it is false, its FileName, Line and Column must be properly written.
        /// </summary>
        public bool Valid {
            get { return this.valid; }
        }
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of Location.
        /// </summary>
        /// <param name="fileName">File name of its source code</param>
        /// <param name="lineNumber">Line number of its source code</param>
        /// <param name="columnNumber">Line column of its source code</param>
        public Location(string fileName, int lineNumber, int columnNumber) {
            this.line = lineNumber;
            this.column = columnNumber;
            this.fileName = fileName;
            this.valid = true;
        }

        public Location() {
            this.valid = false;
        }
        #endregion
        
        #region ToString

        ///Gets a string representation of the Location Object
        ///</summary>

        public override string ToString() {
            if (valid)
            return FileName + "[" + Line + ", " + Column + "]";
            return "[Cannot stablish location of the element]";
        }
        #endregion

        #region Equals & HasCode
        public override int GetHashCode() {
            return this.line.GetHashCode() * this.column.GetHashCode() * this.fileName.GetHashCode();
        }

        
        public override bool Equals(object obj) {
            if (obj == this) return true;
            
            Location node = obj as Location;
            
            if (obj == null) return false;
            
            return this.line == node.line && this.column == node.column && this.fileName.Equals(node.fileName);
        }
        public Location Clone() {
            return new Location(this.fileName, this.line, this.column);
        }
        #endregion
    }
}