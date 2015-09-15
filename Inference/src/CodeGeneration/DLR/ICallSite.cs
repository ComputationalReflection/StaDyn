using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGeneration
{
    public interface ICallSite
    {        
        string FullName { get; }
        string Name { get; }
        String CallSiteType { get; }
        String CallSiteSubType { get; }
        void WriteDeclaration(int indent, DLRCodeGenerator codeGenerator);
        void WriteInitialization(int indent, DLRCodeGenerator codeGenerator);       
    }
}
