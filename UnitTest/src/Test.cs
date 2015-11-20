using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using ErrorManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TargetPlatforms;

namespace UnitTest
{   
    public class Test
    {
        public static String TESTS_PATH = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\res\\tests\\";

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

        public Test(bool dynamic, bool server, bool specialized)
        {
            this.generateCode = true;
            this.run = true;
            this.targetPlatform = TargetPlatform.CLR;
            this.dynamic = dynamic;
            this.server = server;
            this.specialized = specialized;
        }
        #endregion

        
        /// <summary>
        /// Executes a test
        /// </summary>
        /// <param name="fileNames">The set of file names</param>
        /// <param name="outputFileName">The name of the output file name. Null implies no code generation.</param>
        private void runTest(string[] fileNames, string outputFileName)
        {            
            this.FromError = ErrorManager.Instance.ErrorCount;
            Compiler.Parser.Parse(
                fileNames,
                this.generateCode ? outputFileName : null,
                this.generateCode ? this.targetPlatform : TargetPlatform.CLR,
                TESTS_PATH,
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

        /// <summary>
        /// A code genertion tests calls the run tests the name of the output file
        /// as the name of the first input file, with exe extension
        /// </summary>
        /// <param name="fileNames"></param>
        public void runTest(string[] fileNames)
        {            
            this.runTest(fileNames, Path.ChangeExtension(fileNames[0], ".exe"));            
            Assert.AreEqual(this.ExpectedErrors, this.ToError - this.FromError, this.ExpectedErrors + " errors expected, " + (this.ToError - this.FromError) + " found.");
            if (this.ExpectedErrors != 0)
                Console.Error.WriteLine(this.ExpectedErrors + " errors expected, " + (this.ToError - this.FromError) + " found.");
            Assert.IsTrue(this.Success);
        }

        /// <summary>
        /// A code genertion tests calls the run tests the name of the output file
        /// as the name of the first input file, with exe extension
        /// </summary>
        /// <remarks>
        /// Method obtains file name based on namespace name
        /// </remarks>
        public void runTest()
        {
            MethodBase method = new StackTrace().GetFrame(1).GetMethod();
            Type declaringType = method.DeclaringType;
            String methodName = "";            
            if(!declaringType.Namespace.Equals("UnitTest.Tests"))
                methodName = declaringType.Namespace.Replace("UnitTest.Tests.", "");            
            methodName += "\\" + declaringType.Name + "\\" + method.Name + ".cs";
            this.runTest(new[] { TESTS_PATH + methodName });            
        }        
    }
}
