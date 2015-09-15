////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: Parser.cs                                                            //
// Author: Francisco Ortin  -  francisco.ortin@gmail.com                      //
// Coauthor: Miguel Garcia - miguel.uniovi@gmail.com                          //
// Description:                                                               //
//    Refactor parsing a set of source files.                                 //
// -------------------------------------------------------------------------- //
// Create date: 04-04-2007                                                    //
// Modification date: 23-02-2011                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using DynVarManagement;
using ErrorManagement;
using System.IO;
using TargetPlatforms;

namespace Compiler {
    /// <summary>
    /// Parsing of a set of files
    /// </summary>
    public class Parser {
        /// <summary>
        /// Compiles a project
        /// </summary>
        /// <param name="files">The list of file names (including subdirectory)</param>
        /// <param name="outputFileName">The output file name. A null value means no executable generation.</param>
        /// <param name="targetPlatform">The target platform (clr, rrotor...). A null value means no executable generation.</param>
        /// <param name="run">If the program must be executed after compilation</param>
        /// <param name="dynamic">Using "dynamic" to refer to a "dynamic var"</param>
        /// <param name="server">Server option, make use of the DLR</param>
        /// <param name="specialized">Specializing methods with the type information of their arguments</param>
        /// <param name="debugFilePath">Path where files with debug info will be created (does not include filename).</param>
        /// <param name="typeTableFileName">Path to file with types table info that will be created (includes filename).</param>
        /// <param name="ilasmFileName">Path to the ilasm.exe executable file (includes filename).</param>
        public static void Parse(string[] files, string outputFileName, TargetPlatform targetPlatform, string debugFilePath, string ilasmFileName, string typeTableFileName, bool run, bool dynamic, bool server, bool specialized)
        {
            if (files == null)
                return;
#if DEBUG
            ConsoleColor previousColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Error.WriteLine("Compiling...");
            Console.ForegroundColor = previousColor;
            long startTime = DateTime.Now.Ticks;
#endif
            try {
                // if we have at least one command-line argument
                if (files.Length > 0) {
                    ErrorManager.Instance.ShowInConsole = true;
                    Program initApp = new Program();
                    // * filename : directoryname map
                    IDictionary<string, string> directories = new Dictionary<string, string>();

                    // for each directory/file specified on the command line
                    for (int i = 0; i < files.Length; i++) {
                        if ((File.Exists(files[i])) || (Directory.Exists(files[i])))
                            initApp.LoadFile(new FileInfo(files[i]), directories,dynamic);
                        else {
                            ErrorManager.Instance.NotifyError(new FileNotFoundError(files[i]));
                            return;
                        }
                    }
                    // starts the compilation process                     
                    initApp.Run(directories, outputFileName, debugFilePath, ilasmFileName, typeTableFileName, targetPlatform, run, dynamic, server,specialized);

                }
                else
                    ErrorManager.Instance.NotifyError(new CommandLineArgumentsError());
            }   catch (System.Exception e) {
               Program.ClearMemory();
               Console.Error.WriteLine("An internal error has ocurred. Please, see " + Path.ChangeExtension(outputFileName,".log") + " for details.");
               File.WriteAllLines(Path.ChangeExtension(outputFileName, ".log") , new[] { "Exception: " + e, e.StackTrace });                
            }
#if DEBUG
            double elapsedTime = ((DateTime.Now.Ticks - startTime) / TimeSpan.TicksPerMillisecond) / 1000.0;
            previousColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            System.Console.Out.WriteLine("Total compilation time: {0} seconds.", elapsedTime);
            Console.ForegroundColor = previousColor;
#endif

        }

    }
}
 