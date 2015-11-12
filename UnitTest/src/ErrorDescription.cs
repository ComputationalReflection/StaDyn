using ErrorManagement;

namespace UnitTest {
    public class ErrorDescription {

        #region Fields
        /// <summary>
        /// Line of the error. 0 represents any line is valid.
        /// </summary>
        int line;

        /// <summary>
        /// Column of the error. 0 represents any column is valid.
        /// </summary>
        int column;

        /// <summary>
        /// Class of the error
        /// </summary>
        string type;
        #endregion

        #region Properties
        /// <summary>
        /// The name of the type
        /// </summary>
        public string Type {
            get { return this.type; }
        }
        /// <summary>
        /// The line of the error
        /// </summary>
        public int Line {
            get { return this.line; }
        }

        /// <summary>
        /// The column of the error
        /// </summary>
        public int Column {
            get { return this.column; }
        }
        #endregion

        #region Constructor
        public ErrorDescription(int line, int column, string type) {
            this.line = line;
            this.column = column;
            this.type = type;
        }
        
        #endregion

        #region Equals&GetHashCode
        public override bool Equals(object obj) {
            // * Two error descriptions
            ErrorDescription errorDescription = obj as ErrorDescription;
            if (errorDescription != null) {
                if (this.Line != 0 && errorDescription.line != 0 && this.Line != errorDescription.line)
                    return false;
                if (this.Column != 0 && errorDescription.column != 0 && this.Column != errorDescription.column)
                    return false;
                if (this.Type != null && errorDescription.type != null && !this.Type.Equals(errorDescription.type))
                    return false;
                return true;
            }
            // * An error description and an ErrorAdapter
            ErrorAdapter errorAdapter = obj as ErrorAdapter;
            if (errorAdapter != null) {
                if (this.Line != 0 && this.Line != errorAdapter.Location.Line)
                    return false;
                if (this.Column != 0 && this.Column != errorAdapter.Location.Column)
                    return false;
                if (!this.Type.Equals("") && !this.type.Equals(errorAdapter.GetType().ToString()))
                    return false;
                return true;
            }
            return false;
        }

        public override int GetHashCode() {
            return this.Line;
        }

        #endregion
    }
}