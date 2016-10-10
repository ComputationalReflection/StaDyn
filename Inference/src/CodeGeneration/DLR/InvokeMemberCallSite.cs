using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypeSystem;

namespace CodeGeneration
{
    public class InvokeMemberCallSite : AbstractCallSite
    {
        private IList<String> Parameters { get; set; }        
        public InvokeMemberCallSite(int id, String memberName, IList<String> parameters, CallSiteContainer callSiteContainer):base(id, memberName, callSiteContainer)
        {
            Parameters = parameters;            
        }

        public override String CallSiteType
        {
            get
            {
                return "[System.Core]System.Runtime.CompilerServices.CallSite`1<class [mscorlib]System.Func`" + (Parameters.Count + 3) + "<class [System.Core]System.Runtime.CompilerServices.CallSite, object," + String.Join(",", Parameters.ToArray()) + (Parameters.Count > 0 ? "," : "") + " object>>";
            }
        }

        public override String CallSiteSubType
        {
            get
            {
                return "[mscorlib]System.Func`" + (Parameters.Count + 3) + "<class [System.Core]System.Runtime.CompilerServices.CallSite, object," + String.Join(",", Parameters.ToArray()) + (Parameters.Count > 0 ? "," : "") + " object>";
            }
        }

        public override void WriteInitialization(int indent, DLRCodeGenerator codeGenerator)
        {
            //codeGenerator.ldci4(indent, 0); //CSharpBinderFlags.None -> Only works with return values
            codeGenerator.ldci4(indent, 256); //CSharpBinderFlags.ResultDiscarded -> Works with and without return value
            codeGenerator.ldstr(indent, MemberName);
            codeGenerator.ldnull(indent);
            codeGenerator.ldtoken(indent, CallSiteContainer.ReferenceClass);
            codeGenerator.Call(indent, "class", "[mscorlib]System.Type", "[mscorlib]System.Type", "GetTypeFromHandle", new string[] { "valuetype [mscorlib]System.RuntimeTypeHandle" });
            codeGenerator.ldci4(indent, Parameters.Count+1);
            codeGenerator.newarr(indent, "[Microsoft.CSharp]Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo");
            codeGenerator.stloc(indent, "V" + Name);
            codeGenerator.ldloc(indent, "V" + Name);
            codeGenerator.ldci4(indent, 0);
            codeGenerator.ldci4(indent, 0);
            codeGenerator.ldnull(indent);
            codeGenerator.Call(indent, "class", "[Microsoft.CSharp]Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo", "[Microsoft.CSharp]Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo", "Create", new string[] { "valuetype [Microsoft.CSharp]Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfoFlags", "string" });
            codeGenerator.stelemRef(indent);
            for (int i = 0; i < Parameters.Count; i++ )
            {
                codeGenerator.ldloc(indent, "V" + Name);
                codeGenerator.ldci4(indent, i+1);
                codeGenerator.ldci4(indent, 0);
                codeGenerator.ldnull(indent);
                codeGenerator.Call(indent, "class", "[Microsoft.CSharp]Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo", "[Microsoft.CSharp]Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo", "Create", new string[] { "valuetype [Microsoft.CSharp]Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfoFlags", "string" });
                codeGenerator.stelemRef(indent);
            }
            codeGenerator.ldloc(indent, "V" + Name);
            codeGenerator.Call(indent, "class", "[System.Core]System.Runtime.CompilerServices.CallSiteBinder", "[Microsoft.CSharp]Microsoft.CSharp.RuntimeBinder.Binder", "InvokeMember", new string[] { "valuetype [Microsoft.CSharp]Microsoft.CSharp.RuntimeBinder.CSharpBinderFlags", "string", "class [mscorlib]System.Collections.Generic.IEnumerable`1<class [mscorlib]System.Type>", "class [mscorlib]System.Type", "class [mscorlib]System.Collections.Generic.IEnumerable`1<class [Microsoft.CSharp]Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo>" });
            codeGenerator.Call(indent, "class", "[System.Core]System.Runtime.CompilerServices.CallSite`1<!0>", "class " + CallSiteType, "Create", new string[] { "class [System.Core]System.Runtime.CompilerServices.CallSiteBinder" });
            codeGenerator.WriteLine(indent, "stsfld class " + CallSiteType + " " + FullName);
            codeGenerator.WriteLine();
        }
    }
}
