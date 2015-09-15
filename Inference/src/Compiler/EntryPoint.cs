////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: EntryPoint.cs                                                        //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //  
// Co-author: Miguel Garcia - miguel.uniovi@gmail.com                         // 
// Description:                                                               //
//    Application's entry point.                                              //
// -------------------------------------------------------------------------- //
// Create date: 28-11-2006                                                    //
// Modification date: 23-02-2011                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.IO;
using System.Collections.Generic;
using ErrorManagement;
using System.Windows.Forms;
using TargetPlatforms;

namespace Compiler {
    class EntryPoint {
        #region Main

        static void Main(string[] args) {
#if DEBUG
            long startTime = DateTime.Now.Ticks;
            ConsoleColor previousColor;
#endif

            try {
                // if we have at least one command-line argument
                if (args.Length > 0) {
#if DEBUG
                    previousColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Error.WriteLine("Compiling...");
                    Console.ForegroundColor = previousColor;
#endif
                    Program initApp = new Program();
                    // * filename : directoryname map
                    IDictionary<string, string> directories = new Dictionary<string, string>();

                    // for each directory/file specified on the command line
                    for (int i = 0; i < args.Length; i++) {
                        if ((File.Exists(args[i])) || (Directory.Exists(args[i])))
                            initApp.LoadFile(new FileInfo(args[i]), directories,false);
                        else {
                            ErrorManager.Instance.NotifyError(new FileNotFoundError(args[i]));
                            return;
                        }
                    }

                    // starts the compilation process
                    initApp.Run(directories, null, Application.StartupPath+"\\Tests\\","ilasm.exe", "TypeTable.txt", TargetPlatform.CLR, false,false,false,false);
                }
                else
                    ErrorManager.Instance.NotifyError(new CommandLineArgumentsError());
            } catch (System.Exception e) {
                Console.Error.WriteLine("Exception: " + e);
                Console.Error.WriteLine(e.StackTrace);
            }
#if DEBUG
            double elapsedTime = ((DateTime.Now.Ticks - startTime) / TimeSpan.TicksPerMillisecond) / 1000.0;
            previousColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            System.Console.Out.WriteLine("\n\nTotal compilation time was {0} seconds.", elapsedTime);
            Console.ForegroundColor = previousColor;
#endif
            //Console.ReadLine();
        }

        #endregion
    }
}
