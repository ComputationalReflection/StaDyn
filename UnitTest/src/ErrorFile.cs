using System;
using System.Collections.Generic;
using System.IO;
using ErrorManagement;

namespace UnitTest {
    public static class ErrorFile {

        #region CheckErrors()
        /// <summary>
        /// Checks that the compilation of a file produces the expected errors
        /// </summary>
        /// <param name="fileNames">The names of the files</param>
        /// <param name="fromError">The number from the error to check that match with the expected ones.</param>
        /// <param name="toError">The number to the error to check that match with the expected ones.</param>
        /// <param name="expectedErrors">Output parameter: The number of expected errors.</param>
        /// <returns>True if the expected errors where found</returns>
        public static bool CheckErrors(string[] fileNames, int fromError, int toError, out int expectedErrors) {
            expectedErrors = 0;
            // * Gets the file names with the directories
            IDictionary<string, string> directories = new Dictionary<string, string>();
            foreach (string fileName in fileNames)
                GetDirectories(fileName, directories);
            // * Gets the text error description files
            IList<string> errorDescriptionFileNames = new List<string>();
            foreach (KeyValuePair<string, string> pair in directories) {
                string errorDescriptionFileName = Path.ChangeExtension(pair.Value + "\\" + pair.Key, ".txt");
                if (File.Exists(errorDescriptionFileName))
                    errorDescriptionFileNames.Add(errorDescriptionFileName);
            }
            // * Takes the expected errors
            IList<ErrorDescription> expectedErrorList = ExpectedErrors(errorDescriptionFileNames);
            // * Checks that both lists match
            return CheckMatch(expectedErrorList, fromError, toError, out expectedErrors);
        }


        private static bool CheckMatch(IList<ErrorDescription> expectedErrorList, int fromError, int toError, out int expectedErrors) {
            expectedErrors = expectedErrorList.Count;
            if (toError - fromError != expectedErrors)
                return false;
            // * Checks all the errors
            for (int errorNumber = fromError; errorNumber < toError; errorNumber++)
            {
                IError error = ErrorManager.Instance.GetError(errorNumber);
                bool found = false;
                foreach (ErrorDescription errorDescription in expectedErrorList)
                    if (errorDescription.Equals(error))
                        found = true;
                if (!found)
                {
                    ErrorAdapter ea = error as ErrorAdapter;
                    if(ea != null)
                        Console.Error.WriteLine("Unexpected " + ea.ErrorType + " [" + ea.Location.Line + "," + ea.Location.Column + "]:" + ea.Description);
                    else
                        Console.Error.WriteLine("Unexpected error: " + ErrorManager.Instance.GetError(errorNumber));
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region ExpectedErrors()
        /// <summary>
        /// Takes error description from a text file.
        /// The format of the text file is a list of lines of the form:
        /// line, column, type
        /// </summary>
        /// <param name="fileNames">The names of the file</param>
        /// <returns>The list of expected errors</returns>
        private static IList<ErrorDescription> ExpectedErrors(IList<string> fileNames) {
            IList<ErrorDescription> errors = new List<ErrorDescription>();
            foreach (string fileName in fileNames) {
                TextReader tr = new StreamReader(fileName);
                if (tr == null)
                    return null;
                string input;
                do {
                    input = tr.ReadLine();
                    if (input != null) {
                        char[] separators = { ',' };
                        string[] values = input.Split(separators);
                        int line, column;
                        string type;
                        try {
                            line = Convert.ToInt32(values[0]);
                        } catch (Exception) {
                            line = 0;
                        }
                        try {
                            column = Convert.ToInt32(values[1]);
                        } catch (Exception) {
                            column = 0;
                        }
                        try {
                            type = values[2].Trim();
                        } catch (Exception) {
                            type = "";
                        }
                        errors.Add(new ErrorDescription(line, column, type));
                    }
                } while (input != null);
                tr.Close();
            }
            return errors;
        }
        #endregion

        #region GetDirectories()
        /// <summary>
        /// Gest a map with the directory(ies) of (a) file(s)
        /// </summary>
        /// <param name="fileName">The name of the file or directory</param>
        /// <param name="directories">A map fileName:directoryName</param>
        private static void GetDirectories(string fileName, IDictionary<string, string> directories) {
            if (File.Exists(fileName) || Directory.Exists(fileName))
                GetDirectories(new FileInfo(fileName), directories);
        }


        /// <summary>
        /// Gets a map with the directory of each file
        /// </summary>
        /// <param name="fileName">FileInfo with path information or a directory.</param>
        /// <param name="directories">A filename : directoryname map</param>
        private static void GetDirectories(FileInfo f, IDictionary<string, string> directories) {
            if (Directory.Exists(f.FullName)) {
                string[] files = Directory.GetFileSystemEntries(f.FullName);
                for (int i = 0; i < files.Length; i++) {
                    GetDirectories(new FileInfo(files[i]), directories);
                    directories[files[i]] = f.FullName;
                }
            }
            if ((f.Name.Length > 3) && f.Name.Substring(f.Name.Length - 3).Equals(".cs"))
                directories[f.Name] = f.Directory.FullName;
            else if((f.Name.Length > 7) && f.Name.Substring(f.Name.Length - 7).Equals(".stadyn"))
                directories[f.Name] = f.Directory.FullName;
        }

        #endregion

    }
}