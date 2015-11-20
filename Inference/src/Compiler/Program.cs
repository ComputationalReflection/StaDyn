////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: Program.cs                                                           //
// Authors: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                   //
//          Francisco Ortin - francisco.ortin@gmail.com                       //
// Co-author: Miguel Garcia - miguel.uniovi@gmail.com                         // 
// Description:                                                               //
//    Main class for the application.                                         //
// -------------------------------------------------------------------------- //
// Create date: 23-01-2006                                                    //
// Modification date: 23-02-2011                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using AST;
using CodeGeneration;
using Debugger;
using DynVarManagement;
using ErrorManagement;
using Parser;
using Semantic;
using Semantic.SSAAlgorithm;
using antlr;
using TargetPlatforms;

namespace Compiler
{
   public class Program
   {
      #region Fields

      /// <summary>
      /// Stores the ast information of each code file
      /// </summary>
      private List<SourceFile> astList;

      /// <summary>
      /// List of entry points. If the list has zero or more than one elements the 
      /// source code is incorrect. If the list has exactly one element
      /// </summary>
      private static List<entryPointInfo> entryPointList = new List<entryPointInfo>();

      #region struct entryPointInfo

      private struct entryPointInfo
      {
         #region Fields

          Location location;
         #endregion

         #region Properties
          public Location Location {
              get { return this.location; }
          }

         #endregion

         #region Constructor

         /// <summary>
         /// Constructor of entryPointInfo
         /// </summary>
         /// <param name="fName">File name.</param>
         /// <param name="lineNumber">Line number.</param>
         /// <param name="columnNumber">Column number.</param>
         public entryPointInfo(Location location)
         {
              this.location = location;
         }

         #endregion
      }

      #endregion

      #endregion

      #region SetEntryPointFound()

