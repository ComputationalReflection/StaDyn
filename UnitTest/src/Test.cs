using System;
using System.Diagnostics;
using System.Windows.Forms;
using ErrorManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TargetPlatforms;

namespace UnitTest
{    
    abstract class Test
    {
        #region Fields
        /// <summary>
        /// The two indexes of actual errors
        /// </summary>
        private int fromError, toError;

        /// <summary>
        /// The expected number of errors
        /// </summary>
        private int expectedErrors;

        /// <summary>
        /// The test has been run correctly
        /// </summary>
        private bool success;

        /// <summary>
        /// If the code generation phase will be executed. A false value implies only semantic test.
        /// </summary>
        private bool generateCode;

        /// <summary>
        /// Once the file is assembled, this boolean indicates it the executable file will be run.
        /// </summary>
        private bool run;

        /// <summary>
        /// The name of the target platform
        /// </summary>
        private TargetPlatform targetPlatform;

        /// <summary>
        /// Using "dynamic" to refer to a "dynamic var"
        /// </summary>
        public bool dynamic;

        /// <summary>
        /// Server option, make use of the DLR
        /// </summary>        
        public bool server;

        /// <summary>
        /// Specialized option, specializing methods with the type information of their arguments
        /// </summary>        
        public bool specialized;

        #endregion

        #region Properties
        /// <summary>
        /// The lower index of actual errors
        /// </summary>
        public int FromError
        {
            get { return this.fromError; }
            set { this.fromError = value; }
        }

        /// <summary>
        /// The upper index of actual errors
        /// </summary>
        public int ToError
        {
            get { return this.toError; }
            set { this.toError = value; }
        }

        /// <summary>
        /// The expected number of errors
        /// </summary>
        public int ExpectedErrors
        {
            get { return this.expectedErrors; }
            set { this.expectedErrors = value; }
        }

        /// <summary>
        /// The test has been run correctly
        /// </summary>
        public bool Success
        {
            get { return this.success; }
            set { this.success = value; }
        }

        /// <summary>
        /// If the code generation phase will be executed. A false value implies only semantic test.
        /// </summary>
        public bool GenerateCode
        {
            get { return this.generateCode; }
        }

        /// <summary>
        /// Once the file is assembled, this boolean indicates it the executable file will be run.
        /// </summary>
        public bool Run
        {
            get { return this.run; }
        }

        /// <summary>
        /// Using "dynamic" to refer to a "dynamic var".
        /// </summary>
        public bool Dynamic
        {
            get { return this.dynamic; }
            set { this.dynamic = value; }
        }

        /// <summary>
        /// Server option, make use of the DLR
        /// </summary>
        public bool Server
        {
            get { return this.server; }
            set { this.server = value; }

        }

        /// <summary>
        /// Specialized option, specializing methods with the type information of their arguments
        /// </summary>
        public bool Specialized
        {
            get { return this.specialized; }
            set { this.specialized = value; }
        }

        /// <summary>
        /// The name of the target platform
        /// </summary>
        public TargetPlatform TargetPlatform
        {
            get { return this.targetPlatform; }
        }
        #endregion

        #region Constructor

        protected Test(bool generateCode, bool run, bool dynamic, TargetPlatform targetPlatform, bool server)
        {
            this.generateCode = generateCode;
            this.run = run;
            this.targetPlatform = targetPlatform;
            this.dynamic = dynamic;
            this.server = server;
        }
        #endregion

        #region runTest()
        /// <summary>
        /// Executes a test
        /// </summary>
        /// <param name="fileNames">The set of file names</param>
        /// <param name="outputFileName">The name of the output file name. Null implies no code generation.</param>
        protected void runTest(string[] fileNames, string outputFileName)
        {            
            this.FromError = ErrorManager.Instance.ErrorCount;
            Compiler.Parser.Parse(
                fileNames,
                this.generateCode ? outputFileName : null,
                this.generateCode ? this.targetPlatform : TargetPlatform.CLR,
                AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\res\\tests\\",
                "ilasm.exe",
                "TypeTable.txt",
                this.run,
                this.dynamic,
                this.server,
                this.specialized
            );
            this.ToError = ErrorManager.Instance.ErrorCount;
            int expectedNumberOfErrors;
            this.Success = ErrorFile.CheckErrors(fileNames, this.FromError, this.ToError, out expectedNumberOfErrors);
            this.ExpectedErrors = expectedNumberOfErrors;
        }
        #endregion

    }
}
