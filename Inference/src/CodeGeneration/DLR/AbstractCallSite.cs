using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeGeneration;

namespace CodeGeneration
{
    public abstract class AbstractCallSite : ICallSite
    {
        protected AbstractCallSite(int id, String memberName, CallSiteContainer callSiteContainer)
        {
            this.MemberName = memberName;            
            this.Id = id;
            this.CallSiteContainer = callSiteContainer;
        }

        public int Id { get; private set; }
        public string MemberName { get; set; }
        public CallSiteContainer CallSiteContainer { get; set; }


        public virtual string Name
        {
            get
            {
                return "_callSite_" + Id;
            }
        }

        public virtual string FullName
        {
            get
            {
                return CallSiteContainer.FullName + "::" + Name;
            }
        }

        public abstract String CallSiteType { get; }
        public abstract String CallSiteSubType { get; }
        public virtual void WriteDeclaration(int indent, DLRCodeGenerator codeGenerator)
        {
            codeGenerator.WriteLine(indent, ".field public static class " + CallSiteType + " " + Name);
        }
        public abstract void WriteInitialization(int indent, DLRCodeGenerator codeGenerator);
        
    }
}
