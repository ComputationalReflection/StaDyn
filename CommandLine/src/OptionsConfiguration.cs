//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                            
// -------------------------------------------------------------------------- 
// File: OptionsConfiguration.cs                                                             
// Author: Francisco Ortin  -  francisco.ortin@gmail.com                    
// Description:                                                               
//    Configuration of all the parameters.
//    Implements Data Transfer Object (aka, Value Object) pattern [DTO].
// -------------------------------------------------------------------------- 
// Create date: 22-06-2007                                                    
// Modification date: 22-06-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using TargetPlatforms;

namespace CommandLine {
    struct OptionsConfiguration {
        
        // * Messages

        private const string copyrightMessage = "StaDyn C# 2007 Compiler for Reflective Rotor (SSCLI) 1.0\n" +
                                                " and Microsoft (R) Windows (R) 2005 Framework.\n";

        public const string noInputMessage = copyrightMessage + "\nNo inputs specified. Type /help for help.\n";
        public const string wrongTarget = copyrightMessage + "\nThe target specified is not correct. Type /help for help.\n";
        public const string helpMessage = copyrightMessage + "\n              StaDyn C# 2013 Compiler Options\n" +
                                "/help                    Displays this usage message (Short form: /?).\n" +
                                "/out:<file>              Specify output filename.\n" +
                                "/everythingDynamic       Ignores all the dyn files, setting all the references\n" +
                                "                         to dynamic (Short form: /ed).\n" +
                                "/everythingStatic        Ignores all the dyn files, setting all the references\n" +
                                "                         to static (Short form: /es).\n" +
                                "/target:clr              Builds a CLR executable (Short form: /t:clr).\n" +
                                "/target:rrotor           Builds a Rrotor executable (Short form: /t:Rrotor).\n" +
                                "/run                     Runs the program if compilation success\n" +
                                "                                   (Short form: /r).\n" +
                                "/dynamic                 Allows using the dynamic type (disabled by default)\n" +
                                "                                   (Short form: /d).\n" +
                                "/server                  Allows using the DLR (disabled by default)\n" +
                                "                                   (Short form: /s).\n" +
                                "\n";
        public const string errorMessage = copyrightMessage + "\nSome error in the input parameters. Type /help for help.\n";
        public const string dynAndStaErrorMessage = copyrightMessage + "\nEverything dynamic and static options cannot be both enabled.\n";
        
        // * Options (lower letters)
        public static readonly string[] helpOptions = { "help", "?" };
        public static readonly string[] everythigDynamicOptions = { "everythingdynamic", "ed" };
        public static readonly string[] everythigStaticOptions = { "everythingstatic", "es" };
        public static readonly string[] outputOptions = { "output", "out" };
        public static readonly string[] targetOptions = { "target", "t" };
        public static readonly string[] runOptions = { "run", "r" };
        public static readonly string[] dynamicOptions = {"dynamic","d"};
        public static readonly string[] serverOptions = { "server", "s" };

        // * Possible target names
        public static readonly string[] targetNames = { "rrotor", "clr" };

        // * Default values
        public static readonly TargetPlatform defaultTargetPlatform = TargetPlatform.CLR;
        public static readonly bool defaultRunOption = false;


        // * Options prefix
        public static readonly string[] optionsPrefix = { "-", "/" };

        // * Options assignment
        public static readonly string[] optionsAssignment = { ":", "=" };        
    }
}
