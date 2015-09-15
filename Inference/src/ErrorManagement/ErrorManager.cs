////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: ErrorManager.cs                                                      //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
//         Francisco Ortin - francisco.orgin@gmail.com                        //
// Description:                                                               //
//    Class to allow the management of all different error types happened.    //
//    Implements Singleton pattern.                                           //
// -------------------------------------------------------------------------- //
// Create date: 24-10-2006                                                    //
// Modification date: 21-05-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ErrorManagement {
    /// <summary>
    /// Class to allow the management of all different error types happened.
    /// </summary>
    /// <remarks>
    /// Implements Singleton pattern.
    /// </remarks>
    public sealed class ErrorManager {
        #region Fields

        /// <summary>
        /// Unique instance of ErrorManager.
        /// </summary>
        private static readonly ErrorManager instance = new ErrorManager();

        /// <summary>
        /// File name to register the error.
        /// </summary>
        private string logFileName; 

        /// <summary>
        /// In order to show errors in console
        /// </summary>
        private bool showInConsole;

        /// <summary>
        /// TRUE if an error occurred, FALSE otherwise.
        /// </summary>
        private bool errorFound;

        /// <summary>
        /// True if messages are shown when the method notify error is called.
        /// </summary>
        private bool showMessages;

        /// <summary>
        /// The list of errors previusly shown
        /// </summary>
        private IList<IError> errorList = new List<IError>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the unique instance of ErrorManager
        /// </summary>
        public static ErrorManager Instance {
            get { return instance; }
        }


        /// <summary>
        /// To show the error mesagges in console
        /// </summary>
        public bool ShowInConsole {
            get { return showInConsole; }
            set { showInConsole = value; }
        }


        /// <summary>
        /// Returns TRUE if an error occurred, FALSE otherwise.
        /// </summary>
        public bool ErrorFound {
            get { return this.errorFound; }
        }

        /// <summary>
        /// True if messages are shown when the method notify error is called.
        /// </summary>
        public bool ShowMessages {
            get { return this.showMessages; }
            set { this.showMessages = value; }
        }

        /// <summary>
        /// Filename of the log file.
        /// </summary>
        public string LogFileName {
            get { return logFileName; }
            set { logFileName = value; }
        }

        /// <summary>
        /// Returns the number of errors
        /// </summary>
        public int ErrorCount {
            get { return this.errorList.Count; }
        }
        #endregion

        #region Constructor

        /// <summary>
        /// Private constructor of ErrorManager.
        /// </summary>
        private ErrorManager() {
            this.logFileName = Application.StartupPath + @"\error.log";
            this.errorFound = false;
            this.showMessages = true;
        }

        /// <summary>
        /// Private and static constructor of ErrorManager.
        /// </summary>
        static ErrorManager() {
        }

        #endregion

        #region writeErrorLogHeader()

        /// <summary>
        /// Writes a header for error information
        /// </summary>
        /// <param name="tw">TextWriter to write the header.</param>
        private static void writeErrorLogHeader(TextWriter tw) {
            try {
                tw.WriteLine();
                tw.WriteLine("-------------------------------------------------------------------------------------------");

                tw.Write("Date: ");
                tw.WriteLine(System.DateTime.Now.ToLongDateString());
                tw.Write("Time: ");
                tw.WriteLine(System.DateTime.Now.ToLongTimeString());

                tw.Flush();
            } catch (IOException e) {
                Console.Error.WriteLine("[ErrorManager]: It hasn't been possible to write in error log.");
                Console.Error.WriteLine(e.Message);
                Environment.Exit(-1);
            }
        }

        #endregion

        #region writeError
        /// <summary>
        /// To write errors in both the file and console
        /// </summary>
        /// <param name="s">the error message</param>
        private void writeError(string s, TextWriter tw) {
            if (!this.showMessages)
                return;
            tw.Write(s);
            if (ShowInConsole)
                Console.Error.Write(s);
        }
        #endregion

        #region writeErrorLogEntry()

        /// <summary>
        /// Write the error information.
        /// </summary>
        /// <param name="tw">TextWriter to write the information.</param>
        /// <param name="error">Error information.</param>
        private void writeErrorLogEntry(TextWriter tw, IError error) {
            if (!this.showMessages)
                return;
            try {
                ErrorAdapter errorAdapter = error as ErrorAdapter;
                if (errorAdapter != null) {
                    writeError(errorAdapter.Location.ToString(), tw);
                    writeError(": Error " + errorAdapter.GetType().FullName + " (" + errorAdapter.ErrorType+ "). ", tw);
                    writeError(errorAdapter.Description + "\n", tw);
                }
                else {
                    writeError("Error: ", tw);
                    writeError(error.ErrorType + "\r\n", tw);
                    writeError("Description: ", tw);
                    writeError(error.Description + "\r\n", tw);
                }
                tw.Flush();
            } catch (IOException e) {
                Console.Error.WriteLine("[ErrorManager]: It has not been possible to write in error log.");
                Console.Error.WriteLine(e.Message);
                Environment.Exit(-1);
            }
        }

        #endregion

        #region NotifyError()

        /// <summary>
        /// Notify the error
        /// </summary>
        /// <param name="error">Error to notify.</param>
        public void NotifyError(IError error) {
            if (!this.showMessages)
                return;

            // * Checks if the error was previously shown
            if (this.errorList.Contains(error))
                return;
            this.errorList.Add(error);


            errorFound = true;
            try {
                StreamWriter writer = new StreamWriter(this.logFileName, true);

                //#if DEBUG
                //   writeErrorLogEntry(Console.Error, error);
                //#endif

                // Writes the header
                writeErrorLogHeader(writer);
                // Writes error information
                writeErrorLogEntry(writer, error);

                writer.Close();
            } catch (Exception e) {
                Console.Error.WriteLine("[ErrorManager]: It has not been possible to write in error log.");
                Console.Error.WriteLine(e.Message);
                Environment.Exit(-1);
            }
        }

        #endregion

        #region GetError()
        /// <summary>
        /// Returns an error object
        /// </summary>
        /// <param name="errorNumber">The error index</param>
        /// <returns>The error</returns>
        public IError GetError(int errorNumber) {
            return this.errorList[errorNumber];
        }
        #endregion

        #region Clear()
        /// <summary>
        /// Clears error list, sets errorFound to false, and deletes log file.
        /// </summary>
        public void Clear() {
            errorList.Clear();
            errorFound = false;
            try {
                FileInfo logFile = new FileInfo(LogFileName);
                if (logFile.Exists)
                    logFile.Delete();
            } catch (IOException ex) {
                System.Diagnostics.Trace.WriteLine(ex);
            }

        }
        #endregion

    }
}