      /// <summary>
      /// Sets the information of entry point.
      /// </summary>
      /// <param name="file">File name in which the entry point is located.</param>
      /// <param name="line">Line number in which the entry point is located.</param>
      /// <param name="colum">Column number in which the entry point is located.</param>
      public static void SetEntryPointFound(Location location)
      {
         entryPointList.Add(new entryPointInfo(location));
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of Program
      /// </summary>
      public Program()
      {
         this.astList = new List<SourceFile>();
      }

      #endregion

      #region LoadFile()

      /// <summary>
      /// Starts the parsing process
      /// </summary>
      /// <param name="f">FileInfo with path information.</param>
      /// <param name="directories">A filename : directoryname map</param>
      /// <param name="dynamic">Using "dynamic" to refer to a "dynamic var"</param>      
      public string LoadFile(FileInfo f, IDictionary<string, string> directories, bool dynamic)
      {
         if (Directory.Exists(f.FullName))
         {
            string[] files = Directory.GetFileSystemEntries(f.FullName);
            for (int i = 0; i < files.Length; i++)
            {
               LoadFile(new FileInfo(files[i]), directories,dynamic);
               directories[files[i]] = f.FullName;
            }
         }
         if (f.Extension.Equals(".stadyn")||f.Extension.Equals(".cs")) {
             parseFile(f, new FileStream(f.FullName, FileMode.Open, FileAccess.Read),dynamic);
             directories[f.Name] = f.Directory.FullName;
         }
         return null;

      }

      #endregion

      #region parseFile()

      /// <summary>
      /// Parses the specified file
      /// </summary>
      /// <param name="f">File information.</param>
      /// <param name="s">Stream with the file code.</param>
      /// <param name="dynamic">Using "dynamic" to refer to a "dynamic var"</param>      
      private void parseFile(FileInfo f, Stream s, bool dynamic)
      {
            ICSharpLexer antlrLexer;
            ICSharpParser parser;
            TokenStreamHiddenTokenFilter filter;
            try {
            
                TokenStream lexer;

                // Create a scanner that reads from the input stream passed to us
                if(dynamic)
                    antlrLexer = new CSharpLexerDynamic(new StreamReader(s));
                else
                    antlrLexer = new CSharpLexer(new StreamReader(s));

                // Define a selector that can switch from the C# codelexer to the C# preprocessor lexer
                TokenStreamSelector selector = new TokenStreamSelector();
                antlrLexer.Selector = selector;
                antlrLexer.setFilename(f.Name);

                CSharpPreprocessorLexer preproLexer = new CSharpPreprocessorLexer(antlrLexer.getInputState());
                preproLexer.Selector = selector;
                CSharpPreprocessorHooverLexer hooverLexer = new CSharpPreprocessorHooverLexer(antlrLexer.getInputState());
                hooverLexer.Selector = selector;

                // use the special token object class
                antlrLexer.setTokenCreator(new CustomHiddenStreamToken.CustomHiddenStreamTokenCreator());
                antlrLexer.setTabSize(1);
                preproLexer.setTokenCreator(new CustomHiddenStreamToken.CustomHiddenStreamTokenCreator());
                preproLexer.setTabSize(1);
                hooverLexer.setTokenCreator(new CustomHiddenStreamToken.CustomHiddenStreamTokenCreator());
                hooverLexer.setTabSize(1);

                // notify selector about various lexers; name them for convenient reference later
                selector.addInputStream(antlrLexer, "codeLexer");
                selector.addInputStream(preproLexer, "directivesLexer");
                selector.addInputStream(hooverLexer, "hooverLexer");
                selector.select("codeLexer"); // start with main the CSharp code lexer
                lexer = selector;

                // create the stream filter; hide WS and SL_COMMENT
                filter = new TokenStreamHiddenTokenFilter(lexer);
                filter.hide(CSharpTokenTypes.WHITESPACE);
                filter.hide(CSharpTokenTypes.NEWLINE);
                filter.hide(CSharpTokenTypes.ML_COMMENT);
                filter.hide(CSharpTokenTypes.SL_COMMENT);

                //------------------------------------------------------------------

                // Create a parser that reads from the scanner
                if (dynamic)
                {
                    parser = new CSharpParserDynamic(filter);
                    DynVarManager.DynamicOption = true;
                }
                else
                    parser = new CSharpParser(filter);
                parser.setFilename(f.FullName);
                //parser.setFilename(f.Name);

                // Start parsing at the compilationUnit rule
                long startTime = DateTime.Now.Ticks;

                this.astList.Add(parser.compilationUnit());

    #if DEBUG
                double elapsedTime = ((DateTime.Now.Ticks - startTime) / TimeSpan.TicksPerMillisecond) / 1000.0;
                ConsoleColor previousColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkGray;
                System.Console.Out.WriteLine("Parsed {0} in: {1} seconds.", f.Name, elapsedTime);
                Console.ForegroundColor = previousColor;
    #endif
             }
             catch (RecognitionException e)
             {
                ErrorManager.Instance.NotifyError(new ParserError(new Location(e.fileName, e.line, e.column), e.Message));
             }
      }

      #endregion

      #region Run()

      /// <summary>
      /// Runs the application
      /// <param name="directories">A filename : directoryname map</param>
      /// <param name="outputFileName">The output file name. A null value means no executable generation.</param>
      /// <param name="debugFilePath">Path where files with debug info will be created (does not include filename).</param>
      /// <param name="typeTableFileName">Path to file with types table info that will be created (includes filename).</param>
      /// <param name="ilasmFileName">Path to the ilasm.exe executable file (includes filename).</param>
      /// <param name="run">If the program must be executed after compilation</param>
      /// <param name="dynamic">Using "dynamic" to refer to a "dynamic var"</param>
      /// <param name="server">Server option, make use of the DLR</param>
      /// <param name="specialized">Specializing methods with the type information of their arguments</param>
      /// <param name="targetPlatform">The target platform to compile the code</param>
      /// </summary>
      public void Run(IDictionary<string, string> directories, string outputFileName, 
          string debugFilePath, string ilasmFileName,string typeTableFileName,  TargetPlatform targetPlatform, bool run, bool dynamic, bool server, bool specialized)
      {
         int previousNumberOfErrors = ErrorManager.Instance.ErrorCount;

         if (entryPointList.Count == 0)
            ErrorManager.Instance.NotifyError(new EntryPointNotFoundError());
         else
         {
            if (entryPointList.Count != 1)
            {
               for (int i = 0; i < entryPointList.Count; i++)
               {
                  ErrorManager.Instance.NotifyError(new EntryPointFoundError(entryPointList[i].Location));
               }
            }
         }

         for (int i = 0; i < this.astList.Count; i++)
         {
             this.astList[i].Accept(new VisitorSSA(), null);
         }

         for (int i = 0; i < this.astList.Count; i++)
         {
            this.astList[i].Accept(new VisitorTypeLoad(), null);
         }

         for (int i = 0; i < this.astList.Count; i++)
         {
            this.astList[i].Accept(new VisitorTypeDefinition(), null);
         }

         for (int i = 0; i < this.astList.Count; i++)
         {
            this.astList[i].Accept(new VisitorSymbolIdentification(directories), null);
         }

         VisitorTypeInference visitorTypeInference = new VisitorTypeInference();
         for (int i = 0; i < this.astList.Count; i++)
            // * The same visitor type inference should be used in the whole process
            this.astList[i].Accept(visitorTypeInference, null);

         //Specialized option
         if (specialized)
         {
            VisitorSpelializer visitorSpecializer = new VisitorSpelializer(visitorTypeInference);
            for (int i = 0; i < this.astList.Count; i++)
                // * The same visitor type inference should be used in the whole process
                this.astList[i].Accept(visitorSpecializer, null);
         }
         
         //for (int i = 0; i < this.astList.Count; i++)
         //    this.astList[i].Accept(new VisitorDebug(new StreamWriter("debug.out")),0);

         // * Code is generated if no error has been found...
         if (ErrorManager.Instance.ErrorCount == previousNumberOfErrors &&
            // * ... and the output file and platform are not null
                     outputFileName != null)
         {
            if (entryPointList.Count == 1)  {
               // * The same visitor code generation should be used in the whole process
               string ilFileName = Path.ChangeExtension(outputFileName, ".il");                
               VisitorCodeGenerationBase visitorCodeGeneration = createVisitorCodeGeneration(ilFileName, outputFileName, targetPlatform, server);
               for (int i = 0; i < this.astList.Count; i++)
               {
                  this.astList[i].Accept(visitorCodeGeneration, null);
               }
               visitorCodeGeneration.AddExceptionCode();
               visitorCodeGeneration.Close();

                
               // If no errors found, the executable file is generated
               if (previousNumberOfErrors == ErrorManager.Instance.ErrorCount)
                  switch (targetPlatform)
                  {
                     case TargetPlatform.CLR:
                          assembleAndRun(ilFileName, outputFileName, ilasmFileName, run);
                        break;
                     case TargetPlatform.RRotor:
                        // TODO (assemble and run the IL code in the Rrotor platform)
                        break;
                     default:
                        System.Diagnostics.Debug.Assert(false, "Unknown target platform.");
                        break;
                  }                
            }
         }


#if DEBUG
         // * Dumps the types table
        // debug(debugFilePath, typeTableFileName);
#endif

         ClearMemory();

      }

      #endregion

      #region ClearMemory()

      /// <summary>
      /// Frees information in memory
      /// </summary>
      public static void ClearMemory()
      {
         // * Frees all the types in memory
         TypeSystem.TypeTable.Instance.Clear();
         // * Clear the list of entry points (main methods in C#)
         Program.entryPointList.Clear();
      }

      #endregion

      #region createVisitorCodeGeneration()
      /// <summary>
      /// Factory method that creates a concrete visitor to generate code
      /// </summary>
      /// <param name="ilFileName">The name of the IL file</param>
      /// <param name="outputFileName">The name of the exe file</param>
      /// <param name="target">The name of the target platform</param>
      /// <param name="server">Server option, make use of the DLR</param>
      private VisitorCodeGenerationBase createVisitorCodeGeneration (string ilFileName, string outputFileName, TargetPlatform target, bool server)
      {          
         switch (target)
         {
             case TargetPlatform.CLR:
                 if(server)
                     return new VisitorDLRCodeGeneration<DLRCodeGenerator>(Path.GetFileNameWithoutExtension(outputFileName),
                   new DLRCodeGenerator(new StreamWriter(ilFileName)));
                   //new DLRCodeGenerator(Console.Out)); 
                 return new VisitorCLRCodeGeneration<CLRCodeGenerator>(Path.GetFileNameWithoutExtension(outputFileName),
                   new CLRCodeGenerator(new StreamWriter(ilFileName)));                   
                   //new CLRCodeGenerator(Console.Out)); 
             //case TargetPlatform.RRotor:
               //  return new VisitorRrotorCodeGeneration<RrotorCodeGenerator>(Path.GetFileNameWithoutExtension(outputFileName),
                 //  new RrotorCodeGenerator(new StreamWriter(ilFileName)));
            default:
               System.Diagnostics.Debug.Assert(false, "Wrong target platform");
               break;
         }
         return null;
      }
      #endregion

      #region assembleAndRun()
      /// <summary>
      /// Once the assembly code has been renerated, it is assembled and executed
      /// </summary>
      /// <param name="ilFileName">The name of the IL file</param>
      /// <param name="outputFileName">The name of the exe file</param>
      /// <param name="ilasmFileName">Path to the ilasm.exe executable file (includes filename).</param>
      /// <param name="run">If the program must be executed after compilation</param>
      private void assembleAndRun(string ilFileName, string outputFileName, string ilasmFileName, bool run)
        {
         Process process = new Process();
         process.StartInfo.UseShellExecute = false;
         process.StartInfo.CreateNoWindow = true; //Uncomment this to execute large source code, but StandardOutput and StandardError will be not visible.
         process.StartInfo.RedirectStandardOutput = false; //Set to false to execute large source code
         process.StartInfo.RedirectStandardError = false; //Set to false to execute large source code  

         process.StartInfo.FileName = ilasmFileName;         
         process.StartInfo.Arguments = "\"" + ilFileName + "\"" + " /output=" + "\"" + outputFileName + "\" /optimize";
         process.Start();
         process.WaitForExit();

         if (process.ExitCode != 0)
         {
            ErrorManager.Instance.NotifyError(new AssemblerError(ilFileName));
            process.StartInfo.RedirectStandardOutput = false;
            process.StartInfo.RedirectStandardError = false;
            process.Start();
            process.WaitForExit();
         }
         else if (run)
         {
            // * The compilation has successed
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.FileName = outputFileName;
            process.Start();
            Console.Out.WriteLine(process.StandardOutput.ReadToEnd());
            Console.Error.WriteLine(process.StandardOutput.ReadToEnd());
            process.WaitForExit();
            if (process.ExitCode != 0)
               ErrorManager.Instance.NotifyError(new ExecutionError(outputFileName));
         }
      }
      #endregion

      #region debug()
      /// <summary>
      /// Calls to debug visit
      /// <param name="debugFilePath">Path where files with debug info will be created (does not include filename).</param>
      /// <param name="typeTableFileName">Path to file with types table info that will be created (includes filename).</param>
      /// </summary>
      private void debug(string debugFilePath, string typeTableFileName)  {
#if DEBUG
          //TODO: OJO Si la carpeta Test no existe falla en tiempo de ejecución al menos en consola.
         for (int i = 0; i < this.astList.Count; i++)
         {
             string debugFile = Path.Combine(debugFilePath, Path.ChangeExtension(this.astList[i].Location.FileName, ".out.txt"));
            using (StreamWriter sw = new StreamWriter(debugFile.ToString()))
            {
               this.astList[i].Accept(new VisitorDebug(sw), 0);
               sw.Close();
            }
            //this.astList[i].Accept(new VisitorDebug(Console.Out), 0);
         }

         using (StreamWriter sw2 = new StreamWriter(typeTableFileName))
         {
            sw2.Write(TypeSystem.TypeTable.Instance.ToString());
            sw2.Close();
         }
#endif
      }

      #endregion

   }
}
