//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                            
// -------------------------------------------------------------------------- 
// File: Program.cs                                                             
// Author: Francisco Ortin  -  francisco.ortin@gmail.com 
// Co-author: Miguel Garcia - miguel.uniovi@gamail.com                   
// Description:                                                               
//    Parses all the command line arguments.
// -------------------------------------------------------------------------- 
// Create date: 22-06-2007                                                    
// Modification date: 23-02-2011 
//////////////////////////////////////////////////////////////////////////////

using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using DynVarManagement;
using TargetPlatforms;

namespace CommandLine {
    static class Program {
        static void Main(string[] args) {
            //if(args.Length == 0)
              //  args = new string[] { "sample.cs", "/run" };

            //ParseArguments(new string[] { "tests/sample.cs", "/target=clr", "/run" });
            //ParseArguments(new string[] { "tests/sample.cs", "/target=rrotor" });
            //ParseArguments(new string[] { "tests/sample.cs", "/out=a.out.exe", "/target=clr", "/everythingDynamic" });

            ParseArguments(args);
            Compiler.Parser.Parse(parameters.InputFileNames, parameters.OutputFileName,parameters.TargetPlatform, Application.StartupPath + "\\", "ilasm.exe","TypeTable.txt", parameters.Run, parameters.Dynamic);            
        }

        /// <summary>
        /// All the parameters passed in the command line
        /// </summary>
        private static Parameters parameters;

        public static void ParseArguments(string[] args) {
            // * No parameters
            if (args.Length == 0) {
                Console.WriteLine(OptionsConfiguration.noInputMessage);
                return;
            }
            // * Parses each parameter
            IList<string> inputFileNames = new List<string>();
            foreach (string parameter in args)
                ParseParameter(parameter, inputFileNames);
            // * Not posible to specify -ed and -es
            if (DynVarOptions.Instance.EverythingDynamic && DynVarOptions.Instance.EverythingStatic) {
                Console.Error.WriteLine(OptionsConfiguration.dynAndStaErrorMessage);
                System.Environment.Exit(3); // 3 == Both dynamic and static options cannot be set
            }
            // * Sets the input file names
            parameters.InputFileNames = new string[inputFileNames.Count];
            inputFileNames.CopyTo(parameters.InputFileNames, 0);
            // * Sets the output file name
            if (parameters.OutputFileName == null)
                parameters.OutputFileName = ObtainOutputFileName(parameters.InputFileNames);
        }


        /// <summary>
        /// Parses a parameter
        /// </summary>
        /// <param name="parameter">The text of the parameter</param>
        /// <param name="inputFileNames">A list of file names</param>
        private static void ParseParameter(string parameter, IList<string> inputFileNames) {
            foreach (string parameterPrefix in OptionsConfiguration.optionsPrefix) {
                if (parameter.Substring(0, parameterPrefix.Length).ToLower().Equals(parameterPrefix)) {
                    // * Option
                    ParseOption(parameter.Substring(parameterPrefix.Length, parameter.Length - parameterPrefix.Length).ToLower());
                    return;
                }
            }
            // * Input file
            inputFileNames.Add(parameter);
        }

        /// <summary>
        /// Set the default parameters for every option
        /// </summary>
        private static void SetDefaultParameters() {
            parameters.Run = OptionsConfiguration.defaultRunOption; // * Default value
            parameters.TargetPlatform = OptionsConfiguration.defaultTargetPlatform; // * Default value
        }

        /// <summary>
        /// Executes the option specified by the parameter
        /// </summary>
        /// <param name="option">The actual option (in lowercase)</param>
        private static void ParseOption(string option) {
            // * Help option
            foreach (string opString in OptionsConfiguration.helpOptions)
                if (option.Equals(opString)) {
                    // * Help requested
                    Console.WriteLine(OptionsConfiguration.helpMessage);
                    System.Environment.Exit(0);
                }
            // * Everything dynamic option
            foreach (string opString in OptionsConfiguration.everythigDynamicOptions)
                if (option.Equals(opString)) {
                    DynVarOptions.Instance.EverythingDynamic = true;
                    return;
                }
            // * Everything static option
            foreach (string opString in OptionsConfiguration.everythigStaticOptions)
                if (option.Equals(opString)) {
                    DynVarOptions.Instance.EverythingStatic = true;
                    return;
                }
            // * Run option
            foreach (string opString in OptionsConfiguration.runOptions)
                if (option.Equals(opString)) {
                    parameters.Run = true;
                    return;
                }
            // * Dynamic option
            foreach (string opString in OptionsConfiguration.dynamicOptions)
                if (option.Equals(opString))
                {
                    parameters.Dynamic = true;
                    return;
                }  
            // * Out option
            foreach (string opString in OptionsConfiguration.outputOptions)
                if (option.StartsWith(opString)) {
                    string outFileName = ParseValue(option.Substring(opString.Length, option.Length - opString.Length));
                    parameters.OutputFileName = outFileName;
                    return;
                }
            // * Target option
            foreach (string opString in OptionsConfiguration.targetOptions)
                if (option.StartsWith(opString)) {
                    string targetName = ParseValue(option.Substring(opString.Length, option.Length - opString.Length));
                    if (Array.IndexOf(OptionsConfiguration.targetNames, targetName) == -1) {
                        Console.WriteLine(OptionsConfiguration.wrongTarget);
                        System.Environment.Exit(4); // 4 == Bad target
                    }
                    parameters.TargetPlatform = TargetPlatformRepresentation.Instance.getPlatformFromName(targetName);
                    return;
                }
            // * If the method has not returned, an error exists
            Console.Error.WriteLine(OptionsConfiguration.errorMessage);
            System.Environment.Exit(1);  // 1 == Unknown option
        }

        /// <summary>
        /// Parses a value of an option
        /// </summary>
        /// <param name="value">The option prefix and the option value</param>
        /// <returns>The value of an option</returns>
        private static string ParseValue(string value) {
            // * Option prefix
            foreach (string opAssignment in OptionsConfiguration.optionsAssignment)
                if (value.StartsWith(opAssignment))
                    return value.Substring(opAssignment.Length, value.Length - opAssignment.Length);
            // * If the method has not returned, an error exists
            Console.Error.WriteLine(OptionsConfiguration.errorMessage);
            System.Environment.Exit(2);  // 2 == Bad option assignment
            return null;
        }

        /// <summary>
        /// Returns the name of the output file
        /// </summary>
        /// <param name="inputFileNames">The list of file names</param>
        /// <returns>The output file name</returns>
        private static string ObtainOutputFileName(string[] inputFileNames) {
            return Path.ChangeExtension(inputFileNames[0], ".exe");
        }

    }
}
