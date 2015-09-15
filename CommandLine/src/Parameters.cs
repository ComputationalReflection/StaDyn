//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                            
// -------------------------------------------------------------------------- 
// File: Parameters.cs                                                             
// Author: Francisco Ortin  -  francisco.ortin@gmail.com                    
// Description:                                                               
//    Encapsulates all the parameter data.
// -------------------------------------------------------------------------- 
// Create date: 22-06-2007                                                    
// Modification date: 22-06-2007                                              
//////////////////////////////////////////////////////////////////////////////

using TargetPlatforms;
namespace CommandLine {
    struct Parameters {
        // * Input file names
        public string[] InputFileNames;
        // * Output file name
        public string OutputFileName;
        // * Target name (default value is CLR)
        public TargetPlatform TargetPlatform;
        // * If the program must be executed after compilation (default value is false)
        public bool Run;
        // * Using "dynamic" to refer to a "dynamic var" (default value is false)
        public bool Dynamic;
        // * Server option, make use of the DLR (default value is false)
        public bool Server;

    }
}
