//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: CLRCodeGenerator.cs                                                     
// Author: Francisco Ortin  -  francisco.ortin@gmail.com                    
// Description:                                                               
//    This class encapsulates the IL CLR code generator IL.   
//    Inheritance: CodeGenerator
//    Implements Factory method  [Abstract Product].       
// -------------------------------------------------------------------------- 
// Create date: 21-08-2007                                                    
// Modification date: 21-08-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using TypeSystem;

namespace CodeGeneration {
    /// <summary>
    /// This class encapsulates the IL DLR code generator IL.
    /// </summary>
  public class DLRCodeGenerator : CLRCodeGenerator {         
        #region Constructor
        /// <summary>
        /// Constructor of CodeGenerator.
        /// </summary>
        /// <param name="writer">Writer to write the intermediate code.</param>
        public DLRCodeGenerator(TextWriter writer)
            : base(writer) {

        }
        #endregion
      
        public override void WriteHeader(string fileName)
        {
            this.WriteLine(0, ".assembly extern mscorlib {}");

            this.WriteLine(0, ".assembly extern System.Core");
            this.WriteLine(0, "{");
            this.WriteLine(1, ".publickeytoken = (B7 7A 5C 56 19 34 E0 89 )");
            this.WriteLine(1, ".ver 4:0:0:0");
            this.WriteLine(0, "}");

            this.WriteLine(0, ".assembly extern Microsoft.CSharp");
            this.WriteLine(0, "{");
            this.WriteLine(1, ".publickeytoken = (B0 3F 5F 7F 11 D5 0A 3A )");
            this.WriteLine(1, ".ver 4:0:0:0");
            this.WriteLine(0, "}");

            this.WriteLine(0, ".assembly " + fileName);
            this.WriteLine(0, "{");
            this.WriteLine(1, ".custom instance void [mscorlib]System.Runtime.CompilerServices.CompilationRelaxationsAttribute::.ctor(int32) = ( 01 00 08 00 00 00 00 00 )");
            this.WriteLine(1, ".custom instance void [mscorlib]System.Runtime.CompilerServices.RuntimeCompatibilityAttribute::.ctor() = ( 01 00 01 00 54 02 16 57 72 61 70 4E 6F 6E 45 78 63 65 70 74 69 6F 6E 54 68 72 6F 77 73 01 )");
            this.WriteLine(0, "}");
            this.WriteLine(0,".module " + fileName + ".exe");            
        } 

        public void WriteCallSiteContainer(CallSiteContainer callSiteContainer)
        {
            int indent = 1;
            this.Comment(indent, "=============== CALL SITE CONTAINER DECLARATION ===================");
            this.WriteLine(indent, ".class public abstract auto ansi sealed beforefieldinit " + callSiteContainer.Name + " extends [mscorlib]System.Object");
            this.WriteLine(indent++, "{");
            foreach (var callSiteReference in callSiteContainer.CallSiteReferences)
                callSiteReference.WriteDeclaration(indent, this);
            this.WriteLine(indent, ".method private hidebysig specialname rtspecialname static void  .cctor() cil managed");
            this.WriteLine(indent++, "{");
            this.WriteLine(indent, ".maxstack 7");
            this.WriteLine(indent, ".locals init");
            this.WriteLine(indent++, "(");
            for (int i = 0; i < callSiteContainer.CallSiteReferences.Count; i++)
                this.WriteLine(indent, "class [Microsoft.CSharp]Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo[] V" + callSiteContainer.CallSiteReferences[i].Name + (i != callSiteContainer.CallSiteReferences.Count - 1 ? "," : ""));    
            this.WriteLine(--indent, ")");
            foreach (var callSiteReference in callSiteContainer.CallSiteReferences)
                callSiteReference.WriteInitialization(indent,this);
            this.WriteLine(indent, "ret");
            this.WriteLine(--indent, "} // end of method " + callSiteContainer.Name + "::.cctor");
            this.WriteLine(--indent, "} // end of class " + callSiteContainer.Name);            
        }  
    }
}